# RimworldDireRaids

![Version](https://img.shields.io/badge/Rimworld-B18-brightgreen.svg)

![Alt text](About/Preview.png?raw=true "DireRaids")

[B18] Creates a mid-to-late game event called a Dire Raid. The new event triggers like a normal Raid, but more rarely, and multiplies the points available for the raid to use in purchasing pawns.

The mod is intended to add more threat to established bases. Many late game bases can feel unthreatened by any events, making late game a bit dull. Thus this mod is to meant to add a potential threat to such bases.

By default the Dire Raid event is rare and can't occur back-to-back. Dire raids can't occur until your base is fairly developed, so no day 20 death balls. The event is limited to augmenting Raid events, not any other events (no Dire Infestations). If any new raids are introduced by other mods they can also become Dire Raid variants.

If you load this module after HugsLib you can configure the difficulty and occurrance of the mod in the Mod Options menu. Changing Danger Multiplier value modifies the multiplier against pawn strength pool used to generate raids.


## Suggested Mods:
- Hugs Lib (or you'll see an error on load -- it recovers gracefully to use default settings if you don't want HugsLib installed).

## FAQ:

#### Is this another type of raid?

> No. In game terms this is a new Incident like Infestations, Mad Animals, and Tornados. It triggers independently as a Big Thread and reuses the Raids available while giving them bonus points to pick build enemies.

#### How can I reduce lag during raids?

> For reducing lag in late game raids (not just Dire Raids) you can use either:
>   RuntimeGC: http://steamcommunity.com/sharedfiles/filedetails/?id=962732083
> or:
>   MakeLoveNotWar: https://ludeon.com/forums/index.php?topic=31646.0
>
> Each pawn that gets added adds social interactions between other pawns, making large numbers of pawns a problem for the game. Cleaning these relationships up after pawns are killed or eliminating them entirely can help with the issue. Otherwise it's a core game inefficiency that mods can't always fix.

## Current (Sometimes Crude) Translations:
- English
- French
- German
- Japanese
- Polish
- Portuguese
- Russian
- Spanish

Link me translation fixes/additions and I'll add them in.


## Changes:

#### 0.18.1

> Increased spawn rate slightly as it took longer than a year for the average Dire Raid to occur in test runs.
    
#### 0.18.2

> Added support for modifying many more event trigger controls. See Mod Options to configure mod how you want it. WARNING: not having HugsLib loaded will now result in an error message on load. It's safe to ignore this message.

