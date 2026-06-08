# User Guide

## Starting The Game

Open `Assets/Scenes/StartScene.unity` in Unity and press Play. Use the start menu to enter level selection, then choose a level.

## Controls

- Move with `WASD` or arrow keys.
- Use the pause button to pause, restart, resume, or return to level selection.
- Use the view-map button during the action phase if the current level provides view-map uses.

## Rules

During the observation phase, the map is visible and the player cannot move. Use this time to memorize important positions.

During the action phase, most map objects become hidden. The player must move according to memory. To win, collect the key first, then reach the goal.

Obstacles reduce lives. Hearts restore lives. Portals teleport the player between two configured cells. Some traps may appear only after the observation phase, and some obstacles may move.

If a memory thief is enabled in the level, it moves toward the key. The player must collect the key before the thief reaches it.

## Level Editing

Most level content is configured through `LevelData` assets in `Assets/Levels/`.

To create or adjust a level:

1. Open or create a `LevelData` ScriptableObject.
2. Set the grid size.
3. Set the player start, key, and goal positions.
4. Add obstacles, hearts, portals, traps, and memory thief data as needed.
5. Add the level asset to the `LevelLoader` level list.
6. Add or unlock the corresponding level selection button if needed.

Coordinates are grid-based. The project has been using the lower-left cell as `(0, 0)`.
