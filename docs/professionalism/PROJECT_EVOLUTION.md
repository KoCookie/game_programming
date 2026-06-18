# Project Evolution

## Initial Concept

The original idea was a memory-based 2D puzzle game. The player would observe a grid for a limited time, remember the positions of important objects, then navigate after the map became hidden. The main design focus was short-term memory, spatial planning, and the tension between remembering the layout and acting correctly.

## Early Prototype

The earliest prototype already contained the required basics of the game idea: a player, key, goal, obstacles, lives, View Map, and level selection. However, these early systems were much more fixed and manual. The first level relied more on directly configured or manually placed objects, which worked for a simple prototype but would not scale well to many levels.

## Shift to Reusable Prefabs and Level Data

When expanding beyond the first level, it became clear that the project needed a more reusable structure. The player, tile, key, goal, obstacle, life, and other repeated objects were made into prefabs so they could be reused across levels instead of recreated or hard-coded.

The next major change was the `LevelData` and `LevelLoader` system. Instead of placing every object manually in a scene, each level could store coordinates and parameters in a data asset. `LevelLoader` then generated the board and objects dynamically. This made later development faster and more flexible, because new levels could be designed through data rather than by rewriting code.

## Core Product Features

After the reusable level system was stable, the project added supporting product features such as pause, restart, level selection, View Map behaviour, result panels, and clearer UI. These features helped the game feel like a playable vertical slice rather than only a mechanics test.

## Advanced Mechanics

The first advanced mechanics added were portals, hidden traps, and moving traps. These were implemented before the final UI replacement. They changed the game from a simple memory test into a more varied puzzle experience.

After the visual style was replaced with a more consistent Kenney-based asset set, later mechanics were added using the same reusable pattern: create or configure a prefab, expose the required parameters in Unity, then connect the mechanic to level data. This approach was used for memory thief, mirror shift, gate blocker, and disappearing tiles.

## Feedback-Driven Changes

Feedback from friends and classmates changed the project in several ways. The game became more challenging after the prototype was judged too simple. Observation times became level-specific after players found some mechanic-heavy levels too hard to study in the same amount of time. Instruction pages and level intro popups were added because new mechanics needed to be explained inside the game itself. View Map was changed to observation-only because allowing movement during View Map weakened the memory challenge.

## Final Version

The final submitted game is a macOS-tested Unity vertical slice with eleven levels, a consistent UI style, multiple reusable mechanics, configurable level data, and documentation/licensing evidence. It still focuses on the original idea of memory and spatial reconstruction, but it developed from a small prototype into a more complete puzzle game with escalating challenge.

