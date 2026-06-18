# AI and External Resources Declaration

This project used external assets, Unity tools, and AI assistance. The final decisions, Unity integration, asset selection, level design, testing, balancing, and submission responsibility remained my own.

## External Assets and Tools

| Resource | Type | Source | Licence / permission | What it provided | What was used unchanged | What was modified | What I created myself | Where it appears | How it is credited |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Unity 2022.3 LTS | Game engine and editor | Unity | Unity terms | 2D scene system, editor workflow, build system, UI components, collision/trigger components | Core Unity editor/runtime features | Project settings, scenes, prefabs, scripts, UI layout | Game concept, scripts, levels, prefabs, tuning, build | Whole project | Mentioned in README and reports |
| TextMesh Pro | Unity text rendering package | Unity package system | Unity package licence/terms | Clearer text rendering for UI | Text rendering components | Font assignment, sizing, layout, UI styling | UI structure and content | Menus, HUD, buttons, popups | Mentioned in resource declaration |
| Kenney Abstract Platformer | 2D game art asset pack | Kenney | Creative Commons CC0 | Abstract character and gameplay sprites suitable for player, obstacles, key/goal-like icons, enemies, and map objects | Sprite artwork | Selected, scaled, assigned to prefabs, combined with game logic | Prefabs, object behaviour, level design | Gameplay objects and legend icons | `THIRD_PARTY_NOTICES.md`, `docs/licenses/` |
| Kenney Puzzle Pack 2 | 2D puzzle/tile asset pack | Kenney | Creative Commons CC0 | Tile/grid-style sprites and puzzle icons | Sprite artwork | Selected, scaled, assigned to tile prefabs and UI where needed | Grid generation and level layout | Board/grid and puzzle visuals | `THIRD_PARTY_NOTICES.md`, `docs/licenses/` |
| Kenney UI Pack | UI art asset pack | Kenney | Creative Commons CC0 | Buttons, panels, and UI sprites | Sprite artwork | Selected and styled in Unity UI | Menu/popup structure and button behaviour | Start menu, pause/result UI, buttons | `THIRD_PARTY_NOTICES.md`, `docs/licenses/` |
| Kenney UI Pack Sci-Fi | UI art/font asset pack | Kenney | Creative Commons CC0 | Sci-fi UI elements and font assets | Sprite/font artwork | Selected, resized, and assigned to the project UI | Layout and UI behaviour | Game title, buttons, UI text styling | `THIRD_PARTY_NOTICES.md`, `docs/licenses/` |

No complete external game template was used as the basis for the project. The gameplay systems, level logic, level data, and Unity integration were created for this project. Some imported package or demo folders may remain in the Unity project, but the submitted gameplay relies on the systems and assets documented above.

## AI Assistance Declaration

| Tool used | What I asked | What output I used | What I changed | How I tested it | What I understand | What I still need more practice with |
| --- | --- | --- | --- | --- | --- | --- |
| ChatGPT | Brainstorming possible ways to make the memory game more interesting. | General discussion of possible mechanics and design directions. | I did not simply accept the suggested story-heavy directions. I kept the project focused on a simple memory puzzle and selected mechanics that supported gameplay, such as pressure, route planning, and map transformation. | Tested ideas by implementing levels and asking friends/classmates to play. | I understand why the selected mechanics support the observation-memory-action loop. | I can improve at documenting design decisions immediately while developing. |
| Codex | Implementation support for Unity/C# systems, especially when adding reusable mechanics or refactoring systems. | Code assistance and implementation suggestions for systems such as configurable level data, managers, and UI behaviour. | I integrated changes into the Unity project, set up prefabs and Inspector references, adjusted level data, tuned levels, and tested the result. | Tested in Unity Play Mode and macOS builds. | I understand the implemented game systems, how the data-driven levels work, and how the prefabs/managers interact. | I still need more independent practice with some Unity editor configuration details. |
| Codex | Debugging guidance for errors, missing references, UI layout problems, and build presentation issues. | Debugging steps and code changes to investigate and fix specific problems. | I checked Console errors, Inspector references, prefab bindings, scene objects, and build results myself before accepting fixes. | Retested the affected levels and rebuilt the game where needed. | I understand the cause and fix of the major bugs documented in the testing log. | I can improve by writing bug notes at the time the bug happens rather than reconstructing them later. |

## AI Use Boundary

AI was used as support for brainstorming, implementation support, and debugging guidance. It did not replace my personal contribution. The game idea, theme, asset choices, mechanic choices, Unity integration decisions, level designs, tuning, playtesting decisions, and final submission responsibility remained mine.

I understand the project code and gameplay systems. Where AI helped produce or adjust code, I reviewed it in context, connected it to the existing Unity objects and prefabs, tested it, and made final decisions based on whether it worked in my actual game.

## Audio

The submitted game does not use sound effects or music. This was a project-scope decision: the priority was to make the observation-memory-action gameplay, level mechanics, UI clarity, testing, and final build stable. Audio was considered less important than proving the memory puzzle loop and could have added extra distraction during the memory observation process.

