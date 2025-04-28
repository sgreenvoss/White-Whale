# White Whale
 
Created for CS 410 Game Programming at the University of Oregon in Spring 2025.

Team members: Stella Greenvoss, Dylan Tiffany, Clio Tsao, Julia Wheless


## Proof of Concept (PoC)
This repository currently contains a Proof of Concept (PoC) for White Whale to de-risk core game mechanics.

**Core Mechanics**:  
  - Player movement (using WASD keys relative to view; press SPACE to dash)
  - "Catching" fish with a projectile (click to shoot)

## How to Build and Run
1. Clone the repository, move to the parent folder, and use the "poc" checkout tag.

   ```
   git clone https://github.com/sgreenvoss/White-Whale.git
   cd White-Whale
   git checkout poc
   ```

2. Open the project in Unity 6000.0.44f1
3. Go to "File" → "Build Profiles" → "Web":
   - If not already configured, set the platform to WebGL and ensure Scenes/Proof of Concept is selected under "Scene List".
4. Click "Build and Run" to create the WebGL Player build.

## Requirements
- Unity version: 6000.0.44f1
- Build Target: WebGL
