# Summary of Important Commits

This file summarises the most important commits in the development history of **Afterimage**. The full commit history remains available in GitHub.

| Commit | Area | Why it matters |
| --- | --- | --- |
| `357d8f2` | Early prototype | First prototype evidence. It shows the earliest playable direction before the final architecture and visual style were developed. |
| `b1520ad` | Minimal playable version | Added a basic playable prototype with scene navigation and observation phase. This established the project as an actual Unity game rather than only a design idea. |
| `cd3a87b` | Core gameplay loop | Added observation/action phases, countdown timers, grid movement, key/goal interactions, life management, View Map, pause, result panels, and initial level selection. This commit represents the first complete core loop. |
| `ab3a0cd` | Data-driven architecture | Refactored the game into a scalable `LevelData` and `LevelLoader` architecture with reusable prefabs, dynamic map generation, life UI, legend UI, View Map limits, and collectible hearts. This was a major technical foundation for later levels. |
| `54285e7` | Level 2 and reusable systems | Added configurable Level 2 data, dynamic player spawn positions, obstacles, hearts, scalable life UI, and improved legend support. This confirmed that the architecture could support multiple levels. |
| `cf304e6` | Portal mechanic | Added Level 3 and bidirectional portal support. This was the first major advanced mechanic beyond static obstacles. |
| `d8defb5` | Hidden and moving traps | Added Level 4, `TrapManager`, hidden traps, moving traps, blinking, smooth movement, and invisible collision persistence. This increased challenge and required more careful memory reconstruction. |
| `4e15085` | Visual/UI style update | Replaced placeholder visuals with Kenney-style assets for the grid, player, key, goal, hearts, obstacles, portals, traps, UI font, buttons, legends, and key indicator. This improved readability and style consistency. |
| `d81cd3a` | Repository documentation | Added stronger repository structure, README, user guide, game design notes, development log, asset credits, `.gitignore`, and `.gitattributes`. This made the project easier to assess and maintain. |
| `d2c863e` | Instructions and tutorial UI | Added homepage instructions and per-level intro popups driven by `LevelData`. This responded directly to playtester feedback that mechanics needed in-game explanation. |
| `e3273bc` | Later level expansion | Added level-selection pagination, disappearing tiles, redesigned Level 9, and Level 10. This expanded the later-game challenge and combined several existing mechanics. |
| `6833c81` | Licences | Added licensing evidence for third-party assets. This supports copyright and professional submission requirements. |
| `e7953c3` | Level 11 | Designed and implemented Level 11 using existing elements. This completed the intended submitted level set without introducing a new mechanic. |
| `990318a` | Level 11 tuning | Updated the Level 11 asset after testing and adjustment. |
| `101bd9a` | Final build polish | Improved responsive gameplay UI layout, camera sizing, UI spacing, and observation/action display rules after build testing revealed scaling and overlap problems. |

## Commit History Reflection

The commit history shows real project progression from prototype to final build, but it is not a perfect record of daily work. Some commits are large because development happened in concentrated sessions. For future work, smaller and more frequent commits would provide clearer evidence of day-to-day task management.

