using HarmonyLib;
using RimWorld;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(ResearchPrerequisitesUtility), "UnlockedDefsGroupedByPrerequisites")]
    public static class ResearchPrerequisitesUtility_UnlockedDefsGroupedByPrerequisites_Patch
    {
        private static void Prefix()
        {
            foreach (var project in DefDatabase<ResearchProjectDef>.AllDefsListForReading)
            {
                var unlockedDefs = project.UnlockedDefs;
                for (int i = 0; i < unlockedDefs.Count; i++)
                {
                    if (unlockedDefs[i].defNameHash == 0)
                    {
                        unlockedDefs[i].ResolveDefNameHash();
                    }
                }
            }
        }
    }
}
