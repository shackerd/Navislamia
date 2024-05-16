﻿namespace Navislamia.Game.DataAccess.Entities.Enums
{
    public enum ItemWearType
    {
        None = -1,
        CantWear = -1,
        Weapon = 0,
        Shield = 1,
        Armor = 2,
        Helm = 3,
        Glove = 4,
        Boots = 5,
        Belt = 6,
        Mantle = 7,
        Armulet = 8,
        Ring = 9,
        Ear = 11,
        Face = 12,
        Hair = 13,
        DecoWeapon = 14,
        DecoShield = 15,
        DecoArmor = 16,
        DecoHelm = 17,
        DecoGlove = 18,
        DecoBoots = 19,
        DecoMantle = 20,
        DecoShoulder = 21,
        RideItem = 22,
        BagSlot = 23,
        SpareWeapon = 24,
        SpareShield = 25,
        SpareDecoWeapon = 26,
        SpareDecoShield = 27,
        TwofingerRing = 94,
        Twohand = 99,
        Skill = 100,
        SummonOnly = 200,
        SecondRing = 10,
        Righthand = Weapon,
        Lefthand = Shield,
        Bullet = Shield,
        SpareRighthand = SpareWeapon,
        SpareLefthand = SpareShield,
        SpareBullet = SpareShield,
    };
}