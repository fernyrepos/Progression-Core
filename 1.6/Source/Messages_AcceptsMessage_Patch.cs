using HarmonyLib;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(Messages), "AcceptsMessage")]
    public static class Messages_AcceptsMessage_Patch
    {
        private static bool Prefix(string text, ref bool __result)
        {
            if (text.NullOrEmpty() || IsResearchBenchWarning(text) is false)
            {
                return true;
            }
            __result = false;
            return false;
        }

        private static bool IsResearchBenchWarning(string text)
        {
            string warning = "MessageResearchMenuWithoutBench".Translate();
            return text == warning || text == warning.CapitalizeFirst();
        }
    }
}
