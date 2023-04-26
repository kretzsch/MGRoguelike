# Genre-Switching Game
![Unity](https://img.shields.io/badge/Unity-3D-green)
![FMOD](https://img.shields.io/badge/FMOD-audio-red)

## Description

This genre-switching game offers a unique and engaging gaming experience by combining elements of first-person shooter (FPS), platformer, topdown, and other game genres. The project was motivated by the desire to create a diverse gaming experience that challenges players and offers variety in gameplay. It includes a rich main menu, a loadout system, weapon and ammo purchasing, and interactive music using FMOD.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Credits](#credits)
- [Copyright and No License](#copyright-and-no-license)

## Features

- Genre-switching gameplay with FPS, platformer, topdown, and more.
- Main menu with tabs and panels for weapon and ammo purchasing.
- Loadout system for managing weapons and ammo.
- Interactive music using FMOD for seamless transitions and dynamic audio.

## Backlog

- Refactor the runtime loading code to use Addressables API instead of Resources API.
- Divide levels up into several scenes with Loadsceneasync
- Optimize projectile handling by using object pooling to reduce the overhead of instantiating and destroying projectiles during gameplay.
- Weapon stats display 
- Improve error handling
- Implement support for different projectile types, such as homing projectiles or projectiles that bounce off surfaces.
- Add support for weapon attachments, like scopes, silencers, or extended magazines, which would modify weapon behavior and visuals.

## Codebase Flowchart: Weapons from MainMenu to Playable Scene

1. **MainMenu scene**
   - Player selects weapons and ammo in the shop.
   - Selected weapons and ammo are stored in `LoadoutData.selectedWeaponsAndAmmo` (a dictionary) and `LoadoutData.remainingAmmo` (also a dictionary).
   - When the player starts the game, the playable scene is loaded.

2. **Playable scene**
   - **TopDownGameController** initializes the game.
      - Finds a reference to the `WeaponManager` in the scene.
      - Calls `WeaponManager.SetupWeapons` with `LoadoutData.selectedWeaponsAndAmmo` as the parameter.
   - **WeaponManager.SetupWeapons** creates instances of the selected weapons.
      - For each weapon in the `LoadoutData.selectedWeaponsAndAmmo`, loads the corresponding `WeaponData` (ScriptableObject) and weapon prefab.
      - Instantiates the weapon prefab and assigns the `WeaponData` to the `ProjectileWeapon` component.
      - Sets up ammo for each weapon and adds it to the `weapons` dictionary.
      - Sets the initial current weapon.

3. **InGameUISetup**
   - Sets up in-game UI elements for the selected weapons and ammo.
      - Adds weapon images and ammo count text elements to the UI.
      - Initializes ammo counts in `LoadoutData.remainingAmmo`.

4. **TopDownController2D**
   - Controls player movement, aiming, shooting, reloading, and weapon switching.
      - Listens for input actions and calls corresponding methods (e.g., `OnShoot`, `OnReload`, `OnSwitchWeapon`).
      - Interacts with the `WeaponManager` and the currently selected weapon.

5. **Weapon and ProjectileWeapon classes**
   - **Weapon**: Abstract base class for weapons, manages weapon data and ammo.
   - **ProjectileWeapon**: Inherits from Weapon, implements shooting logic by spawning projectiles.

6. **Projectile**
   - Represents a projectile fired by a weapon.
   - Handles collisions and applies damage to objects implementing the IDamageable interface.

## Installation

Clone the repository and open the project in Unity. Ensure that all required assets and plugins are installed, including FMOD. Follow the instructions in the `Readme` and `Setup` files for proper configuration of the project.

## Usage

Launch the game and navigate through the main menu to purchase weapons, ammo, and other items. The game features various genres and levels, allowing players to experience diverse gameplay styles. Complete levels to progress through the game and unlock new challenges.

![Main Menu Screenshot](assets/images/main-menu-screenshot.png)

## Credits
Third-party assets: 
- [Synty] Polygon Prototype pack https://syntystore.com/
- [More Mountains] Feel https://feel.moremountains.com/
- [Odin] Odin Inspector https://odininspector.com/
- [Fmod] Fmod https://fmod.com/

## Copyright and No License

Copyright © [2023] [Thomas Kretzschmar]. All rights reserved.

This project and its contents are not licensed for distribution, modification, or use by any third party. Unauthorized copying, distribution, or use is strictly prohibited. For inquiries regarding permissions, please contact the author.
