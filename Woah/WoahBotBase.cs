using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Windows.Forms;

namespace Woah
{
    public abstract class WoahBotBase
    {
        public WoahEnvironment env;
        public Cooldowns cooldowns;
        public BotAction Action;
        public WoahQueue ActionQueue;
        public Random rnd = new Random();
        public WoahFish theform;

        public Hashtable keys;

        public WoahBotBase(WoahEnvironment env, WoahFish form)
        {
            this.env = env;
            theform = form;
            keys = new Hashtable();
            keys.Add("1", 0x02);
            keys.Add("2", 0x03);
            keys.Add("3", 0x04);
            keys.Add("4", 0x05);
            keys.Add("5", 0x06);
            keys.Add("6", 0x07);
            keys.Add("7", 0x08);
            keys.Add("8", 0x09);
            keys.Add("9", 0x0A);
            keys.Add("0", 0x0B);
            keys.Add("q", 0x10);
            keys.Add("w", 0x11);
            keys.Add("e", 0x12);
            keys.Add("r", 0x13);
            keys.Add("t", 0x14);
            keys.Add("y", 0x15);
            keys.Add("u", 0x16);
            keys.Add("i", 0x17);
            keys.Add("o", 0x18);
            keys.Add("p", 0x19);
            keys.Add("a", 0x1E);
            keys.Add("s", 0x1F);
            keys.Add("d", 0x20);
            keys.Add("f", 0x21);
            keys.Add("g", 0x22);
            keys.Add("h", 0x23);
            keys.Add("j", 0x24);
            keys.Add("k", 0x25);
            keys.Add("l", 0x26);
            keys.Add("z", 0x2C);
            keys.Add("x", 0x2D);
            keys.Add("c", 0x2E);
            keys.Add("v", 0x2F);
            keys.Add("b", 0x30);
            keys.Add("n", 0x31);
            keys.Add("m", 0x32);


            cooldowns = new Cooldowns();
            cooldowns.DefineCooldown("Global", 1650);
            cooldowns.DefineCooldown("Potion", 2 * 60 * 1000);
        }

        public void Target(string name)
        {
            lock (this)
            {
                if (name == null || name == "") return;
                char[] player = name.ToCharArray();
                string cmd = "/target " + name;
                Clipboard.SetText(cmd);


                ArrayList inputs = new ArrayList();
                inputs.Add(new keyinput(0x1C, true));
                inputs.Add(new keyinput(0x1C, false));
                inputs.Add(new keyinput(0x1D, true)); // LCTRL


                inputs.Add(new keyinput(0x2F, true));
                inputs.Add(new keyinput(0x2F, false));



                inputs.Add(new keyinput(0x1D, false));
               
                inputs.Add(new keyinput(0x1C, true));
                inputs.Add(new keyinput(0x1C, false));
                theform.sendline(inputs);
            }
        }

        public void Press(int key)
        {
            theform.press(key);
            System.Threading.Thread.Sleep(10);
            theform.release(key);
            System.Threading.Thread.Sleep(10);
        }

        public abstract void DoAction();
        public abstract string GetName();
        public abstract void SetupMenu(MenuItem mbase);

    }
}
