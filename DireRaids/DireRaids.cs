using RimWorld;
using Verse;

namespace DireRaids {
  namespace RimWorld {
    public class IncidentWorker_DireRaidEnemy : IncidentWorker_RaidEnemy {
      public const int POINT_MULTIPLIER = 3;

      protected override void ResolveRaidPoints(IncidentParms parms) {
        base.ResolveRaidPoints(parms);
        parms.points *= POINT_MULTIPLIER;
      }
    }
  }
}
