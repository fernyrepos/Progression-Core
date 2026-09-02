using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ProgressionCore
{
    [StaticConstructorOnStartup]
    public static class PlayerFactionTechLevelCache
    {
        private static readonly Dictionary<FactionDef, TechLevel> pristineTechLevels =
            new Dictionary<FactionDef, TechLevel>();

        static PlayerFactionTechLevelCache()
        {
            foreach (var factionDef in DefDatabase<FactionDef>.AllDefsListForReading)
            {
                if (factionDef.isPlayer)
                {
                    pristineTechLevels[factionDef] = factionDef.techLevel;
                }
            }
        }

        public static void RestoreAll()
        {
            foreach (var pair in pristineTechLevels)
            {
                pair.Key.techLevel = pair.Value;
            }
        }
    }
}
