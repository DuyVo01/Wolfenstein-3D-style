# Wolfenstein-3D-style
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
