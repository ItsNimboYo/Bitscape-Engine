using System;

namespace Intersect.Client.Core
{
    public static class SkillXpHelper
    {
        private static readonly long[] XpTable = BuildXpTable(99);

        private static long[] BuildXpTable(int maxLevel)
        {
            var table = new long[maxLevel + 1];
            table[0] = 0;
            table[1] = 0;

            double points = 0;
            for (int level = 1; level < maxLevel; level++)
            {
                points += Math.Floor(level + 300 * Math.Pow(2, level / 7.0));
                table[level + 1] = (long)(points / 4);
            }

            return table;
        }

        public static long GetXpForLevel(int level)
        {
            if (level < 1)
            {
                return 0;
            }

            if (level >= XpTable.Length)
            {
                return XpTable[XpTable.Length - 1];
            }

            return XpTable[level];
        }
    }
}