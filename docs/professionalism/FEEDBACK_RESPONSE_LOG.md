# Feedback Response Log

Feedback mainly came from friends and classmates who played the game at different stages: after the initial prototype, after new mechanics were added, after individual level tests, and before the final build.

| Stage | Feedback or observation | Response | Evidence |
| --- | --- | --- | --- |
| After the initial prototype | The basic version was playable, but it did not yet feel challenging or varied enough because it relied mainly on simple obstacles. | Reduced the emphasis on basic-only levels and added more varied mechanics such as portals, hidden traps, moving traps, memory thief, mirror shift, gate blocker, and disappearing tiles. | Advanced mechanics commits: `cf304e6`, `d8defb5`, `e3273bc`; level assets in `Assets/Levels/` |
| During level playtests | A single observation time did not suit every level. When levels became more complex, three seconds could be too short for some layouts. | Adjusted observation time per level through `LevelData` so that difficulty could be tuned by level rather than globally. | `LevelData` assets and configurable observation time fields |
| After new mechanics were introduced | Players needed in-game explanation. Verbal explanation before play was not a good final-user experience. | Added homepage instructions and per-level intro popups that explain new mechanics before the observation phase starts. | `d2c863e`, `Assets/Scripts/GameManager.cs`, `Assets/Scripts/StartMenu.cs`, level intro fields |
| During visual testing | Earlier UI and sprites were too concrete or inconsistent for a memory game. Simple icon-like objects were easier to remember. | Replaced placeholder/realistic elements with a more abstract and consistent Kenney-based visual style. | `4e15085`, screenshots in `screenshots/` |
| View Map testing | The View Map feature originally allowed movement during the temporary reveal. This weakened the memory challenge because players could move quickly while seeing the map. | Changed View Map so it only allows temporary observation. Player movement is disabled while the map is being viewed. | Later gameplay/UI changes in `GameManager` and player control handling |
| Earlier Level 6 test | The earlier version of Level 6 was too easy because the player could collect the key before the memory thief created real pressure. | Revised the level so the thief creates a stronger timing pressure and the route is less direct. | Updated `Level_6` asset and memory thief configuration |
| Earlier Level 9 test | The earlier version of Level 9 had a route that was too easy to remember, so the disappearing-tile rule did not create enough planning challenge. | Redesigned Level 9 into a more demanding route-planning puzzle. | `e3273bc` |
| Final build self-testing | Build testing showed UI scaling, overlap, and positioning issues that were not obvious inside the Unity editor view. | Improved responsive camera/UI layout, reduced overlap, repositioned HUD elements, separated legend and board spacing, and hid life icons during observation phase. | `101bd9a` |

## Feedback Reflection

The most useful feedback was not only whether a level was too easy or too hard, but why. This helped separate visual clarity problems, explanation problems, and actual mechanic-balance problems. The final game changed significantly because of this process: it became less like a basic grid prototype and more like a structured memory-puzzle game with escalating mechanics and clearer player guidance.

