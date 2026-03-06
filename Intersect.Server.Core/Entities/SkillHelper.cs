using System;
using Intersect.Enums;

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
            // Get current XP and Level
            long currentXp = GetSkillXp(player, skill);
            int currentLevel = GetSkillLevel(player, skill);

            // Add XP
            long newXp = currentXp + xpAmount;
            SetSkillXp(player, skill, newXp);

            // Check for level up
            int newLevel = GetLevelForXp(newXp);
            if (newLevel > currentLevel)
            {
                SetSkillLevel(player, skill, newLevel);
                return true; // Leveled up!
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
    }
}