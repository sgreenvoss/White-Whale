# White Whale
## Venture into the depths and face the White Whale...

Created for CS 410 Game Programming at the University of Oregon in Spring 2025.

Team members: Stella Greenvoss, Dylan Tiffany, Clio Tsao, Julia Wheless

## Final Build
This repository contains a final build for the game White Whale.

**Gameplay**:  
  - Player movement (using WASD keys to move relative to view; press SPACE to dash; press LEFT SHIFT to boost upwards)
  - "Catching" fish with a projectile (click to shoot): there is a reload period after 6 shots for the starter pistol
  - Different fish have different swim patterns and health... but the harder-to-catch ones may be worth more points!

**Environments**:  
  - Home base: Click the fridge to purchase upgrades with your Sand Dollars and the computer for tutorial information. Dive to various levels from the base!
  - Sunlight Zone: The fish here are harmless and easy to catch... although certain ones may attack in self defense if provoked.
  - Twilight Zone: At this depth, it's hard to see normally. What is lurking in the depths?

## How to Build and Run
1. Clone the repository, move to the parent folder, and use the "final" checkout tag.

   ```
   git clone https://github.com/sgreenvoss/White-Whale.git
   cd White-Whale
   git checkout final
   ```

2. Open the project in Unity 6000.0.44f1
3. Go to "File" → "Build Profiles" → "Web":
   - If not already configured, set the platform to WebGL and ensure Scenes/UnderwaterBase/Underwater Base, Scenes/SunlightZone/Sunlight Zone, Scenes/UpgradeScene, and Scenes/TwilightZone/Twilight Zone are selected under "Scene List" in that specific order.
4. Click "Build and Run" to create the WebGL Player build.

## Requirements
- Unity version: 6000.0.44f1
- Build Target: WebGL

## Credits

This game uses the asset pack Thalassophobia: Stylized Oceans by Distant Lands.

Gun sound effects are from "Kuantum" Guerreau @ Komposite Sound.

Home Base ambient music is from Aquatic Soundscapes - Adventure Game Music by DHSFX (https://soundcloud.com/dhsfx/sets/aquatic-soundscapes-adventure-game-music-demo).

Sunlight Zone, Twilight Zone, and Upgrade Menu BGM are from Fun Happy Beats Vol. 1 by Michael Barker (juanjo_sound).
