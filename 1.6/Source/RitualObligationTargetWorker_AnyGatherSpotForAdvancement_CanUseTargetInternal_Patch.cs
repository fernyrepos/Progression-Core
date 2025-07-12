using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(VFETribals.RitualObligationTargetWorker_AnyGatherSpotForAdvancement), "CanUseTargetInternal")]
    public static class RitualObligationTargetWorker_AnyGatherSpotForAdvancement_CanUseTargetInternal_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();
            for (var i = 0; i < codes.Count; i++)
            {
                var code = codes[i];
                yield return code;
                if (code.opcode == OpCodes.Brfalse_S && codes[i + 1].OperandIs("VFET.NotAllReseachProjectsResearched"))
                {
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(RitualObligationTargetWorker_AnyGatherSpotForAdvancement_CanUseTargetInternal_Patch),
                        "NoEnoughResearchProjectsFinished"));
                    yield return code;
                }
            }
        }

        public static bool NoEnoughResearchProjectsFinished()
        {
            var allResearch = DefDatabase<ResearchProjectDef>.AllDefsListForReading
                        .Where(x => x.techLevel == Faction.OfPlayer.def.techLevel && x.techprintCount == 0).ToList();
            var finished = allResearch.Where(x => x.IsFinished).ToList(); 
            var complection = finished.Count / (float)allResearch.Count;
            return complection < ProgressionCoreSettings.researchComplectionPercent;
        }
    }
}
