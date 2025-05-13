using HarmonyLib;
using RimWorld;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(Game), "InitNewGame")]
    public static class Game_InitNewGame_Patch
    {
        public static void Postfix()
        {
            if (Faction.OfPlayer.def.techLevel == TechLevel.Animal)
            {
                var delay = 5 * 60;
                Find.LetterStack.ReceiveLetter("GameStartedAnimalTechLetterTitle".Translate(),
                    "GameStartedAnimalTechLetterDesc".Translate(), LetterDefOf.NeutralEvent,
                    delayTicks: delay);
            }
        }
    }
}
