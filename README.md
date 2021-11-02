# World of Warcraft hacking notes

This repository contains the notes and remnants of World of Warcraft bots for
fishing and player-versus-player combat. The primary attack behind this code
was conducted by putting a signature into a long string from within the Lua
scripting environment that Blizzard provides for WoW addons.

Once the large buffer is found from an external application, an internal add-on
written in Lua can expose the state of the game to the external application,
which uses that information to inject key and mouse events into the WoW
process.
