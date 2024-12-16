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
-   End Condition: A game ends when the player exhausts all their stocks
-   Scoring: The player earns points by eliminating enemies

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

### Special Ability
-   The player has a special ability that grants **_Invincibility_** for one second upon activation.
-   While in **_Invincibility_**, movement speed is greatly increased, and all damage is ignored.

### Bomb
-   Using a bomb instantly removes all bullets and enemies on the field and converts them into points.

### High Scores
-   Keeps track of the highest score the player reached.

### Boss Enemy
-   Reaching specific score causes to spawn the boss ship with more health.

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

-   Shooting a fire when enemy's ship is in the scene throws a NullReferenceException but works fine.

### Requirements not completed

_If you did not complete a project requirement, notate that here_

