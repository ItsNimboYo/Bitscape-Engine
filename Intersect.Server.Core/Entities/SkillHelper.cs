using System;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.PlayerClass;
using Intersect.Server.Entities;

namespace Intersect.Server.Entities
{
    public static class SkillHelper
    {
        // ---- BSC Skill XP Table ----
        // OSRS style XP requirements

        private static readonly long[] XpTable =
            BuildXpTable(99);

        /// <summary>
        /// Builds XP table up to maxLevel
        /// using OSRS formula
        /// </summary>
        private static long[] BuildXpTable(int maxLevel)
        {
            var table = new long[maxLevel + 1];
            table[0] = 0;
            table[1] = 0;

            double points = 0;
            for (int level = 1; level < maxLevel; level++)
            {
                points += Math.Floor(
                    level + 300 * Math.Pow(2, level / 7.0)
                );
                table[level + 1] = (long)(points / 4);
            }
            return table;
        }

        /// <summary>
        /// Get XP required to reach a level
        /// </summary>
        public static long GetXpForLevel(int level)
        {
            if (level < 1) return 0;
            if (level >= XpTable.Length)
                return XpTable[XpTable.Length - 1];
            return XpTable[level];
        }

        /// <summary>
        /// Get level for a given XP amount
        /// </summary>
        public static int GetLevelForXp(long xp)
        {
            int level = 1;
            for (int i = 1; i < XpTable.Length; i++)
            {
                if (xp >= XpTable[i])
                    level = i;
                else
                    break;
            }
            return level;
        }

        /// <summary>
        /// Get XP needed to reach next level
        /// </summary>
        public static long GetXpToNextLevel(
            int currentLevel, long currentXp)
        {
            if (currentLevel >= 99) return 0;
            long nextLevelXp = GetXpForLevel(
                currentLevel + 1);
            return nextLevelXp - currentXp;
        }

        /// <summary>
        /// Award XP to a skill and handle level ups
        /// Returns true if player leveled up!
        /// </summary>
        public static bool AwardSkillXp(
    Player player,
    SkillType skill,
    long xpAmount)
        {
            // BSC - Apply class multiplier
            double multiplier = GetClassXpMultiplier(player, skill);
            long finalXp = (long)(xpAmount * multiplier);

            // Get current XP and Level
            long currentXp = GetSkillXp(player, skill);
            int currentLevel = GetSkillLevel(player, skill);

            // Add XP with multiplier applied
            long newXp = currentXp + finalXp;
            SetSkillXp(player, skill, newXp);

            // Check for level up
            int newLevel = GetLevelForXp(newXp);
            if (newLevel > currentLevel)
            {
                SetSkillLevel(player, skill, newLevel);
                return true;
            }
            return false;
        }

        // ---- Skill Getters ----
        public static long GetSkillXp(
            Player player, SkillType skill)
        {
            return skill switch
            {
                SkillType.Melee => player.MeleeExp,
                SkillType.Shielding => player.ShieldingExp,
                SkillType.Magic => player.MagicExp,
                SkillType.Distance => player.DistanceExp,
                SkillType.Mining => player.MiningExp,
                SkillType.Smithing => player.SmithingExp,
                SkillType.Woodcutting => player.WoodcuttingExp,
                SkillType.Cooking => player.CookingExp,
                SkillType.Fishing => player.FishingExp,
                SkillType.Crafting => player.CraftingExp,
                _ => 0
            };
        }

        public static int GetSkillLevel(
            Player player, SkillType skill)
        {
            return skill switch
            {
                SkillType.Melee => player.MeleeLevel,
                SkillType.Shielding => player.ShieldingLevel,
                SkillType.Magic => player.MagicLevel,
                SkillType.Distance => player.DistanceLevel,
                SkillType.Mining => player.MiningLevel,
                SkillType.Smithing => player.SmithingLevel,
                SkillType.Woodcutting => player.WoodcuttingLevel,
                SkillType.Cooking => player.CookingLevel,
                SkillType.Fishing => player.FishingLevel,
                SkillType.Crafting => player.CraftingLevel,
                _ => 1
            };
        }

        // ---- Skill Setters ----
        public static void SetSkillXp(
            Player player, SkillType skill, long xp)
        {
            switch (skill)
            {
                case SkillType.Melee:
                    player.MeleeExp = xp; break;
                case SkillType.Shielding:
                    player.ShieldingExp = xp; break;
                case SkillType.Magic:
                    player.MagicExp = xp; break;
                case SkillType.Distance:
                    player.DistanceExp = xp; break;
                case SkillType.Mining:
                    player.MiningExp = xp; break;
                case SkillType.Smithing:
                    player.SmithingExp = xp; break;
                case SkillType.Woodcutting:
                    player.WoodcuttingExp = xp; break;
                case SkillType.Cooking:
                    player.CookingExp = xp; break;
                case SkillType.Fishing:
                    player.FishingExp = xp; break;
                case SkillType.Crafting:
                    player.CraftingExp = xp; break;
            }
        }

        public static void SetSkillLevel(
            Player player, SkillType skill, int level)
        {
            switch (skill)
            {
                case SkillType.Melee:
                    player.MeleeLevel = level; break;
                case SkillType.Shielding:
                    player.ShieldingLevel = level; break;
                case SkillType.Magic:
                    player.MagicLevel = level; break;
                case SkillType.Distance:
                    player.DistanceLevel = level; break;
                case SkillType.Mining:
                    player.MiningLevel = level; break;
                case SkillType.Smithing:
                    player.SmithingLevel = level; break;
                case SkillType.Woodcutting:
                    player.WoodcuttingLevel = level; break;
                case SkillType.Cooking:
                    player.CookingLevel = level; break;
                case SkillType.Fishing:
                    player.FishingLevel = level; break;
                case SkillType.Crafting:
                    player.CraftingLevel = level; break;
            }
        }

        /// <summary>
        /// Get total level across all skills
        /// </summary>
        public static int GetTotalLevel(Player player)
        {
            return player.MeleeLevel +
                   player.ShieldingLevel +
                   player.MagicLevel +
                   player.DistanceLevel +
                   player.MiningLevel +
                   player.SmithingLevel +
                   player.WoodcuttingLevel +
                   player.CookingLevel +
                   player.FishingLevel +
                   player.CraftingLevel;
        }
        // ---- BSC Combat Formulas ----

        /// <summary>
        /// Calculate melee max hit
        /// Formula: (MeleeLevel * 0.8) + (WeaponDamage * 0.5)
        /// </summary>
        public static int CalculateMeleeDamage(
            Player player, int weaponDamage)
        {
            int meleeLevel = player.MeleeLevel < 1
                ? 1 : player.MeleeLevel;
            double maxHit = (meleeLevel * 0.8) +
                            (weaponDamage * 0.5);

            // Random roll between 0 and max hit
            // (Tibia style random damage)
            var rng = new Random();
            int minHit = (int)(maxHit * 0.3);
            int maxHitInt = (int)Math.Ceiling(maxHit);

            return rng.Next(minHit, maxHitInt + 1);
        }

        /// <summary>
        /// Calculate magic max hit
        /// Formula: (MagicLevel * 0.8) + (SpellDamage * 0.5)
        /// </summary>
        public static int CalculateMagicDamage(
            Player player, int spellDamage)
        {
            int magicLevel = player.MagicLevel < 1
                ? 1 : player.MagicLevel;
            double maxHit = (magicLevel * 0.8) +
                            (spellDamage * 0.5);

            var rng = new Random();
            int minHit = (int)(maxHit * 0.3);
            int maxHitInt = (int)Math.Ceiling(maxHit);

            return rng.Next(minHit, maxHitInt + 1);
        }

        /// <summary>
        /// Calculate distance max hit
        /// Formula: (DistanceLevel * 0.8) + (WeaponDamage * 0.5)
        /// </summary>
        public static int CalculateDistanceDamage(
            Player player, int weaponDamage)
        {
            int distanceLevel = player.DistanceLevel < 1
                ? 1 : player.DistanceLevel;
            double maxHit = (distanceLevel * 0.8) +
                            (weaponDamage * 0.5);

            var rng = new Random();
            int minHit = (int)(maxHit * 0.3);
            int maxHitInt = (int)Math.Ceiling(maxHit);

            return rng.Next(minHit, maxHitInt + 1);
        }

        /// <summary>
        /// Calculate defense reduction
        /// Formula: ShieldingLevel * 0.3
        /// Returns damage after reduction
        /// </summary>
        public static int CalculateDefenseReduction(
            Player player, int incomingDamage)
        {
            int shieldingLevel = player.ShieldingLevel < 1
                ? 1 : player.ShieldingLevel;
            double reduction = shieldingLevel * 0.3;

            // Cap reduction at 75% max
            reduction = Math.Min(reduction,
                incomingDamage * 0.75);

            int finalDamage = incomingDamage - (int)reduction;

            // Always take at least 1 damage
            return Math.Max(1, finalDamage);
        }

        /// <summary>
        /// Calculate XP reward based on damage dealt
        /// More damage = more XP
        /// </summary>
        public static long CalculateCombatXp(
            int damageDealt, int enemyLevel)
        {
            // Base XP from damage
            long baseXp = damageDealt * 4;

            // Bonus XP for higher level enemies
            double levelMultiplier = 1.0 +
                (enemyLevel * 0.1);

            return (long)(baseXp * levelMultiplier);
        }
        public static double GetClassXpMultiplier(
    Player player, SkillType skill)
        {
            string className = ClassDescriptor
                .Get(player.ClassId)?.Name ?? "Warrior";

            return (className, skill) switch
            {
                // Warrior - Melee tank
                ("Warrior", SkillType.Melee) => 1.0,
                ("Warrior", SkillType.Shielding) => 1.0,
                ("Warrior", SkillType.Distance) => 0.3,
                ("Warrior", SkillType.Magic) => 0.2,

                // Archer - Distance fighter
                ("Archer", SkillType.Melee) => 0.5,
                ("Archer", SkillType.Shielding) => 0.6,
                ("Archer", SkillType.Distance) => 1.0,
                ("Archer", SkillType.Magic) => 0.5,

                // Mage - Magic specialist
                ("Mage", SkillType.Melee) => 0.2,
                ("Mage", SkillType.Shielding) => 0.3,
                ("Mage", SkillType.Distance) => 0.3,
                ("Mage", SkillType.Magic) => 1.0,

                // Default fallback
                _ => 1.0
            };
        }
    }
}
