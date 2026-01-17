# Who Let the Dog Out?

A small Unity game jam prototype where a dog tries to cross a busy road while dodging traffic. Built to explore fast iteration on player movement, obstacle pacing, and animation-driven feedback.

**Team & context**: This was a three-person game jam project built in our game design class during our final semester of college.

**Published build**: https://peanut-and-butter.itch.io/who-let-the-dog-out

## Project Highlights
- **Immediate, readable controls**: Movement is mapped to WASD/arrow keys for quick pick-up-and-play sessions.
- **Traffic obstacle loop**: Vehicles continuously wrap at screen bounds to keep pressure on the player.
- **Animation feedback**: Movement and crash triggers drive the dog’s animations for clear player feedback.

## Gameplay Snapshot
- **Goal**: Navigate the dog upward/downward across lanes without colliding with vehicles.
- **Failure state**: Colliding with traffic stops vehicle movement and triggers a crash animation.

## Tech Stack
- **Engine**: Unity 6
- **Language**: C#
- **Input**: Unity Input System + keyboard fallbacks
- **Rendering**: Universal Render Pipeline (URP)

## Controls
- **Move Up**: `W` or `↑`
- **Move Down**: `S` or `↓`

## How to Run
**Play the published build**: https://peanut-and-butter.itch.io/who-let-the-dog-out

**Or run locally in Unity**:
1. Open the project in **Unity 6 (6000.0.56f1)** or newer.
2. Load `Assets/Scenes/Map.unity`.
3. Press **Play** in the editor.

## Notable Systems (Code)
- **Player movement & animation triggers**: `BasicMovement`, `Movement2`
- **Obstacle looping & collision stop**: `CarController`
- **Crash detection**: `Collider`, `ColliderTrigger`

## Project Goals
- Rapidly prototype a readable, arcade-style crossing experience.
- Validate a minimal movement + obstacle loop with clear visual feedback.

## Future Improvements
- Add a death counter so players can track attempts.
- Add a timer for speed runs.

---

If you have any inquiries, feel free to reach out.
