# Project SHMUP

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Hyunbin Park
-   Section: 01

## Game Design

-   Camera Orientation: Topdown View
-   Camera Movement: Fixed Camera
    -   Returns the player back to the boundaries if they move out of bounds
-   Player Health: 3 Stocks
    -   Player loses a stock upon its collision to enemy's ship or bullet
    -   Remaining stock is indicated on top right corner
-   Player Projectile: Player fires projectiles by pressing fire button
    -   There's 0.2 second of delay between shoots
-   Bomb: Instantly removes all bullets and enemies on the field and converts them into points.
    -   Player starts a game with 2 bombs
-   Special Ability: The player has a special ability that grants **_Invincibility_** for one second upon activation.
    -   Special ability is one time use.
    -   While in **_Invincibility_**, movement speed is greatly increased, and all damage is ignored.
-   Scoring: The player earns points by eliminating enemies
-   Enemy ship
    -   Spawns every 2 second
    -   Fires a bullet every second and each bullet deals one stock amount of damage
    -   Has 1 HP which is equivalent to the damage of single player's bullet
    -   Gives 50 scores upon its destruction
-   Boss ship
    -   Spawns when the player earns 1500 scores
    -   Fires two homing missiles every three seconds and they seek for player ship
    -   Has 50 HP
    -   Gives 300 scores upon its destruction
-   End Condition: A game ends when the player exhausts all their stocks

### Game Description

The player will face endless enemies. The player must earn the most points.

### Controls

-   Movement
    -   Up: W, ↑
    -   Down: S, ↓
    -   Left: A, ←
    -   Right: D, →
-   Fire: Z, Left Click
-   Ability: X, Right Click
-   Bomb: C, Space

## Your Additions

### Vertical Resolution
-   Vertical 3:4 aspect ratio makes a game look more professional.

### Special Ability
-   The player has a special ability that grants **_Invincibility_** for one second upon activation.

### Bomb
-   Using a bomb instantly removes all bullets and enemies on the field and converts them into points.

### High Scores
-   Keeps track of the highest score the player reached.

### Boss Enemy
-   Reaching specific score causes to spawn the boss ship with more health.

### Retry
-   Player can retry after the game overs.

### Background Music
-   Exciting music adds a zest to the game.
 
## Sources

-   Player ship and bullet - Void - Main Ship
    -   https://foozlecc.itch.io/void-main-ship
-   Enemy ship and bullet - Void - Fleet Pack 1 - Kla'ed
    -   https://foozlecc.itch.io/void-fleet-pack-1
-   Space background
    -   https://opengameart.org/content/space-background-1
-   Heart sprite - Heart 16*16 by NicoleMarieProductions
    -   https://opengameart.org/content/heart-1616
-   Bomb sprite
    -   https://opengameart.org/content/16-bit-animated-bomb
-    Background music - Lunar Harvest by FoxSynergy
    -   https://opengameart.org/content/lunar-harvest

## Known Issues

-   NullReferenceException is thrown when the enemy ship spawns but the game itself works fine.
-   NullReferenceException is thrown when the player shoots but the game itself works fine.
-   NullReferenceException is thrown when the player shoots while the enemy ship exists in the scene but the game itself works fine.

### Requirements not completed

-   Player's steering projectile is not made.