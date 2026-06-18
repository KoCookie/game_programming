# Organisation, Time Management, Independent Work, and Professionalism Reflection

## What Went Well

The strongest part of the project process was having a clear staged direction. I did not try to build every mechanic at once. The project moved from a minimum playable version, to reusable level systems, to supporting features such as pause and level selection, to richer mechanics such as portals and memory thief, then to UI replacement, build testing, and final polish.

This staged approach helped keep the project manageable. Even when the exact order changed, each stage still had a clear goal. For example, I originally expected to add more mechanics before spending time on visual assets, but once more objects were introduced, plain colour blocks became too unclear for testing. Replacing the UI and object assets earlier made later testing more useful.

Another important strength was the focus on reusability. Most game elements became prefabs, and most level-specific information moved into configurable data. This meant that later levels could be designed by setting coordinates, observation time, and mechanic parameters rather than rewriting the same logic for every level.

## Independent Contribution

My personal contribution includes the game concept, theme, and core idea. The game was based on my own interest in memory-test style tasks and the idea that this could become a more playful spatial puzzle.

I selected and integrated the external assets myself, including the Kenney asset packs, and adapted them into a complete Unity game style. I also decided which mechanics to add, such as moving traps, memory thief, portals, gate blocker, mirror shift, and disappearing tiles. These mechanics were chosen because they support the core memory and route-planning challenge.

I designed the levels myself. This required deciding where each object should be placed, how much observation time was fair, how mechanics should combine, and how difficult each route should feel. I repeatedly tested and adjusted levels based on playtesting and my own observations.

I also structured the project around prefabs and level data so the game could be extended more efficiently. This was important because the game is level-based and many elements repeat across levels.

## What Did Not Go Well

The main weakness was time distribution. I did not work on the project in small even daily sessions. Instead, I often worked in longer concentrated periods. This helped me make progress, but it also meant the commit dates were not as continuous as they could have been and the number of commits was lower than ideal.

Another weakness was evidence management. I did many planning, testing, and feedback activities, but I did not always record them in GitHub immediately. This meant that near the end of the project I had to reconstruct evidence from memory, screenshots, commit history, and notes. The evidence is truthful, but it would have been stronger if recorded at the time.

Some final polish also happened close to the deadline because build testing revealed layout problems that were not obvious in the editor. This was not a full late redesign of the game style, but it did require last-stage UI layout fixes.

## Professional Lessons

This project taught me that professional game development is not only about making a feature work once. It also requires making the feature reusable, testable, understandable, and documented.

The most useful technical workflow lesson was the value of prefabs and configurable level data. A level-based game becomes much easier to extend when repeated objects and mechanics are reusable. The most useful production lesson was the importance of testing in the real build, not only in the editor. The most useful professional lesson was that evidence should be collected continuously, especially feedback, bugs, and planning changes.

For future projects, I would set up a task board at the start, create issues for mechanics and bugs, commit smaller changes more often, and keep a short testing log throughout development. This would make the final professionalism portfolio easier to produce and would also make the project easier to manage while building it.

