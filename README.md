# windows_which
simple implementations of the which command for windows. the 'simplest' folder contains implementations that assume the only parameter is the exact file name including extension. the 'withargs' folder has some implementations that allow customizing the run mode, as well as matching without the file extension (ie. 'notepad' would match 'notepad.bat', 'notepad.exe', 'notepad.com', etc. in the order specified by the 'PATHEXT' environment variable, or a default order (.bat, .exe, .com, .cmd... etc.)).

parameters:
- -p shows directories during search (only some implementations)

languages currently implemented:
- c#
- python
- nodejs
- php
- perl
- f#





