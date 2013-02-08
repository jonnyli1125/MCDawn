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
        public const string color = "\x03";
        public const string white = color + "0";
        public const string black = color + "1";
        public const string navy = color + "2";
        public const string green = color + "3";
        public const string red = color + "4";
        public const string maroon = color + "5";
        public const string purple = color + "6";
        public const string gold = color + "7";
        public const string yellow = color + "8";
        public const string lime = color + "9";
        public const string teal = color + "10";
        public const string aqua = color + "11";
        public const string blue = color + "12";
        public const string pink = color + "13";
        public const string gray = color + "14";
        public const string silver = color + "15";
        public const string reset = "\x0f";
        public const string bold = "\x02";
        public const string italic = "\x09";
        public const string strikethrough = "\x013";
        public const string underline = "\x015";
        public const string underline2 = "\x01f";
        public const string reverse = "\x016";

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
            text = text.Replace(color, "&g");
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
            text = text.Replace("&g", color);
            return text;
        }
    }
}