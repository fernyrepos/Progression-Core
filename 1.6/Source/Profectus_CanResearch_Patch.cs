using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch]
    public static class Profectus_CanResearch_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("OskarPotocki.VFE.Classical");
        public static MethodBase TargetMethod() => AccessTools.Method("VFEC.Perks.Workers.Profectus:CanResearch");
        public static void Postfix(ResearchProjectDef proj, ref bool __result)
        {
            if (__result)
            {
                __result = proj.techLevel <= Faction.OfPlayer.def.techLevel;
            }
        }
    }
}
