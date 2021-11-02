using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Woah
{
    /// <summary>
    /// The GUID structure used to identify WOW Objects
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct WGUID
    {
        /// <summary>
        /// The full GUID
        /// </summary>
        [FieldOffset(0)]
        public ulong GUID;

        /// <summary>
        /// The low part of the GUID
        /// </summary>
        [FieldOffset(0)]
        public int Low;

        /// <summary>
        /// The high part of the GUID
        /// </summary>
        [FieldOffset(4)]
        public int High;
    };

    /// <summary>
    /// These are for the bit mask that is 0x4 deep into the WOW Object
    /// Meaning a player will have type mask (1 | 8 | 16). 
    /// A bag (container) will have (1 | 2 | 4). Etc.
    /// </summary>
    /// 
    // MORE TROUBLE THAN IT IS WORTH AT THIS POINT
//     public enum WoahObjectType
//     {
//         Object = 1,
//         Item = 2,
//         Container = 4,
//         Unit = 8,
//         Player = 16,
//         GameObject = 32,
//         DynamicObject = 64,
//         Corpse = 128,
//     }

    /// <summary>
    /// These are NOT bitmask values, found in the WoW linked list object
    /// </summary>
    public enum WoahObjectType
    {
        Unknown = 0,
        Item = 1,
        Container = 2,
        Unit = 3,
        Player = 4,
        GameObject = 5,
        Dynamic = 6,
        Corpse = 7
    }

    public class WoahObject
    {
        WoahEnvironment env;

        /// <summary>
        /// Pointer to the object and its storage
        /// </summary>
        private int ptrBase;
        private int ptrStorage;
        private int ptrStorageEnd;

        //private WoahObjectTypeIDs typeID;
        private WoahObjectType type;

        private WGUID guid;

        internal bool invalid = false;

        /// <summary>
        /// Position information, not all objects have positioning, those without
        /// will have these variables set to 0
        /// </summary>
        private float x, y, z, facing;

        /// <summary>
        /// Object name (if any)
        /// </summary>
        private string name;

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ptr">Pointer to the object</param>
        /// <param name="env"></param>
        internal WoahObject(int ptr, WoahEnvironment environment)
		{
            this.env = environment;
			this.ptrBase = ptr;

            type = (WoahObjectType) env.Memory.ReadInteger(ptrBase + 0x14);
            guid.GUID = (ulong)env.Memory.ReadLong(ptrBase + 0x30);

			ptrStorage = env.Memory.ReadInteger( ptrBase + 0x08);
			ptrStorageEnd = env.Memory.ReadInteger( ptrBase + 0x0C);

		}

        internal WoahObject(WoahObject woah)
        {
            this.env = woah.env;
            this.ptrBase = woah.ptrBase;
            this.ptrStorage = woah.ptrStorage;
            this.ptrStorageEnd = woah.ptrStorageEnd;
            this.type = woah.type;
            this.guid.GUID = woah.guid.GUID;
        }

        /// <summary>
        /// GUID of the object
        /// </summary>
        public WGUID GUID
        {
            get
            {
                return guid;
            }
        }

        /// <summary>
        /// Name of the object (if any)
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public float X
        {
            get
            {
                return x;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
        }
        public float Z
        {
            get
            {
                return z;
            }
        }

        /// <summary>
        /// Type of the object
        /// </summary>
//         public WoahObjectType Typeasdf
//         {
//             get
//             {
//                 return type;
//             }
//         }

        /// <summary>
        /// Type ID of the object
        /// </summary>
        public WoahObjectType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Checks if the object is still valid.
        /// Currently only done by GUID.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (invalid)
                    return false;

                // If the object GUID has changed, it no longer is valid
                if (guid.GUID != (ulong)env.Memory.ReadLong(ptrBase + 0x30))
                {
                    invalid = true;
                    return false;
                }

                return true;
            }
        }

        public int BasePtr
        {
            get
            {
                return ptrBase;
            }
        }




        /// <summary>
        /// Updates the (cached) value's
        /// </summary>
        /// <returns>False, the object is no longer valid</returns>
        public bool Update()
        {
            lock (this)
            {


                // Check if the object is still valid
                if (!IsValid)
                    return false;

                // Although this *should* never change, just to be sure
                type = (WoahObjectType)env.Memory.ReadInteger(ptrBase + 0x14);

                if (type == WoahObjectType.Item)
                {
                    x = y = z = facing = 0;

                    int itemid = ReadStorageInt((int)EObjectFields.OBJECT_FIELD_ENTRY);//owner.Descriptors[type, "OBJECT_FIELD_ENTRY"]);
                    //name = (string)owner.itemNames[itemid];
                }

                if (type == WoahObjectType.Container)
                {
                    x = y = z = facing = 0;

                    int itemid = ReadStorageInt((int)EObjectFields.OBJECT_FIELD_ENTRY);//owner.Descriptors[type, "OBJECT_FIELD_ENTRY"]);
                    //name = (string)owner.itemNames[itemid];
                }

                if (type == WoahObjectType.Unit)
                {
                    y = env.Memory.ReadFloat(ptrBase + 0xbe0);
                    x = env.Memory.ReadFloat(ptrBase + 0xbe4);
                    z = env.Memory.ReadFloat(ptrBase + 0xbe8);
                    facing = env.Memory.ReadFloat(ptrBase + 0xb8c);
                    //speed = env.Memory.ReadFloat(ptrBase + owner.offsets.getOffset("CGPlayer_C__Speed"));

                    int basePtr = env.Memory.ReadInteger(ptrBase + 0xD30);
                    if (basePtr != 0)
                    {
                        basePtr = env.Memory.ReadInteger(basePtr + 0x04);
                        if (basePtr != 0)
                            name = env.Memory.ReadString(basePtr, 64);
                    }
                }

                if (type == WoahObjectType.Player)
                {
                    y = env.Memory.ReadFloat(ptrBase + 0xbe0);
                    x = env.Memory.ReadFloat(ptrBase + 0xbe4);
                    z = env.Memory.ReadFloat(ptrBase + 0xbe8);
                    facing = env.Memory.ReadFloat(ptrBase + 0xb8c);

                    //name = (string)owner.playerNames[GUID.GUID];
                }

                if (type == WoahObjectType.GameObject)
                {
                    //y = env.Memory.ReadFloat(ptrBase + 0xb80);
                    //x = env.Memory.ReadFloat(ptrBase + 0xb84);
                    //z = env.Memory.ReadFloat(ptrBase + 0xb88);
                    //facing = env.Memory.ReadFloat(ptrBase + 0xb8c);
                    x = ReadStorageFloat((int)EGameobjectFields.GAMEOBJECT_POS_X);
                    y = ReadStorageFloat((int)EGameobjectFields.GAMEOBJECT_POS_Y);
                    z = ReadStorageFloat((int)EGameobjectFields.GAMEOBJECT_POS_Z);
                    int basePtr = env.Memory.ReadInteger(ptrBase + 0xD30);
                    if (basePtr != 0)
                    {
                        basePtr = env.Memory.ReadInteger(basePtr + 0x04);
                        if (basePtr != 0)
                            name = env.Memory.ReadString(basePtr, 64);
                    }
                }

                //             if (type == WoahObjectTypeIDs.Corpse)
                //             {
                //                 int basePtr = owner.Memory.ReadInteger(ptrBase + 0x110);
                // 
                //                 x = y = z = facing = speed = 0;
                //                 if (basePtr != 0)
                //                 {
                //                     y = owner.Memory.ReadFloat(basePtr + 0x0C);
                //                     x = owner.Memory.ReadFloat(basePtr + 0x10);
                //                     z = owner.Memory.ReadFloat(basePtr + 0x14);
                //                     facing = owner.Memory.ReadFloat(basePtr + 0x18);
                // 
                //                     long ownerid = ReadStorageLong(owner.Descriptors[type, "CORPSE_FIELD_OWNER"]);
                //                     name = "Corpse of " + (string)owner.playerNames[ownerid];
                //                 }
                //             }
            }
            return true;
        }

        /// <summary>
        /// Read a integer from the object storage
        /// </summary>
        /// <param name="field">Field offset</param>
        /// <returns>Value</returns>
        public int ReadStorageInt(int field)
        {
            if ((ptrStorage + field) > ptrStorageEnd)
                return -1;

            return env.Memory.ReadInteger(ptrStorage + field);
        }

        public int ReadStorageChar(int field)
        {
            if ((ptrStorage + field) > ptrStorageEnd)
                return -1;

            byte[] ret = env.Memory.ReadBuffer(ptrStorage + field, 1);

            return ret[0];
        }



        /// <summary>
        /// Read a long from the object storage
        /// </summary>
        /// <param name="field">Field offset</param>
        /// <returns>Value</returns>
        public long ReadStorageLong(int field)
        {
            if ((ptrStorage + field) > ptrStorageEnd)
                return -1;

            return env.Memory.ReadLong(ptrStorage + field);
        }

        /// <summary>
        /// Read a float from the object storage
        /// </summary>
        /// <param name="field">Field offset</param>
        /// <returns>Value</returns>
        public float ReadStorageFloat(int field)
        {
            if ((ptrStorage + field) > ptrStorageEnd)
                return -1;

            return env.Memory.ReadFloat(ptrStorage + field);
        }

        #region Item Specific stuff

        /// <summary>
        /// Is this item openable ?
        /// </summary>
        public bool IsOpenable
        {
            get
            {
                if (Type != WoahObjectType.Item)
                    return false;

                return (ReadStorageInt((int)EItemFields.ITEM_FIELD_FLAGS) & 0x4) != 0;
            }
        }

        /// <summary>
        /// Returns the Id of an item
        /// </summary>
        public int ItemId
        {
            get
            {
                if (Type != WoahObjectType.Item)
                    return 0;

                return ReadStorageInt((int)EObjectFields.OBJECT_FIELD_ENTRY);
            }
        }

        /// <summary>
        /// Returns the amount of items
        /// </summary>
        public int StackCount
        {
            get
            {
                if (Type != WoahObjectType.Item)
                    return 0;

                return ReadStorageInt((int)EItemFields.ITEM_FIELD_STACK_COUNT);
            }
        }
#endregion

        #region GameObject specific things... (Mailboxes, Mines, etc)

        /// <summary>
        /// Returns true if the bobber is bobbing
        /// </summary>
        public bool BobberBobbing
        {
            get
            {
                if (Type != WoahObjectType.GameObject)
                    return false;

                return env.Memory.ReadInteger(ptrBase + 0xF4) != 0;//F4 as of 2.4.2? maybe 2.4.1, 0xfC before
            }
        }
        #endregion

        #region Unit and/or Player specific properties and functions
        /// <summary>
        /// Returns the unit/player's HP
        /// </summary>
        public int Unit_Health
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return ReadStorageInt((int)EUnitFields.UNIT_FIELD_HEALTH);
            }
        }

        public ArrayList Unit_Buffs
        {
            get
            {
                ArrayList buffs = new ArrayList();
                for(int i = 0; i < 56; ++i)
                {
                    int buffID = ReadStorageInt((int)EUnitFields.UNIT_FIELD_AURA + (i*4));
                   if(buffID != 0x0)
                   {
                        buffs.Add(buffID);
                   }
                }
                return buffs;
            }

        }
        

        /// <summary>
        /// Returns the unit/player's Maximum HP
        /// </summary>
        public int Unit_MaximumHealth
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return ReadStorageInt((int)EUnitFields.UNIT_FIELD_MAXHEALTH);
            }
        }

        /// <summary>
        /// Returns the health percentage
        /// </summary>
        public float Unit_HealthPercentage
        {
            get
            {
                return (float)Unit_Health / (float)Unit_MaximumHealth * 100f;
            }
        }

        /// <summary>
        /// Returns the current mana
        /// </summary>
        public int Unit_Mana
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return (int)ReadStorageInt((int)EUnitFields.UNIT_FIELD_POWER1);
            }
        }

        /// <summary>
        /// Returns the unit/player's Maximum Mana
        /// </summary>
        public int Unit_MaximumMana
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return ReadStorageInt((int)EUnitFields.UNIT_FIELD_MAXPOWER1);
            }
        }

        /// <summary>
        /// Returns the mana percentage
        /// </summary>
        public float Unit_ManaPercentage
        {
            get
            {
                // if there's no mana, return 100%
                if (Unit_MaximumMana == 0)
                    return 100f;

                return ((float)Unit_Mana / (float)Unit_MaximumMana) * 100f;
            }
        }

        /// <summary>
        /// Returns the current rage
        /// </summary>
        public int Unit_Rage
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return (int)(ReadStorageInt((int)EUnitFields.UNIT_FIELD_POWER2));
            }
        }

        /// <summary>
        /// Returns the number of combo points
        /// </summary>
        public int Unit_ComboPoints
        {
            get
            {
                if (Type != WoahObjectType.Player)
                    return 0;

                return env.Memory.ReadInteger(0xbe5139) & 0xFF;
            }
        }

        /// <summary>
        /// Returns the current focus
        /// </summary>
        public int Unit_Focus
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return (int)ReadStorageInt((int)EUnitFields.UNIT_FIELD_POWER3);
            }
        }

        /// <summary>
        /// Returns the current energy
        /// </summary>
        public int Unit_Energy
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return 0;

                return (int)ReadStorageInt((int)EUnitFields.UNIT_FIELD_POWER4);
            }
        }

        /// <summary>
        /// Returns the current Happiness of a pet as a percentage
        /// </summary>
        public int Unit_HappinessPercentage
        {
            get
            {
                if (Type != WoahObjectType.Unit)
                    return 0;

                float petHappiness = (float)ReadStorageInt((int)EUnitFields.UNIT_FIELD_POWER5);
                float petMaxHappiness = (float)ReadStorageInt((int)EUnitFields.UNIT_FIELD_MAXPOWER5);

                if (petMaxHappiness == 0)
                    return 100;

                return (int)((petHappiness / petMaxHappiness) * 100);
            }
        }

        /// <summary>
        /// The unit/player's target
        /// </summary>
        public WoahObject Unit_Target
        {
            get
            {
                // Implement a singleton hash table that maps ptrs to GUIDS
                  if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                     return null;
 
                  return (WoahObject)env.GetItem(Unit_TargetGUID);
            }
        }

        /// <summary>
        /// Returns unit/player's pet
        /// </summary>
        public WoahObject Unit_Pet
        {
            get
            {
                // This is mostly complete, just need that handy GUIDS to ptrs singleton
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return null;

                ulong pet = (ulong)ReadStorageLong((int)EUnitFields.UNIT_FIELD_SUMMON);
                if (pet == 0)
                    pet = (ulong)ReadStorageLong((int)EUnitFields.UNIT_FIELD_CHARM);

                return (WoahObject)env.GetItem(pet);
            }
        }

        /// <summary>
        /// If this object is a pet, then this returns its owner
        /// </summary>
        public WoahObject Unit_Owner
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return null;

                ulong ownerid = (ulong)ReadStorageLong((int)EUnitFields.UNIT_FIELD_SUMMONEDBY);
                if (ownerid == 0)
                    ownerid = (ulong)ReadStorageLong((int)EUnitFields.UNIT_FIELD_CHARMEDBY);

                return (WoahObject)env.GetItem(ownerid);
            }
        }

        /// <summary>
        /// Used internally to get number of attackers while updating
        /// </summary>
        internal ulong Unit_TargetGUID
        {
            get
            {
                if (Type == WoahObjectType.Unit || Type == WoahObjectType.Player)
                    return (ulong)ReadStorageLong((int)EUnitFields.UNIT_FIELD_TARGET);

                return 0;
            }
        }

        /// <summary>
        /// Unit/player is dead?
        /// </summary>
        public bool Unit_IsDead
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return false;

                return Unit_Health == 0;
            }
        }

        /// <summary>
        /// Unit/player sitting?
        /// </summary>
        public bool Unit_IsSitting
        {
            get
            {
                if (Type != WoahObjectType.Unit && Type != WoahObjectType.Player)
                    return false;

                return (ReadStorageInt((int)EUnitFields.UNIT_FIELD_BYTES_1) & 0xFF) != 0;
            }
        }

        /// <summary>
        /// Unit is skinnable ?
        /// </summary>
        public bool Unit_IsSkinnable
        {
            get
            {
                if (type != WoahObjectType.Unit)
                    return false;

                return (ReadStorageInt((int)EUnitFields.UNIT_FIELD_FLAGS) & 0x4000000) != 0;
            }
        }

        /// <summary>
        /// Unit can be looted?
        /// </summary>
        public bool Unit_CanLoot
        {
            get
            {
                if (type != WoahObjectType.Unit)
                    return false;

                return (ReadStorageInt((int)EUnitFields.UNIT_DYNAMIC_FLAGS) & 1) != 0;
            }
        }

        /// <summary>
        /// Unit / player level
        /// </summary>
        public int Unit_Level
        {
            get
            {
                if (type == WoahObjectType.Unit || type == WoahObjectType.Player)
                    return ReadStorageInt((int)EUnitFields.UNIT_FIELD_LEVEL);

                if (type == WoahObjectType.GameObject)
                    return ReadStorageInt((int)EGameobjectFields.GAMEOBJECT_LEVEL);

                return 1;
            }
        }

        /// <summary>
        /// Is player casting a spell (either normal or channel spell)
        /// </summary>
        public bool Unit_IsCasting
        {
            get
            {
                if (type != WoahObjectType.Player)
                    return false;

                return (env.Memory.ReadInteger(BasePtr + 0xF38) != 0) || (ReadStorageInt((int)EUnitFields.UNIT_CHANNEL_SPELL) != 0);
            }
        }

        public int Unit_GetCasting
        {
            get
            {
                if (type != WoahObjectType.Player)
                    return 0;
                if(Unit_IsCasting == false)
                    return 0;

                int cast = env.Memory.ReadInteger(BasePtr + 0xF38);
                if(cast == 0)
                {
                    cast = ReadStorageInt((int)EUnitFields.UNIT_CHANNEL_SPELL);
                    return cast;
                }
                else
                    return cast;
                
            }
        }

        /// <summary>
        /// Is the player attacking using a melee weapon?
        /// </summary>
//         public bool IsAttacking
//         {
//             get
//             {
//                 if (type != WoahObjectType.Player)
//                     return false;
// 
//                 return (owner.env.Memory.ReadLong(BasePtr + owner.offsets.getOffset("CGPlayer_C__MeleeTarget")) != 0);
//             }
//         }

        /// <summary>
        /// Is the player (auto) shooting using a ranged weapon?
        /// </summary>
//         public bool IsShooting
//         {
//             get
//             {
//                 if (type != WoahObjectType.Player)
//                     return false;
// 
//                 return ((owner.env.Memory.ReadInteger(BasePtr + owner.offsets.getOffset("CGPlayer_C__IsShootingFlag")) & 0x1000) != 0);
//             }
//         }

        /// <summary>
        /// Is unit or player in combat?
        /// </summary>
        public bool Unit_IsInCombat
        {
            get
            {
                if (type != WoahObjectType.Unit && type != WoahObjectType.Player)
                    return false;

                return (ReadStorageInt((int)EUnitFields.UNIT_FIELD_FLAGS) & 0x80000) != 0;
            }
        }

        /// <summary>
        /// Returns the object thats being channeled, for fishing it contains the fishing bobber
        /// </summary>
        public WoahObject Unit_ChannelObject
        {
            get
            {
                if (type != WoahObjectType.Player)
                    return null;

                ulong guid = (ulong)ReadStorageLong((int)EUnitFields.UNIT_FIELD_CHANNEL_OBJECT);
                if (guid != 0x0)
                    return (WoahObject)env.GetItem(guid);
                else
                    return null;
                 // See note about guid singleton above
            }
        }

        private int _racecache = -1;
        /// <summary>
        /// Returns the unit's race
        /// </summary>
        public int Unit_UnitRace
        {
            get
            {
                if (_racecache != -1)
                    return _racecache;

                if (type != WoahObjectType.Unit && type != WoahObjectType.Player)
                    return -1;

                int basePtr = env.Memory.ReadInteger(ptrBase + 0x128);
                int raceid = env.Memory.ReadInteger(basePtr + 0x78) & 0xFF;

                _racecache = raceid;
                return raceid;
            }
        }

        private int _classcache = -1;
        /// <summary>
        /// Returns the unit's class
        /// </summary>
        public int Unit_UnitClass
        {
            get
            {
                if (_classcache != -1)
                    return _classcache;

                if (type != WoahObjectType.Unit && type != WoahObjectType.Player)
                    return -1;

       
                int basePtr = env.Memory.ReadInteger(ptrBase + 0x128);
                int classid = env.Memory.ReadInteger(basePtr + 0x79) & 0xFF;
                _classcache = classid;
                return classid;
            }
        }

        public ArrayList Player_Equipped
        {
            get
            {
                if (type != WoahObjectType.Player)
                    return null;
                ArrayList items = new ArrayList();
                for(int i = 0; i < 46; ++i)
                {
                    long item = ReadStorageLong((int)EUnitFields.PLAYER_FIELD_INV_SLOT_HEAD + i * 8);
                    items.Add(item);
                }

                return items;

            }
        }

        #endregion

        public float GetDistance(WoahObject obj)
        {
            if (obj == null)
                return 0;

            return GetDistance(obj.X, obj.Y, obj.Z);
        }

        public float GetDistance(float x, float y, float z)
        {
            float dX = X - x;
            float dY = Y - y;
            float dZ = (z != 0 ? Z - z : 0);

            return (float)Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        }

    }
}
