using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading; 
/*Camera:
  Position:
    00DBBCB8 : Camera Position Y
    00DBBCBC : Camera Position X
    00DBBCC0 : Camera Position Z
  Rotation:
    00DBBD00 : Camera Rotation X
    00DBBD04 : Camera Rotation Y 
 * 
 * 
 * Mouse cursor: 00CB05E0
 */

namespace Woah
{

 
    class FishBot : WoahBotBase
    {
        // Given list of strings below, look for the "unable" part, follow the function call to watch the %s get filled in, then walk through the code until you start seeing constants
        private static int MOUSECURSOR = 0xCF5750; // <- version 2.4.3// //0xD4EC48; // <- version 2.4.2 - In olly, right click in CPU, search for all referenced strings, search for "Cursor", look for the interface\\cursor\\xx.blp line, the address is there
        private MenuItem Enabledcheck;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }

        public FishBot(WoahEnvironment env, WoahFish theform)
            : base(env, theform)
        {
            theform.HookKeyPress += new myKeyEventHandler(onKeyEvent);

            cooldowns.DefineCooldown("Fishing", 20 * 1000);
            cooldowns.DefineCooldown("Bauble", 10 * 60 * 1010);
            cooldowns.DefineCooldown("BaublePause", 5 * 1100);
        }
        public void onKeyEvent(Object o, int key)
        {

            if (key == 0x75) // F6
            {
                theform.Enable();
            }
            if (key == 0x76) // F7
            {
                theform.Disable();
            }
        }
        bool mousefound = false;
        public override string GetName()
        {
            return "FishBot";
        }
        public override void SetupMenu(MenuItem mbase)
        {
            Enabledcheck = new MenuItem("Enable", new System.EventHandler(popup));
            Enabledcheck.Checked = true;

            mbase.MenuItems.Add(Enabledcheck);
        }
        private void popup(object sender, EventArgs e)
        {
            MenuItem miClicked = (MenuItem)sender;
            string item = miClicked.Text;
            if (item == "Enable")
            {
                if (Enabledcheck.Checked == true)
                    Enabledcheck.Checked = false;
                else
                    Enabledcheck.Checked = true;
            }
        }
        public override void DoAction()
        {
            if (Enabledcheck.Checked == false)
                return;
            WoahObject player = env.GetItem(env.PlayerID);
            WoahObject testbob = player.Unit_ChannelObject;
            if (testbob == null) // No bobber out
            {
                cooldowns.ClearTime("Fishing");
                mousefound = false;
            }
            if (!cooldowns.IsReady("BaublePause"))
                return;
            if (theform.checkBox1.Checked)
            {
                int timeleft = (int)cooldowns.GetTime("Bauble");
                theform.label2.Text = "Time left on lure: " + timeleft.ToString() + " seconds";
            }
            if(theform.resetbauble == true)
            {
                cooldowns.ClearTime("Bauble");
                theform.resetbauble = false;
            }
            if (theform.checkBox1.Checked && cooldowns.IsReady("Bauble") && testbob == null) // Reset bobber only when appropriate
            {
                
                cooldowns.SetTime("Bauble");
                // open char screen

                ArrayList input = new ArrayList();
                input.Add(new keyinput(0x2, true));
                input.Add(new keyinput(0x2, false));
//                 input.Add(new keyinput(0x2E, true));
//                 input.Add(new keyinput(0x2E, false));

                theform.sendline(input);
//                 Cursor.Position = new Point(109, 406);
//                 Thread.Sleep(1000);
// 
//                 theform.fullclick(false);
//                 Thread.Sleep(1000);
// 
//                 ArrayList input2 = new ArrayList();
// 
//                 input2.Add(new keyinput(0x2E, true));
//                 input2.Add(new keyinput(0x2E, false));
// 
//                 theform.sendline(input2);
                cooldowns.SetTime("BaublePause");
                return;

            }

            if (mousefound == true && env.Memory.ReadInteger(MOUSECURSOR) != 5)
            { // try again, we overshot
                mousefound = false;
                FindBobber();
            }

            WoahObject woah = env.GetItem(env.PlayerID);
            if (woah == null) return;
            WoahObject bobber = woah.Unit_ChannelObject;
            if (bobber == null)
            {
                mousefound = false;
                if (cooldowns.IsReady("Fishing"))
                {
                    cooldowns.SetTime("Fishing");
                    ArrayList input = new ArrayList();

                    input.Add(new keyinput(0x0D, true));
                    input.Add(new keyinput(0x0D, false));
                    theform.sendline(input);
                }
                return;
            }

            FindBobber();

            if (!bobber.BobberBobbing) return;

            cooldowns.ClearTime("Fishing");

            if (theform.checkBox2.Checked)
            {
                theform.press(0x2A);
            }
            theform.fullclick(true);

            if (theform.checkBox2.Checked)
            {
                theform.release(0x2A);
            }
            bobber.Update();

        }

        private void FindBobber()
        {
            int mouseid = env.Memory.ReadInteger(MOUSECURSOR);
            if (mousefound) return;
            for (int i = 150; i < 618; i += 20)
            {
                if (mousefound) break;
                for (int j = 200; j < 824; j += 20)
                {
                    if (mousefound) break;
                    mouseid = env.Memory.ReadInteger(MOUSECURSOR);
                    if (mouseid == 5)
                    {
                        mousefound = true;
                        break;
                    }

                    Cursor.Position = new Point(j, i);

                    Thread.Sleep(5);

                }
            }
        }
    }
}
