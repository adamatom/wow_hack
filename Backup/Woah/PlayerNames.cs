using System;
using System.Collections;
using System.Text;

namespace Woah
{
    public class PlayerNames
    {
       // private Hashtable pointers;
        public Hashtable players;
        private WoahEnvironment env;
        private int basePtr;
        public PlayerNames(WoahEnvironment env)
        {
            this.env = env;

            players = new Hashtable();
            basePtr = 0xCE0900 + 0x1c; // Known table address plus offset
        }

        public void Update()
        {
            int addr = env.Memory.ReadInteger(basePtr);
            int marker = env.Memory.ReadInteger(addr);
            string name;
            ArrayList knownPtrs = new ArrayList();
            while (marker == 0x04) // not sure what the 0x04 is, but the table structure is like this:
                                // 0x00000004,somepointer,pointer_to_whatwewant,repeat
            {
                name = "";
                int objectoffset = addr + 0x4; // Load up our first player
                int objectptr = env.Memory.ReadInteger(objectoffset);
                int nextobject = env.Memory.ReadInteger(objectptr); // first item is the next player

                //load first object info - first object isnt from a "nextobject" pointer
                ulong guid = (ulong)env.Memory.ReadLong(objectptr + 0x14);
                if (guid < 0x0000000100000000 && guid >= 0x0000000000001000 && guid != 0x0 && guid != 0xdeadbeef)
                {
                    name = env.Memory.ReadString(objectptr + 0x1c, 64);

                    //store in hash
                    if (!players.ContainsKey(guid))
                    {
                        players.Add(guid, name);
                    }
                    else
                    {
                        if ((string)players[guid] == "" && name != "")
                        {
                            players[guid] = name;
                        }
                    }
                }
                knownPtrs.Add(objectptr);
                objectptr = nextobject;


                while (!((objectptr & 1) != 0 || objectptr == 0 || objectptr == 0x1c))
                {
                    name = "";
                    nextobject = env.Memory.ReadInteger(objectptr);
                    knownPtrs.Add(objectptr);
                    //load first object info
                    guid = (ulong)env.Memory.ReadLong(objectptr + 0x14);
                    if (guid < 0x0000000100000000 && guid >= 0x0000000000001000 && guid != 0x0 && guid != 0xdeadbeef)
                    {
                        name = env.Memory.ReadString(objectptr + 0x1c, 64);


                        //store in hash
                        if (!players.ContainsKey(guid))
                        {
                            players.Add(guid, name);
                        }
                        else
                        {
                            if ((string)players[guid] == "" && name != "")
                            {
                                players[guid] = name;
                            }
                        }
                    }

                    if (objectptr != nextobject && !knownPtrs.Contains(nextobject))
                        objectptr = nextobject;
                    else
                        break;
                }
                addr = addr + 0xc;
                marker = env.Memory.ReadInteger(addr);
            }
            knownPtrs.Clear();
            while (marker == 0x04) // not sure what the 0x04 is, but the table structure is like this:
            // 0x00000004,somepointer,pointer_to_whatwewant,repeat
            {
                name = "";
                int objectoffset = addr + 0x8; // Load up our first player
                int objectptr = env.Memory.ReadInteger(objectoffset);
                int nextobject = env.Memory.ReadInteger(objectptr); // first item is the next player

                //load first object info - first object isnt from a "nextobject" pointer
                ulong guid = (ulong)env.Memory.ReadLong(objectptr + 0x18);
                if (guid < 0x0000000100000000 && guid >= 0x0000000000001000 && guid != 0x0 && guid != 0xdeadbeef)
                {
                    name = env.Memory.ReadString(objectptr + 0x20, 64);
                    
                    //store in hash
                    if (!players.ContainsKey(guid))
                    {
                        players.Add(guid, name);
                    }
                    else
                    {
                        if ((string)players[guid] == "" && name != "")
                        {
                            players[guid] = name;
                        }
                    }
                }

                knownPtrs.Add(objectptr);
                objectptr = nextobject;


                while (!((objectptr & 1) != 0 || objectptr == 0 || objectptr == 0x1c))
                {
                    name = "";
                    nextobject = env.Memory.ReadInteger(objectptr + 0x8);
                    knownPtrs.Add(objectptr);
                    //load first object info
                    guid = (ulong)env.Memory.ReadLong(objectptr + 0x18);
                    if (guid < 0x0000000100000000 && guid >= 0x0000000000001000 && guid != 0x0 && guid != 0xdeadbeef)
                    {
                        name = env.Memory.ReadString(objectptr + 0x20, 64);

                        //store in hash
                        if (!players.ContainsKey(guid))
                        {
                            players.Add(guid, name);
                        }
                        else
                        {
                            if ((string)players[guid] == "" && name != "")
                            {
                                players[guid] = name;
                            }
                        }
                    }
                    if (objectptr != nextobject && !knownPtrs.Contains(nextobject))
                        objectptr = nextobject;
                    else
                        break;
                }
                addr = addr + 0xc;
                marker = env.Memory.ReadInteger(addr);
            }
        }
    }
}
