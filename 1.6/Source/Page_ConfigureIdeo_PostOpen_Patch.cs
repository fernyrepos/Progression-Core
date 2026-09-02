using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ProgressionCore
{
    [HarmonyPatch(typeof(Page_ConfigureIdeo), "PostOpen")]
    public static class Page_ConfigureIdeo_PostOpen_Patch
    {
        private static readonly WeakReference shownForPage = new WeakReference(null);

        private static void Postfix(Page_ConfigureIdeo __instance)
        {
            if (ProgressionCoreUtility.StartingOnAnimalTech is false || shownForPage.Target == __instance)
            {
                return;
            }
            shownForPage.Target = __instance;
            string text = "AnimalTechIdeoPageIntro".Translate() + "\n\n"
                + ("<b>" + "AnimalTechIdeoPageCulture".Translate() + "</b>").Colorize(ColoredText.TipSectionTitleColor);
            Find.WindowStack.Add(new Dialog_MessageBox(text));
        }
    }
}
