using System;
using Assets.scripts.Enums;

namespace Assets.scripts.service
{
    public static class Config
    {
        public static readonly bool IsDevelopmentVersion = false;
        public static readonly int commonPlayerHealth = 3;
        public static readonly int MegaPlayerHealth = 5;
        public static readonly float TitorCulDown = 100f;
        public static readonly float PopovEggCulDown = 30f;
        public static readonly float CleanerChargeCulDown = 20f;
        public static readonly int PopovEggsCount = 3;
        public static readonly int CleanerChargeCount = 3;
        public static readonly float TitorBoost = 20f;
        public static readonly float PopovBoost = 15f;
        public static readonly float CleanerBoost = 20f;
        public static readonly int MainEnemyFakeHealth = 1000;
        public static readonly float GameStartTime = 2.0f;
        public static readonly float CommonUnhittableTime = 1.0f;
        public static readonly float GyperJumpUnhittableTime = 0.35f;

        public static readonly string[] BonusTags =
        {
            "FullRockets",
            "FullHealth",
            "MegaHealth",
            "TitorBoost",
            "PopovBoost",
            "CleanerBoost"
        };

        public static readonly string[] GoodTags =
        {
            "rocket",
            "bullet",
            "Titor",
            "Popov",
            "Egg",
            "CleaningShield"
        };

        public static readonly int[] AllowedAttackAmountByState =
        {
            4,
            7
        };

        public static readonly int[] MainEnemyStateHealth =
        {
            2000,
            1650,
            1000
        };

        public static readonly int[] MainEnemyStateHealthOffset =
        {
            1000,
            1000,
            0
        };

        public static float GetGameZoneLimits(Border border)
        {
            return border switch
            {
                Border.Top => 4.74f,
                Border.Bottom => -4.04f,
                Border.Left => -8.34f,
                Border.Right => 8.34f,
                _ => throw new ArgumentOutOfRangeException(nameof(border), border, null)
            };
        }
    }
}