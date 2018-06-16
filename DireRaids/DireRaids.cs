using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using System.Reflection;

namespace DireRaids {
  public interface DireRaidLoader {
    float PointMultiplier();
  }

  [StaticConstructorOnStartup]
  public class IncidentWorker_DireRaidEnemy : IncidentWorker_RaidEnemy {
    protected static DireRaidLoader modLoader;

    static IncidentWorker_DireRaidEnemy() {
      try
      {
        ((Action)(() => {
          var DLL = Assembly.Load("HugsDireRaidLoader");
          Type type = DLL.GetType("DireRaids.HugsDireRaidLoader", true);
          //Type type = Type.GetType("DireRaids.HugsDireRaidLoader", true);

          object loaderInstance = Activator.CreateInstance(type);
          modLoader = loaderInstance as DireRaidLoader;
        }))();
      }
      catch (Exception ex)
      {
        if (ex is TypeLoadException || ex is ReflectionTypeLoadException)
        {
          Log.Warning(ex.ToString());
          Type type = Type.GetType("DireRaids.NonHugsDireRaidLoader", true);
          object loaderInstance = Activator.CreateInstance(type);
          modLoader = loaderInstance as DireRaidLoader;
          Log.Message("[DireRaids] Unable to load Hug variant of Dire Raids... loaded default settings.");
        } else {
          throw;
        }
      }
    }

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
