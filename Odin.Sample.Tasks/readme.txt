Intent is to be able to call it like Nant tasks.
Values are stored and available to all tasks.
The tasks are run in the order specified.

Example: 
tasks.exe REVERSE FLIPFLOP SAVE "input string"

REVERSE reverses the string to "gnirts tupni"
FLIPFLOP splits on the space and swaps; "tupni gnirts"
SAVE saves the file

tasks.exe --value "input string" REVERSE FLIPFLOP SAVE would have the same output.


