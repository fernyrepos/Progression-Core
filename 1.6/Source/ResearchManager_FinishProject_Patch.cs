using HarmonyLib;
using RimWorld;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
    public static class ResearchManager_FinishProject_Patch
    {
        private static void Prefix(out bool __state)
        {
            __state = RitualObligationTargetWorker_AnyGatherSpotForAdvancement_CanUseTargetInternal_Patch.NoEnoughResearchProjectsFinished();
        }

        private static void Postfix(bool __state)
        {
            if (__state && RitualObligationTargetWorker_AnyGatherSpotForAdvancement_CanUseTargetInternal_Patch.NoEnoughResearchProjectsFinished() is false)
            {
                Find.LetterStack.ReceiveLetter("ReadyToAdvance".Translate(), 
                    "ReadyToAdvanceDesc".Translate(), LetterDefOf.NeutralEvent, delayTicks: 60);
            }
        }
    }
}
