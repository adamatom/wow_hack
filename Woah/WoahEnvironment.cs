using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace Woah
{
    public class WoahEnvironment
    {
        private Hashtable objsById;
        private Hashtable objsByPtr;

        public PlayerNames playernames;

        // required properties - updateproc wont run until you feed these
        private ulong playerID;
        private ulong targetID;
        private int WowObjectBasePointer = 0;

        // Properties
        private int numcontainers = 0;
        private int playerptr = 0;
        private int numattackers = 0;
        public string Location;
        DateTime lastSpell = DateTime.Now;
        private MemoryReader memory;
        WoahFish theform;
        public WoahEnvironment(WoahFish form)
        {
            theform = form;
            objsById = new Hashtable();
            objsByPtr = new Hashtable();
            memory = new MemoryReader();



        }

        #region Accessors
        public Hashtable Objects
        {
            get
            {
                return objsById;
            }
        }

        public ulong TargetID
        {
            get
            {
                return targetID;
            }
        }

        public ulong PlayerID
        {
            get
            {
                return playerID;
            }
        }

        public int PlayerPtr
        {
            get
            {
                return playerptr;
            }
        }

        public int NumContainers
        {
            get
            {
                return numcontainers;
            }
        }

        public int NumAttackers
        {
            get
            {
                return numattackers;
            }
        }

        public int WoahBasePointer
        {
            get
            {
                return WowObjectBasePointer;
            }
            set
            {
                WowObjectBasePointer = value;
            }
        }

        public int LastWrittenSlot = 0;
       

        /// <summary>
        /// Active MemoryReader
        /// </summary>
        public MemoryReader Memory
        {
            get
            {
                return memory;
            }
        }
        #endregion

        /// <summary>
        /// You HAVE to call these with values before the thread starts reading memory. 
        /// </summary>
        /// <param name="PlayerGUID"></param>
        /// <param name="WowBase"></param>
        public void Initialize(ulong PlayerGUID, int WowBase)
        {
            playerID = PlayerGUID;
            WowObjectBasePointer = WowBase;
            playernames = new PlayerNames(this);
        }

        private int arrayOffset = 0;
        private int lastreadslot = -1;
        private int stringoffset = 0;
        private byte[] luaArray;
        private int addr = 0;
        private int rsize = 0;
        public bool FindLuaArray()
        {
            try
            {

                if (luaArray != null && arrayOffset + 23 < luaArray.Length && luaArray[arrayOffset] == 10 && luaArray[arrayOffset + 1] == 215 && luaArray[arrayOffset + 2] == 163 && luaArray[arrayOffset + 3] == 112 && luaArray[arrayOffset + 4] == 61 && luaArray[arrayOffset + 5] == 74 && luaArray[arrayOffset + 6] == 147 && luaArray[arrayOffset + 7] == 64
                               && luaArray[arrayOffset + 16] == 0x9a && luaArray[arrayOffset + 17] == 0x99 && luaArray[arrayOffset + 18] == 0x99 && luaArray[arrayOffset + 19] == 0x99 && luaArray[arrayOffset + 20] == 0x99 && luaArray[arrayOffset + 21] == 0x4a && luaArray[arrayOffset + 22] == 0x93 && luaArray[arrayOffset + 23] == 0x40)
                {
                    luaArray = memory.ReadBuffer(addr, rsize);
                    return true;
                }
                List<MemoryRange> rl = memory.FindUnprotectedMemory(memory.GetAppMemoryRange());


                foreach (MemoryRange r in rl)
                {
                    luaArray = memory.ReadBuffer((int)r.Start, (int)r.RangeSize);
                    if (luaArray != null)
                    {
                        for (int i = 0; i < (int)r.RangeSize - 8; i += 8)
                        {

                            ///double f = BitConverter.ToDouble(asdfa, i);
                            if (luaArray[i] == 10 && luaArray[i + 1] == 215 && luaArray[i + 2] == 163 && luaArray[i + 3] == 112 && luaArray[i + 4] == 61 && luaArray[i + 5] == 74 && luaArray[i + 6] == 147 && luaArray[i + 7] == 64
                                && luaArray[i + 16] == 0x9a && luaArray[i + 17] == 0x99 && luaArray[i + 18] == 0x99 && luaArray[i + 19] == 0x99 && luaArray[i + 20] == 0x99 && luaArray[i + 21] == 0x4a && luaArray[i + 22] == 0x93 && luaArray[i + 23] == 0x40)
                            {
                                addr = (int)r.Start;
                                rsize = (int)r.RangeSize;
                                arrayOffset = i;
                                return true;
                            }

                        }
                    }
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        private string oldmessage;


        public bool Update()
        {
            numcontainers = 0;

            //try
            //{
            if (memory.IsOpen)
            {
                if (WowObjectBasePointer == 0)
                    return false;
                ArrayList attackers = new ArrayList();
                Hashtable byId = new Hashtable();
                Hashtable byPtr = new Hashtable();

                int CurrentObject = memory.ReadInteger(WowObjectBasePointer + 0xac);
                while (CurrentObject > 0x00010000 && CurrentObject != 0 && (CurrentObject & 1) == 0 && CurrentObject != 0x1c && CurrentObject != 0x58)
                {
                    WoahObject obj = null;
                    if (objsByPtr.Contains(CurrentObject))
                     {
                        obj = (WoahObject)objsByPtr[CurrentObject];

                        if (obj != null && !obj.Update()) // returns false if not valid 
                            obj = null;
                    }

                    if (obj == null)
                    {
                        obj = new WoahObject(CurrentObject, this);
                        obj.Update();
                    }
                    if (obj.Type == WoahObjectType.Container)
                        numcontainers++;

                    if (obj.GUID.GUID == playerID)
                    {
                        playerptr = CurrentObject;
                        targetID = obj.Unit_TargetGUID;
                    }

                    if (!byPtr.Contains(CurrentObject))
                        byPtr.Add(CurrentObject, obj);
                    if (!byId.Contains(obj.GUID.GUID))
                        byId.Add(obj.GUID.GUID, obj);

                    CurrentObject = memory.ReadInteger(CurrentObject + 0x3c);

                }

                objsById = byId;
                objsByPtr = byPtr;

            }
            if (Objects.Count == 0)
            {
                return false;
            }
            else
                return true;
            //             }
            //             catch
            //             {
            // 
            //             }
        }
        public void RunLua()
        {
            if (FindLuaArray())
            {
                if (luaArray != null)
                {
                    LastWrittenSlot = System.Convert.ToInt32(BitConverter.ToDouble(luaArray, arrayOffset + 32));
                    if (LastWrittenSlot < lastreadslot) // buffer circled around, just reset 
                    {
                        lastreadslot = -1;
                    }
                    while (lastreadslot < LastWrittenSlot)
                    {
                        int readslot = lastreadslot + 1;
                        int stringpointer = System.Convert.ToInt32(BitConverter.ToInt32(luaArray, arrayOffset + (16 * readslot) + 48));


                        int strlength = Memory.ReadInteger(stringpointer + 0x10);
                        byte[] stringarray;
                        if (strlength > 1000000 || strlength < 0)
                            return;
                        stringarray = memory.ReadBuffer(stringpointer + 0x14, strlength);

                        string str;
                        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                        if (stringarray == null)
                        {
                            str = "EMPTY";
                        }
                        else
                            str = enc.GetString(stringarray);



                        if (str != "EMPTY" && str!="")
                        {
                            //theform.SendIrcMessage(str);
                            string[] bla = str.Split(':');
                            if (bla.Length < 2)
                                return;

                            str = str.Split(':')[1];
                            if (str.Contains("QUIT"))
                            {
                                theform.QuitApp();
                            }
                            if (str.Contains("Blood Boil"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Blood Boil" + ts.TotalMilliseconds.ToString());
                                lastSpell = DateTime.Now;
                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x02, true));
                                input.Add(new keyinput(0x02, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);


                            }
                            if (str.Contains("Blood Strike"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Blood Strike" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x03, true));
                                input.Add(new keyinput(0x03, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Blood Tap"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Blood Tap" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x04, true));
                                input.Add(new keyinput(0x04, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Pestilence"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Pestilence" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x07, true));
                                input.Add(new keyinput(0x07, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Strangulate"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Strangulate" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x08, true));
                                input.Add(new keyinput(0x08, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Horn of Winter"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Horn of Winter" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x0A, true));
                                input.Add(new keyinput(0x0A, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Icebound Fortitude"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Icebound Fortitude");
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x02, true));
                                input.Add(new keyinput(0x02, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Icy Touch"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Icy Touch" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x0B, true));
                                input.Add(new keyinput(0x0B, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Mind Freeze"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Mind Freeze" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x0C, true));
                                input.Add(new keyinput(0x0C, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Obliterate"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Obliterate" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x0D, true));
                                input.Add(new keyinput(0x0D, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Rune Strike"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Rune Strike" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x38, true));// ALT
                                input.Add(new keyinput(0x02, true));
                                input.Add(new keyinput(0x02, false));
                                input.Add(new keyinput(0x38, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Bone Shield"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Bone Shield" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x4, true));
                                input.Add(new keyinput(0x4, false));
                                theform.sendline(input);

                            }
                            if (str.Contains("Death Coil"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Death Coil" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x38, true));// ALT
                                input.Add(new keyinput(0x05, true));
                                input.Add(new keyinput(0x05, false));
                                input.Add(new keyinput(0x38, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Death Strike"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Death Strike" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x38, true));// ALT
                                input.Add(new keyinput(0x06, true));
                                input.Add(new keyinput(0x06, false));
                                input.Add(new keyinput(0x38, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Plague Strike"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Plague Strike" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x38, true));// ALT
                                input.Add(new keyinput(0x07, true));
                                input.Add(new keyinput(0x07, false));
                                input.Add(new keyinput(0x38, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Scourge Strike"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Scourge Strike" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x38, true));// ALT
                                input.Add(new keyinput(0x09, true));
                                input.Add(new keyinput(0x09, false));
                                input.Add(new keyinput(0x38, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Unholy Blight"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Unholy Blight" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x38, true));// ALT
                                input.Add(new keyinput(0x0a, true));
                                input.Add(new keyinput(0x0a, false));
                                input.Add(new keyinput(0x38, false));
                                theform.sendline(input);
                            }

                            if (str.Contains("Lichborne"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Lichborne" + ts.TotalMilliseconds.ToString());
                                ArrayList input = new ArrayList();
                                lastSpell = DateTime.Now;
                                input.Add(new keyinput(0x2, true));
                                input.Add(new keyinput(0x2, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Howling Blast"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x03, true));
                                input.Add(new keyinput(0x03, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Unbreakable Armor"))
                            {

                            }
                            if (str.Contains("Frost Strike"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x2A, true));// LSHIFT
                                input.Add(new keyinput(0x04, true));
                                input.Add(new keyinput(0x04, false));
                                input.Add(new keyinput(0x2A, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Gnaw"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Gnaw");
                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x1D, true));// LSHIFT
                                input.Add(new keyinput(0x06, true));
                                input.Add(new keyinput(0x06, false));
                                input.Add(new keyinput(0x1D, false));
                                theform.sendline(input);
                            }
                            if (str.Contains("Leap"))
                            {
                                TimeSpan ts = DateTime.Now - lastSpell;

                                theform.AddIRCText("Leap");
                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x1D, true));// LSHIFT
                                input.Add(new keyinput(0x07, true));
                                input.Add(new keyinput(0x07, false));
                                input.Add(new keyinput(0x1D, false));
                                theform.sendline(input);
                            }
                            if(str.Contains("Location"))
                            {
                                if(str.Contains("Stormwind"))
                                {
                                    // We're in stormwind
                                    Location = "Stormwind";
                                }
                                else
                                {
                                    Location = "battleground";
                                }

                            }
                        }
                        if (lastreadslot < LastWrittenSlot)
                            lastreadslot = readslot;
                    }
                }

            }
        }
        public WoahObject GetItem(ulong guid)
        {
           // mutex.WaitOne();
            if (guid == 0){
                return null;
            }
            if (((WoahObject)objsById[guid]) == null)
                return null;
            WoahObject returnme = new WoahObject((WoahObject)objsById[guid]);
            

           // mutex.ReleaseMutex();
            return (WoahObject)objsById[guid];
        }
    }
}
