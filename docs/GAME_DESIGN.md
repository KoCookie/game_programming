# Game Design

## Concept

Afterimage is a 2D memory puzzle game about reconstructing space after visual information disappears. The player is shown a grid-based level for a limited observation phase, then must navigate the hidden layout from memory during the action phase.

The intended experience is quiet, focused, and slightly mysterious. The visual direction uses simple geometric forms and restrained UI elements so that the player can concentrate on spatial memory rather than detailed object recognition.

## Player Loop

1. Observe the visible map.
2. Memorize the positions of the player, key, goal, obstacles, hearts, portals, and special mechanisms.
3. Enter the action phase after the map becomes hidden.
4. Move through the grid using remembered information.
5. Verify the memory by collecting the key and reaching the goal.

## Level Progression

The game introduces complexity gradually:

- Early levels teach the observation/action structure and key-goal objective.
- Middle levels add limited lives, hearts, and portals.
- Later levels add hidden traps, moving obstacles, and larger maps.
- Advanced levels can add the memory thief, which creates time pressure by moving toward the key.

## Main Elements

- Player: the controlled object. Movement is grid-based.
- Tile: the board cell used for movement and spatial reference.
- Key: required before the goal can complete the level.
- Goal: the exit condition after the key is collected.
- Obstacle: causes life loss when stepped on.
- Heart: restores one life.
- Portal: teleports the player between paired cells.
- Trap obstacle: appears after the observation phase.
- Moving trap: moves between configured positions.
- Memory thief: follows a configured path and attempts to reach the key before the player.

## Reusable Level Data

Levels are configured as `LevelData` ScriptableObjects in `Assets/Levels/`. Each level can define:

- Grid width and height
- Player, key, and goal positions
- Obstacle and heart positions
- Portal positions
- Observation time
- Lives
- View-map usage count and duration
- Spawn traps and moving traps
- Memory thief start position, path, and move speed

This keeps new levels data-driven and avoids hard-coding each level's behavior.
