Unity Genre Switcher Game

This Unity project is a genre-switching game where players progress through different genres, each represented by a level with its own unique style and enemies. When all enemies in a level are defeated, the game transitions to the next level using a custom animation effect and changes the background music. The project contains three main scripts: LevelManager, LevelAudioController, and StoreChildren. This README provides an overview of the project and instructions on how to set it up.

Features

Progress through multiple genres, each represented by a level with its own unique style and enemies.
Defeat all enemies in a level to advance to the next genre.
Transition between genres using a custom animation effect.
Background music changes to match the genre of the current level.
Setup

Download or clone the repository.
Open the project in Unity.
Add the main scene to the build and build it. 
Run the game and enjoy the genre-switching experience!

Scripts Overview
LevelManager: Handles switching between genres, activating and deactivating level objects, and coordinating the transition animations and audio changes.
LevelAudioController: Manages the background music, switching between different tracks depending on the current genre.
StoreChildren: Monitors enemy counts for each level and raises an event when all enemies in a level have been defeated.
