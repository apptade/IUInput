# What is this?

This is an additional input system for the Unity engine, which has different interactions with the input, which you just need to check or subscribe to their change.

> [!NOTE]
> This system uses only the new [Unity Input System (Package)](https://github.com/Unity-Technologies/InputSystem) for now.
> There are no plans to support the [Legacy system](https://docs.unity3d.com/6000.0/Documentation/Manual/InputLegacy.html) yet.

### Main differences:

- Predicate system - you can check incoming input changes and cancel them if necessary.
- Flexible and unified architecture, created to handle input from different devices, as well as to easily expand your interactions.

# Installation

> [!WARNING]
> Requires **Unity** vesrion > **or** = <ins>2021.3</ins>

### Install a UPM package from a Git URL

Follow the official [Unity instructions](https://docs.unity3d.com/Manual/upm-ui-giturl.html) for installing this package, using the following link

```
https://github.com/apptade/IUInput.git
```

> [!NOTE]
> All samples on how to work with this system are already included.

# Existing interactions

## Screen interactions

Supported input devices:
- Mouse and keyboard
- Everything related to touch (touchscreen, touchpad)
> If the input device you need is not here, don't worry, it might appear here very soon

### Tracking movement

![screen-movement-sample](https://github.com/user-attachments/assets/7c3ded15-5864-47a8-a246-8a2b0d5f1ee8)

- There is a prevention of erroneous movement, useful for touch screen/pad.

### Contact/Hold/MultiTap/SlowTap/Tap

![screen-contact-sample](https://github.com/user-attachments/assets/60b1c8ac-86f5-4552-9a2b-1cda56b4635e)

Unlike standard interactions here:
- You can find out where on the screen this interaction occurred.
- When the pointer is moved sufficiently, the interaction stops.
> It is also possible that other non-standard interactions will appear here.

### Pinch/Zoom

![screen-pinch-sample](https://github.com/user-attachments/assets/7601c6c3-8ae1-4a98-805e-cd52a98281e7)

- You can track all ten fingers, that is, as many as five pinch gestures.

> [!WARNING]
> But keep in mind that not all touch screens/pads support tracking of all ten fingers.

### Predicates

- Checking input on the **UI**, and blocking it if necessary.
> This list might be expanded very soon.

# Soon

- Even more interactions.
- Convenient addition/removal of input predicates.
- Simple subscription to track changes in any input information.

# Author

```
https://github.com/apptade
```

> Of course, everything that this system can do is not described here, so it is better for you to try it yourself several times than to read it many times.
