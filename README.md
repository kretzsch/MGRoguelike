# Genre-Switching Game
![Unity](https://img.shields.io/badge/Unity-3D-green)
![FMOD](https://img.shields.io/badge/FMOD-audio-red)

## Description

Note: This Game is still in it's white boxing prototype phase; therefore a lot of code optimization is still not implemented to prevent the potential of painting myself into a corner. "Premature optimisation is the root of all evil". 

This genre-switching game offers a unique and engaging gaming experience by combining elements of first-person shooter (FPS), platformer, topdown, and other game genres. The project was motivated by the desire to create a diverse gaming experience that challenges players and offers variety in gameplay. It includes a rich main menu, a loadout system, weapon and ammo purchasing, and interactive music using FMOD.

## Table of Contents

- [Features](#features)
- [Screenshots](#screenshots)
- [Backlog](#backlog)
- [Installation](#installation)
- [Usage](#usage)
- [Credits](#credits)
- [Copyright and No License](#copyright-and-no-license)

## Features

- Genre-switching gameplay with FPS, platformer, topdown, and more.
- Main menu with tabs and panels for weapon and ammo purchasing.
- Loadout system for managing weapons and ammo with scriptableobjects.
- Interactive music using FMOD for seamless transitions and dynamic audio.
- Object pooling based on loadout through scriptableobjects.
- New unity Inputsystem with different action maps for each genre.
- Different weapon functions for each genre even with the same weapons
- Easily expandable due to decoupling of code (check backlog for future additions)

## Screenshots 
![Main Menu](Assets/Sprites/mainmenugif.gif)
![FPS](Assets/Sprites/fpsgif.gif)

## Backlog

-  ~~Object Pooling~~
-  ~~Dynamic main menu audio~~
- Create SFX variety through FMOD
- Dynamic footsteps with FMOD
- Make Main menu buttons reuseable and working with a single buybutton class
- Recoil animation and reuseable reload animation. 
- Platformer controller upgrade ->  use coyote timer; jump holding ; fall gravity scale; 2D physics material on player
- Refactor the runtime loading code to use Addressables API instead of Resources API.
- SOLID when rapid prototype phase is over
- Divide levels up into several scenes with Loadsceneasync
- Weapon stats display 
- Improve error handling
- Implement support for different projectile types, such as homing projectiles or projectiles that bounce off surfaces.
- Add support for weapon attachments, like scopes, silencers, or extended magazines, which would modify weapon behavior and visuals.
- Basic enemy AI 
- Usage of null propagation on Unity Objects is incorrect


## Installation

Clone the repository and open the project in Unity. Ensure that all required assets and plugins are installed, including FMOD. Follow the instructions in the `Readme` and `Setup` files for proper configuration of the project.

## Usage

Launch the game and navigate through the main menu to purchase weapons, ammo, and other items. The game features various genres and levels, allowing players to experience diverse gameplay styles. Complete levels to progress through the game and unlock new challenges.

## Credits
Third-party assets (not included in git repo): 
- [Synty] Polygon Prototype pack https://syntystore.com/
- [More Mountains] Feel https://feel.moremountains.com/
- [Odin] Odin Inspector https://odininspector.com/
- [Fmod] Fmod https://fmod.com/

## Copyright and No License

Copyright © [2023] [Thomas Kretzschmar]. All rights reserved.

This project and its contents are not licensed for distribution, modification, or use by any third party. Unauthorized copying, distribution, or use is strictly prohibited. For inquiries regarding permissions, please contact the author.
