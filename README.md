# SoundBound Unity Project

Full Unity project files for the SoundBound game project.

To Play
1. Download the `WindowsBuild` folder.  
2. Open the folder and launch `SoundBoundProject.exe`.

Known Bugs
- Sprite bug for spikes (sprite does not match collision)  
- UI bug for death counter (does not appear where it should in Windows build)  
- Death counter does not properly track deaths  
- Death counter is not implemented in every level  
- Buttons are not the current size for Windows build


M1:
Added scripts and prefabs needed for level creation:  
- `KillOnTouch.cs`  
- `GameManager.cs`  
- `Movement.cs`  

Game objects:  
- spikes  
- player  
- platforms  
- gameManager  
  - player spawner

M2:
- Fixed bugs involving player movement and platforms  
- Added 3–5 levels for playtesting and final build

M3:
- More levels added  
- Added death counter  
- Sound control added

M4:
- Sprites added to player, spikes, and platforms  
- Main menu, level select, help, and victory screens added  
- Final levels implemented (15 total)
