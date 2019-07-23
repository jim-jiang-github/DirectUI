using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public sealed class DUIBrushes
    {
        private static DUISolidBrush aliceblue = new DUISolidBrush(Color.AliceBlue);
        private static DUISolidBrush antiquewhite = new DUISolidBrush(Color.AntiqueWhite);
        private static DUISolidBrush aqua = new DUISolidBrush(Color.Aqua);
        private static DUISolidBrush aquamarine = new DUISolidBrush(Color.Aquamarine);
        private static DUISolidBrush azure = new DUISolidBrush(Color.Azure);
        private static DUISolidBrush beige = new DUISolidBrush(Color.Beige);
        private static DUISolidBrush bisque = new DUISolidBrush(Color.Bisque);
        private static DUISolidBrush black = new DUISolidBrush(Color.Black);
        private static DUISolidBrush blanchedalmond = new DUISolidBrush(Color.BlanchedAlmond);
        private static DUISolidBrush blue = new DUISolidBrush(Color.Blue);
        private static DUISolidBrush blueviolet = new DUISolidBrush(Color.BlueViolet);
        private static DUISolidBrush brown = new DUISolidBrush(Color.Brown);
        private static DUISolidBrush burlywood = new DUISolidBrush(Color.BurlyWood);
        private static DUISolidBrush cadetblue = new DUISolidBrush(Color.CadetBlue);
        private static DUISolidBrush chartreuse = new DUISolidBrush(Color.Chartreuse);
        private static DUISolidBrush chocolate = new DUISolidBrush(Color.Chocolate);
        private static DUISolidBrush coral = new DUISolidBrush(Color.Coral);
        private static DUISolidBrush cornflowerblue = new DUISolidBrush(Color.CornflowerBlue);
        private static DUISolidBrush cornsilk = new DUISolidBrush(Color.Cornsilk);
        private static DUISolidBrush crimson = new DUISolidBrush(Color.Crimson);
        private static DUISolidBrush cyan = new DUISolidBrush(Color.Cyan);
        private static DUISolidBrush darkblue = new DUISolidBrush(Color.DarkBlue);
        private static DUISolidBrush darkcyan = new DUISolidBrush(Color.DarkCyan);
        private static DUISolidBrush darkgoldenrod = new DUISolidBrush(Color.DarkGoldenrod);
        private static DUISolidBrush darkgray = new DUISolidBrush(Color.DarkGray);
        private static DUISolidBrush darkgreen = new DUISolidBrush(Color.DarkGreen);
        private static DUISolidBrush darkkhaki = new DUISolidBrush(Color.DarkKhaki);
        private static DUISolidBrush darkmagenta = new DUISolidBrush(Color.DarkMagenta);
        private static DUISolidBrush darkolivegreen = new DUISolidBrush(Color.DarkOliveGreen);
        private static DUISolidBrush darkorange = new DUISolidBrush(Color.DarkOrange);
        private static DUISolidBrush darkorchid = new DUISolidBrush(Color.DarkOrchid);
        private static DUISolidBrush darkred = new DUISolidBrush(Color.DarkRed);
        private static DUISolidBrush darksalmon = new DUISolidBrush(Color.DarkSalmon);
        private static DUISolidBrush darkseagreen = new DUISolidBrush(Color.DarkSeaGreen);
        private static DUISolidBrush darkslateblue = new DUISolidBrush(Color.DarkSlateBlue);
        private static DUISolidBrush darkslategray = new DUISolidBrush(Color.DarkSlateGray);
        private static DUISolidBrush darkturquoise = new DUISolidBrush(Color.DarkTurquoise);
        private static DUISolidBrush darkviolet = new DUISolidBrush(Color.DarkViolet);
        private static DUISolidBrush deeppink = new DUISolidBrush(Color.DeepPink);
        private static DUISolidBrush deepskyblue = new DUISolidBrush(Color.DeepSkyBlue);
        private static DUISolidBrush dimgray = new DUISolidBrush(Color.DimGray);
        private static DUISolidBrush dodgerblue = new DUISolidBrush(Color.DodgerBlue);
        private static DUISolidBrush firebrick = new DUISolidBrush(Color.Firebrick);
        private static DUISolidBrush floralwhite = new DUISolidBrush(Color.FloralWhite);
        private static DUISolidBrush forestgreen = new DUISolidBrush(Color.ForestGreen);
        private static DUISolidBrush fuchsia = new DUISolidBrush(Color.Fuchsia);
        private static DUISolidBrush gainsboro = new DUISolidBrush(Color.Gainsboro);
        private static DUISolidBrush ghostwhite = new DUISolidBrush(Color.GhostWhite);
        private static DUISolidBrush gold = new DUISolidBrush(Color.Gold);
        private static DUISolidBrush goldenrod = new DUISolidBrush(Color.Goldenrod);
        private static DUISolidBrush gray = new DUISolidBrush(Color.Gray);
        private static DUISolidBrush green = new DUISolidBrush(Color.Green);
        private static DUISolidBrush greenyellow = new DUISolidBrush(Color.GreenYellow);
        private static DUISolidBrush honeydew = new DUISolidBrush(Color.Honeydew);
        private static DUISolidBrush hotpink = new DUISolidBrush(Color.HotPink);
        private static DUISolidBrush indianred = new DUISolidBrush(Color.IndianRed);
        private static DUISolidBrush indigo = new DUISolidBrush(Color.Indigo);
        private static DUISolidBrush ivory = new DUISolidBrush(Color.Ivory);
        private static DUISolidBrush khaki = new DUISolidBrush(Color.Khaki);
        private static DUISolidBrush lavender = new DUISolidBrush(Color.Lavender);
        private static DUISolidBrush lavenderblush = new DUISolidBrush(Color.LavenderBlush);
        private static DUISolidBrush lawngreen = new DUISolidBrush(Color.LawnGreen);
        private static DUISolidBrush lemonchiffon = new DUISolidBrush(Color.LemonChiffon);
        private static DUISolidBrush lightblue = new DUISolidBrush(Color.LightBlue);
        private static DUISolidBrush lightcoral = new DUISolidBrush(Color.LightCoral);
        private static DUISolidBrush lightcyan = new DUISolidBrush(Color.LightCyan);
        private static DUISolidBrush lightgoldenrodyellow = new DUISolidBrush(Color.LightGoldenrodYellow);
        private static DUISolidBrush lightgray = new DUISolidBrush(Color.LightGray);
        private static DUISolidBrush lightgreen = new DUISolidBrush(Color.LightGreen);
        private static DUISolidBrush lightpink = new DUISolidBrush(Color.LightPink);
        private static DUISolidBrush lightsalmon = new DUISolidBrush(Color.LightSalmon);
        private static DUISolidBrush lightseagreen = new DUISolidBrush(Color.LightSeaGreen);
        private static DUISolidBrush lightskyblue = new DUISolidBrush(Color.LightSkyBlue);
        private static DUISolidBrush lightslategray = new DUISolidBrush(Color.LightSlateGray);
        private static DUISolidBrush lightsteelblue = new DUISolidBrush(Color.LightSteelBlue);
        private static DUISolidBrush lightyellow = new DUISolidBrush(Color.LightYellow);
        private static DUISolidBrush lime = new DUISolidBrush(Color.Lime);
        private static DUISolidBrush limegreen = new DUISolidBrush(Color.LimeGreen);
        private static DUISolidBrush linen = new DUISolidBrush(Color.Linen);
        private static DUISolidBrush magenta = new DUISolidBrush(Color.Magenta);
        private static DUISolidBrush maroon = new DUISolidBrush(Color.Maroon);
        private static DUISolidBrush mediumaquamarine = new DUISolidBrush(Color.MediumAquamarine);
        private static DUISolidBrush mediumblue = new DUISolidBrush(Color.MediumBlue);
        private static DUISolidBrush mediumorchid = new DUISolidBrush(Color.MediumOrchid);
        private static DUISolidBrush mediumpurple = new DUISolidBrush(Color.MediumPurple);
        private static DUISolidBrush mediumseagreen = new DUISolidBrush(Color.MediumSeaGreen);
        private static DUISolidBrush mediumslateblue = new DUISolidBrush(Color.MediumSlateBlue);
        private static DUISolidBrush mediumspringgreen = new DUISolidBrush(Color.MediumSpringGreen);
        private static DUISolidBrush mediumturquoise = new DUISolidBrush(Color.MediumTurquoise);
        private static DUISolidBrush mediumvioletred = new DUISolidBrush(Color.MediumVioletRed);
        private static DUISolidBrush midnightblue = new DUISolidBrush(Color.MidnightBlue);
        private static DUISolidBrush mintcream = new DUISolidBrush(Color.MintCream);
        private static DUISolidBrush mistyrose = new DUISolidBrush(Color.MistyRose);
        private static DUISolidBrush moccasin = new DUISolidBrush(Color.Moccasin);
        private static DUISolidBrush navajowhite = new DUISolidBrush(Color.NavajoWhite);
        private static DUISolidBrush navy = new DUISolidBrush(Color.Navy);
        private static DUISolidBrush oldlace = new DUISolidBrush(Color.OldLace);
        private static DUISolidBrush olive = new DUISolidBrush(Color.Olive);
        private static DUISolidBrush olivedrab = new DUISolidBrush(Color.OliveDrab);
        private static DUISolidBrush orange = new DUISolidBrush(Color.Orange);
        private static DUISolidBrush orangered = new DUISolidBrush(Color.OrangeRed);
        private static DUISolidBrush orchid = new DUISolidBrush(Color.Orchid);
        private static DUISolidBrush palegoldenrod = new DUISolidBrush(Color.PaleGoldenrod);
        private static DUISolidBrush palegreen = new DUISolidBrush(Color.PaleGreen);
        private static DUISolidBrush paleturquoise = new DUISolidBrush(Color.PaleTurquoise);
        private static DUISolidBrush palevioletred = new DUISolidBrush(Color.PaleVioletRed);
        private static DUISolidBrush papayawhip = new DUISolidBrush(Color.PapayaWhip);
        private static DUISolidBrush peachpuff = new DUISolidBrush(Color.PeachPuff);
        private static DUISolidBrush peru = new DUISolidBrush(Color.Peru);
        private static DUISolidBrush pink = new DUISolidBrush(Color.Pink);
        private static DUISolidBrush plum = new DUISolidBrush(Color.Plum);
        private static DUISolidBrush powderblue = new DUISolidBrush(Color.PowderBlue);
        private static DUISolidBrush purple = new DUISolidBrush(Color.Purple);
        private static DUISolidBrush red = new DUISolidBrush(Color.Red);
        private static DUISolidBrush rosybrown = new DUISolidBrush(Color.RosyBrown);
        private static DUISolidBrush royalblue = new DUISolidBrush(Color.RoyalBlue);
        private static DUISolidBrush saddlebrown = new DUISolidBrush(Color.SaddleBrown);
        private static DUISolidBrush salmon = new DUISolidBrush(Color.Salmon);
        private static DUISolidBrush sandybrown = new DUISolidBrush(Color.SandyBrown);
        private static DUISolidBrush seagreen = new DUISolidBrush(Color.SeaGreen);
        private static DUISolidBrush seashell = new DUISolidBrush(Color.SeaShell);
        private static DUISolidBrush sienna = new DUISolidBrush(Color.Sienna);
        private static DUISolidBrush silver = new DUISolidBrush(Color.Silver);
        private static DUISolidBrush skyblue = new DUISolidBrush(Color.SkyBlue);
        private static DUISolidBrush slateblue = new DUISolidBrush(Color.SlateBlue);
        private static DUISolidBrush slategray = new DUISolidBrush(Color.SlateGray);
        private static DUISolidBrush snow = new DUISolidBrush(Color.Snow);
        private static DUISolidBrush springgreen = new DUISolidBrush(Color.SpringGreen);
        private static DUISolidBrush steelblue = new DUISolidBrush(Color.SteelBlue);
        private static DUISolidBrush tan = new DUISolidBrush(Color.Tan);
        private static DUISolidBrush teal = new DUISolidBrush(Color.Teal);
        private static DUISolidBrush thistle = new DUISolidBrush(Color.Thistle);
        private static DUISolidBrush tomato = new DUISolidBrush(Color.Tomato);
        private static DUISolidBrush transparent = new DUISolidBrush(Color.Transparent);
        private static DUISolidBrush turquoise = new DUISolidBrush(Color.Turquoise);
        private static DUISolidBrush violet = new DUISolidBrush(Color.Violet);
        private static DUISolidBrush wheat = new DUISolidBrush(Color.Wheat);
        private static DUISolidBrush white = new DUISolidBrush(Color.White);
        private static DUISolidBrush whitesmoke = new DUISolidBrush(Color.WhiteSmoke);
        private static DUISolidBrush yellow = new DUISolidBrush(Color.Yellow);
        private static DUISolidBrush yellowgreen = new DUISolidBrush(Color.YellowGreen);

        public static DUISolidBrush AliceBlue { get { return aliceblue; } }
        public static DUISolidBrush AntiqueWhite { get { return antiquewhite; } }
        public static DUISolidBrush Aqua { get { return aqua; } }
        public static DUISolidBrush Aquamarine { get { return aquamarine; } }
        public static DUISolidBrush Azure { get { return azure; } }
        public static DUISolidBrush Beige { get { return beige; } }
        public static DUISolidBrush Bisque { get { return bisque; } }
        public static DUISolidBrush Black { get { return black; } }
        public static DUISolidBrush BlanchedAlmond { get { return blanchedalmond; } }
        public static DUISolidBrush Blue { get { return blue; } }
        public static DUISolidBrush BlueViolet { get { return blueviolet; } }
        public static DUISolidBrush Brown { get { return brown; } }
        public static DUISolidBrush BurlyWood { get { return burlywood; } }
        public static DUISolidBrush CadetBlue { get { return cadetblue; } }
        public static DUISolidBrush Chartreuse { get { return chartreuse; } }
        public static DUISolidBrush Chocolate { get { return chocolate; } }
        public static DUISolidBrush Coral { get { return coral; } }
        public static DUISolidBrush CornflowerBlue { get { return cornflowerblue; } }
        public static DUISolidBrush Cornsilk { get { return cornsilk; } }
        public static DUISolidBrush Crimson { get { return crimson; } }
        public static DUISolidBrush Cyan { get { return cyan; } }
        public static DUISolidBrush DarkBlue { get { return darkblue; } }
        public static DUISolidBrush DarkCyan { get { return darkcyan; } }
        public static DUISolidBrush DarkGoldenrod { get { return darkgoldenrod; } }
        public static DUISolidBrush DarkGray { get { return darkgray; } }
        public static DUISolidBrush DarkGreen { get { return darkgreen; } }
        public static DUISolidBrush DarkKhaki { get { return darkkhaki; } }
        public static DUISolidBrush DarkMagenta { get { return darkmagenta; } }
        public static DUISolidBrush DarkOliveGreen { get { return darkolivegreen; } }
        public static DUISolidBrush DarkOrange { get { return darkorange; } }
        public static DUISolidBrush DarkOrchid { get { return darkorchid; } }
        public static DUISolidBrush DarkRed { get { return darkred; } }
        public static DUISolidBrush DarkSalmon { get { return darksalmon; } }
        public static DUISolidBrush DarkSeaGreen { get { return darkseagreen; } }
        public static DUISolidBrush DarkSlateBlue { get { return darkslateblue; } }
        public static DUISolidBrush DarkSlateGray { get { return darkslategray; } }
        public static DUISolidBrush DarkTurquoise { get { return darkturquoise; } }
        public static DUISolidBrush DarkViolet { get { return darkviolet; } }
        public static DUISolidBrush DeepPink { get { return deeppink; } }
        public static DUISolidBrush DeepSkyBlue { get { return deepskyblue; } }
        public static DUISolidBrush DimGray { get { return dimgray; } }
        public static DUISolidBrush DodgerBlue { get { return dodgerblue; } }
        public static DUISolidBrush Firebrick { get { return firebrick; } }
        public static DUISolidBrush FloralWhite { get { return floralwhite; } }
        public static DUISolidBrush ForestGreen { get { return forestgreen; } }
        public static DUISolidBrush Fuchsia { get { return fuchsia; } }
        public static DUISolidBrush Gainsboro { get { return gainsboro; } }
        public static DUISolidBrush GhostWhite { get { return ghostwhite; } }
        public static DUISolidBrush Gold { get { return gold; } }
        public static DUISolidBrush Goldenrod { get { return goldenrod; } }
        public static DUISolidBrush Gray { get { return gray; } }
        public static DUISolidBrush Green { get { return green; } }
        public static DUISolidBrush GreenYellow { get { return greenyellow; } }
        public static DUISolidBrush Honeydew { get { return honeydew; } }
        public static DUISolidBrush HotPink { get { return hotpink; } }
        public static DUISolidBrush IndianRed { get { return indianred; } }
        public static DUISolidBrush Indigo { get { return indigo; } }
        public static DUISolidBrush Ivory { get { return ivory; } }
        public static DUISolidBrush Khaki { get { return khaki; } }
        public static DUISolidBrush Lavender { get { return lavender; } }
        public static DUISolidBrush LavenderBlush { get { return lavenderblush; } }
        public static DUISolidBrush LawnGreen { get { return lawngreen; } }
        public static DUISolidBrush LemonChiffon { get { return lemonchiffon; } }
        public static DUISolidBrush LightBlue { get { return lightblue; } }
        public static DUISolidBrush LightCoral { get { return lightcoral; } }
        public static DUISolidBrush LightCyan { get { return lightcyan; } }
        public static DUISolidBrush LightGoldenrodYellow { get { return lightgoldenrodyellow; } }
        public static DUISolidBrush LightGray { get { return lightgray; } }
        public static DUISolidBrush LightGreen { get { return lightgreen; } }
        public static DUISolidBrush LightPink { get { return lightpink; } }
        public static DUISolidBrush LightSalmon { get { return lightsalmon; } }
        public static DUISolidBrush LightSeaGreen { get { return lightseagreen; } }
        public static DUISolidBrush LightSkyBlue { get { return lightskyblue; } }
        public static DUISolidBrush LightSlateGray { get { return lightslategray; } }
        public static DUISolidBrush LightSteelBlue { get { return lightsteelblue; } }
        public static DUISolidBrush LightYellow { get { return lightyellow; } }
        public static DUISolidBrush Lime { get { return lime; } }
        public static DUISolidBrush LimeGreen { get { return limegreen; } }
        public static DUISolidBrush Linen { get { return linen; } }
        public static DUISolidBrush Magenta { get { return magenta; } }
        public static DUISolidBrush Maroon { get { return maroon; } }
        public static DUISolidBrush MediumAquamarine { get { return mediumaquamarine; } }
        public static DUISolidBrush MediumBlue { get { return mediumblue; } }
        public static DUISolidBrush MediumOrchid { get { return mediumorchid; } }
        public static DUISolidBrush MediumPurple { get { return mediumpurple; } }
        public static DUISolidBrush MediumSeaGreen { get { return mediumseagreen; } }
        public static DUISolidBrush MediumSlateBlue { get { return mediumslateblue; } }
        public static DUISolidBrush MediumSpringGreen { get { return mediumspringgreen; } }
        public static DUISolidBrush MediumTurquoise { get { return mediumturquoise; } }
        public static DUISolidBrush MediumVioletRed { get { return mediumvioletred; } }
        public static DUISolidBrush MidnightBlue { get { return midnightblue; } }
        public static DUISolidBrush MintCream { get { return mintcream; } }
        public static DUISolidBrush MistyRose { get { return mistyrose; } }
        public static DUISolidBrush Moccasin { get { return moccasin; } }
        public static DUISolidBrush NavajoWhite { get { return navajowhite; } }
        public static DUISolidBrush Navy { get { return navy; } }
        public static DUISolidBrush OldLace { get { return oldlace; } }
        public static DUISolidBrush Olive { get { return olive; } }
        public static DUISolidBrush OliveDrab { get { return olivedrab; } }
        public static DUISolidBrush Orange { get { return orange; } }
        public static DUISolidBrush OrangeRed { get { return orangered; } }
        public static DUISolidBrush Orchid { get { return orchid; } }
        public static DUISolidBrush PaleGoldenrod { get { return palegoldenrod; } }
        public static DUISolidBrush PaleGreen { get { return palegreen; } }
        public static DUISolidBrush PaleTurquoise { get { return paleturquoise; } }
        public static DUISolidBrush PaleVioletRed { get { return palevioletred; } }
        public static DUISolidBrush PapayaWhip { get { return papayawhip; } }
        public static DUISolidBrush PeachPuff { get { return peachpuff; } }
        public static DUISolidBrush Peru { get { return peru; } }
        public static DUISolidBrush Pink { get { return pink; } }
        public static DUISolidBrush Plum { get { return plum; } }
        public static DUISolidBrush PowderBlue { get { return powderblue; } }
        public static DUISolidBrush Purple { get { return purple; } }
        public static DUISolidBrush Red { get { return red; } }
        public static DUISolidBrush RosyBrown { get { return rosybrown; } }
        public static DUISolidBrush RoyalBlue { get { return royalblue; } }
        public static DUISolidBrush SaddleBrown { get { return saddlebrown; } }
        public static DUISolidBrush Salmon { get { return salmon; } }
        public static DUISolidBrush SandyBrown { get { return sandybrown; } }
        public static DUISolidBrush SeaGreen { get { return seagreen; } }
        public static DUISolidBrush SeaShell { get { return seashell; } }
        public static DUISolidBrush Sienna { get { return sienna; } }
        public static DUISolidBrush Silver { get { return silver; } }
        public static DUISolidBrush SkyBlue { get { return skyblue; } }
        public static DUISolidBrush SlateBlue { get { return slateblue; } }
        public static DUISolidBrush SlateGray { get { return slategray; } }
        public static DUISolidBrush Snow { get { return snow; } }
        public static DUISolidBrush SpringGreen { get { return springgreen; } }
        public static DUISolidBrush SteelBlue { get { return steelblue; } }
        public static DUISolidBrush Tan { get { return tan; } }
        public static DUISolidBrush Teal { get { return teal; } }
        public static DUISolidBrush Thistle { get { return thistle; } }
        public static DUISolidBrush Tomato { get { return tomato; } }
        public static DUISolidBrush Transparent { get { return transparent; } }
        public static DUISolidBrush Turquoise { get { return turquoise; } }
        public static DUISolidBrush Violet { get { return violet; } }
        public static DUISolidBrush Wheat { get { return wheat; } }
        public static DUISolidBrush White { get { return white; } }
        public static DUISolidBrush WhiteSmoke { get { return whitesmoke; } }
        public static DUISolidBrush Yellow { get { return yellow; } }
        public static DUISolidBrush YellowGreen { get { return yellowgreen; } }
    }
}
