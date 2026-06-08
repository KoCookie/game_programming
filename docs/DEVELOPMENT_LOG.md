# Development Log

## Current Version

The current project version is a Unity 2022.3.62f3 2D memory puzzle prototype with multiple playable levels, a redesigned Kenney-style visual set, and reusable level mechanics.

## Implemented Features

- Start scene, level selection scene, and game scene.
- Grid-based player movement with keyboard input.
- Observation phase and action phase.
- Hidden map objects after observation.
- Key collection and goal completion.
- Life system and heart pickups.
- Pause, restart, level select, win, and lose panels.
- View-map action with movement disabled while the map is visible.
- Portal teleportation.
- Trap obstacles that appear after observation.
- Moving obstacle support.
- Memory thief support with configurable start position, path, and movement interval.
- Data-driven level configuration through `LevelData` ScriptableObjects.
- Updated visual style using imported Kenney assets.

## Recent Work

- Replaced placeholder colored squares with a unified abstract/puzzle asset style.
- Updated player, key, goal, heart, obstacle, trap obstacle, portal, life, legend, and key-status visuals.
- Added Level 5 and began preparing Level 6 mechanics.
- Added a `MemoryThiefManager` and `MemoryThiefData` so the thief mechanic can be reused across future levels.
- Improved player-object detection so large sprites do not accidentally trigger neighboring cells.
- Prevented player movement during the temporary view-map state.

## Repository Cleanup Goals

- Keep Unity source files, project settings, packages, scenes, scripts, prefabs, levels, and imported assets in version control.
- Ignore generated folders such as `Library/`, `Temp/`, `Logs/`, `Obj/`, local IDE files, crash reports, and build outputs.
- Keep documentation in `docs/` so the project can be understood without opening Unity first.
