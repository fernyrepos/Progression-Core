using HarmonyLib;
using Verse.Profile;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(MemoryUtility), "ClearAllMapsAndWorld")]
    public static class MemoryUtility_ClearAllMapsAndWorld_Patch
    {
        private static void Prefix()
        {
            PlayerFactionTechLevelCache.RestoreAll();
        }
    }
}
