using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using System.Reflection;
using HugsLib;
using HugsLib.Settings;

namespace DireRaids {
  public class DireRaidLoader : ModBase
  {
    const float DEFAULT_POINT_MULT = 3.0F;
    const float DEFAULT_BASE_CHANCE = 1.8F;
    const int DEFAULT_MIN_REFIRE = 60;
    const int DEFAULT_MIN_THREAT = 2000;
    const int DEFAULT_MIN_POP = 1;

    protected SettingHandle<float> pointMultHandler;
    protected SettingHandle<float> baseChanceHandler;
    protected SettingHandle<int> minRefireDaysHandler;
    protected SettingHandle<int> minThreatPointsHandler;
    protected SettingHandle<int> minPopulationHandler;

    public float PointMultiplier() {
      LoadHandles();
      return pointMultHandler.Value;
    }

    public override string ModIdentifier {
      get { return "DireRaids"; }
    }

    public override void Initialize() {
      // add a mod name to display in the Mods Settings menu
      Settings.EntryName = "DireRaids.SettingName".Translate();
    }

    public static SettingHandle.ValueIsValid FloatNonNegativeValidator()
    {
      return str => {
        float parsed;
        if (!float.TryParse(str, out parsed)) return false;
        return parsed >= 0.0;
      };
    }

    public static SettingHandle.ValueIsValid IntNonNegativeValidator()
    {
      return str => {
        int parsed;
        if (!int.TryParse(str, out parsed)) return false;
        return parsed >= 0;
      };
    }

    protected void LoadHandles()
    {
      pointMultHandler = Settings.GetHandle<float>(
        "pointMultiplier",
        "DireRaids.DangerSetting".Translate(),
        "DireRaids.DangerSettingDescription".Translate(),
        DEFAULT_POINT_MULT,
        FloatNonNegativeValidator());
      pointMultHandler.OnValueChanged = newValue => { ApplySettings(); };

      baseChanceHandler = Settings.GetHandle<float>(
         "baseChance",
         "DireRaids.BaseChance".Translate(),
         "DireRaids.BaseChanceDescription".Translate(),
         DEFAULT_BASE_CHANCE,
         FloatNonNegativeValidator());
      baseChanceHandler.OnValueChanged = newValue => { ApplySettings(); };

      minRefireDaysHandler = Settings.GetHandle<int>(
        "minRefireDays",
        "DireRaids.MinRefire".Translate(),
        "DireRaids.MinRefireDescription".Translate(),
        DEFAULT_MIN_REFIRE,
        IntNonNegativeValidator());
      minRefireDaysHandler.OnValueChanged = newValue => { ApplySettings(); };

      minThreatPointsHandler = Settings.GetHandle<int>(
        "minThreatPoints",
        "DireRaids.MinThreat".Translate(),
        "DireRaids.MinThreatDescription".Translate(),
        DEFAULT_MIN_THREAT,
        IntNonNegativeValidator());
      minThreatPointsHandler.OnValueChanged = newValue => { ApplySettings(); };

      minPopulationHandler = Settings.GetHandle<int>(
        "minPopulation",
        "DireRaids.MinPopulation".Translate(),
        "DireRaids.MinPopulationDescription".Translate(),
        DEFAULT_MIN_POP,
        IntNonNegativeValidator());
      minPopulationHandler.OnValueChanged = newValue => { ApplySettings(); };
    }

    public void ApplySettings()
    {
      IncidentDef raid = IncidentDef.Named("DireRaidEnemy");
      raid.baseChance = baseChanceHandler;
      raid.minRefireDays = minRefireDaysHandler;
      raid.minThreatPoints = minThreatPointsHandler;
      raid.minPopulation = minPopulationHandler;

      Logger.Message(String.Format(
        "Settings loaded:\n\tpointMultiplier: {0}\n\tbaseChange: {1}\n\tminRefireDays: {2}\n\tminThreatPoints: {3}\n\tminPopulation: {4}",
        PointMultiplier(), raid.baseChance, raid.minRefireDays, raid.minThreatPoints, raid.minPopulation));
    }

    public override void DefsLoaded()
    {
      LoadHandles();
      ApplySettings();
    }
  }

  [StaticConstructorOnStartup]
  public class IncidentWorker_DireRaidEnemy : IncidentWorker_RaidEnemy {
    protected static DireRaidLoader modLoader = new DireRaidLoader();

    protected override void ResolveRaidPoints(IncidentParms parms) {
      base.ResolveRaidPoints(parms);
      float originPoints = parms.points;
      parms.points *= modLoader.PointMultiplier();
      Log.Message(String.Format("[DireRaids] points recalculated: {0} to {1}", originPoints, parms.points));
    }

    protected override string GetLetterLabel(IncidentParms parms) {
      return "DireRaidEnemy.letterLabel".Translate();
    }

    protected override string GetLetterText(IncidentParms parms, List<Pawn> pawns) {
      return "DireRaidEnemy.letterText".Translate() + "\n\n" + base.GetLetterText(parms, pawns);
    }
  }
}
