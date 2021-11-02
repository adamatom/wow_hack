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

            if (luaArray != null && luaArray[arrayOffset] == 10 && luaArray[arrayOffset + 1] == 215 && luaArray[arrayOffset + 2] == 163 && luaArray[arrayOffset + 3] == 112 && luaArray[arrayOffset + 4] == 61 && luaArray[arrayOffset + 5] == 74 && luaArray[arrayOffset + 6] == 147 && luaArray[arrayOffset + 7] == 64
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

        private string oldmessage;


        public void Update()
        {
            numcontainers = 0;

            try
            {
                if (memory.IsOpen)
                {
                    if (WowObjectBasePointer == 0)
                        return;

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
                            if (!obj.Update()) // returns false if not valid 
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

                    playernames.Update();
                    if (FindLuaArray())
                    {
                        LastWrittenSlot = System.Convert.ToInt32(BitConverter.ToDouble(luaArray, arrayOffset + 32));
                        if(LastWrittenSlot < lastreadslot) // buffer circled around, just reset 
                        {
                            lastreadslot = -1;
                        }
                        while (lastreadslot < LastWrittenSlot)
                        {
                            int readslot = lastreadslot + 1;
                            int stringpointer = System.Convert.ToInt32(BitConverter.ToInt32(luaArray, arrayOffset + (16 * readslot) + 48));


                            int strlength = Memory.ReadInteger(stringpointer + 0x10);
                            byte[] stringarray;
                            stringarray = memory.ReadBuffer(stringpointer + 0x14, strlength);

                            string str;
                            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                            str = enc.GetString(stringarray);



                            if(str != "EMPTY")
                                theform.SendIrcMessage(str);
                            if (lastreadslot < LastWrittenSlot)
                                lastreadslot = readslot;
                        }
                    }
                }
            }
            catch
            {

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
