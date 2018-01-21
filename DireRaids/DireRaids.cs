using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using HugsLib;
using HugsLib.Settings;

namespace DireRaids {
  [StaticConstructorOnStartup]
  public class IncidentWorker_DireRaidEnemy : IncidentWorker_RaidEnemy {
    protected static Func<float> getPointMultiplier;

    static IncidentWorker_DireRaidEnemy() {
      // load order does not matter- HugsLib initializes before StaticConstructorOnStartup
      InitializePointMultiplier();
      Log.Message("Dire Raids: Setting point multiplier: " + getPointMultiplier());
    }

    protected static void InitializePointMultiplier() {
      const float default_point_multiplier = 3.0F;

      // Need a wrapper method/lambda to be able to catch the TypeLoadException when HugsLib isn't present
      try {
        ((Action) (() => {
          var settings = HugsLibController.Instance.Settings.GetModSettings("DireRaids");
          // add a mod name to display in the Mods Settings menu
          settings.EntryName = "DireRaids.SettingName".Translate();
          // handle can't be saved as a SettingHandle<> type; otherwise the compiler generated closure class will throw a typeloadexception
          object handle = settings.GetHandle(
            "pointMultiplier",
            "DireRaids.DangerSetting".Translate(),
            "DireRaids.DangerSettingDescription".Translate(),
            default_point_multiplier);
          getPointMultiplier = () => (SettingHandle<float>) handle;
        }))();
      } catch (TypeLoadException) {
        getPointMultiplier = () => default_point_multiplier;
      }
    }

    protected override void ResolveRaidPoints(IncidentParms parms) {
      base.ResolveRaidPoints(parms);
      float originPoints = parms.points;
      parms.points *= getPointMultiplier();
      Log.Message("Dire Raid points recalculated: " + originPoints + " to " + parms.points);
    }

    protected override string GetLetterLabel(IncidentParms parms) {
      return "DireRaidEnemy.letterLabel".Translate();
    }

    protected override string GetLetterText(IncidentParms parms, List<Pawn> pawns) {
      return "DireRaidEnemy.letterText".Translate() + "\n\n" + base.GetLetterText(parms, pawns);
    }
  }
}
