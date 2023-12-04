﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Navislamia.World.Entities;

namespace Navislamia.Data.Entities;

public enum ItemType
{
    ETC,
    ARMOR,
    CARD,
    SUPPLY,
    CUBE,
    CHARM,
    USE,
    SOULSTONE,
    USE_CARD
}

public enum ItemGroup
{
    ETC,
    WEAPON,
    ARMOR,
    SHIELD,
    HELM,
    GLOVE,
    BOOTS,
    BELT,
    MANTLE,
    ACCESSORY,
    SKILLCARD,
    ITEMCARD,
    SPELLCARD,
    SUMMONCARD,
    FACE = 15,
    UNDERWEAR,
    BAG,
    PET_CAGE,
    STRIKE_CUBE = 21,
    DEFENSE_CUBE,
    SKILL_CUBE,
    RESTORATION_CUBE,
    SOULSTONE = 93,
    BULLET,
    CONSUMABLE,
    NPC_FACE = 100,
    DECO = 110,
    RIDING = 120,
    ARTIFACT = 130,
    EQUIPMENT_ON_BELT = 140
}


public enum ItemWearType
{
    CANTWEAR = -1,
    NONE = -1,

    WEAPON = 0,
    SHIELD = 1,
    ARMOR = 2,
    HELM = 3,
    GLOVE = 4,
    BOOTS = 5,
    BELT = 6,
    MANTLE = 7, 
    ARMULET = 8,
    RING = 9,	

    EAR = 11,
    FACE = 12,
    HAIR = 13, 
    DECO_WEAPON = 14,
    DECO_SHIELD = 15,
    DECO_ARMOR = 16,
    DECO_HELM = 17,
    DECO_GLOVE = 18,
    DECO_BOOTS = 19,
    DECO_MANTLE = 20,
    DECO_SHOULDER = 21,
    RIDE_ITEM = 22,
    BAG_SLOT = 23,
    SPARE_WEAPON = 24,
    SPARE_SHIELD = 25,
    SPARE_DECO_WEAPON = 26,
    SPARE_DECO_SHIELD = 27,

    TWOFINGER_RING = 94,
    TWOHAND = 99,

    SKILL = 100,
    SUMMON_ONLY = 200,

    SECOND_RING = 10,
    RIGHTHAND = WEAPON, 
    LEFTHAND = SHIELD,
    BULLET = SHIELD,

    SPARE_RIGHTHAND = SPARE_WEAPON, 
    SPARE_LEFTHAND = SPARE_SHIELD,
    SPARE_BULLET = SPARE_SHIELD,
}

public enum LimitFlag
{
    DEVA = 1 << 2,
    ASURA = 1 << 3,
    GAIA = 1 << 4,

    FIGHTER = 1 << 10,
    HUNTER = 1 << 11,
    MAGICIAN = 1 << 12,
    SUMMONER = 1 << 13
}

[Flags]
public enum ItemFlag
{
    CANT_DONATE = 0,
    CANT_STORAGE = 1,
    CANT_ENHANCE = 2,
    USE = 4,
    CARD = 8,
    SOCKET = 16,
    JOIN = 32,
    TARGET_USE = 64,
    WARP = 128,
    CANT_TRADE = 256,
    CANT_SELL = 512,
    QUEST = 1024,
    CANT_USE_OVERWEIGHT = 2048,
    CASHITEM = 4096,
    CANT_USE_RIDING = 8192,
    CANT_DROP = 16384,
    CANT_USE_MOVING = 32768,
    QUEST_DISTRIBUTE = 65536,
    CANT_USE_SIT = 131072,
    CANT_USE_IN_RAID_SIEGE = 262114,
    CANT_USE_IN_SECROUTE = 524288,
    CANT_USE_IN_EVENTMAP = 1048576,
    CANT_USE_IN_HUNTAHOLIC = 2097152,
    USABLE_IN_ONLY_HUNTAHOLIC = 4194304,
    CANT_USE_IN_DEATHMATCH = 8388608,
    USABLE_IN_ONLY_DEATHMATCH = 16777216,
    NOT_ERASABLE = 33554432
}

public enum ItemDecreaseType
{
    PERMANENT,
    IN_GAME,
    ALWAYS,

}

public class ItemBase
{
    const int MAX_COOLTIME_GROUP = 40;
    const int MAX_OPTION_NUMBER = 4;
    const int MAX_SOCKET_NUMBER = 4;
    const int MAX_ITEM_NAME_LENGTH = 32;
    const int MAX_ITEM_WEAR = 24;
    const int MAX_SPEAR_ITEM_WEAR = 28;


    public int Code;
    public int NameID;
    public ItemType Type;
    public ItemGroup Group;
    // public ItemClass ClassDeprecated;
    public int SetID;
    public int SetPartFlag;
    public byte Grade;
    public int Rank;
    public int Level;
    public int Enhance;
    public int SocketCount;
    public int InstanceFlag;
    public byte JobDepth;
    public int MinLevel;
    public int MaxLevel;
    public int TargetMinLevel;
    public int TargetMaxLevel;
    public int Range;
    public float Weight;
    public long Price;
    public int HuntaholicPoint;
    public int EtherealDurability;
    public int Endurance;
    public ItemWearType WearType;
    public LimitFlag Limit;
    public ItemFlag Flag;
    public int Material;
    public int SummonID;
    public int ThrowRange;
    public short[] BaseType = new short[MAX_OPTION_NUMBER];
    public VNumber[] BaseVar1 = new VNumber[MAX_OPTION_NUMBER];
    public VNumber[] BaseVar2 = new VNumber[MAX_OPTION_NUMBER];
    public short[] OptType = new short[MAX_OPTION_NUMBER];
    public VNumber[] OptVar1 = new VNumber[MAX_OPTION_NUMBER];
    public VNumber[] OptVar2 = new VNumber[MAX_OPTION_NUMBER];
    public short[] EnhanceID = new short[2];
    public VNumber[][] EnhanceVar = new VNumber[2][];
    public int SkillID;
    public int StateCode;
    public int StateLevel;
    public int StateTime;
    public int CoolTime;
    public short CoolTimeGroup;
    public int AvailableTime;
    public ItemDecreaseType DecreaseType;
    public bool LogRequiredOnExpiration;
}

public class ItemBaseServer : ItemBase
{
    public List<EffectInfo> EffectList;
    public string Script;
}

public class DbItem : ItemBaseServer
{
    public int Price;

    public int EffectID;

    public byte LimitDeva;
    public byte LimitAsura;
    public byte LimitGaia;

    public byte LimitFighter;
    public byte LimitHunter;
    public byte LimitMagician;
    public byte LimitSummoner;

    public int UseFlag;

    public VNumber Weight;

    public VNumber Range;
    public VNumber[] BaseVar1 = new VNumber[4];
    public VNumber[] BaseVar2 = new VNumber[4];

    public VNumber[] OptVar1 = new VNumber[4];
    public VNumber[] OptVar2 = new VNumber[4];

    public VNumber[] EnhanceVar1 = new VNumber[4];
    public VNumber[] EnhanceVar2 = new VNumber[4];

    public string ScriptString;
}
