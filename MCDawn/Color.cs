using System;

namespace MCDawn
{
    public static class c
    {
        public const string black = "&0";
        public const string navy = "&1";
        public const string green = "&2";
        public const string teal = "&3";
        public const string maroon = "&4";
        public const string purple = "&5";
        public const string gold = "&6";
        public const string silver = "&7";
        public const string gray = "&8";
        public const string blue = "&9";
        public const string lime = "&a";
        public const string aqua = "&b";
        public const string red = "&c";
        public const string pink = "&d";
        public const string yellow = "&e";
        public const string white = "&f";

        public static string Parse(string str)
        {
            switch (str.ToLower())
            {
                case "black": return black;
                case "navy": return navy;
                case "green": return green;
                case "teal": return teal;
                case "maroon": return maroon;
                case "purple": return purple;
                case "gold": return gold;
                case "silver": return silver;
                case "gray": return gray;
                case "blue": return blue;
                case "lime": return lime;
                case "aqua": return aqua;
                case "red": return red;
                case "pink": return pink;
                case "yellow": return yellow;
                case "white": return white;
                default: return "";
            }
        }
        public static string Name(string str)
        {
            switch (str)
            {
                case black: return "black";
                case navy: return "navy";
                case green: return "green";
                case teal: return "teal";
                case maroon: return "maroon";
                case purple: return "purple";
                case gold: return "gold";
                case silver: return "silver";
                case gray: return "gray";
                case blue: return "blue";
                case lime: return "lime";
                case aqua: return "aqua";
                case red: return "red";
                case pink: return "pink";
                case yellow: return "yellow";
                case white: return "white";
                default: return "";
            }
        }
    }

    public static class IRCColor
    {
        public const string white = "\u000300";
        public const string black = "\u000301";
        public const string navy =  "\u000302";
        public const string green =  "\u000303";
        public const string red = "\u000304";
        public const string maroon = "\u000305";
        public const string purple = "\u000306";
        public const string gold = "\u000307";
        public const string yellow = "\u000308";
        public const string lime = "\u000309";
        public const string teal = "\u000310";
        public const string aqua = "\u000311";
        public const string blue = "\u000312";
        public const string pink = "\u000313";
        public const string gray = "\u000314";
        public const string silver = "\u000315";
        public const string reset = "\u000f";
        public const string bold = "\u0002";
        public const string italic = "\u0009";
        public const string strikethrough = "\u0013";
        public const string underline = "\u0015";
        public const string underline2 = "\u001f";
        public const string reverse = "\u0016";

        public static string IRCToMinecraftColor(string text)
        {
            text = text.Replace(white, c.white);
            text = text.Replace(black, c.black);
            text = text.Replace(navy, c.navy);
            text = text.Replace(green, c.green);
            text = text.Replace(red, c.red);
            text = text.Replace(maroon, c.maroon);
            text = text.Replace(purple, c.purple);
            text = text.Replace(gold, c.gold);
            text = text.Replace(yellow, c.yellow);
            text = text.Replace(lime, c.lime);
            text = text.Replace(teal, c.teal);
            text = text.Replace(aqua, c.aqua);
            text = text.Replace(blue, c.blue);
            text = text.Replace(pink, c.pink);
            text = text.Replace(gray, c.gray);
            text = text.Replace(silver, c.silver);
            text = text.Replace(reset, "&g");
            return text;
        }

        public static string MinecraftToIRCColor(string text)
        {
            text = text.Replace(c.white, white);
            text = text.Replace(c.black, black);
            text = text.Replace(c.navy, navy);
            text = text.Replace(c.green, green);
            text = text.Replace(c.red, red);
            text = text.Replace(c.maroon, maroon);
            text = text.Replace(c.purple, purple);
            text = text.Replace(c.gold, gold);
            text = text.Replace(c.yellow, yellow);
            text = text.Replace(c.lime, lime);
            text = text.Replace(c.teal, teal);
            text = text.Replace(c.aqua, aqua);
            text = text.Replace(c.blue, blue);
            text = text.Replace(c.pink, pink);
            text = text.Replace(c.gray, gray);
            text = text.Replace(c.silver, silver);
            text = text.Replace("&g", reset);
            return text;
        }
    }
}