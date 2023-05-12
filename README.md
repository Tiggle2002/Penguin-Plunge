# Penguin-Plunge

![PenguinPlungeLogo](https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Assets/Art/UI/Interface/PenguinPlungeTextLarge.png)
Welcome to the repository for Penguin Plunge

Penguin Plunge is a Unity-based endless-runner game inspired by Jetpack Joyride, which was developed to improve my game design abilities and produce higher quality games.
I encourage you to read the <a href="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/PenguinPlungeGDD.pdf">Game Design Document</a> written to understand design decisions made in the game.

Penguin Plunge is an endless runner game with simple yet engaging gameplay mechanics. The player controls a penguin that continuously swims forward, and must navigate through various obstacles to achieve a high score. The game features three types of obstacles: eels, jellyfish, and sharks.

Eels:
Eels appear in a variety of layouts often in consecutively through random selection. These eel designs can be effortlessly modified via the Unity Inspector, facilitating level creation for non-programmers and offering a testing ground for rapid experimentation.

![EelInspector](https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Editor/EelDiagram.png)



Jellyfish:
Jellyfish occur in various rotations and sizes, each intended to give the player a fresh obstacle a test their reflexes. 
<table>
 <tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_004.jpg" alt="image description" height="50%"></td>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_005.jpg" alt="image description" height="50%"></td>
 </tr>
  <tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_029.jpg" alt="image description" height="50%"></td>
  <td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_022.jpg" alt="image description" height="50%"></td>
 </tr>
 </table>
 
 Although jellyfish positioning in either the top, middle, or bottom position can be random, custom layouts can be designed in editor to provide a more tailored experience, blending procedural generation with deliberate design choices. 
![JellyfishInspector](https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Editor/JellyfishLayoutMakerInspector.png)



Sharks
The frequency of shark appearances increases with the length of play, adding a layer of challenge to the player's positioning and reflexes. When combined with jellyfish, they present a more challenging experience that tests the player's skills to the limit.
<table>
 <tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_013.jpg" alt="image description" height="50%"></td>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_024.jpg" alt="image description" height="50%"></td>
 </tr>
  <tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_025.jpg" alt="image description" height="50%"></td>
  <td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/Screenshot_028.jpg" alt="image description" height="50%"></td>
 </tr>
 </table>
 
 
 Architecture:
 I used a Finite State Machine, a pattern I'm strongly familiar with, to manage player behaviour. It makes for clear and efficient coding by representating the range of possible player behaviours, and allows for easy addition of more states should I ever want to increase the scale of the game.
 <table>
<tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/FSMStateManagement.png" alt="image description" height="50%"></td>
  <td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/FSMStateConstruction.png" alt="image description" height="50%"></td>
 </tr>
 </table>
To manage large scale events, I employed the event bus pattern to faciliate the communication to classes that rely on that information without tight coupling. 
 <table>
<tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/EventBus.png" alt="image description" height="50%"></td>
  <td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/EventReception.png" alt="image description" height="50%"></td>
 </tr>
 </table>
To manage statistic collection, I used the singleton pattern which providing a global access point with getters to information such as the current score or the highest score achieved. It ensuring only one instance has the information, preventing any contradictions.

To efficiently handle the abundance of jellyfish and shark obstacles in the game, I implemented object pooling, enabling optimized object reuse and better performance.
 <table>
<tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/ObjectPool.png" alt="image description" height="50%"></td>
  <td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/ObjectPoolExtensionMethod.png" alt="image description" height="50%"></td>
 </tr>
 </table>

I employed a diverse range of extension methods in my code to maintain clean and organized classes, simplifying the addition of similar features and promoting code reusability. This approach made it easier to expand and modify the game's functionality while keeping the codebase manageable and maintainable. Example Extension Methods: 
 <table>
<tr>
<td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/CoroutineExtensionMethods.png" alt="image description" height="50%"></td>
  <td><img src="https://github.com/Tiggle2002/Penguin-Plunge/blob/main/Screenshots/CodeSamples/ChangeValueOverTimeCoroutine.png" alt="image description" height="50%"></td>
 </tr>
 </table>

