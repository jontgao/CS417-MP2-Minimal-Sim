# VR Incremental Resource Game

I used XR Device Simulator for now.
Resource B starts locked and can be unlocked by spending a large amount of Resource A, after which it begins generating and its buttons become active. Power-ups can be purchased via VR buttons to multiply generator output, and resource amounts are always displayed in the player’s view. The system is designed to be modular and easily expandable for adding new resources, generators, and upgrades.

All the rates can be set in `ResourceManager` Game Object.

## Code Reference

- **`RampingResources.cs`** – Core resource manager handling Resource A and B, generator rates, power-up multipliers, and Resource B unlock logic.  
- **`PowerUpButton.cs`** – VR button script that applies power-up multipliers when selected.  
- **Generator Buttons** – Call `AddGeneratorA()` / `AddGeneratorB()` on the resource manager to increase base rates.  
- **Unlock Button** – Calls `TryUnlockB()` on `RampingResources` to unlock Resource B.  

> For VR interaction, check the **XR Simple Interactable** components on buttons and cubes; they connect physical grabs to the corresponding functions in the scripts.
