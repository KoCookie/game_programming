# Planning and Task Management Evidence

## Project Planning Approach

The project was managed as a staged Unity prototype rather than as one large single implementation task. The main planning principle was to make the memory-game loop playable first, then expand it through reusable level data, prefabs, new mechanics, visual polish, testing, and final build preparation.

The development plan was not followed in a perfectly linear way. Some tasks changed order because of practical testing needs. For example, the visual asset replacement happened before several later mechanics were added, because once the game contained many different object types, coloured placeholder squares were no longer clear enough for testing memory and recognition.

## Development Milestones

| Phase | Main objective | Evidence in repository |
| --- | --- | --- |
| 1. Prototype | Prove that the observation-memory-action loop could work as a simple playable idea. | `357d8f2`, `b1520ad` |
| 2. Core gameplay loop | Add observation/action phases, player movement, key, goal, lives, View Map, pause, result panels, and level selection. | `cd3a87b` |
| 3. Reusable level architecture | Replace hard-coded/manual level setup with prefab-based objects, `LevelData`, `LevelLoader`, dynamic lives, and dynamic legends. | `ab3a0cd`, `54285e7` |
| 4. First advanced mechanics | Add portals, hidden traps, and moving traps through configurable level data. | `cf304e6`, `d8defb5` |
| 5. Visual style and UI replacement | Replace placeholder visuals with a more unified Kenney-based style and improve key gameplay sprites and UI elements. | `4e15085` |
| 6. Documentation and repository structure | Add README, documentation, development notes, asset credits, `.gitignore`, and licensing evidence. | `d81cd3a`, `6833c81` |
| 7. Instruction systems and later mechanics | Add homepage instructions, per-level intro popups, memory thief, mirror shift, gate blocker, disappearing tiles, level pagination, and more levels. | `d2c863e`, `e3273bc`, `e7953c3`, `990318a` |
| 8. Final build polish | Improve responsive UI layout, build presentation, observation/action visibility rules, and final gameplay layout. | `101bd9a` |

## Retrospective Task Board

This project did not use a live GitHub Kanban board during the whole development period. To make the planning evidence clearer, the work can be reconstructed as the following retrospective task board.

| To do / planned | In progress during development | Done / evidence |
| --- | --- | --- |
| Create a memory-based puzzle concept | Build a minimal observation and action prototype | Prototype and core loop commits |
| Support multiple levels | Refactor hard-coded objects into reusable prefabs and level data | `LevelData`, `LevelLoader`, prefab workflow |
| Add challenge beyond basic obstacles | Add portals, hidden traps, moving traps, memory thief, mirror shift, gate blocker, disappearing tiles | Advanced mechanics commits and level assets |
| Make the game understandable without verbal explanation | Add homepage instruction and per-level intro popups | Instruction UI commit |
| Improve visual clarity | Replace colour-block placeholders with consistent Kenney assets | Visual polish commit and screenshots |
| Prepare repository for assessment | Add README, documentation, asset credits, licences, evidence files | Documentation and professionalism evidence |
| Prepare final build | Test macOS build, fix UI scaling and overlap issues | Final build polish commit |

## Priority Management

The project used a practical priority model:

| Priority | Features |
| --- | --- |
| Must-have | Observation phase, action phase, grid movement, key, goal, obstacles, health/lives, level loading, win/lose states, basic UI, level intro popups |
| Should-have | Pause menu, restart, level selection, View Map, clear legend/key/life UI, repository documentation |
| Could-have | Collectible hearts, portals, hidden traps, moving traps, memory thief, mirror shift, gate blocker, disappearing tiles, visual polish, additional level variety |
| Cut-first | Sound effects, complex animations, large story sequences, online features, procedural level generation, advanced visual effects |

This priority model helped keep the project focused on the core memory puzzle. The final game includes most must-have, should-have, and many could-have features, while deliberately cutting sound effects, online features, procedural generation, and large story systems.

## Time Management Notes

The work was planned in phases, but the time distribution was not perfectly even. A significant amount of development happened in concentrated work sessions rather than small daily commits. This means the repository shows clear milestone commits, but not a perfectly continuous day-by-day record.

This is one of the main lessons from the project. Future projects should use a live task board from the start, commit smaller pieces more regularly, and record testing evidence immediately instead of reconstructing some evidence near submission time.

