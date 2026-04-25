# StickMan Hack (MelonLoader)

Mod/Cheat menu for **Stickman Killing Zombie** using MelonLoader + ImGui

## Info

I made this because I was bored and my friend played this game lol

The UI is completely vibecoded because fucking I hate UI/UX dev

## Building

1. Install .NET SDK (the project targets `net472`).
2. Put required game/MelonLoader DLLs in the `Libs/` folder. You need to get the ImGui.NET dll
3. Build:

```bash
dotnet build
```

Your output DLL will be:

`bin/Debug/net472/StickManHack.dll`

## How to use in game

1. Install MelonLoader for the game.
2. Copy `StickManHack.dll` into the game's `Mods/` folder.
3. Launch the game.
4. Press `Insert` to open/close the menu.

## Current cheats

- Infinite Health
- Max Armor
- Infinite Money
- Freeze Timer
- Objective helper and utility buttons (I have no clue what this is for Cursor randomly added this)

## Credits
- [ImGui.NET](https://github.com/ImGuiNET/ImGui.NET)
- [Stickman Killing Zombie](https://store.steampowered.com/app/2792610/Stickman_Killing_Zombie/)