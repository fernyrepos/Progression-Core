using HarmonyLib;
using UnityEngine;
using Verse;

namespace ProgressionCore
{
    public class ProgressionCoreMod : Mod
    {
        public static ProgressionCoreSettings settings;
        public ProgressionCoreMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<ProgressionCoreSettings>();
            new Harmony("ProgressionCoreMod").PatchAll();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return this.Content.Name;
        }
    }
}
