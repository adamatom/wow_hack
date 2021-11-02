using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        //OBJECT_FIELD_GUID = 0x0, // Type: Guid , Size: 2
        //OBJECT_FIELD_TYPE = 0x8, // Type: Int32, Size: 1
        //OBJECT_FIELD_ENTRY = 0xC, // Type: Int32, Size: 1
        //OBJECT_FIELD_SCALE_X = 0x10, // Type: Float, Size: 1
        //OBJECT_FIELD_PADDING = 0x14, // Type: Int32, Size: 1

        OBJECT_FIELD_GUID = 0x0,
        OBJECT_FIELD_TYPE = 0x2,
        OBJECT_FIELD_ENTRY = 0x3,
        OBJECT_FIELD_SCALE_X = 0x4,
        OBJECT_FIELD_PADDING = 0x5,
        TOTAL_OBJECT_FIELDS = 0x5,
    }

    public enum EItemFields
    {
        ITEM_FIELD_OWNER = 0x6,
        ITEM_FIELD_CONTAINED = 0x8,
        ITEM_FIELD_CREATOR = 0xA,
        ITEM_FIELD_GIFTCREATOR = 0xC,
        ITEM_FIELD_STACK_COUNT = 0xE,
        ITEM_FIELD_DURATION = 0xF,
        ITEM_FIELD_SPELL_CHARGES = 0x10,
        ITEM_FIELD_FLAGS = 0x15,
        ITEM_FIELD_ENCHANTMENT_1_1 = 0x16,
        ITEM_FIELD_ENCHANTMENT_1_3 = 0x18,
        ITEM_FIELD_ENCHANTMENT_2_1 = 0x19,
        ITEM_FIELD_ENCHANTMENT_2_3 = 0x1B,
        ITEM_FIELD_ENCHANTMENT_3_1 = 0x1C,
        ITEM_FIELD_ENCHANTMENT_3_3 = 0x1E,
        ITEM_FIELD_ENCHANTMENT_4_1 = 0x1F,
        ITEM_FIELD_ENCHANTMENT_4_3 = 0x21,
        ITEM_FIELD_ENCHANTMENT_5_1 = 0x22,
        ITEM_FIELD_ENCHANTMENT_5_3 = 0x24,
        ITEM_FIELD_ENCHANTMENT_6_1 = 0x25,
        ITEM_FIELD_ENCHANTMENT_6_3 = 0x27,
        ITEM_FIELD_ENCHANTMENT_7_1 = 0x28,
        ITEM_FIELD_ENCHANTMENT_7_3 = 0x2A,
        ITEM_FIELD_ENCHANTMENT_8_1 = 0x2B,
        ITEM_FIELD_ENCHANTMENT_8_3 = 0x2D,
        ITEM_FIELD_ENCHANTMENT_9_1 = 0x2E,
        ITEM_FIELD_ENCHANTMENT_9_3 = 0x30,
        ITEM_FIELD_ENCHANTMENT_10_1 = 0x31,
        ITEM_FIELD_ENCHANTMENT_10_3 = 0x33,
        ITEM_FIELD_ENCHANTMENT_11_1 = 0x34,
        ITEM_FIELD_ENCHANTMENT_11_3 = 0x36,
        ITEM_FIELD_ENCHANTMENT_12_1 = 0x37,
        ITEM_FIELD_ENCHANTMENT_12_3 = 0x39,
        ITEM_FIELD_PROPERTY_SEED = 0x3A,
        ITEM_FIELD_RANDOM_PROPERTIES_ID = 0x3B,
        ITEM_FIELD_ITEM_TEXT_ID = 0x3C,
        ITEM_FIELD_DURABILITY = 0x3D,
        ITEM_FIELD_MAXDURABILITY = 0x3E,
        ITEM_FIELD_PAD = 0x3F,
        TOTAL_ITEM_FIELDS = 0x26
    }

    public enum EContainerFields
    {
        CONTAINER_FIELD_NUM_SLOTS = 0x6,
        CONTAINER_ALIGN_PAD = 0x7,
        CONTAINER_FIELD_SLOT_1 = 0x8,
        TOTAL_CONTAINER_FIELDS = 0x3
    }

    public enum EUnitFields
    {
        UNIT_FIELD_CHARM = 0x6,
        UNIT_FIELD_SUMMON = 0x8*4,
        UNIT_FIELD_CRITTER = 0xA*4,
        UNIT_FIELD_CHARMEDBY = 0xC,
        UNIT_FIELD_SUMMONEDBY = 0xE,
        UNIT_FIELD_CREATEDBY = 0x10,
        UNIT_FIELD_TARGET = 0x12*4,
        UNIT_FIELD_CHANNEL_OBJECT = 0x14,
        UNIT_FIELD_BYTES_0 = 0x16,
        UNIT_FIELD_HEALTH = 0x17 * 4,
        UNIT_FIELD_POWER1 = 0x18,
        UNIT_FIELD_POWER2 = 0x19,
        UNIT_FIELD_POWER3 = 0x1A,
        UNIT_FIELD_POWER4 = 0x1B,
        UNIT_FIELD_POWER5 = 0x1C,
        UNIT_FIELD_POWER6 = 0x1D,
        UNIT_FIELD_POWER7 = 0x1E,
        UNIT_FIELD_MAXHEALTH = 0x1F,
        UNIT_FIELD_MAXPOWER1 = 0x20,
        UNIT_FIELD_MAXPOWER2 = 0x21,
        UNIT_FIELD_MAXPOWER3 = 0x22,
        UNIT_FIELD_MAXPOWER4 = 0x23,
        UNIT_FIELD_MAXPOWER5 = 0x24,
        UNIT_FIELD_MAXPOWER6 = 0x25,
        UNIT_FIELD_MAXPOWER7 = 0x26,
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER = 0x27,
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER = 0x2E,
        UNIT_FIELD_LEVEL = 0x35,
        UNIT_FIELD_FACTIONTEMPLATE = 0x36,
        UNIT_VIRTUAL_ITEM_SLOT_ID = 0x37,
        UNIT_FIELD_FLAGS = 0x3A,
        UNIT_FIELD_FLAGS_2 = 0x3B,
        UNIT_FIELD_AURASTATE = 0x3C,
        UNIT_FIELD_BASEATTACKTIME = 0x3D,
        UNIT_FIELD_RANGEDATTACKTIME = 0x3F,
        UNIT_FIELD_BOUNDINGRADIUS = 0x40,
        UNIT_FIELD_COMBATREACH = 0x41,
        UNIT_FIELD_DISPLAYID = 0x42,
        UNIT_FIELD_NATIVEDISPLAYID = 0x43,
        UNIT_FIELD_MOUNTDISPLAYID = 0x44,
        UNIT_FIELD_MINDAMAGE = 0x45,
        UNIT_FIELD_MAXDAMAGE = 0x46,
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x47,
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x48,
        UNIT_FIELD_BYTES_1 = 0x49,
        UNIT_FIELD_PETNUMBER = 0x4A,
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x4B,
        UNIT_FIELD_PETEXPERIENCE = 0x4C,
        UNIT_FIELD_PETNEXTLEVELEXP = 0x4D,
        UNIT_DYNAMIC_FLAGS = 0x4E,
        UNIT_CHANNEL_SPELL = 0x4F,
        UNIT_MOD_CAST_SPEED = 0x50,
        UNIT_CREATED_BY_SPELL = 0x51,
        UNIT_NPC_FLAGS = 0x52,
        UNIT_NPC_EMOTESTATE = 0x53,
        UNIT_FIELD_STAT0 = 0x54,
        UNIT_FIELD_STAT1 = 0x55,
        UNIT_FIELD_STAT2 = 0x56,
        UNIT_FIELD_STAT3 = 0x57,
        UNIT_FIELD_STAT4 = 0x58,
        UNIT_FIELD_POSSTAT0 = 0x59,
        UNIT_FIELD_POSSTAT1 = 0x5A,
        UNIT_FIELD_POSSTAT2 = 0x5B,
        UNIT_FIELD_POSSTAT3 = 0x5C,
        UNIT_FIELD_POSSTAT4 = 0x5D,
        UNIT_FIELD_NEGSTAT0 = 0x5E,
        UNIT_FIELD_NEGSTAT1 = 0x5F,
        UNIT_FIELD_NEGSTAT2 = 0x60,
        UNIT_FIELD_NEGSTAT3 = 0x61,
        UNIT_FIELD_NEGSTAT4 = 0x62,
        UNIT_FIELD_RESISTANCES = 0x63,
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x6A,
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x71,
        UNIT_FIELD_BASE_MANA = 0x78,
        UNIT_FIELD_BASE_HEALTH = 0x79,
        UNIT_FIELD_BYTES_2 = 0x7A,
        UNIT_FIELD_ATTACK_POWER = 0x7B,
        UNIT_FIELD_ATTACK_POWER_MODS = 0x7C,
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0x7D,
        UNIT_FIELD_RANGED_ATTACK_POWER = 0x7E,
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0x7F,
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0x80,
        UNIT_FIELD_MINRANGEDDAMAGE = 0x81,
        UNIT_FIELD_MAXRANGEDDAMAGE = 0x82,
        UNIT_FIELD_POWER_COST_MODIFIER = 0x83,
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0x8A,
        UNIT_FIELD_MAXHEALTHMODIFIER = 0x91,
        UNIT_FIELD_HOVERHEIGHT = 0x92,
        UNIT_FIELD_PADDING = 0x93,
        TOTAL_UNIT_FIELDS = 0x59,

        // Player fields
        PLAYER_DUEL_ARBITER = 0x94,
        PLAYER_FLAGS = 0x96,
        PLAYER_GUILDID = 0x97,
        PLAYER_GUILDRANK = 0x98,
        PLAYER_BYTES = 0x99,
        PLAYER_BYTES_2 = 0x9A,
        PLAYER_BYTES_3 = 0x9B,
        PLAYER_DUEL_TEAM = 0x9C,
        PLAYER_GUILD_TIMESTAMP = 0x9D,
        PLAYER_QUEST_LOG_1_1 = 0x9E,
        PLAYER_QUEST_LOG_1_2 = 0x9F,
        PLAYER_QUEST_LOG_1_3 = 0xA0,
        PLAYER_QUEST_LOG_1_4 = 0xA1,
        PLAYER_QUEST_LOG_2_1 = 0xA2,
        PLAYER_QUEST_LOG_2_2 = 0xA3,
        PLAYER_QUEST_LOG_2_3 = 0xA4,
        PLAYER_QUEST_LOG_2_4 = 0xA5,
        PLAYER_QUEST_LOG_3_1 = 0xA6,
        PLAYER_QUEST_LOG_3_2 = 0xA7,
        PLAYER_QUEST_LOG_3_3 = 0xA8,
        PLAYER_QUEST_LOG_3_4 = 0xA9,
        PLAYER_QUEST_LOG_4_1 = 0xAA,
        PLAYER_QUEST_LOG_4_2 = 0xAB,
        PLAYER_QUEST_LOG_4_3 = 0xAC,
        PLAYER_QUEST_LOG_4_4 = 0xAD,
        PLAYER_QUEST_LOG_5_1 = 0xAE,
        PLAYER_QUEST_LOG_5_2 = 0xAF,
        PLAYER_QUEST_LOG_5_3 = 0xB0,
        PLAYER_QUEST_LOG_5_4 = 0xB1,
        PLAYER_QUEST_LOG_6_1 = 0xB2,
        PLAYER_QUEST_LOG_6_2 = 0xB3,
        PLAYER_QUEST_LOG_6_3 = 0xB4,
        PLAYER_QUEST_LOG_6_4 = 0xB5,
        PLAYER_QUEST_LOG_7_1 = 0xB6,
        PLAYER_QUEST_LOG_7_2 = 0xB7,
        PLAYER_QUEST_LOG_7_3 = 0xB8,
        PLAYER_QUEST_LOG_7_4 = 0xB9,
        PLAYER_QUEST_LOG_8_1 = 0xBA,
        PLAYER_QUEST_LOG_8_2 = 0xBB,
        PLAYER_QUEST_LOG_8_3 = 0xBC,
        PLAYER_QUEST_LOG_8_4 = 0xBD,
        PLAYER_QUEST_LOG_9_1 = 0xBE,
        PLAYER_QUEST_LOG_9_2 = 0xBF,
        PLAYER_QUEST_LOG_9_3 = 0xC0,
        PLAYER_QUEST_LOG_9_4 = 0xC1,
        PLAYER_QUEST_LOG_10_1 = 0xC2,
        PLAYER_QUEST_LOG_10_2 = 0xC3,
        PLAYER_QUEST_LOG_10_3 = 0xC4,
        PLAYER_QUEST_LOG_10_4 = 0xC5,
        PLAYER_QUEST_LOG_11_1 = 0xC6,
        PLAYER_QUEST_LOG_11_2 = 0xC7,
        PLAYER_QUEST_LOG_11_3 = 0xC8,
        PLAYER_QUEST_LOG_11_4 = 0xC9,
        PLAYER_QUEST_LOG_12_1 = 0xCA,
        PLAYER_QUEST_LOG_12_2 = 0xCB,
        PLAYER_QUEST_LOG_12_3 = 0xCC,
        PLAYER_QUEST_LOG_12_4 = 0xCD,
        PLAYER_QUEST_LOG_13_1 = 0xCE,
        PLAYER_QUEST_LOG_13_2 = 0xCF,
        PLAYER_QUEST_LOG_13_3 = 0xD0,
        PLAYER_QUEST_LOG_13_4 = 0xD1,
        PLAYER_QUEST_LOG_14_1 = 0xD2,
        PLAYER_QUEST_LOG_14_2 = 0xD3,
        PLAYER_QUEST_LOG_14_3 = 0xD4,
        PLAYER_QUEST_LOG_14_4 = 0xD5,
        PLAYER_QUEST_LOG_15_1 = 0xD6,
        PLAYER_QUEST_LOG_15_2 = 0xD7,
        PLAYER_QUEST_LOG_15_3 = 0xD8,
        PLAYER_QUEST_LOG_15_4 = 0xD9,
        PLAYER_QUEST_LOG_16_1 = 0xDA,
        PLAYER_QUEST_LOG_16_2 = 0xDB,
        PLAYER_QUEST_LOG_16_3 = 0xDC,
        PLAYER_QUEST_LOG_16_4 = 0xDD,
        PLAYER_QUEST_LOG_17_1 = 0xDE,
        PLAYER_QUEST_LOG_17_2 = 0xDF,
        PLAYER_QUEST_LOG_17_3 = 0xE0,
        PLAYER_QUEST_LOG_17_4 = 0xE1,
        PLAYER_QUEST_LOG_18_1 = 0xE2,
        PLAYER_QUEST_LOG_18_2 = 0xE3,
        PLAYER_QUEST_LOG_18_3 = 0xE4,
        PLAYER_QUEST_LOG_18_4 = 0xE5,
        PLAYER_QUEST_LOG_19_1 = 0xE6,
        PLAYER_QUEST_LOG_19_2 = 0xE7,
        PLAYER_QUEST_LOG_19_3 = 0xE8,
        PLAYER_QUEST_LOG_19_4 = 0xE9,
        PLAYER_QUEST_LOG_20_1 = 0xEA,
        PLAYER_QUEST_LOG_20_2 = 0xEB,
        PLAYER_QUEST_LOG_20_3 = 0xEC,
        PLAYER_QUEST_LOG_20_4 = 0xED,
        PLAYER_QUEST_LOG_21_1 = 0xEE,
        PLAYER_QUEST_LOG_21_2 = 0xEF,
        PLAYER_QUEST_LOG_21_3 = 0xF0,
        PLAYER_QUEST_LOG_21_4 = 0xF1,
        PLAYER_QUEST_LOG_22_1 = 0xF2,
        PLAYER_QUEST_LOG_22_2 = 0xF3,
        PLAYER_QUEST_LOG_22_3 = 0xF4,
        PLAYER_QUEST_LOG_22_4 = 0xF5,
        PLAYER_QUEST_LOG_23_1 = 0xF6,
        PLAYER_QUEST_LOG_23_2 = 0xF7,
        PLAYER_QUEST_LOG_23_3 = 0xF8,
        PLAYER_QUEST_LOG_23_4 = 0xF9,
        PLAYER_QUEST_LOG_24_1 = 0xFA,
        PLAYER_QUEST_LOG_24_2 = 0xFB,
        PLAYER_QUEST_LOG_24_3 = 0xFC,
        PLAYER_QUEST_LOG_24_4 = 0xFD,
        PLAYER_QUEST_LOG_25_1 = 0xFE,
        PLAYER_QUEST_LOG_25_2 = 0xFF,
        PLAYER_QUEST_LOG_25_3 = 0x100,
        PLAYER_QUEST_LOG_25_4 = 0x101,
        PLAYER_VISIBLE_ITEM_1_ENTRYID = 0x102,
        PLAYER_VISIBLE_ITEM_1_ENCHANTMENT = 0x103,
        PLAYER_VISIBLE_ITEM_2_ENTRYID = 0x104,
        PLAYER_VISIBLE_ITEM_2_ENCHANTMENT = 0x105,
        PLAYER_VISIBLE_ITEM_3_ENTRYID = 0x106,
        PLAYER_VISIBLE_ITEM_3_ENCHANTMENT = 0x107,
        PLAYER_VISIBLE_ITEM_4_ENTRYID = 0x108,
        PLAYER_VISIBLE_ITEM_4_ENCHANTMENT = 0x109,
        PLAYER_VISIBLE_ITEM_5_ENTRYID = 0x10A,
        PLAYER_VISIBLE_ITEM_5_ENCHANTMENT = 0x10B,
        PLAYER_VISIBLE_ITEM_6_ENTRYID = 0x10C,
        PLAYER_VISIBLE_ITEM_6_ENCHANTMENT = 0x10D,
        PLAYER_VISIBLE_ITEM_7_ENTRYID = 0x10E,
        PLAYER_VISIBLE_ITEM_7_ENCHANTMENT = 0x10F,
        PLAYER_VISIBLE_ITEM_8_ENTRYID = 0x110,
        PLAYER_VISIBLE_ITEM_8_ENCHANTMENT = 0x111,
        PLAYER_VISIBLE_ITEM_9_ENTRYID = 0x112,
        PLAYER_VISIBLE_ITEM_9_ENCHANTMENT = 0x113,
        PLAYER_VISIBLE_ITEM_10_ENTRYID = 0x114,
        PLAYER_VISIBLE_ITEM_10_ENCHANTMENT = 0x115,
        PLAYER_VISIBLE_ITEM_11_ENTRYID = 0x116,
        PLAYER_VISIBLE_ITEM_11_ENCHANTMENT = 0x117,
        PLAYER_VISIBLE_ITEM_12_ENTRYID = 0x118,
        PLAYER_VISIBLE_ITEM_12_ENCHANTMENT = 0x119,
        PLAYER_VISIBLE_ITEM_13_ENTRYID = 0x11A,
        PLAYER_VISIBLE_ITEM_13_ENCHANTMENT = 0x11B,
        PLAYER_VISIBLE_ITEM_14_ENTRYID = 0x11C,
        PLAYER_VISIBLE_ITEM_14_ENCHANTMENT = 0x11D,
        PLAYER_VISIBLE_ITEM_15_ENTRYID = 0x11E,
        PLAYER_VISIBLE_ITEM_15_ENCHANTMENT = 0x11F,
        PLAYER_VISIBLE_ITEM_16_ENTRYID = 0x120,
        PLAYER_VISIBLE_ITEM_16_ENCHANTMENT = 0x121,
        PLAYER_VISIBLE_ITEM_17_ENTRYID = 0x122,
        PLAYER_VISIBLE_ITEM_17_ENCHANTMENT = 0x123,
        PLAYER_VISIBLE_ITEM_18_ENTRYID = 0x124,
        PLAYER_VISIBLE_ITEM_18_ENCHANTMENT = 0x125,
        PLAYER_VISIBLE_ITEM_19_ENTRYID = 0x126,
        PLAYER_VISIBLE_ITEM_19_ENCHANTMENT = 0x127,
        PLAYER_CHOSEN_TITLE = 0x128,
        PLAYER_FIELD_PAD_0 = 0x129,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12A,
        PLAYER_FIELD_PACK_SLOT_1 = 0x158,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1B0,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1BE,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1D6,
        PLAYER_FIELD_CURRENCYTOKEN_SLOT_1 = 0x216,
        PLAYER_FARSIGHT = 0x256,
        PLAYER__FIELD_KNOWN_TITLES = 0x258,
        PLAYER__FIELD_KNOWN_TITLES1 = 0x25A,
        PLAYER__FIELD_KNOWN_TITLES2 = 0x25C,
        PLAYER_FIELD_KNOWN_CURRENCIES = 0x25E,
        PLAYER_XP = 0x260,
        PLAYER_NEXT_LEVEL_XP = 0x261,
        PLAYER_SKILL_INFO_1_1 = 0x262,
        PLAYER_CHARACTER_POINTS1 = 0x3E2,
        PLAYER_CHARACTER_POINTS2 = 0x3E3,
        PLAYER_TRACK_CREATURES = 0x3E4,
        PLAYER_TRACK_RESOURCES = 0x3E5,
        PLAYER_BLOCK_PERCENTAGE = 0x3E6,
        PLAYER_DODGE_PERCENTAGE = 0x3E7,
        PLAYER_PARRY_PERCENTAGE = 0x3E8,
        PLAYER_EXPERTISE = 0x3E9,
        PLAYER_OFFHAND_EXPERTISE = 0x3EA,
        PLAYER_CRIT_PERCENTAGE = 0x3EB,
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x3EC,
        PLAYER_OFFHAND_CRIT_PERCENTAGE = 0x3ED,
        PLAYER_SPELL_CRIT_PERCENTAGE1 = 0x3EE,
        PLAYER_SHIELD_BLOCK = 0x3F5,
        PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE = 0x3F6,
        PLAYER_EXPLORED_ZONES_1 = 0x3F7,
        PLAYER_REST_STATE_EXPERIENCE = 0x477,
        PLAYER_FIELD_COINAGE = 0x478,
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x479,
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x480,
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x487,
        PLAYER_FIELD_MOD_HEALING_DONE_POS = 0x48E,
        PLAYER_FIELD_MOD_TARGET_RESISTANCE = 0x48F,
        PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE = 0x490,
        PLAYER_FIELD_BYTES = 0x491,
        PLAYER_AMMO_ID = 0x492,
        PLAYER_SELF_RES_SPELL = 0x493,
        PLAYER_FIELD_PVP_MEDALS = 0x494,
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x495,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x4A1,
        PLAYER_FIELD_KILLS = 0x4AD,
        PLAYER_FIELD_TODAY_CONTRIBUTION = 0x4AE,
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x4AF,
        PLAYER_FIELD_LIFETIME_HONORBALE_KILLS = 0x4B0,
        PLAYER_FIELD_BYTES2 = 0x4B1,
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x4B2,
        PLAYER_FIELD_COMBAT_RATING_1 = 0x4B3,
        PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = 0x4CC,
        PLAYER_FIELD_HONOR_CURRENCY = 0x4DE,
        PLAYER_FIELD_ARENA_CURRENCY = 0x4DF,
        PLAYER_FIELD_MAX_LEVEL = 0x4E0,
        PLAYER_FIELD_DAILY_QUESTS_1 = 0x4E1,
        PLAYER_RUNE_REGEN_1 = 0x4FA,
        PLAYER_NO_REAGENT_COST_1 = 0x4FE,
        PLAYER_FIELD_GLYPH_SLOTS_1 = 0x501,
        PLAYER_FIELD_GLYPHS_1 = 0x507,
        PLAYER_GLYPHS_ENABLED = 0x50D,
        TOTAL_PLAYER_FIELDS = 0xD3
    }

    public enum EGameobjectFields
    {
        OBJECT_FIELD_CREATED_BY = 0x6,
        GAMEOBJECT_DISPLAYID = 0x8,
        GAMEOBJECT_FLAGS = 0x9,
        GAMEOBJECT_PARENTROTATION = 0xA,
        GAMEOBJECT_DYNAMIC = 0xE,
        GAMEOBJECT_FACTION = 0xF,
        GAMEOBJECT_LEVEL = 0x10,
        GAMEOBJECT_BYTES_1 = 0x11,
        TOTAL_GAMEOBJECT_FIELDS = 0x8
    }

    public enum EDynamicobjectFields
    {
        DYNAMICOBJECT_CASTER = 0x6,
        DYNAMICOBJECT_BYTES = 0x8,
        DYNAMICOBJECT_SPELLID = 0x9,
        DYNAMICOBJECT_RADIUS = 0xA,
        DYNAMICOBJECT_POS_X = 0xB,
        DYNAMICOBJECT_POS_Y = 0xC,
        DYNAMICOBJECT_POS_Z = 0xD,
        DYNAMICOBJECT_FACING = 0xE,
        DYNAMICOBJECT_CASTTIME = 0xF,
        TOTAL_DYNAMICOBJECT_FIELDS = 0x9
    }

    public enum ECorpseFields
    {
        CORPSE_FIELD_OWNER = 0x6,
        CORPSE_FIELD_PARTY = 0x8,
        CORPSE_FIELD_DISPLAY_ID = 0xA,
        CORPSE_FIELD_ITEM = 0xB,
        CORPSE_FIELD_BYTES_1 = 0x1E,
        CORPSE_FIELD_BYTES_2 = 0x1F,
        CORPSE_FIELD_GUILD = 0x20,
        CORPSE_FIELD_FLAGS = 0x21,
        CORPSE_FIELD_DYNAMIC_FLAGS = 0x22,
        CORPSE_FIELD_PAD = 0x23,
        TOTAL_CORPSE_FIELDS = 0xA
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
        int s_curMgr;
        ulong MyGUID;

        ArrayList thebots;

        Thread oThread;
        Thread luathread;
        Thread memthread;
        SOTAMovementBot wsgbot = null;


        struct Point3d
        {
            public float x;
            public float y;
            public float z;
            public WoahObjectType type;
        }

        public event myKeyEventHandler HookKeyPress;

        //cIRC IrcApp;
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

            this.Text = "woah - woah";
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

        public void AddMovementText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox6.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddMovementText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox6.Text = text + "\r\n" + this.textBox6.Text;
            }
        }

        public void AddWaypointText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox7.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddWaypointText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox7.Text = this.textBox7.Text + text + "\r\n";
            }
        }




        private void ReadWowTLS()
        {
            //this.Text = "woah - woah";
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
                                //g_clientConnection = env.Memory.ReadInteger((int)(0x00D43318));
                                //s_curMgr = env.Memory.ReadInteger((int)(g_clientConnection + 0x2218));

                                THREAD_BASIC_INFORMATION tbi = new THREAD_BASIC_INFORMATION();
                                if (NtQueryInformationThread(threadhandle, 0, ref tbi, (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(THREAD_BASIC_INFORMATION)), IntPtr.Zero) == 0)
                                {
                                    int tlsoffset = env.Memory.ReadInteger((int)tbi.TebBaseAddress + 0x2c);
                                    int targetslot = env.Memory.ReadInteger(tlsoffset + (slot * 4));
                                    s_curMgr = env.Memory.ReadInteger(targetslot + 0x10);
                                    MyGUID = (ulong)env.Memory.ReadLong(s_curMgr + 0xC0);



                                    int curObj, nextObj, localObj = 0;
                                    UInt64 localGUID;

                                    curObj = env.Memory.ReadInteger(s_curMgr + 0xAC);
                                    nextObj = curObj;

//                                     while (curObj != 0 && (curObj & 1) == 0)
//                                     {
//                                         UInt64 cGUID = (ulong)env.Memory.ReadLong(curObj + 0x30);
// 
//                                         if (cGUID == MyGUID)
//                                         {
//                                             AddIRCText("local gui found in list");
//                                             localObj = curObj;
//                                             int storage = env.Memory.ReadInteger(localObj + 0x08);
//                                             int health = env.Memory.ReadInteger(storage + 0x17 * 4);
// 
//                                             WoahObject wow = new WoahObject(localObj, env);
//                                             AddIRCText("localobj guid: " + wow.GUID.GUID.ToString() + " and health: " + health.ToString());
// 
// 
//                                         }
//                                         AddIRCText("guid found:" + cGUID.ToString());
//                                         //Console.WriteLine("0x{0:X08} -- GUID: 0x{1:X016} | {2} {3} {4}", curObj, cGUID, X, Y, Z);
// 
//                                         nextObj = env.Memory.ReadInteger(curObj + 0x3C);
//                                         if (nextObj == curObj)
//                                             break;
//                                         else
//                                             curObj = nextObj;
//                                     }

                                    int typee = env.Memory.ReadInteger(env.Memory.ReadInteger(s_curMgr));

                                    AddIRCText("GUID of player:     " + MyGUID.ToString("X"));


                                    CloseHandle(threadhandle);
                                    found = true;
                                    env.Initialize(MyGUID, s_curMgr);
                                    env.Update();
                                    env.RunLua();
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
            wsgbot = new SOTAMovementBot(env, this);
            thebots = new ArrayList();
            //thebots.Add(new FishBot(env, this));
            thebots.Add(wsgbot);



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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            oThread.Abort();
            luathread.Abort();
            memthread.Abort();
            m_notifyicon.Dispose();

            //IrcApp.die();
            UnhookWindowsHookEx(_hookID);
        }

        public void SendIrcMessage(string msg)
        {
            //IrcApp.SendMessage(msg);
        }



        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public void QuitApp()
        {
            //IrcApp.die();
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

        public void moveclick()
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = 0;
            inputs[0].mi.dy = 0;
            inputs[0].mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
            inputs[0].mi.dwFlags |= MOUSEEVENTF_LEFTDOWN;


            uint result = SendInput(1, inputs, Marshal.SizeOf(inputs[0]));
            if (result != 1)
            {
                throw new Exception("Could not click mouse ");
            }
        }

        public void moveunclick()
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = 0;
            inputs[0].mi.dy = 0;

            inputs[0].mi.dwFlags = MOUSEEVENTF_RIGHTUP;

            inputs[0].mi.dwFlags |= MOUSEEVENTF_LEFTUP;

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

        public void joinWSG()
        {
            Thread.Sleep(1000);
            ArrayList input = new ArrayList();
            input.Add(new keyinput(0x23, true));// H
            input.Add(new keyinput(0x23, false));
            sendline(input);


            Thread.Sleep(1000);
            Cursor.Position = new Point(150,675);
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(100, 250);
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(250, 635);
            fullclick(false, false);

            Thread.Sleep(1000);

            ArrayList input2 = new ArrayList();
            input2.Add(new keyinput(0x23, true));// =
            input2.Add(new keyinput(0x23, false));
            sendline(input2);


            Thread.Sleep(1000);

            ArrayList input3 = new ArrayList();
            input3.Add(new keyinput(0x23, true));// =
            input3.Add(new keyinput(0x23, false));
            sendline(input3);


            Thread.Sleep(1000);

//             Thread.Sleep(500);
//             Cursor.Position = new Point(410, 156);
//             fullclick(false, false);
            Thread.Sleep(500);
            // Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
        }
        public bool joinblocking = false;
        public void joinAB()
        {
            joinblocking = true;
            Thread.Sleep(1000);
            ArrayList input = new ArrayList();
            input.Add(new keyinput(0x23, true));// H
            input.Add(new keyinput(0x23, false));
            sendline(input);


            Thread.Sleep(1000);
            Cursor.Position = new Point(150, 675);
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(100, 275);
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(250, 635);
            fullclick(false, false);

            Thread.Sleep(1000);

            ArrayList input2 = new ArrayList();
            input2.Add(new keyinput(0x23, true));// =
            input2.Add(new keyinput(0x23, false));
            sendline(input2);


            Thread.Sleep(1000);

            //             Thread.Sleep(500);
            //             Cursor.Position = new Point(410, 156);
            //             fullclick(false, false);
            Thread.Sleep(500);
            // Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
            joinblocking = false;
        }
        public void joinAV()
        {
            joinblocking = true;
            Thread.Sleep(1000);
            ArrayList input = new ArrayList();
            input.Add(new keyinput(0x23, true));// H
            input.Add(new keyinput(0x23, false));
            sendline(input);


            Thread.Sleep(1000);
            Cursor.Position = new Point(150, 675); // Battleground button
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(100, 229); // AV thing
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(250, 635); // join button
            fullclick(false, false);

            Thread.Sleep(1000);

            ArrayList input2 = new ArrayList();
            input2.Add(new keyinput(0x23, true));// =
            input2.Add(new keyinput(0x23, false));
            sendline(input2);


            Thread.Sleep(1000);

            //             Thread.Sleep(500);
            //             Cursor.Position = new Point(410, 156);
            //             fullclick(false, false);
            Thread.Sleep(500);
            // Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
            joinblocking = false;
        }

        public void joinSOTA()
        {
            joinblocking = true;
            Thread.Sleep(1000);
            ArrayList input = new ArrayList();
            input.Add(new keyinput(0x23, true));// H
            input.Add(new keyinput(0x23, false));
            sendline(input);


            Thread.Sleep(1000);
            Cursor.Position = new Point(150, 675); // Battleground button
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(100, 310); // AV thing
            fullclick(false, false);

            Thread.Sleep(1000);
            Cursor.Position = new Point(250, 635); // join button
            fullclick(false, false);

            Thread.Sleep(1000);

            ArrayList input2 = new ArrayList();
            input2.Add(new keyinput(0x23, true));// =
            input2.Add(new keyinput(0x23, false));
            sendline(input2);


            Thread.Sleep(1000);

            //             Thread.Sleep(500);
            //             Cursor.Position = new Point(410, 156);
            //             fullclick(false, false);
            Thread.Sleep(500);
            // Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
            joinblocking = false;
        }

        public void fullclick(bool right, bool doubleclick)
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

            if (doubleclick)
            {
                System.Threading.Thread.Sleep(100);
                uint result1 = SendInput(2, inputs, Marshal.SizeOf(inputs[0]));
                if (result1 != 2)
                {
                    throw new Exception("Could not click mouse ");
                }
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
            //if (IrcApp != null)
            //    IrcApp.die();
            //IrcApp = new cIRC(textBox4.Text, 6667, textBox3.Text, textBox2.Text, this);

            timer2.Enabled = true;
            RunBots();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //env.RunLua();
          //  this.Text = "woah - woah";

        }



        int failcount = 0;
        DateTime Watchdog = new DateTime();
        private void timer2_Tick(object sender, EventArgs e)
        {
            Watchdog = DateTime.Now;
            this.Text = Cursor.Position.ToString();
//             Graphics hdl = radar.CreateGraphics();
//             Bitmap bmp;
//             Graphics gbuf;
//             Pen pen1;
//             Pen pen2;
//             Pen pen3;
//             Pen pen4;
//             Pen pen5;
// 
//             //Create Our Buffer
//             bmp = new Bitmap(radar.Width, radar.Height);
//             gbuf = Graphics.FromImage(bmp);
// 
//             //Set Drawing To Antailias (Smooth)
//             gbuf.SmoothingMode = SmoothingMode.AntiAlias;
//             hdl.SmoothingMode = SmoothingMode.AntiAlias;
// 
//             //Create Some Pens
//             pen1 = new Pen(Color.Green, 2);
//             pen2 = new Pen(Color.Red, 2);
//             pen3 = new Pen(Color.Blue, 2);
// 
// 
// 
//             gbuf.FillRectangle(Brushes.White, 0, 0, radar.Width, radar.Height);
// 
// 
// 
//             double furthestaway = -1.0f;
//             WoahObject plyr = (WoahObject)env.Objects[MyGUID];
//             if (plyr != null)
//             {
// 
// 
//                 List<Point3d> points = new List<Point3d>();
//                 // update our radar
//                 foreach (DictionaryEntry d in env.Objects)
//                 {
// 
//                     WoahObject obj = (WoahObject)d.Value;
//                     if (obj.GUID.GUID != env.PlayerID)
//                     {
//                         if (obj.Type == WoahObjectType.Player || obj.Type == WoahObjectType.Unit)
//                         {
//                             int health = obj.Unit_Health;
//                             float x = obj.X;
//                             float y = obj.Y;
//                             float z = obj.Z;
// 
//                             double dsqrd = Math.Sqrt(Math.Pow(x - plyr.X, 2) + Math.Pow(y - plyr.Y, 2) + Math.Pow(z - plyr.Z, 2));
//                             if (dsqrd > furthestaway)
//                             {
//                                 furthestaway = dsqrd;
//                             }
//                             Point3d p3d;
//                             p3d.x = (x - plyr.X) * 2;
//                             p3d.y = (y - plyr.Y) * 2;
//                             p3d.z = (z - plyr.Z) * 2;
//                             p3d.type = obj.Type;
// 
//                             // To rotate, if I ever want to do that
//                             float bias = -(float)Math.PI;
//                             float xtemp = (float)(Math.Cos(plyr.Facing - bias) * p3d.x - Math.Sin(plyr.Facing - bias) * p3d.y);
//                             p3d.y = (float)(Math.Sin(plyr.Facing - bias) * p3d.x + Math.Cos(plyr.Facing - bias) * p3d.y);
//                             p3d.x = xtemp;
// 
//                             points.Add(p3d);
//                         }
//                     }
//                 }
// 
//                 foreach (Point3d p in points)
//                 {
//                     Rectangle rect = new Rectangle(((int)p.x + 250), ((int)p.y + 250), 6, 6);
//                     if (p.type == WoahObjectType.Player)
//                     {
//                         gbuf.DrawEllipse(pen3, rect);
//                     }
//                     else
//                     {
//                         gbuf.DrawEllipse(pen1, rect);
//                     }
// 
//                 }
//                 Rectangle prect = new Rectangle(250, 250, 7, 7);
//                 gbuf.DrawEllipse(pen2, prect);
// 
// 
//             }
// 
// 
// 
//             hdl.DrawImage(bmp, new Point(0, 0));
//             pen1.Dispose();
//             pen2.Dispose();
//             pen3.Dispose();
// 
//             hdl.Dispose();
//             bmp.Dispose();
//             gbuf.Dispose();
        }

        public void RunLuaScans()
        {
            while (true)
            {
                env.RunLua();
                Thread.Sleep(500);
            }
        }
        DateTime delta = new DateTime();
        public void RunMemoryScans()
        {

            while (true)
            {
                delta = Watchdog;
                delta.AddSeconds(10);
                if (this.Watchdog == null)
                {
                    // Major failure

                    int i = 3;
                    int j = i;
                    j++;
                }
                else if (DateTime.Now > delta)
                {
                    // Sounds like the main app crashes
                    int i = 3;
                    int j = i;
                    j++;
                }

                if (!env.Update())
                {
                    failcount++;
                }
                if (failcount > 500)
                {
                    Thread.Sleep(1000);
                    ReadWowTLS();
                    //button1_Click_1(null, null);
                    failcount = 0;
                }
            }
            Thread.Sleep(100);

            //WoahObject plyr = (WoahObject)env.Objects[MyGUID];
            //if (plyr != null)
            //{
            //    this.Text = Cursor.Position.ToString();//plyr.X.ToString() + " " + plyr.Y.ToString() + " " + plyr.Z.ToString();
            //}
        }
        public void ReSetup()
        {
            ReadWowTLS();
        }
        public void RunBots()
        {
            memthread = new Thread(new ThreadStart(this.RunMemoryScans));
            memthread.Start();

            luathread = new Thread(new ThreadStart(this.RunLuaScans));
            luathread.Start();

            oThread = new Thread(new ThreadStart(wsgbot.DoAction));
            oThread.Start();

//             foreach (WoahBotBase b in thebots)
//             {
//                 b.DoAction();
//             }
        }
    }
}