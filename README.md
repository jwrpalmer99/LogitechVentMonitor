# LogitechVentMonitor
controls logitech keyboard LED effects and can monitor Ventrilo and send who is talking to Arx/Keyboard (G910)

This program can generate lighting effectgs on a G910 keyboard in response to user key presses.. the lighting effects have several user configurable options which allow for a considerable array of different effects.

This program has only been tested on a UK keyboard - it may well require changes on other layouts with different keys/scancodes

The program hooks the keyboard using windows API functions - it is possible that may cause problems in anti-cheat or anti-virus systems.

In order to capture the ventrilo window and find out who is speaking Ventrilo must not be minimised. If the option "Use ARX" is set to false AND the option Ventrilo Keyboard LED is set to "Off" then the program will not scan for Ventrilo processes/windows.

Settings can be saved to a profile file (.prf) and reloaded for later use, the default setting can be changed so that the program will load with the last loaded/saved profile.

If you want to start the program minimzed, create a shortcut and set the "Run" drop down to "Minimized"
