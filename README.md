Banshee.StatusUpdater
==========
Banshee Status updater is a [Banshee](http://banshee.fm/) extension that updates your 
[Telepathy](http://telepathy.freedesktop.org/) / [Empathy] (http://live.gnome.org/Empathy) status
message to show the song you're currently playing. 

There's a few existing extensions to do this, but I couldn't get them working, or they didn't handle
radio streams. This extension handles radio streams :).

Dependencies
============
 - [NTelepathy](https://github.com/Daniel15/NTelepathy)
   - [dbus-sharp](http://mono.github.com/dbus-sharp/) - This can be installed on Ubuntu by running `apt-get install libdbus1.0-cil`
   
Compiling
=========
1. `mkdir ~/.config/banshee-1/addins/` if it doesn't already exist
2. `xbuild Banshee.StatusUpdater.sln`
3. `cp Banshee.StatusUpdater/bin/Debug/* ~/.config/banshee-1/addins/`
4. Enable the extension in Banshee
    
Related Projects
================
 - [banshee-telepathy-extension](https://github.com/nloko/banshee-telepathy-extension) has a similar
   goal, but it is a lot more complicated than this extension, and aims to do a lot more. It 
   currently (26th November 2011) doesn't handle showing radio stations in the status message.

Licence
=======
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
