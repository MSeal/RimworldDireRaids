using System;
using RimWorld;
using Verse;

namespace DireRaids {
  public class NonHugsDireRaidLoader : DireRaidLoader {
    public static Func<float> getPointMultiplier;
    const float DEFAULT_POINT_MULT = 3.0F;
    
    public float PointMultiplier() {
      return DEFAULT_POINT_MULT;
    }
  }
}
