# Project SHMUP

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Hyunbin Park
-   Section: 01

## Game Design

-   Camera Orientation: Topdown View
-   Camera Movement: Fixed Camera
    -   Returns the player back to the boundaries if they move out of bounds
-   Player Health: Stocks
-   End Condition: A wave ends when the player:
    -   Exhausts all their stocks
    -   Eliminates all enemies
-   Scoring: The player earns points by eliminating enemies and finishing each wave. Faster the wave ends, more the points earned.

### Game Description

The player will face enemies divided into 50 waves. To achieve ultimate victory, the player must clear all the waves.

### Controls

-   Movement
    -   Up: W, ↑
    -   Down: S, ↓
    -   Left: A, ←
    -   Right: D, →
-   Fire: Z, Left Click
-   Ability: X
-   Bomb: C

## Your Additions

### Special Ability
-   The player has a special ability that grants **_Invincibility_** for one second upon activation.
-   While in **_Invincibility_**, movement speed is greatly increased, and all damage is ignored.

### Bomb
-   Using a bomb instantly removes all bullets on the field and converts them into points.

### High Scores
-   Keeps track of the highest score the player reached.

### Boss Enemy
-   Reaching specific score causes to spawn the boss ship with more health.

## Sources

-   Space ship sprite sheet - https://www.spriters-resource.com/snes/strikegunner/sheet/75354/

## Known Issues

-   Shooting a fire when enemy's ship is in the scene throws a NullReferenceException but works fine.
-   Ship shoots a fire again when a player releases the fire button.

### Requirements not completed

_If you did not complete a project requirement, notate that here_

