using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace Woah
{
    #region Game object enums
    public enum ERaceID
    {
        PET = 0,
        HUMAN = 1,
        ORC = 2,
        DWARF = 3,
        NIGHTELF = 4,
        UNDEAD = 5,
        TAURAN = 6,
        GNOME = 7,
        TROLL = 8,
        BLOODELF = 10,
        DRAENAI = 11,

    }
    public enum EClassID
    {
        WARRIOR = 1,
        PALADIN = 2,
        HUNTER = 3,
        ROGUE = 4,
        PRIEST = 5,
        SHAMAN = 7,
        MAGE = 8,
        WARLOCK = 9,
        DRUID = 11,
    }
    public enum EObjectFields
    {
        OBJECT_FIELD_GUID = 0x0, // Type: Guid , Size: 2
        OBJECT_FIELD_TYPE = 0x8, // Type: Int32, Size: 1
        OBJECT_FIELD_ENTRY = 0xC, // Type: Int32, Size: 1
        OBJECT_FIELD_SCALE_X = 0x10, // Type: Float, Size: 1
        OBJECT_FIELD_PADDING = 0x14, // Type: Int32, Size: 1
    }

    public enum EItemFields
    {
        ITEM_FIELD_OWNER = 0x18, // Type: Guid , Size: 2
        ITEM_FIELD_CONTAINED = 0x20, // Type: Guid , Size: 2
        ITEM_FIELD_CREATOR = 0x28, // Type: Guid , Size: 2
        ITEM_FIELD_GIFTCREATOR = 0x30, // Type: Guid , Size: 2
        ITEM_FIELD_STACK_COUNT = 0x38, // Type: Int32, Size: 1
        ITEM_FIELD_DURATION = 0x3C, // Type: Int32, Size: 1
        ITEM_FIELD_SPELL_CHARGES = 0x40, // Type: Int32, Size: 5
        ITEM_FIELD_FLAGS = 0x54, // Type: Int32, Size: 1
        ITEM_FIELD_ENCHANTMENT = 0x58, // Type: Int32, Size: 33
        ITEM_FIELD_PROPERTY_SEED = 0xDC, // Type: Int32, Size: 1
        ITEM_FIELD_RANDOM_PROPERTIES_ID = 0xE0, // Type: Int32, Size: 1
        ITEM_FIELD_ITEM_TEXT_ID = 0xE4, // Type: Int32, Size: 1
        ITEM_FIELD_DURABILITY = 0xE8, // Type: Int32, Size: 1
        ITEM_FIELD_MAXDURABILITY = 0xEC, // Type: Int32, Size: 1
    }

    public enum EContainerFields
    {
        CONTAINER_FIELD_NUM_SLOTS = 0xF0, // Type: Int32, Size: 1
        CONTAINER_ALIGN_PAD = 0xF4, // Type: Chars, Size: 1
        CONTAINER_FIELD_SLOT_1 = 0xF8, // Type: Guid , Size: 72
    }

    public enum EUnitFields
    {
        UNIT_FIELD_CHARM = 0x18, // Type: Guid , Size: 2
        UNIT_FIELD_SUMMON = 0x20, // Type: Guid , Size: 2
        UNIT_FIELD_CHARMEDBY = 0x28, // Type: Guid , Size: 2
        UNIT_FIELD_SUMMONEDBY = 0x30, // Type: Guid , Size: 2
        UNIT_FIELD_CREATEDBY = 0x38, // Type: Guid , Size: 2
        UNIT_FIELD_TARGET = 0x40, // Type: Guid , Size: 2
        UNIT_FIELD_PERSUADED = 0x48, // Type: Guid , Size: 2
        UNIT_FIELD_CHANNEL_OBJECT = 0x50, // Type: Guid , Size: 2
        UNIT_FIELD_HEALTH = 0x58, // Type: Int32, Size: 1
        UNIT_FIELD_POWER1 = 0x5C, // Type: Int32, Size: 1
        UNIT_FIELD_POWER2 = 0x60, // Type: Int32, Size: 1
        UNIT_FIELD_POWER3 = 0x64, // Type: Int32, Size: 1
        UNIT_FIELD_POWER4 = 0x68, // Type: Int32, Size: 1
        UNIT_FIELD_POWER5 = 0x6C, // Type: Int32, Size: 1
        UNIT_FIELD_MAXHEALTH = 0x70, // Type: Int32, Size: 1
        UNIT_FIELD_MAXPOWER1 = 0x74, // Type: Int32, Size: 1
        UNIT_FIELD_MAXPOWER2 = 0x78, // Type: Int32, Size: 1
        UNIT_FIELD_MAXPOWER3 = 0x7C, // Type: Int32, Size: 1
        UNIT_FIELD_MAXPOWER4 = 0x80, // Type: Int32, Size: 1
        UNIT_FIELD_MAXPOWER5 = 0x84, // Type: Int32, Size: 1
        UNIT_FIELD_LEVEL = 0x88, // Type: Int32, Size: 1
        UNIT_FIELD_FACTIONTEMPLATE = 0x8C, // Type: Int32, Size: 1
        UNIT_FIELD_BYTES_0 = 0x90, // Type: Chars, Size: 1
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 0x94, // Type: Int32, Size: 3
        UNIT_VIRTUAL_ITEM_INFO = 0xA0, // Type: Chars, Size: 6
        UNIT_FIELD_FLAGS = 0xB8, // Type: Int32, Size: 1
        UNIT_FIELD_FLAGS_2 = 0xBC, // Type: Int32, Size: 1
        UNIT_FIELD_AURA = 0xC0, // Type: Int32, Size: 56
        UNIT_FIELD_AURAFLAGS = 0x1A0, // Type: Chars, Size: 7
        UNIT_FIELD_AURALEVELS = 0x1BC, // Type: Chars, Size: 14
        UNIT_FIELD_AURAAPPLICATIONS = 0x1F4, // Type: Chars, Size: 14
        UNIT_FIELD_AURASTATE = 0x22C, // Type: Int32, Size: 1
        UNIT_FIELD_BASEATTACKTIME = 0x230, // Type: Int32, Size: 2
        UNIT_FIELD_RANGEDATTACKTIME = 0x238, // Type: Int32, Size: 1
        UNIT_FIELD_BOUNDINGRADIUS = 0x23C, // Type: Float, Size: 1
        UNIT_FIELD_COMBATREACH = 0x240, // Type: Float, Size: 1
        UNIT_FIELD_DISPLAYID = 0x244, // Type: Int32, Size: 1
        UNIT_FIELD_NATIVEDISPLAYID = 0x248, // Type: Int32, Size: 1
        UNIT_FIELD_MOUNTDISPLAYID = 0x24C, // Type: Int32, Size: 1
        UNIT_FIELD_MINDAMAGE = 0x250, // Type: Float, Size: 1
        UNIT_FIELD_MAXDAMAGE = 0x254, // Type: Float, Size: 1
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x258, // Type: Float, Size: 1
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x25C, // Type: Float, Size: 1
        UNIT_FIELD_BYTES_1 = 0x260, // Type: Chars, Size: 1
        UNIT_FIELD_PETNUMBER = 0x264, // Type: Int32, Size: 1
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x268, // Type: Int32, Size: 1
        UNIT_FIELD_PETEXPERIENCE = 0x26C, // Type: Int32, Size: 1
        UNIT_FIELD_PETNEXTLEVELEXP = 0x270, // Type: Int32, Size: 1
        UNIT_DYNAMIC_FLAGS = 0x274, // Type: Int32, Size: 1
        UNIT_CHANNEL_SPELL = 0x294, // Type: Int32, Size: 1
        UNIT_MOD_CAST_SPEED = 0x27C, // Type: Float, Size: 1
        UNIT_CREATED_BY_SPELL = 0x280, // Type: Int32, Size: 1
        UNIT_NPC_FLAGS = 0x284, // Type: Int32, Size: 1
        UNIT_NPC_EMOTESTATE = 0x288, // Type: Int32, Size: 1
        UNIT_TRAINING_POINTS = 0x28C, // Type: Chars, Size: 1
        UNIT_FIELD_STR = 0x290, // Type: Int32, Size: 1
        UNIT_FIELD_AGILITY = 0x294, // Type: Int32, Size: 1
        UNIT_FIELD_STAMINA = 0x298, // Type: Int32, Size: 1
        UNIT_FIELD_IQ = 0x29C, // Type: Int32, Size: 1
        UNIT_FIELD_SPIRIT = 0x2A0, // Type: Int32, Size: 1
        UNIT_FIELD_POSSTAT0 = 0x2A4, // Type: Int32, Size: 1
        UNIT_FIELD_POSSTAT1 = 0x2A8, // Type: Int32, Size: 1
        UNIT_FIELD_POSSTAT2 = 0x2AC, // Type: Int32, Size: 1
        UNIT_FIELD_POSSTAT3 = 0x2B0, // Type: Int32, Size: 1
        UNIT_FIELD_POSSTAT4 = 0x2B4, // Type: Int32, Size: 1
        UNIT_FIELD_NEGSTAT0 = 0x2B8, // Type: Int32, Size: 1
        UNIT_FIELD_NEGSTAT1 = 0x2BC, // Type: Int32, Size: 1
        UNIT_FIELD_NEGSTAT2 = 0x2C0, // Type: Int32, Size: 1
        UNIT_FIELD_NEGSTAT3 = 0x2C4, // Type: Int32, Size: 1
        UNIT_FIELD_NEGSTAT4 = 0x2C8, // Type: Int32, Size: 1
        UNIT_FIELD_RESISTANCES = 0x2CC, // Type: Int32, Size: 7
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x2E8, // Type: Int32, Size: 7
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x304, // Type: Int32, Size: 7
        UNIT_FIELD_BASE_MANA = 0x320, // Type: Int32, Size: 1
        UNIT_FIELD_BASE_HEALTH = 0x324, // Type: Int32, Size: 1
        UNIT_FIELD_BYTES_2 = 0x328, // Type: Chars, Size: 1
        UNIT_FIELD_ATTACK_POWER = 0x32C, // Type: Int32, Size: 1
        UNIT_FIELD_ATTACK_POWER_MODS = 0x330, // Type: Chars, Size: 1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0x334, // Type: Float, Size: 1
        UNIT_FIELD_RANGED_ATTACK_POWER = 0x338, // Type: Int32, Size: 1
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0x33C, // Type: Chars, Size: 1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0x340, // Type: Float, Size: 1
        UNIT_FIELD_MINRANGEDDAMAGE = 0x344, // Type: Float, Size: 1
        UNIT_FIELD_MAXRANGEDDAMAGE = 0x348, // Type: Float, Size: 1
        UNIT_FIELD_POWER_COST_MODIFIER = 0x34C, // Type: Int32, Size: 7
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0x368, // Type: Float, Size: 7
        UNIT_FIELD_PADDING = 0x384, // Type: Int32, Size: 1

        // Player fields
        PLAYER_DUEL_ARBITER = 0x388, // Type: Guid , Size: 2
        PLAYER_FLAGS = 0x390, // Type: Int32, Size: 1
        PLAYER_GUILDID = 0x394, // Type: Int32, Size: 1
        PLAYER_GUILDRANK = 0x398, // Type: Int32, Size: 1
        PLAYER_BYTES = 0x39C, // Type: Chars, Size: 1
        PLAYER_BYTES_2 = 0x3A0, // Type: Chars, Size: 1
        PLAYER_BYTES_3 = 0x3A4, // Type: Chars, Size: 1
        PLAYER_DUEL_TEAM = 0x3A8, // Type: Int32, Size: 1
        PLAYER_GUILD_TIMESTAMP = 0x3AC, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_1_1 = 0x3B0, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_1_2 = 0x3B4, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_2_1 = 0x3BC, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_2_2 = 0x3C0, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_3_1 = 0x3C8, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_3_2 = 0x3CC, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_4_1 = 0x3D4, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_4_2 = 0x3D8, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_5_1 = 0x3E0, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_5_2 = 0x3E4, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_6_1 = 0x3EC, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_6_2 = 0x3F0, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_7_1 = 0x3F8, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_7_2 = 0x3FC, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_8_1 = 0x404, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_8_2 = 0x408, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_9_1 = 0x410, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_9_2 = 0x414, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_10_1 = 0x41C, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_10_2 = 0x420, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_11_1 = 0x428, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_11_2 = 0x42C, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_12_1 = 0x434, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_12_2 = 0x438, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_13_1 = 0x440, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_13_2 = 0x444, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_14_1 = 0x44C, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_14_2 = 0x450, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_15_1 = 0x458, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_15_2 = 0x45C, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_16_1 = 0x464, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_16_2 = 0x468, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_17_1 = 0x470, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_17_2 = 0x474, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_18_1 = 0x47C, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_18_2 = 0x480, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_19_1 = 0x488, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_19_2 = 0x48C, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_20_1 = 0x494, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_20_2 = 0x498, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_21_1 = 0x4A0, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_21_2 = 0x4A4, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_22_1 = 0x4AC, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_22_2 = 0x4B0, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_23_1 = 0x4B8, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_23_2 = 0x4BC, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_24_1 = 0x4C4, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_24_2 = 0x4C8, // Type: Int32, Size: 2
        PLAYER_QUEST_LOG_25_1 = 0x4D0, // Type: Int32, Size: 1
        PLAYER_QUEST_LOG_25_2 = 0x4D4, // Type: Int32, Size: 2
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x4DC, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_1_0 = 0x4E4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x514, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x518, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_2_CREATOR = 0x51C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_2_0 = 0x524, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_2_PROPERTIES = 0x554, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_2_PAD = 0x558, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_3_CREATOR = 0x55C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_3_0 = 0x564, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_3_PROPERTIES = 0x594, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_3_PAD = 0x598, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_4_CREATOR = 0x59C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_4_0 = 0x5A4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_4_PROPERTIES = 0x5D4, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_4_PAD = 0x5D8, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_5_CREATOR = 0x5DC, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_5_0 = 0x5E4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_5_PROPERTIES = 0x614, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_5_PAD = 0x618, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_6_CREATOR = 0x61C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_6_0 = 0x624, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_6_PROPERTIES = 0x654, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_6_PAD = 0x658, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_7_CREATOR = 0x65C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_7_0 = 0x664, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_7_PROPERTIES = 0x694, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_7_PAD = 0x698, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_8_CREATOR = 0x69C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_8_0 = 0x6A4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_8_PROPERTIES = 0x6D4, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_8_PAD = 0x6D8, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_9_CREATOR = 0x6DC, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_9_0 = 0x6E4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_9_PROPERTIES = 0x714, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_9_PAD = 0x718, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_10_CREATOR = 0x71C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_10_0 = 0x724, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_10_PROPERTIES = 0x754, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_10_PAD = 0x758, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_11_CREATOR = 0x75C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_11_0 = 0x764, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_11_PROPERTIES = 0x794, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_11_PAD = 0x798, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_12_CREATOR = 0x79C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_12_0 = 0x7A4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_12_PROPERTIES = 0x7D4, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_12_PAD = 0x7D8, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_13_CREATOR = 0x7DC, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_13_0 = 0x7E4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_13_PROPERTIES = 0x814, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_13_PAD = 0x818, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_14_CREATOR = 0x81C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_14_0 = 0x824, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_14_PROPERTIES = 0x854, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_14_PAD = 0x858, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_15_CREATOR = 0x85C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_15_0 = 0x864, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_15_PROPERTIES = 0x894, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_15_PAD = 0x898, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_16_CREATOR = 0x89C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_16_0 = 0x8A4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_16_PROPERTIES = 0x8D4, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_16_PAD = 0x8D8, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_17_CREATOR = 0x8DC, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_17_0 = 0x8E4, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_17_PROPERTIES = 0x914, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_17_PAD = 0x918, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_18_CREATOR = 0x91C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_18_0 = 0x924, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_18_PROPERTIES = 0x954, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_18_PAD = 0x958, // Type: Int32, Size: 1
        PLAYER_VISIBLE_ITEM_19_CREATOR = 0x95C, // Type: Guid , Size: 2
        PLAYER_VISIBLE_ITEM_19_0 = 0x964, // Type: Int32, Size: 12
        PLAYER_VISIBLE_ITEM_19_PROPERTIES = 0x994, // Type: Chars, Size: 1
        PLAYER_VISIBLE_ITEM_19_PAD = 0x998, // Type: Int32, Size: 1
        PLAYER_CHOSEN_TITLE = 0x99C, // Type: Int32, Size: 1
        PLAYER_FIELD_INV_SLOT_HEAD = 0x9A0, // Type: Guid , Size: 46
        PLAYER_FIELD_PACK_SLOT_1 = 0xA58, // Type: Guid , Size: 32
        PLAYER_FIELD_BANK_SLOT_1 = 0xAD8, // Type: Guid , Size: 56
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0xBB8, // Type: Guid , Size: 14
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0xBF0, // Type: Guid , Size: 24
        PLAYER_FIELD_KEYRING_SLOT_1 = 0xC50, // Type: Guid , Size: 64
        PLAYER_FARSIGHT = 0xD50, // Type: Guid , Size: 2
        PLAYER__FIELD_KNOWN_TITLES = 0xD58, // Type: Guid , Size: 2
        PLAYER_XP = 0xD60, // Type: Int32, Size: 1
        PLAYER_NEXT_LEVEL_XP = 0xD64, // Type: Int32, Size: 1
        PLAYER_SKILL_INFO_START = 0xD68, // Type: Chars, Size: 384
        PLAYER_CHARACTER_POINTS1 = 0x1368, // Type: Int32, Size: 1
        PLAYER_CHARACTER_POINTS2 = 0x136C, // Type: Int32, Size: 1
        PLAYER_TRACK_CREATURES = 0x1370, // Type: Int32, Size: 1
        PLAYER_TRACK_RESOURCES = 0x1374, // Type: Int32, Size: 1
        PLAYER_BLOCK_PERCENTAGE = 0x1378, // Type: Float, Size: 1
        PLAYER_DODGE_PERCENTAGE = 0x137C, // Type: Float, Size: 1
        PLAYER_PARRY_PERCENTAGE = 0x1380, // Type: Float, Size: 1
        PLAYER_CRIT_PERCENTAGE = 0x1384, // Type: Float, Size: 1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x1388, // Type: Float, Size: 1
        PLAYER_OFFHAND_CRIT_PERCENTAGE = 0x138C, // Type: Float, Size: 1
        PLAYER_SPELL_CRIT_PERCENTAGE1 = 0x1390, // Type: Float, Size: 7
        PLAYER_EXPLORED_ZONES_1 = 0x13AC, // Type: Chars, Size: 64
        PLAYER_REST_STATE_EXPERIENCE = 0x14AC, // Type: Int32, Size: 1
        PLAYER_FIELD_COINAGE = 0x14B0, // Type: Int32, Size: 1
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x14B4, // Type: Int32, Size: 7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x14D0, // Type: Int32, Size: 7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x14EC, // Type: Int32, Size: 7
        PLAYER_FIELD_MOD_HEALING_DONE_POS = 0x1508, // Type: Int32, Size: 1
        PLAYER_FIELD_MOD_TARGET_RESISTANCE = 0x150C, // Type: Int32, Size: 1
        PLAYER_FIELD_BYTES = 0x1510, // Type: Chars, Size: 1
        PLAYER_AMMO_ID = 0x1514, // Type: Int32, Size: 1
        PLAYER_SELF_RES_SPELL = 0x1518, // Type: Int32, Size: 1
        PLAYER_FIELD_PVP_MEDALS = 0x151C, // Type: Int32, Size: 1
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x1520, // Type: Int32, Size: 12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x1550, // Type: Int32, Size: 12
        PLAYER_FIELD_KILLS = 0x1580, // Type: Chars, Size: 1
        PLAYER_FIELD_TODAY_CONTRIBUTION = 0x1584, // Type: Int32, Size: 1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x1588, // Type: Int32, Size: 1
        PLAYER_FIELD_LIFETIME_HONORBALE_KILLS = 0x158C, // Type: Int32, Size: 1
        PLAYER_FIELD_BYTES2 = 0x1590, // Type: Chars, Size: 1
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x1594, // Type: Int32, Size: 1
        PLAYER_FIELD_COMBAT_RATING_1 = 0x1598, // Type: Int32, Size: 23
        PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = 0x15F4, // Type: Int32, Size: 15
        PLAYER_FIELD_HONOR_CURRENCY = 0x1630, // Type: Int32, Size: 1
        PLAYER_FIELD_ARENA_CURRENCY = 0x1634, // Type: Int32, Size: 1
        PLAYER_FIELD_MOD_MANA_REGEN = 0x1638, // Type: Float, Size: 1
        PLAYER_FIELD_MOD_MANA_REGEN_INTERRUPT = 0x163C, // Type: Float, Size: 1
        PLAYER_FIELD_MAX_LEVEL = 0x1640, // Type: Int32, Size: 1
        PLAYER_FIELD_DAILY_QUESTS_1 = 0x1644, // Type: Int32, Size: 10
        PLAYER_FIELD_PADDING = 0x166C, // Type: Int32, Size: 1
    }

    public enum EGameobjectFields
    {
        OBJECT_FIELD_CREATED_BY = 0x18, // Type: Guid , Size: 2
        GAMEOBJECT_DISPLAYID = 0x20, // Type: Int32, Size: 1
        GAMEOBJECT_FLAGS = 0x24, // Type: Int32, Size: 1
        GAMEOBJECT_ROTATION = 0x28, // Type: Float, Size: 4
        GAMEOBJECT_STATE = 0x38, // Type: Int32, Size: 1
        GAMEOBJECT_POS_Y = 0x3C, // Type: Float, Size: 1
        GAMEOBJECT_POS_X = 0x40, // Type: Float, Size: 1
        GAMEOBJECT_POS_Z = 0x44, // Type: Float, Size: 1
        GAMEOBJECT_FACING = 0x48, // Type: Float, Size: 1
        GAMEOBJECT_DYN_FLAGS = 0x4C, // Type: Int32, Size: 1
        GAMEOBJECT_FACTION = 0x50, // Type: Int32, Size: 1
        GAMEOBJECT_TYPE_ID = 0x54, // Type: Int32, Size: 1
        GAMEOBJECT_LEVEL = 0x58, // Type: Int32, Size: 1
        GAMEOBJECT_ARTKIT = 0x5C, // Type: Int32, Size: 1
        GAMEOBJECT_ANIMPROGRESS = 0x60, // Type: Int32, Size: 1
        GAMEOBJECT_PADDING = 0x64, // Type: Int32, Size: 1
    }

    public enum EDynamicobjectFields
    {
        DYNAMICOBJECT_CASTER = 0x18, // Type: Guid , Size: 2
        DYNAMICOBJECT_BYTES = 0x20, // Type: Chars, Size: 1
        DYNAMICOBJECT_SPELLID = 0x24, // Type: Int32, Size: 1
        DYNAMICOBJECT_RADIUS = 0x28, // Type: Float, Size: 1
        DYNAMICOBJECT_POS_X = 0x2C, // Type: Float, Size: 1
        DYNAMICOBJECT_POS_Y = 0x30, // Type: Float, Size: 1
        DYNAMICOBJECT_POS_Z = 0x34, // Type: Float, Size: 1
        DYNAMICOBJECT_FACING = 0x38, // Type: Float, Size: 1
        DYNAMICOBJECT_PAD = 0x3C, // Type: Chars, Size: 1
    }

    public enum ECorpseFields
    {
        CORPSE_FIELD_OWNER = 0x18, // Type: Guid , Size: 2
        CORPSE_FIELD_FACING = 0x20, // Type: Float, Size: 1
        CORPSE_FIELD_POS_X = 0x24, // Type: Float, Size: 1
        CORPSE_FIELD_POS_Y = 0x28, // Type: Float, Size: 1
        CORPSE_FIELD_POS_Z = 0x2C, // Type: Float, Size: 1
        CORPSE_FIELD_DISPLAY_ID = 0x30, // Type: Int32, Size: 1
        CORPSE_FIELD_ITEM = 0x34, // Type: Int32, Size: 19
        CORPSE_FIELD_BYTES_1 = 0x80, // Type: Chars, Size: 1
        CORPSE_FIELD_BYTES_2 = 0x84, // Type: Chars, Size: 1
        CORPSE_FIELD_GUILD = 0x88, // Type: Int32, Size: 1
        CORPSE_FIELD_FLAGS = 0x8C, // Type: Int32, Size: 1
        CORPSE_FIELD_DYNAMIC_FLAGS = 0x90, // Type: Int32, Size: 1
        CORPSE_FIELD_PAD = 0x94, // Type: Int32, Size: 1
    }
    #endregion

    public delegate void myKeyEventHandler(Object o, int key);

    public struct keyinput
    {
        public keyinput(int k, bool p) { press = p; key = k; }
        public bool press;
        public int key;
    };

    public partial class WoahFish : Form
    {
        #region P/INVOKE crap

        //[DllImport("User32.dll")]
        //private static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] KEYBOARD_INPUT[] input, int structSize);
        [DllImport("User32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);
        [DllImport("kernel32.dll")]
        private static extern bool Thread32First(IntPtr hSnapshot, ref THREADENTRY32 lppe);
        [DllImport("kernel32.dll")]
        private static extern bool Thread32Next(IntPtr hSnapshot, ref THREADENTRY32 lppe);
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("ntdll.dll")]
        private static extern uint NtQueryInformationThread(IntPtr handle, uint infclass, ref THREAD_BASIC_INFORMATION info, uint length, IntPtr bytesread);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);
        #endregion
        #region Supporting Structs for P/INVOKE crap
        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBOARD_INPUT
        {
            public uint type;
            public ushort vk;
            public ushort scanCode;
            public uint flags;
            public uint time;
            public uint extrainfo;
            public uint padding1;
            public uint padding2;
        }


        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct THREADENTRY32
        {
            public UInt32 dwSize;
            public UInt32 cntUsage;
            public UInt32 th32ThreadID;
            public UInt32 th32OwnerProcessID;
            public Int32 tpBasePri;
            public Int32 tpDeltaPri;
            public UInt32 dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct THREAD_BASIC_INFORMATION
        {
            public bool ExitStatus;
            public IntPtr TebBaseAddress;
            public uint processid;
            public uint threadid;
            public uint AffinityMask;
            public uint Priority;
            public uint BasePriority;
        }
        #endregion
        #region Supporting Member variables for P/INVOKE crap
        private const int INPUT_MOUSE = 0;
        private const int INPUT_KEYBOARD = 1;
        private const int INPUT_HARDWARE = 2;
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;
        private const uint KEYEVENTF_SCANCODE = 0x0008;
        private const uint XBUTTON1 = 0x0001;
        private const uint XBUTTON2 = 0x0002;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const uint MOUSEEVENTF_XDOWN = 0x0080;
        private const uint MOUSEEVENTF_XUP = 0x0100;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;
        private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelKeyboardProc _proc;
        private Process[] m_Process;
        #endregion

        private NotifyIcon m_notifyicon;
        private ContextMenu m_menu;
        private MenuItem Enabledcheck;

        private WoahEnvironment env;
        int WowObjectBasePointer;
        ulong MyGUID;

        ArrayList thebots;

        public event myKeyEventHandler HookKeyPress;

        cIRC IrcApp;
        String status_string;
        private bool enabled = false;

        public WoahFish()
        {
            InitializeComponent();

            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0,
                new MenuItem("Enable", new System.EventHandler(popup)));

            Enabledcheck = m_menu.MenuItems[0];

            m_notifyicon = new NotifyIcon();
            m_notifyicon.Text = "Right click for context menu";
            m_notifyicon.Visible = true;
            m_notifyicon.Icon = new Icon(GetType(), "Icon2.ico");
            m_notifyicon.ContextMenu = m_menu;
            m_notifyicon.MouseClick += new MouseEventHandler(m_notifyicon_Click);
        }

        public bool resetbauble = false;

        public void Enable()
        {
            enabled = true;
            Enabledcheck.Checked = true;
            m_notifyicon.Icon = new Icon(GetType(), "Icon1.ico");
        }

        public void Disable()
        {
            enabled = false;
            Enabledcheck.Checked = false;
            m_notifyicon.Icon = new Icon(GetType(), "Icon2.ico");
        }

        private void m_notifyicon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Enabledcheck.Checked == true)
                {
                    Enable();
                }
                else
                {
                    Disable();
                }
                Invalidate();
            }
        }
        private void popup(object sender, EventArgs e)
        {
            MenuItem miClicked = (MenuItem)sender;
            string item = miClicked.Text;
            if (item == "Enable")
            {
                if (Enabledcheck.Checked == true)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }
            }
            Invalidate();
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);


        public void AddIRCText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddIRCText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox5.Text = text + "\r\n" + this.textBox5.Text;
            }
        }




        private void ReadWowTLS()
        {
            uint THREAD_QUERY_INFORMATION = 0x40;
            IntPtr snaphandle = IntPtr.Zero;
            IntPtr threadhandle = IntPtr.Zero;

            int g_clientConnection = 0;

            int slot = 0;// env.Memory.ReadInteger(0xE530C4); // LIT

            snaphandle = CreateToolhelp32Snapshot(MemoryReader.TH32CS_SNAPTHREAD, 0);
            if (snaphandle != null)
            {
                THREADENTRY32 info = new THREADENTRY32();
                info.dwSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(THREADENTRY32));
                bool morethreads = true;
                bool found = false;
                if (Thread32First(snaphandle, ref info))
                {
                    while (morethreads && !found)
                    {
                        if (info.th32OwnerProcessID == env.Memory.ReadProcess.Id)
                        {
                            threadhandle = OpenThread(THREAD_QUERY_INFORMATION, false, info.th32ThreadID);
                            if (threadhandle != null)
                            {
                                g_clientConnection = env.Memory.ReadInteger((int)(0x00D43318));
                                WowObjectBasePointer = env.Memory.ReadInteger((int)(g_clientConnection + 0x2218));

                                THREAD_BASIC_INFORMATION tbi = new THREAD_BASIC_INFORMATION();
                                if (NtQueryInformationThread(threadhandle, 0, ref tbi, (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(THREAD_BASIC_INFORMATION)), IntPtr.Zero) == 0)
                                {
//                                     int tlsoffset = env.Memory.ReadInteger((int)tbi.TebBaseAddress + 0x2c);
//                                     int targetslot = env.Memory.ReadInteger(tlsoffset + (slot * 4));
//                                     WowObjectBasePointer = env.Memory.ReadInteger(targetslot + 16);
                                    MyGUID = (ulong)env.Memory.ReadLong(WowObjectBasePointer + 0xC0);
                                    //int typee = env.Memory.ReadInteger(env.Memory.ReadInteger(WowObjectBasePointer));
                                    //status_string = "Base pointer found: " + WowObjectBasePointer.ToString("X") + "\n";
                                    //status_string += "GUID of player:     " + MyGUID.ToString("X") + "\n";
                                    CloseHandle(threadhandle);
                                    found = true;
                                    env.Initialize(MyGUID, WowObjectBasePointer);
                                    env.Update();
                                }
                            }
                        }
                        info.dwSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(THREADENTRY32));
                        morethreads = Thread32Next(snaphandle, ref info);
                    }
                }
                CloseHandle(snaphandle);
            }
        }

        private void SetupBotMenus()
        {
            thebots = new ArrayList();
            thebots.Add(new FishBot(env, this));
            


            foreach (WoahBotBase b in thebots)
            {
                string name = b.GetName();
                MenuItem botbase = new MenuItem(name);
                b.SetupMenu(botbase);
                m_menu.MenuItems.Add(botbase);
                Invalidate();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _proc = new LowLevelKeyboardProc(HookCallback);
            _hookID = SetHook(_proc);
            env = new WoahEnvironment(this);

            try
            {
                m_Process = env.Memory.GetProcessesByExe("wow.exe");
                if (m_Process.Length == 0)
                {
                    status_string = "Could not open wow.exe.  Are you logged into World of Warcraft?";
                    return;
                }
            }
            catch (Exception E)
            {
                throw E;
            }
            try
            {
                env.Memory.Open(m_Process[0]);
                if ((int)env.Memory.Handle == 0)
                    status_string = "Unknown Error.";
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        private bool menussetup = false;

        private void button1_Click(object sender, EventArgs e)
        {
            ReadWowTLS();

            if (!menussetup)
            {
                SetupBotMenus();
                menussetup = true;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IrcApp.die();
            UnhookWindowsHookEx(_hookID);
        }

        public void SendIrcMessage(string msg)
        {
            IrcApp.SendMessage(msg);
        }



        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public void QuitApp()
        {
            IrcApp.die();
            Environment.Exit(0);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);


        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (HookKeyPress != null)
                    HookKeyPress(this, vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void press(int scanCode)
        {
            sendKey(scanCode, true);
        }

        public void release(int scanCode)
        {
            sendKey(scanCode, false);
        }

        public void click(bool right)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = 0;
            inputs[0].mi.dy = 0;
            if (right)
                inputs[0].mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
            else
                inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;


            uint result = SendInput(1, inputs, Marshal.SizeOf(inputs[0]));
            if (result != 1)
            {
                throw new Exception("Could not click mouse ");
            }
        }

        public void unclick(bool right)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = 0;
            inputs[0].mi.dy = 0;
            if (right)
                inputs[0].mi.dwFlags = MOUSEEVENTF_RIGHTUP;
            else
                inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTUP;

            uint result = SendInput(1, inputs, Marshal.SizeOf(inputs[0]));
            if (result != 1)
            {
                throw new Exception("Could not click mouse ");
            }
        }

        private void sendKey(int scanCode, bool press)
        {
            // KEYBOARD_INPUT[] input = new KEYBOARD_INPUT[1];
            // input[0] = new KEYBOARD_INPUT();
            // input[0].type = INPUT_KEYBOARD;
            // input[0].flags = KEYEVENTF_SCANCODE;

            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].ki.dwFlags = KEYEVENTF_SCANCODE;

            if ((scanCode & 0xFF00) == 0xE000)
            { // extended key?
                inputs[0].ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
            }

            if (press)
            { // press?
                inputs[0].ki.wScan = (ushort)(scanCode & 0xFF);
            }
            else
            { // release?
                inputs[0].ki.wScan = (ushort)scanCode;
                inputs[0].ki.dwFlags |= KEYEVENTF_KEYUP;
            }

            uint result = SendInput(1, inputs, Marshal.SizeOf(inputs[0]));

            if (result != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }

        public void fullclick(bool right)
        {
            INPUT[] inputs = new INPUT[2];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = 0;
            inputs[0].mi.dy = 0;

            inputs[1].type = INPUT_MOUSE;
            inputs[1].mi.dx = 0;
            inputs[1].mi.dy = 0;
            if (right)
                inputs[0].mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
            else
                inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

             if (right)
                inputs[1].mi.dwFlags = MOUSEEVENTF_RIGHTUP;
            else
                inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;



            uint result = SendInput(2, inputs, Marshal.SizeOf(inputs[0]));
            if (result != 2)
            {
                throw new Exception("Could not click mouse ");
            }
        }
        

        public void sendline(ArrayList inputline)
        {
            // KEYBOARD_INPUT[] input = new KEYBOARD_INPUT[1];
            // input[0] = new KEYBOARD_INPUT();
            // input[0].type = INPUT_KEYBOARD;
            // input[0].flags = KEYEVENTF_SCANCODE;
            INPUT[] inputs = new INPUT[inputline.Count];
            int i = 0;

            foreach (keyinput ki in inputline)
            {
                inputs[i].type = INPUT_KEYBOARD;
                inputs[i].ki.dwFlags = KEYEVENTF_SCANCODE;

                if ((ki.key & 0xFF00) == 0xE000)
                { // extended key?
                    inputs[i].ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
                }

                if (ki.press)
                { // press?
                    inputs[i].ki.wScan = (ushort)(ki.key & 0xFF);
                }
                else
                { // release?
                    inputs[i].ki.wScan = (ushort)ki.key;
                    inputs[i].ki.dwFlags |= KEYEVENTF_KEYUP;
                }
                ++i;

            }




            uint result = SendInput((uint)inputline.Count, inputs, Marshal.SizeOf(inputs[0]));

            if (result != (uint)inputline.Count)
            {
                int err = Marshal.GetLastWin32Error();
                throw new Exception("Could not send keys");
            }
        }
        private string clipstring;
        // separate method to get data
        private void GetClipboardDataObject()
        {

            Clipboard.SetText(clipstring);
        } 
        
        public void ClipboardCopy(string msg)
        {
            clipstring = msg;
            Thread t = new Thread(new ThreadStart(GetClipboardDataObject));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
           
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            ReadWowTLS();

            if (!menussetup)
            {
                SetupBotMenus();
                menussetup = true;
            }
            if (IrcApp != null)
                IrcApp.die();
            IrcApp = new cIRC(textBox4.Text, 6667, textBox3.Text, textBox2.Text, this);

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            env.Update();
            int bla = env.Memory.ReadInteger(0xCF5750);
            this.Text = Cursor.Position.ToString();
            if (enabled)
            {
                foreach (WoahBotBase b in thebots)
                {
                    b.DoAction();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


    }
}