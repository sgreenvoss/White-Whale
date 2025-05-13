# White Whale
 
Created for CS 410 Game Programming at the University of Oregon in Spring 2025.

Team members: Stella Greenvoss, Dylan Tiffany, Clio Tsao, Julia Wheless


## Alpha Build
This repository currently contains an alpha build for White Whale with core game mechanics and one playable stage/environment.

**Core Mechanics**:  
  - Player movement (using WASD keys relative to view; press SPACE to dash)
  - "Catching" fish with a projectile (click to shoot): there is a reload period after 6 shots
  - Added home base with upgrade screen (upgrade player speed and weapons)
  - Implemented different score for different fish

## How to Build and Run
1. Clone the repository, move to the parent folder, and use the "alpha" checkout tag.

   ```
   git clone https://github.com/sgreenvoss/White-Whale.git
   cd White-Whale
   git checkout alpha
   ```

2. Open the project in Unity 6000.0.44f1
3. Go to "File" → "Build Profiles" → "Web":
   - If not already configured, set the platform to WebGL and ensure Scenes/Sunlight Zone, Scenes/Underwater Base, and Scenes/UpgradeScene are selected under "Scene List" in that specific order.
4. Click "Build and Run" to create the WebGL Player build.

## Requirements
- Unity version: 6000.0.44f1
- Build Target: WebGL
