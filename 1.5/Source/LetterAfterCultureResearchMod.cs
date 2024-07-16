using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Verse;

namespace LetterAfterCultureResearch
{
    public class LetterAfterCultureResearchMod : Mod
    {
        public LetterAfterCultureResearchMod(ModContentPack pack) : base(pack)
        {
            new Harmony("LetterAfterCultureResearchMod").PatchAll();
        }
    }

    [DefOf]
    public static class DefsOf
    {
        public static ResearchProjectDef VFET_Culture;
        public static FactionDef VFEC_CentralRepublic, VFEC_WesternRepublic, VFEC_EasternRepublic;
    }

    [HarmonyPatch(typeof(VFETribals.ResearchManager_FinishProject_Patch), "Postfix")]
    public static class Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            var call = AccessTools.Method(typeof(VFETribals.GameComponent_Tribals), "TryRegisterAdvancementObligation");
            foreach (var code in instr)
            {
                if (code.Calls(call))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch), "RegisterLetter"));
                }
                yield return code;
            }
        }

        public static void RegisterLetter(ResearchProjectDef researchProjectDef)
        {
            if (researchProjectDef == DefsOf.VFET_Culture)
            {
                var factions = Find.FactionManager.GetFactions().Where(x => x.def == DefsOf.VFEC_EasternRepublic
                || x.def == DefsOf.VFEC_CentralRepublic
                || x.def == DefsOf.VFEC_WesternRepublic).ToList();
                if (factions.Any() && Find.LetterStack.letters.Any(x => x.Label == "CultureLetterTitle".Translate()) is false)
                {
                    var delay = Current.ProgramState == ProgramState.Playing ? GenDate.TicksPerDay
                    : GenDate.TicksPerDay * 8;
                    Find.LetterStack.ReceiveLetter("CultureLetterTitle".Translate(),
                    "CultureLetterTitleDesc".Translate(factions.FormatList()).Resolve(), LetterDefOf.NeutralEvent,
                    delayTicks: delay);
                }
            }
        }

        static string FormatList(this List<Faction> factions)
        {
            if (factions.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < factions.Count; i++)
            {
                sb.AppendTagged(factions[i].NameColored);

                if (i < factions.Count - 2)
                    sb.Append(", ");
                else if (i == factions.Count - 2)
                    sb.Append(" and ");
            }

            return sb.ToString();
        }
    }
}
