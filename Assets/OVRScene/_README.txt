/*
------------------------------
  Dialogue System for Unity  
  Oculus Rift Example Scene
         2015-08-12
------------------------------

Instructions:

1. Use Unity 5.1.2+.

2. Follow the instructions on the Unity 5.1 VR Getting Started thread:
   http://forum.unity3d.com/threads/unity-5-1-vr-getting-started.332316/
   This includes downloading and installing the Oculus Runtime for Windows.
   
3. Download Oculus Utilities for Unity 5:
   http://developer.oculus.com/downloads/

Then play the example scene. The UI is spread across two world space canvases:

- The dialogue panel is in actual world space next to Private Hart. 

- The alert panel is a child of the CenterEyeAnchor so it always appears in 
  front of the player. (The example scene uses OVRPlayerController.)
  
The scene uses BasicLookInputModule. To select a dialogue option, look at it.
Then click the mouse button ("Fire1").


In your own scenes, note the following:

- You cannot use Unity GUI. Use Unity UI.

- To use look input like the example scene, add the BasicLookInputModule script to
  to your EventSystem.
*/