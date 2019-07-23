using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public sealed class DUIPens
    {
        private static DUIPen aliceblue = new DUIPen(Color.AliceBlue);
        private static DUIPen antiquewhite = new DUIPen(Color.AntiqueWhite);
        private static DUIPen aqua = new DUIPen(Color.Aqua);
        private static DUIPen aquamarine = new DUIPen(Color.Aquamarine);
        private static DUIPen azure = new DUIPen(Color.Azure);
        private static DUIPen beige = new DUIPen(Color.Beige);
        private static DUIPen bisque = new DUIPen(Color.Bisque);
        private static DUIPen black = new DUIPen(Color.Black);
        private static DUIPen blanchedalmond = new DUIPen(Color.BlanchedAlmond);
        private static DUIPen blue = new DUIPen(Color.Blue);
        private static DUIPen blueviolet = new DUIPen(Color.BlueViolet);
        private static DUIPen brown = new DUIPen(Color.Brown);
        private static DUIPen burlywood = new DUIPen(Color.BurlyWood);
        private static DUIPen cadetblue = new DUIPen(Color.CadetBlue);
        private static DUIPen chartreuse = new DUIPen(Color.Chartreuse);
        private static DUIPen chocolate = new DUIPen(Color.Chocolate);
        private static DUIPen coral = new DUIPen(Color.Coral);
        private static DUIPen cornflowerblue = new DUIPen(Color.CornflowerBlue);
        private static DUIPen cornsilk = new DUIPen(Color.Cornsilk);
        private static DUIPen crimson = new DUIPen(Color.Crimson);
        private static DUIPen cyan = new DUIPen(Color.Cyan);
        private static DUIPen darkblue = new DUIPen(Color.DarkBlue);
        private static DUIPen darkcyan = new DUIPen(Color.DarkCyan);
        private static DUIPen darkgoldenrod = new DUIPen(Color.DarkGoldenrod);
        private static DUIPen darkgray = new DUIPen(Color.DarkGray);
        private static DUIPen darkgreen = new DUIPen(Color.DarkGreen);
        private static DUIPen darkkhaki = new DUIPen(Color.DarkKhaki);
        private static DUIPen darkmagenta = new DUIPen(Color.DarkMagenta);
        private static DUIPen darkolivegreen = new DUIPen(Color.DarkOliveGreen);
        private static DUIPen darkorange = new DUIPen(Color.DarkOrange);
        private static DUIPen darkorchid = new DUIPen(Color.DarkOrchid);
        private static DUIPen darkred = new DUIPen(Color.DarkRed);
        private static DUIPen darksalmon = new DUIPen(Color.DarkSalmon);
        private static DUIPen darkseagreen = new DUIPen(Color.DarkSeaGreen);
        private static DUIPen darkslateblue = new DUIPen(Color.DarkSlateBlue);
        private static DUIPen darkslategray = new DUIPen(Color.DarkSlateGray);
        private static DUIPen darkturquoise = new DUIPen(Color.DarkTurquoise);
        private static DUIPen darkviolet = new DUIPen(Color.DarkViolet);
        private static DUIPen deeppink = new DUIPen(Color.DeepPink);
        private static DUIPen deepskyblue = new DUIPen(Color.DeepSkyBlue);
        private static DUIPen dimgray = new DUIPen(Color.DimGray);
        private static DUIPen dodgerblue = new DUIPen(Color.DodgerBlue);
        private static DUIPen firebrick = new DUIPen(Color.Firebrick);
        private static DUIPen floralwhite = new DUIPen(Color.FloralWhite);
        private static DUIPen forestgreen = new DUIPen(Color.ForestGreen);
        private static DUIPen fuchsia = new DUIPen(Color.Fuchsia);
        private static DUIPen gainsboro = new DUIPen(Color.Gainsboro);
        private static DUIPen ghostwhite = new DUIPen(Color.GhostWhite);
        private static DUIPen gold = new DUIPen(Color.Gold);
        private static DUIPen goldenrod = new DUIPen(Color.Goldenrod);
        private static DUIPen gray = new DUIPen(Color.Gray);
        private static DUIPen green = new DUIPen(Color.Green);
        private static DUIPen greenyellow = new DUIPen(Color.GreenYellow);
        private static DUIPen honeydew = new DUIPen(Color.Honeydew);
        private static DUIPen hotpink = new DUIPen(Color.HotPink);
        private static DUIPen indianred = new DUIPen(Color.IndianRed);
        private static DUIPen indigo = new DUIPen(Color.Indigo);
        private static DUIPen ivory = new DUIPen(Color.Ivory);
        private static DUIPen khaki = new DUIPen(Color.Khaki);
        private static DUIPen lavender = new DUIPen(Color.Lavender);
        private static DUIPen lavenderblush = new DUIPen(Color.LavenderBlush);
        private static DUIPen lawngreen = new DUIPen(Color.LawnGreen);
        private static DUIPen lemonchiffon = new DUIPen(Color.LemonChiffon);
        private static DUIPen lightblue = new DUIPen(Color.LightBlue);
        private static DUIPen lightcoral = new DUIPen(Color.LightCoral);
        private static DUIPen lightcyan = new DUIPen(Color.LightCyan);
        private static DUIPen lightgoldenrodyellow = new DUIPen(Color.LightGoldenrodYellow);
        private static DUIPen lightgray = new DUIPen(Color.LightGray);
        private static DUIPen lightgreen = new DUIPen(Color.LightGreen);
        private static DUIPen lightpink = new DUIPen(Color.LightPink);
        private static DUIPen lightsalmon = new DUIPen(Color.LightSalmon);
        private static DUIPen lightseagreen = new DUIPen(Color.LightSeaGreen);
        private static DUIPen lightskyblue = new DUIPen(Color.LightSkyBlue);
        private static DUIPen lightslategray = new DUIPen(Color.LightSlateGray);
        private static DUIPen lightsteelblue = new DUIPen(Color.LightSteelBlue);
        private static DUIPen lightyellow = new DUIPen(Color.LightYellow);
        private static DUIPen lime = new DUIPen(Color.Lime);
        private static DUIPen limegreen = new DUIPen(Color.LimeGreen);
        private static DUIPen linen = new DUIPen(Color.Linen);
        private static DUIPen magenta = new DUIPen(Color.Magenta);
        private static DUIPen maroon = new DUIPen(Color.Maroon);
        private static DUIPen mediumaquamarine = new DUIPen(Color.MediumAquamarine);
        private static DUIPen mediumblue = new DUIPen(Color.MediumBlue);
        private static DUIPen mediumorchid = new DUIPen(Color.MediumOrchid);
        private static DUIPen mediumpurple = new DUIPen(Color.MediumPurple);
        private static DUIPen mediumseagreen = new DUIPen(Color.MediumSeaGreen);
        private static DUIPen mediumslateblue = new DUIPen(Color.MediumSlateBlue);
        private static DUIPen mediumspringgreen = new DUIPen(Color.MediumSpringGreen);
        private static DUIPen mediumturquoise = new DUIPen(Color.MediumTurquoise);
        private static DUIPen mediumvioletred = new DUIPen(Color.MediumVioletRed);
        private static DUIPen midnightblue = new DUIPen(Color.MidnightBlue);
        private static DUIPen mintcream = new DUIPen(Color.MintCream);
        private static DUIPen mistyrose = new DUIPen(Color.MistyRose);
        private static DUIPen moccasin = new DUIPen(Color.Moccasin);
        private static DUIPen navajowhite = new DUIPen(Color.NavajoWhite);
        private static DUIPen navy = new DUIPen(Color.Navy);
        private static DUIPen oldlace = new DUIPen(Color.OldLace);
        private static DUIPen olive = new DUIPen(Color.Olive);
        private static DUIPen olivedrab = new DUIPen(Color.OliveDrab);
        private static DUIPen orange = new DUIPen(Color.Orange);
        private static DUIPen orangered = new DUIPen(Color.OrangeRed);
        private static DUIPen orchid = new DUIPen(Color.Orchid);
        private static DUIPen palegoldenrod = new DUIPen(Color.PaleGoldenrod);
        private static DUIPen palegreen = new DUIPen(Color.PaleGreen);
        private static DUIPen paleturquoise = new DUIPen(Color.PaleTurquoise);
        private static DUIPen palevioletred = new DUIPen(Color.PaleVioletRed);
        private static DUIPen papayawhip = new DUIPen(Color.PapayaWhip);
        private static DUIPen peachpuff = new DUIPen(Color.PeachPuff);
        private static DUIPen peru = new DUIPen(Color.Peru);
        private static DUIPen pink = new DUIPen(Color.Pink);
        private static DUIPen plum = new DUIPen(Color.Plum);
        private static DUIPen powderblue = new DUIPen(Color.PowderBlue);
        private static DUIPen purple = new DUIPen(Color.Purple);
        private static DUIPen red = new DUIPen(Color.Red);
        private static DUIPen rosybrown = new DUIPen(Color.RosyBrown);
        private static DUIPen royalblue = new DUIPen(Color.RoyalBlue);
        private static DUIPen saddlebrown = new DUIPen(Color.SaddleBrown);
        private static DUIPen salmon = new DUIPen(Color.Salmon);
        private static DUIPen sandybrown = new DUIPen(Color.SandyBrown);
        private static DUIPen seagreen = new DUIPen(Color.SeaGreen);
        private static DUIPen seashell = new DUIPen(Color.SeaShell);
        private static DUIPen sienna = new DUIPen(Color.Sienna);
        private static DUIPen silver = new DUIPen(Color.Silver);
        private static DUIPen skyblue = new DUIPen(Color.SkyBlue);
        private static DUIPen slateblue = new DUIPen(Color.SlateBlue);
        private static DUIPen slategray = new DUIPen(Color.SlateGray);
        private static DUIPen snow = new DUIPen(Color.Snow);
        private static DUIPen springgreen = new DUIPen(Color.SpringGreen);
        private static DUIPen steelblue = new DUIPen(Color.SteelBlue);
        private static DUIPen tan = new DUIPen(Color.Tan);
        private static DUIPen teal = new DUIPen(Color.Teal);
        private static DUIPen thistle = new DUIPen(Color.Thistle);
        private static DUIPen tomato = new DUIPen(Color.Tomato);
        private static DUIPen transparent = new DUIPen(Color.Transparent);
        private static DUIPen turquoise = new DUIPen(Color.Turquoise);
        private static DUIPen violet = new DUIPen(Color.Violet);
        private static DUIPen wheat = new DUIPen(Color.Wheat);
        private static DUIPen white = new DUIPen(Color.White);
        private static DUIPen whitesmoke = new DUIPen(Color.WhiteSmoke);
        private static DUIPen yellow = new DUIPen(Color.Yellow);
        private static DUIPen yellowgreen = new DUIPen(Color.YellowGreen);

        public static DUIPen AliceBlue { get { return aliceblue; } }
        public static DUIPen AntiqueWhite { get { return antiquewhite; } }
        public static DUIPen Aqua { get { return aqua; } }
        public static DUIPen Aquamarine { get { return aquamarine; } }
        public static DUIPen Azure { get { return azure; } }
        public static DUIPen Beige { get { return beige; } }
        public static DUIPen Bisque { get { return bisque; } }
        public static DUIPen Black { get { return black; } }
        public static DUIPen BlanchedAlmond { get { return blanchedalmond; } }
        public static DUIPen Blue { get { return blue; } }
        public static DUIPen BlueViolet { get { return blueviolet; } }
        public static DUIPen Brown { get { return brown; } }
        public static DUIPen BurlyWood { get { return burlywood; } }
        public static DUIPen CadetBlue { get { return cadetblue; } }
        public static DUIPen Chartreuse { get { return chartreuse; } }
        public static DUIPen Chocolate { get { return chocolate; } }
        public static DUIPen Coral { get { return coral; } }
        public static DUIPen CornflowerBlue { get { return cornflowerblue; } }
        public static DUIPen Cornsilk { get { return cornsilk; } }
        public static DUIPen Crimson { get { return crimson; } }
        public static DUIPen Cyan { get { return cyan; } }
        public static DUIPen DarkBlue { get { return darkblue; } }
        public static DUIPen DarkCyan { get { return darkcyan; } }
        public static DUIPen DarkGoldenrod { get { return darkgoldenrod; } }
        public static DUIPen DarkGray { get { return darkgray; } }
        public static DUIPen DarkGreen { get { return darkgreen; } }
        public static DUIPen DarkKhaki { get { return darkkhaki; } }
        public static DUIPen DarkMagenta { get { return darkmagenta; } }
        public static DUIPen DarkOliveGreen { get { return darkolivegreen; } }
        public static DUIPen DarkOrange { get { return darkorange; } }
        public static DUIPen DarkOrchid { get { return darkorchid; } }
        public static DUIPen DarkRed { get { return darkred; } }
        public static DUIPen DarkSalmon { get { return darksalmon; } }
        public static DUIPen DarkSeaGreen { get { return darkseagreen; } }
        public static DUIPen DarkSlateBlue { get { return darkslateblue; } }
        public static DUIPen DarkSlateGray { get { return darkslategray; } }
        public static DUIPen DarkTurquoise { get { return darkturquoise; } }
        public static DUIPen DarkViolet { get { return darkviolet; } }
        public static DUIPen DeepPink { get { return deeppink; } }
        public static DUIPen DeepSkyBlue { get { return deepskyblue; } }
        public static DUIPen DimGray { get { return dimgray; } }
        public static DUIPen DodgerBlue { get { return dodgerblue; } }
        public static DUIPen Firebrick { get { return firebrick; } }
        public static DUIPen FloralWhite { get { return floralwhite; } }
        public static DUIPen ForestGreen { get { return forestgreen; } }
        public static DUIPen Fuchsia { get { return fuchsia; } }
        public static DUIPen Gainsboro { get { return gainsboro; } }
        public static DUIPen GhostWhite { get { return ghostwhite; } }
        public static DUIPen Gold { get { return gold; } }
        public static DUIPen Goldenrod { get { return goldenrod; } }
        public static DUIPen Gray { get { return gray; } }
        public static DUIPen Green { get { return green; } }
        public static DUIPen GreenYellow { get { return greenyellow; } }
        public static DUIPen Honeydew { get { return honeydew; } }
        public static DUIPen HotPink { get { return hotpink; } }
        public static DUIPen IndianRed { get { return indianred; } }
        public static DUIPen Indigo { get { return indigo; } }
        public static DUIPen Ivory { get { return ivory; } }
        public static DUIPen Khaki { get { return khaki; } }
        public static DUIPen Lavender { get { return lavender; } }
        public static DUIPen LavenderBlush { get { return lavenderblush; } }
        public static DUIPen LawnGreen { get { return lawngreen; } }
        public static DUIPen LemonChiffon { get { return lemonchiffon; } }
        public static DUIPen LightBlue { get { return lightblue; } }
        public static DUIPen LightCoral { get { return lightcoral; } }
        public static DUIPen LightCyan { get { return lightcyan; } }
        public static DUIPen LightGoldenrodYellow { get { return lightgoldenrodyellow; } }
        public static DUIPen LightGray { get { return lightgray; } }
        public static DUIPen LightGreen { get { return lightgreen; } }
        public static DUIPen LightPink { get { return lightpink; } }
        public static DUIPen LightSalmon { get { return lightsalmon; } }
        public static DUIPen LightSeaGreen { get { return lightseagreen; } }
        public static DUIPen LightSkyBlue { get { return lightskyblue; } }
        public static DUIPen LightSlateGray { get { return lightslategray; } }
        public static DUIPen LightSteelBlue { get { return lightsteelblue; } }
        public static DUIPen LightYellow { get { return lightyellow; } }
        public static DUIPen Lime { get { return lime; } }
        public static DUIPen LimeGreen { get { return limegreen; } }
        public static DUIPen Linen { get { return linen; } }
        public static DUIPen Magenta { get { return magenta; } }
        public static DUIPen Maroon { get { return maroon; } }
        public static DUIPen MediumAquamarine { get { return mediumaquamarine; } }
        public static DUIPen MediumBlue { get { return mediumblue; } }
        public static DUIPen MediumOrchid { get { return mediumorchid; } }
        public static DUIPen MediumPurple { get { return mediumpurple; } }
        public static DUIPen MediumSeaGreen { get { return mediumseagreen; } }
        public static DUIPen MediumSlateBlue { get { return mediumslateblue; } }
        public static DUIPen MediumSpringGreen { get { return mediumspringgreen; } }
        public static DUIPen MediumTurquoise { get { return mediumturquoise; } }
        public static DUIPen MediumVioletRed { get { return mediumvioletred; } }
        public static DUIPen MidnightBlue { get { return midnightblue; } }
        public static DUIPen MintCream { get { return mintcream; } }
        public static DUIPen MistyRose { get { return mistyrose; } }
        public static DUIPen Moccasin { get { return moccasin; } }
        public static DUIPen NavajoWhite { get { return navajowhite; } }
        public static DUIPen Navy { get { return navy; } }
        public static DUIPen OldLace { get { return oldlace; } }
        public static DUIPen Olive { get { return olive; } }
        public static DUIPen OliveDrab { get { return olivedrab; } }
        public static DUIPen Orange { get { return orange; } }
        public static DUIPen OrangeRed { get { return orangered; } }
        public static DUIPen Orchid { get { return orchid; } }
        public static DUIPen PaleGoldenrod { get { return palegoldenrod; } }
        public static DUIPen PaleGreen { get { return palegreen; } }
        public static DUIPen PaleTurquoise { get { return paleturquoise; } }
        public static DUIPen PaleVioletRed { get { return palevioletred; } }
        public static DUIPen PapayaWhip { get { return papayawhip; } }
        public static DUIPen PeachPuff { get { return peachpuff; } }
        public static DUIPen Peru { get { return peru; } }
        public static DUIPen Pink { get { return pink; } }
        public static DUIPen Plum { get { return plum; } }
        public static DUIPen PowderBlue { get { return powderblue; } }
        public static DUIPen Purple { get { return purple; } }
        public static DUIPen Red { get { return red; } }
        public static DUIPen RosyBrown { get { return rosybrown; } }
        public static DUIPen RoyalBlue { get { return royalblue; } }
        public static DUIPen SaddleBrown { get { return saddlebrown; } }
        public static DUIPen Salmon { get { return salmon; } }
        public static DUIPen SandyBrown { get { return sandybrown; } }
        public static DUIPen SeaGreen { get { return seagreen; } }
        public static DUIPen SeaShell { get { return seashell; } }
        public static DUIPen Sienna { get { return sienna; } }
        public static DUIPen Silver { get { return silver; } }
        public static DUIPen SkyBlue { get { return skyblue; } }
        public static DUIPen SlateBlue { get { return slateblue; } }
        public static DUIPen SlateGray { get { return slategray; } }
        public static DUIPen Snow { get { return snow; } }
        public static DUIPen SpringGreen { get { return springgreen; } }
        public static DUIPen SteelBlue { get { return steelblue; } }
        public static DUIPen Tan { get { return tan; } }
        public static DUIPen Teal { get { return teal; } }
        public static DUIPen Thistle { get { return thistle; } }
        public static DUIPen Tomato { get { return tomato; } }
        public static DUIPen Transparent { get { return transparent; } }
        public static DUIPen Turquoise { get { return turquoise; } }
        public static DUIPen Violet { get { return violet; } }
        public static DUIPen Wheat { get { return wheat; } }
        public static DUIPen White { get { return white; } }
        public static DUIPen WhiteSmoke { get { return whitesmoke; } }
        public static DUIPen Yellow { get { return yellow; } }
        public static DUIPen YellowGreen { get { return yellowgreen; } }
    }
}
