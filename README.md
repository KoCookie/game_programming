# Afterimage

Afterimage is a 2D memory puzzle game built with Unity. The player first observes a visible map for a short time, then explores after the map information is hidden. Each level asks the player to reconstruct the layout from memory, plan a path, collect the key, avoid hazards, and reach the goal.

The project focuses on a repeated loop of observation, memory, exploration, and verification. As levels progress, the game introduces limited lives, portals, hidden traps, moving obstacles, view-map limitations, and a reusable memory thief mechanic that moves toward the key.

## Project Information

- Engine: Unity 2022.3.62f3
- Platform target: macOS / desktop
- Genre: 2D puzzle, memory, spatial reasoning
- Main scenes:
  - `Assets/Scenes/StartScene.unity`
  - `Assets/Scenes/LevelSelectScene.unity`
  - `Assets/Scenes/GameScene.unity`

## Core Mechanics

- Observation phase: the full map is visible, but the player cannot move.
- Action phase: map objects are hidden and the player moves based on memory.
- Key and goal: the player must collect the key before reaching the goal.
- Lives: obstacles reduce lives; hearts restore lives.
- Portals: paired positions teleport the player across the grid.
- Trap obstacles: some hazards appear only after the observation phase.
- Moving traps: obstacles can move between configured grid positions.
- View map: the player can briefly reveal the map, but cannot move while viewing it.
- Memory thief: a reusable enemy system where a thief follows a configured path toward the key. If it reaches the key first, the player loses.

## Controls

- Move: `WASD` or arrow keys
- Pause: pause button
- View map: view-map button during the action phase when available

## Repository Structure

```text
Assets/
  Art/                 Imported visual assets
  Levels/              ScriptableObject level data
  Prefabs/             Gameplay and UI prefabs
  Scenes/              Unity scenes
  Scripts/             Gameplay scripts
  TextMesh Pro/        TextMesh Pro assets
docs/                  Design notes, user guide, development log, credits
Packages/              Unity package manifest and lock file
ProjectSettings/       Unity project settings
```

Unity-generated folders such as `Library/`, `Temp/`, `Logs/`, `Obj/`, and local IDE files should not be committed.

## How To Open

1. Install Unity Hub and Unity Editor `2022.3.62f3` or another compatible Unity 2022.3 LTS version.
2. Clone this repository.
3. Open the repository folder in Unity Hub.
4. Open `Assets/Scenes/StartScene.unity`.
5. Press Play.

## Documentation

- [Game Design](docs/GAME_DESIGN.md)
- [User Guide](docs/USER_GUIDE.md)
- [Development Log](docs/DEVELOPMENT_LOG.md)
- [Asset Credits](docs/ASSET_CREDITS.md)
- [Professionalism Evidence](docs/professionalism/README.md)

## Notes

This repository is organized as a Unity source project. It does not include build output by default. To submit or publish a playable version, create a build from Unity and place it outside the source tree or in an ignored `Builds/` folder.
