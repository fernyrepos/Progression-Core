using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace ProgressionCore
{
    public class ProgressionCoreSettings : ModSettings
    {
        public static float researchComplectionPercent = 1f;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref researchComplectionPercent, "researchComplectionPercent", 1f);
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            var ls = new Listing_Standard();
            ls.Begin(inRect);
            researchComplectionPercent = ls.SliderLabeled("Overall research completion percentage needed to advance tech-level: " 
                + researchComplectionPercent.ToStringPercent(), researchComplectionPercent, 0.01f, 1f, labelPct: 0.6f);
            ls.End();
        }
    }
}
