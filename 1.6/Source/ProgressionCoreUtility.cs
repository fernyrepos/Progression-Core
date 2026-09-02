using RimWorld;
using Verse;

namespace ProgressionCore
{
    public static class ProgressionCoreUtility
    {
        public static bool StartingOnAnimalTech => StartingTechLevel() == TechLevel.Animal;

        public static TechLevel StartingTechLevel()
        {
            var playerFaction = Find.GameInitData?.playerFaction;
            if (playerFaction?.def != null)
            {
                return playerFaction.def.techLevel;
            }
            var factionDef = Find.Scenario?.playerFaction?.factionDef;
            if (factionDef != null)
            {
                return factionDef.techLevel;
            }
            return Faction.OfPlayerSilentFail?.def?.techLevel ?? TechLevel.Undefined;
        }
    }
}
