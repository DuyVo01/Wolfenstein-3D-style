# Wolfenstein-3D-style
Changelogs 01/03/2023
  - Added additional enemy AI detection, the AI will notice bullet when hit by player and go to player's position.
  - Added Death state, enemy will die when its health reaches 0.
  - Added humanoid model to enemy.
  - Added Animations to all states.
  - Enemy model can rotate to its target position regardless of direction.
  - Added humanoid model to player. Player now has its hands attached to gun instead of just having gun before the camera.
  - Player model can rotate according to camera rotation regardless of direction.

Changelogs 29/12/2022
  - Improved enemy AI behaviour, added more states to enemy state machine for different behaviour such as: patrolling, searching, shooting, stationary.
  - Patrolling: Enemy will move in a fixed set of points, this can be customized.
  - Searching: Enemy will search for player if not detecing player anymore, then return to patrol state after a fixed duration.
  - Shooting; Enemy will shoot at player if detecting. At the momment, this is the only state to have animation.
  - Stationary: Eneny will stand at one point while patroling before moving to next point.

Changelogs 26/12/2022
  - Added basic enemy AI behaviour, managed using state machine pattern.
  - Current enemy behaviour includes:
    + Being able to detect player within certain radius ahead.
    + Being able to detect player within certain range behind.
    + Shooting bullet at player when detecting, shooting has limited duration and will delay between when duration ended.
    + Enemy bullets have same effect as player bullets such as spread, trail, collision detection.

Changelogs 22/12/2022
  - Improved character rotation, now the gun will always point at center of the screen.
  - Improved recoil visual feedback, recoil becomes smoother.
  - Improved bullet high speed detection, now bullet will hit any object regardless of its speed.
  - Added bullet trail, indicating where the bullet is heading.
  - Added bullet hit impact, when making collision with objects, there will be an impact effect feedback.
  - Added gun model, animation and procedural recoil animation, improved the visual feedback when shooting.

 Changelogs 19/12/2022
  - Added basic controls and movement, ground detection as well aas stairs and slope terrian.
  - Added basic gun control, shooting and reloading. Shooting have recoil effect for better visual feedback.
  - Optimized bullet management using object pooling. Bullets when shooted also randomly spread for a small degree to the aiming reticle.
