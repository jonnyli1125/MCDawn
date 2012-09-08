/*
Copyright (c) 2012 by Gamemakergm
This work is licensed under the Attribution-NonCommercial-NoDerivs License. To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/ or send a letter to Creative Commons, 444 Castro Street, Suite 900, Mountain View, California, 94041, USA.
*/
using System;

//Not sure if it works...
namespace MCDawn
{
    public class MapViewer
    {
        public static void Meep(Level l)
        {
            /**
             * @str1 = Unused .-. I'll delete it later.
             * @str2 = Tileset
             * @str3 = Savename
             * @blocks = Level blocks
             */
            IsoCraft.height = l.height;
            IsoCraft.length = l.depth;
            IsoCraft.width = l.width;
            IsoCraft.Main("", "tileset.png", "test.png", l.blocks);
        }
    }
}