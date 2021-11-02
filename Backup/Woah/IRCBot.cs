using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using Meebey.SmartIrc4net;
using System.Windows.Forms;
namespace Woah
{
    public class cIRC
    {

        // make an instance of the high-level API
        public static IrcClient irc = new IrcClient();
        private string ircchan;
        private string ircserver;
        private string ircname;
        WoahFish form;

        // this method we will use to analyse queries (also known as private messages)
        public static void OnQueryMessage(object sender, IrcEventArgs e)
        {
            switch (e.Data.MessageArray[0])
            {
                // debug stuff
                case "dump_channel":
                    string requested_channel = e.Data.MessageArray[1];
                    // getting the channel (via channel sync feature)
                    Channel channel = irc.GetChannel(requested_channel);

                    // here we send messages
                    irc.SendMessage(SendType.Message, e.Data.Nick, "<channel '" + requested_channel + "'>");

                    irc.SendMessage(SendType.Message, e.Data.Nick, "Name: '" + channel.Name + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Topic: '" + channel.Topic + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Mode: '" + channel.Mode + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Key: '" + channel.Key + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "UserLimit: '" + channel.UserLimit + "'");

                    // here we go through all users of the channel and show their
                    // hashtable key and nickname 
                    string nickname_list = "";
                    nickname_list += "Users: ";
                    foreach (DictionaryEntry de in channel.Users)
                    {
                        string key = (string)de.Key;
                        ChannelUser channeluser = (ChannelUser)de.Value;
                        nickname_list += "(";
                        if (channeluser.IsOp)
                        {
                            nickname_list += "@";
                        }
                        if (channeluser.IsVoice)
                        {
                            nickname_list += "+";
                        }
                        nickname_list += ")" + key + " => " + channeluser.Nick + ", ";
                    }
                    irc.SendMessage(SendType.Message, e.Data.Nick, nickname_list);

                    irc.SendMessage(SendType.Message, e.Data.Nick, "</channel>");
                    break;
                case "gc":
                    GC.Collect();
                    break;
                // typical commands
                case "join":
                    irc.RfcJoin(e.Data.MessageArray[1]);
                    break;
                case "part":
                    irc.RfcPart(e.Data.MessageArray[1]);
                    break;
                case "quit":
                    System.Environment.Exit(0);
                    break;
            }
        }

        // this method handles when we receive "ERROR" from the IRC server
        public static void OnError(object sender, Meebey.SmartIrc4net.ErrorEventArgs e)
        {
            System.Console.WriteLine("Error: " + e.ErrorMessage);
            Environment.Exit(0);
        }

        // this method will get all IRC messages
        public static void OnRawMessage(object sender, IrcEventArgs e)
        {
            System.Console.WriteLine("Received: " + e.Data.RawMessage);
        }

        public void OnReadLine(object sender, ReadLineEventArgs e)
        {
            string ircCommand = e.Line;
            form.AddIRCText(ircCommand);
            string[] commandParts = new string[ircCommand.Split(' ').Length];
            commandParts = ircCommand.Split(' ');
            if (commandParts[0].Substring(0, 1) == ":")
            {
                commandParts[0] = commandParts[0].Remove(0, 1);
            }


            // Normal message
            string commandAction = commandParts[1];
            switch (commandAction)
            {
                case "PRIVMSG":
                    string irUser = commandParts[0].Split('!')[0];
                    string irMessage = "";
                    for (int intI = 2; intI < commandParts.Length; intI++)
                    {
                        irMessage += commandParts[intI] + " ";
                    }
                    IrcPrivMsg(irUser, irMessage.Remove(0, 1).Trim());
                    break;
            }
            
        }
        public void OnPing(object sender, PingEventArgs e)
        {
            irc.RfcJoin(ircchan);
        }
        private Thread asdf;

        public cIRC(string IrcServer, int IrcPort, string IrcUserName, string IrcChan, WoahFish Forma)
        {


            // UTF-8 test
            irc.Encoding = System.Text.Encoding.UTF8;

            // wait time between messages, we can set this lower on own irc servers
            irc.SendDelay = 701;

            // we use channel sync, means we can use irc.GetChannel() and so on
            irc.ActiveChannelSyncing = true;

            // here we connect the events of the API to our written methods
            // most have own event handler types, because they ship different data
            irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);
            irc.OnError += new Meebey.SmartIrc4net.ErrorEventHandler(OnError);
            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            irc.OnReadLine += new ReadLineEventHandler(OnReadLine);
            irc.OnPing += new PingEventHandler(OnPing);

            string[] serverlist;
            // the server we want to connect to, could be also a simple string
            serverlist = new string[1];
            serverlist[0] = IrcServer;
            int port = 6667;
            ircchan = IrcChan;
            ircserver = IrcServer;
            ircname = IrcUserName;
            irc.AutoReconnect = true;
            irc.AutoRejoin = true;
            form = Forma;
            try
            {
                // here we try to connect to the server and exceptions get handled
                irc.Connect(serverlist, port);
            }
            catch (ConnectionException e)
            {
                // something went wrong, the reason will be shown
                System.Console.WriteLine("couldn't connect! Reason: " + e.Message);
                Environment.Exit(0);
            }

            try
            {
                asdf = new Thread(new ThreadStart(ReadCommands));

                asdf.ApartmentState = System.Threading.ApartmentState.STA;
                asdf.Start();
                // here we logon and register our nickname and so on 
                irc.Login(IrcUserName, "Fishing bot bot");
                // join the channel
                irc.RfcJoin(IrcChan);
            }
            catch (ConnectionException)
            {
                // this exception is handled because Disconnect() can throw a not
                // connected exception
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                // this should not happen by just in case we handle it nicely
                System.Console.WriteLine("Error occurred! Message: " + e.Message);
                System.Console.WriteLine("Exception: " + e.StackTrace);
                Environment.Exit(0);
            }
        }
        private bool godie = false;
        public void die()
        {
            godie = true;
            if (irc.IsConnected)
                irc.Disconnect();
        }

        
        public void ReadCommands()
        {
            while (!godie)
            {
                irc.ListenOnce();
            }
            if(irc.IsConnected)
                irc.Disconnect();
        }

        public void SendMessage(string msg)
        {
            if (irc.IsConnected)
            {
                if (msg.Contains("chattyp:SAY") || msg.Contains("]|h says:") || msg.Contains(":THISISASAY:"))
                {
                    msg = msg.Replace(":THISISASAY:", "");
                    byte cc = 0x2;
                    msg = (char)cc + msg;
                }
                if (msg.Contains("chattyp:GUILD")||msg.Contains("[Guild]") || msg.Contains(":THISISAGUILD:"))
                {
                    msg = msg.Replace(":THISISAGUILD:", "");
                    byte cc = 0x3;
                    msg = (char)cc + "9 " + msg;
                }
                if (msg.Contains("chattyp:YELL") || msg.Contains("]|h yells:") || msg.Contains(":THISISAYELL:"))
                {
                    msg = msg.Replace(":THISISAYELL:", "");
                    byte cc = 0x3;
                    msg = (char)cc + "4 " + msg;
                }
                if(msg.Contains(":THISISAPM:"))
                {
                    msg = msg.Replace(":THISISAPM:", "");
                    byte cc = 0x3;
                    msg = (char)cc + "13 " + msg;
                }
//                 if(msg.Contains ("You receive"))
//                 {
//                     byte cc = 0x3;
//                     int indx = msg.IndexOfAny("|h[");
//                     msg = msg.Substring (indx+3, msg.)
// 
//                 }

                irc.SendMessage(SendType.Message, ircchan, msg);
            }
        }



        [STAThreadAttribute]
        private void IrcPrivMsg(string UserQuit, string QuitMessage)
        {
            if (QuitMessage.Contains(":enable"))
                form.Enable();
            else if (QuitMessage.Contains(":disable"))
                form.Disable();
            else if (QuitMessage.Contains(":quit"))
            {
                form.QuitApp();
            }
            else if (QuitMessage.Contains(":help"))
            {
                SendMessage(" :Type help [command]. Available commands: enable disable quit echo baubletime resetbauble");
            }
            else if (QuitMessage.Contains(":help enable"))
            {
                SendMessage(" :enable - Enables the fishing bot");
            }
            else if (QuitMessage.Contains(":help disable"))
            {
                SendMessage(" :disable - Disables the fishing bot");
            }
            else if (QuitMessage.Contains(":help echo"))
            {
                SendMessage(" :echo - syntax: echo my raw message - Send a raw message (usage for sending something like '/4 hello!'. Will send text to whatever channel was active last");
            }
            else if (QuitMessage.Contains(":help guild"))
            {
                SendMessage(" :guild - syntax: guild hello guild - Send a message guaranteed to go to guild chat");
            }
            else if (QuitMessage.Contains(":help say"))
            {
                SendMessage(" :say - syntax: say hello people around me - Send a say guaranteed to be sent out through /say");
            }
            else if (QuitMessage.Contains(":help respond"))
            {
                SendMessage(" :respond - syntax: respond hello person - Send a message guaranteed to be sent through /r");
            }
            else if (QuitMessage.Contains(":help quit"))
            {
                SendMessage(" :quit - disconnects the irc bot and closes the bot applicaiton");
            }
            else if (QuitMessage.Contains(":help baubletime"))
            {
                SendMessage(" :baubletime - prints the time left on your bauble, text might be screwy so check a few times");
            }
            else if (QuitMessage.Contains(":help resetbauble"))
            {
                SendMessage(" :resetbauble - Missing a lot of fish? The bot might be out of sync with your bauble. This resets the state and reapplies another bauble.");
            }
            else if (QuitMessage.Contains(":baubletime"))
            {
                SendMessage(form.label2.Text);
            }
            else if (QuitMessage.Contains(":resetbauble"))
            {
                SendMessage("resetting bauble, run buabletime in a second or two");
                form.resetbauble = true;
            }
            else if (QuitMessage.Contains(":echo "))
            {
                string[] split = new string[1];
                split[0] = ":echo ";
                string[] msgs = QuitMessage.Split(split, StringSplitOptions.None);
                form.ClipboardCopy(msgs[1]);


                ArrayList inputs = new ArrayList();
                inputs.Add(new keyinput(0x1C, true));
                inputs.Add(new keyinput(0x1C, false));
                inputs.Add(new keyinput(0x1D, true)); // LCTRL
                inputs.Add(new keyinput(0x2F, true));
                inputs.Add(new keyinput(0x2F, false));
                inputs.Add(new keyinput(0x1D, false));
                inputs.Add(new keyinput(0x1C, true));
                inputs.Add(new keyinput(0x1C, false));
                form.sendline(inputs);

            }
        }
    }

}
