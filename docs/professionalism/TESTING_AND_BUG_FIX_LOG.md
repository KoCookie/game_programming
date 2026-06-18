# Testing and Bug-Fixing Evidence

Testing was carried out through repeated Unity Play Mode testing, individual level testing, friend/classmate playtesting, and final macOS build testing. The main focus was whether each level was playable, whether the memory loop remained fair, whether configured objects spawned correctly, and whether UI layout stayed readable.

## Major Issues and Fixes

| Area | Problem found | How it was found | Fix |
| --- | --- | --- | --- |
| Dynamic level generation | During the shift from manually placed levels to dynamic level data, the map layout became inconsistent. Player, key, goal, and obstacles sometimes appeared in unexpected places because old scene objects and generated objects coexisted. | Play Mode testing while testing `LevelData` and `LevelLoader`. | Removed old manually placed map objects and made the level load consistently from `LevelData` through `LevelLoader`. |
| Prefab and asset references | After replacing placeholder assets with Kenney assets, some objects failed to generate and Unity Console reported missing references. | Running the game after asset replacement. Some objects disappeared and Inspector/Console showed reference errors. | Checked Inspector bindings for `GameManager`, `LevelLoader`, and manager scripts; re-bound prefabs and sprites; removed or ignored incompatible imported plugin/demo objects that were not part of the game. |
| Sprite size and collision feel | After replacing simple colour squares with final sprites, visual size and collision/trigger behaviour felt misaligned. In some cases, enlarged visuals made it feel as if objects were affecting neighbouring tiles. | Manual playtesting after visual replacement. | Adjusted prefab scale, collider sizes, trigger settings, and player interaction handling so logic remained tile-based even when sprites looked larger. |
| Dynamic life UI | Life icons visually rearranged after losing health, causing spacing to change suddenly. | Testing obstacle collision and life loss. | Changed the life UI to update icon visibility/generation more consistently and adjusted the layout so remaining icons stayed stable. |
| Heart pickup | Map hearts initially did not appear or did not restore health. | Testing collectible hearts in levels. | Created a separate `HeartPrefab` for map pickups with `SpriteRenderer`, `BoxCollider2D`, and trigger behaviour, then added heart spawning through `LevelData` and `LevelLoader`. |
| Dynamic legend | Legend icons and labels overlapped, shifted position, or used inconsistent colours. | Testing automatic legend generation for different levels. | Rebuilt `LegendItemPrefab` structure and adjusted `LegendManager` so each level generates only the needed legend items from `LevelData`. |
| Portal loading | Level 3 did not load the intended configuration and portals did not display correctly. | Testing Level 3 from the level selection scene. | Added the correct level-selection method and ensured `LevelLoader` had the level data assets bound in the right order. |
| Moving traps | Dynamic obstacles initially blinked or disappeared without showing movement. When the whole object was disabled, the collider also turned off, so the final hazard did not hurt the player. | Testing Level 4 moving traps. | Changed the logic to hide only the `SpriteRenderer`, not the whole GameObject. Added smooth movement, a visible target pause, and retained collision after the visual disappeared. |
| TrapManager null data | Older levels could report errors because trap systems started even when the current level had no trap data. | Testing earlier levels after adding dynamic traps. | Added null and length checks so trap logic runs only when the current level actually configures traps. |
| View Map rule | View Map originally allowed movement during temporary reveal, reducing the memory challenge. | Playtesting and observing that players could use the reveal time to move quickly. | Disabled player movement during View Map, making it an observation-only feature. |
| Responsive UI build issues | The built game showed UI drift, overlap, board scaling problems, legend overlap, and View Map button placement issues. | macOS build testing after the project seemed correct in the editor. | Added responsive camera/layout logic, scaled board and legend more carefully, repositioned HUD elements, fixed margins, and hid life icons during observation to avoid title overlap. |

## Unity Editor Reference Errors

During asset import and replacement, Unity sometimes showed errors such as `SerializedObjectNotCreatableException` and `UnityEditor.GameObjectInspector` messages. These were mainly caused by Inspector references to missing sprites, prefabs, or imported editor/demo objects, rather than by the final gameplay logic.

The response was to clear the console, inspect affected prefabs for missing sprites or missing scripts, re-bind final prefabs, and remove imported items that were not needed for the submitted game. This improved project stability and made the build process cleaner.

## Testing Reflection

The most important testing lesson was that Unity editor testing is not enough. Some issues, especially UI scaling and layout, only became obvious in the final build window. This is why final build testing became part of the project process rather than only a packaging step.

