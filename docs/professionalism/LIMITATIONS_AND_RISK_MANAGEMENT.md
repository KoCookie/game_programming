# Known Limitations and Risk Management

| Limitation | Impact | How it was managed | Future improvement |
| --- | --- | --- | --- |
| macOS build only confirmed | The game was developed and tested on macOS. Windows compatibility has not been confirmed. | Final build was prepared for macOS and tested on the development machine. | Test and package a Windows build if required in the future. |
| Limited number of levels | The game currently has a fixed set of submitted levels rather than a large full game campaign. | Focused on making the available levels playable, varied, and demonstrative of the mechanics. | Add more levels and continue difficulty progression. |
| No sound effects or music | The game has less audio feedback than a full release product. | Audio was cut to prioritise core mechanics, UI, level design, and build stability. | Add simple non-distracting UI and gameplay sounds if time allows. |
| Limited animation scope | The game uses simple visual changes and movement effects rather than complex animation systems. | Kept animation simple so it supported clarity: object appearing/hiding, trap movement, UI switching, and mechanic transitions. | Add more polished transitions only if they do not reduce readability. |
| Testing pool was small | Feedback came mainly from friends, classmates, and self-testing. | Used repeated testing across different levels and after each mechanic change. | Conduct broader user testing with more players and structured feedback forms. |
| GitHub evidence was not recorded continuously | Some planning, feedback, and testing evidence had to be reconstructed near the end. | Added this professionalism evidence pack to make the development process clearer and aligned with the commit history. | Use GitHub Projects, issues, and regular testing notes from the start of future projects. |
| UI polish could still improve | Some UI choices are functional and consistent but could be more refined. | Fixed major build layout problems and kept the UI readable. | Continue polishing spacing, transitions, and responsive behaviour. |
| No save system or persistent progress | The game is playable as a vertical slice but does not store long-term player progress. | Levels can be selected through the level selection screen, so testing and play are still practical. | Add save data for unlocked levels or best completion records. |
| No level editor or procedural generation | New levels must be configured manually in Unity. | Used `LevelData` and prefabs to make manual configuration much faster and more reusable. | Build an internal level editor or import tool if the game grows. |

## Risk Management Reflection

The largest risks were scope creep, unclear evidence, and unstable Unity references after asset changes. These were managed by keeping the core loop central, making mechanics configurable rather than hard-coded, maintaining asset credits and licences, and testing the build before submission.

The most important future improvement is not only technical. I should record planning, testing, and feedback continuously during development instead of relying on a retrospective evidence pack at the end.

