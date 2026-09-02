using System;
using HarmonyLib;
using RimWorld;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(Page_ConfigureStartingPawns), "CheckWarnRequiredWorkTypesDisabledForEveryone")]
    public static class Page_ConfigureStartingPawns_CheckWarnRequiredWorkTypesDisabledForEveryone_Patch
    {
        private static bool Prefix(Action nextAction)
        {
            if (ProgressionCoreUtility.StartingOnAnimalTech is false)
            {
                return true;
            }
            nextAction();
            return false;
        }
    }
}
