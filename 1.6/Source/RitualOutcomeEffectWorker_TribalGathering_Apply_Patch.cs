using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using VFETribals;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(RitualOutcomeEffectWorker_TribalGathering), "Apply")]
    public static class RitualOutcomeEffectWorker_TribalGathering_Apply_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var replacement = AccessTools.Method(
                typeof(RitualOutcomeEffectWorker_TribalGathering_Apply_Patch), nameof(GatheringProjects));
            var replaced = 0;
            foreach (var instruction in instructions)
            {
                if (instruction.operand is MethodInfo method && method.Name == "InRandomOrder"
                    && method.DeclaringType == typeof(GenCollection))
                {
                    instruction.operand = replacement;
                    replaced++;
                }
                yield return instruction;
            }
            if (replaced != 2)
            {
                Log.Warning("[Progression: Core] Tribal gathering research patch matched "
                    + replaced + " of 2 expected call sites, research points will be spread as usual.");
            }
        }

        public static IEnumerable<ResearchProjectDef> GatheringProjects(
            IEnumerable<ResearchProjectDef> source, IList<ResearchProjectDef> workingList)
        {
            var ordered = source.InRandomOrder(workingList);
            if (ProgressionCoreSettings.allPointsToSelectedProject is false)
            {
                return ordered;
            }
            var selected = Find.ResearchManager.GetProject();
            if (CanReceiveGatheringPoints(selected) is false)
            {
                return ordered;
            }
            return ordered.Where(project => project == selected);
        }

        private static bool CanReceiveGatheringPoints(ResearchProjectDef project)
        {
            return project != null && project.CanStartNow
                && (project.techLevel == TechLevel.Animal || project.techLevel == TechLevel.Neolithic);
        }
    }
}
