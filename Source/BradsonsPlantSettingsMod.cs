using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using UnityEngine;
using RimWorld;
using Verse;

namespace BradsonsPlantSettings
{
    public class BradsonsPlantSettingsMod : Mod
    {
        public static BradsonsPlantSettingsMod mod;
        public static BradsonsPlantSettingsSettings settings;

        public BradsonsPlantSettingsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<BradsonsPlantSettingsSettings>();
            mod = this;
        }

        public override string SettingsCategory()
        {
            return "Bradsons Plant Settings";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            float secondStageHeight;
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(inRect);
            ls.Gap(30);
            secondStageHeight = ls.CurHeight;
            inRect.yMin = secondStageHeight;
            ls.Label("These settings require a restart.");
            ls.Gap(30);
            ls.GapLine();
            ls.CheckboxLabeled("Allow non-wild plants to be sown in hydroponics and other kinds of planter boxes", ref settings.addHydroponic, "Includes Devilstrand and trees, but does not bypass research requirements.");
            ls.CheckboxLabeled("Allow non-wild plants to be sown in plant pots", ref settings.addPots, "Same as above, but for the smaller decorative plant pots.");
            ls.CheckboxLabeled("Allow sowing of most wild plants and trees on fertile terrain", ref settings.allowWildPlants, "Still requires research. This is normally enough to make the interesting wild plants and trees sowable.");
            ls.CheckboxLabeled("Allow wild plants for hydroponics too", ref settings.wildHydroponic, "Requires the relevant above options to be enabled.");
            ls.CheckboxLabeled("Allow wild plants for plant pots too", ref settings.wildPots, "Requires the relevant above options to be enabled.");
            ls.CheckboxLabeled("Remove the spacing requirement between sown trees", ref settings.fixTreeSpacing, "Normally trees need free spaces around them when sowing.");
            ls.CheckboxLabeled("Allow trees to be sown under roofs", ref settings.fixTreeRoofing, "For indoor tree farms.");
            ls.CheckboxLabeled("Allow everything to be sown, including plants that aren't meant to regrow at all", ref settings.addSowTags, "Disabled by default. Respects the above settings in regards to wild plants and hydroponics. Generally bloats the sowing list quite heavily.");
            ls.End();

            base.DoSettingsWindowContents(inRect);
        }

    }

    public class BradsonsPlantSettingsSettings : ModSettings
    {
        public bool addHydroponic = true;
        public bool addPots = false;
        public bool allowWildPlants = true;
        public bool wildHydroponic = true;
        public bool wildPots = false;
        public bool fixTreeSpacing = true;
        public bool fixTreeRoofing = true;
        public bool addSowTags = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref addHydroponic, "addHydroponic", true);
            Scribe_Values.Look(ref addPots, "addPots", false);
            Scribe_Values.Look(ref allowWildPlants, "allowWildPlants", true);
            Scribe_Values.Look(ref wildHydroponic, "wildHydroponic", true);
            Scribe_Values.Look(ref wildPots, "wildPots", false);
            Scribe_Values.Look(ref fixTreeSpacing, "fixTreeSpacing", true);
            Scribe_Values.Look(ref fixTreeRoofing, "fixTreeRoofing", true);
            Scribe_Values.Look(ref addSowTags, "addSowTags", false);
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }
}
