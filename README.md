# Dance Beat

## About the Project

Dance Beat is an interactive rhythm and dance game that uses your computer's camera to detect body movements, immersing you directly into the game.

Unlike traditional games played with keyboards, this project allows the player to use real physical movements as a controller. The camera feed is processed in real-time and mirrored onto a 3D character on the screen, requiring you to dance along with the music.

## Key Features

**No Physical Controllers:** You control the game using only your body. No keyboard or mouse is required.

**Body Movement Tracking:** The system detects the movements of your arms and legs and mirrors them to the character.

**MediaPipe Integration:** The project is built using the MediaPipe plugin within Unity for advanced motion detection.

**Rhythm-Based Gameplay:** You score points by following dance figures synchronized with the music.

**Personal Calibration (T-Pose):** Every player has a different body structure. A T-Pose calibration at the start aligns the character with your specific measurements.

**Instant Feedback:** The accuracy of your movements is calculated in real-time, providing immediate feedback on your performance.

## How to Play

**Preparation:** Stand in an area where the camera can see you clearly.

**Calibration:** When the game starts, stand with your arms wide open (T-Pose). This ensures the character moves in perfect sync with you.

**Dance:** Follow the reference moves that appear in the bottom left corner of the screen.

**Score Points:** Try to achieve the highest score by performing the moves correctly and in rhythm with the music.

## How It Works

The project combines the Unity game engine with MediaPipe technology:

**Image Processing:** The MediaPipe plugin analyzes the feed from the webcam and detects key joint points in the human skeletal structure.
**Game Engine (Unity):** These coordinate data are applied to the 3D character's bone structure. This way, when you move your arm or leg in real life, the in-game character mimics the movement.

## File Note

This repository serves as a portfolio showcase demonstrating the project's core logic and code structure. Some local dependencies and large project files may not be included in this repo. Therefore, the project may not be directly executable.

<img width="393" height="657" alt="image" src="https://github.com/user-attachments/assets/e57bc403-c6e3-4152-8e85-f67edb900ddb" />
<img width="396" height="652" alt="image" src="https://github.com/user-attachments/assets/7eba4fef-caeb-42b3-b2e6-4fee96908c1b" />
