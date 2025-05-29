# White Whale
 
Created for CS 410 Game Programming at the University of Oregon in Spring 2025.

Team members: Stella Greenvoss, Dylan Tiffany, Clio Tsao, Julia Wheless


## Beta Build
This repository currently contains an beta build for White Whale with core game mechanics and three playable environments.

**Core Mechanics**:  
  - Player movement (using WASD keys relative to view; press SPACE to dash)
  - "Catching" fish with a projectile (click to shoot): there is a reload period after 6 shots for the initial weapon
  - Added home base with upgrade screen (upgrades for player movement, weapons, and accessing later levels)
  - Implemented different score for different fish

## How to Build and Run
1. Clone the repository, move to the parent folder, and use the "beta" checkout tag.

   ```
   git clone https://github.com/sgreenvoss/White-Whale.git
   cd White-Whale
   git checkout beta
   ```

2. Open the project in Unity 6000.0.44f1
3. Go to "File" → "Build Profiles" → "Web":
   - If not already configured, set the platform to WebGL and ensure Scenes/SunlightZone/Sunlight Zone, Scenes/UnderwaterBase/Underwater Base, Scenes/UpgradeScene, and Scenes/TwilightZone/Twilight Zone are selected under "Scene List" in that specific order.
4. Click "Build and Run" to create the WebGL Player build.

## Requirements
- Unity version: 6000.0.44f1
- Build Target: WebGL
