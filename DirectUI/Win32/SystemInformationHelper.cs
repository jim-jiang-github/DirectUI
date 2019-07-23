namespace DirectUI.Win32
{
    using System;

    internal static class SystemInformationHelper
    {
        internal static WindowsType GetWindowsVersionType()
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            WindowsType none = WindowsType.None;
            switch (oSVersion.Platform)
            {
                case PlatformID.Win32Windows:
                    switch (oSVersion.Version.Minor)
                    {
                        case 0:
                            return WindowsType.Windows95;

                        case 10:
                            return WindowsType.Windows98;

                        case 90:
                            none = WindowsType.WindowsME;
                            break;
                    }
                    return none;

                case PlatformID.Win32NT:
                    switch (oSVersion.Version.Major)
                    {
                        case 3:
                            return none;

                        case 4:
                            return WindowsType.WindowsNT4;

                        case 5:
                            switch (oSVersion.Version.Minor)
                            {
                                case 0:
                                    return WindowsType.Windows2K;

                                case 1:
                                    return WindowsType.WindowsXP;

                                case 2:
                                    return WindowsType.Windows2003;
                            }
                            return none;

                        case 6:
                            switch (oSVersion.Version.Minor)
                            {
                                case 0:
                                    return WindowsType.WindowsVista;

                                case 1:
                                    return WindowsType.Windows7;
                            }
                            return none;
                    }
                    return none;
            }
            return none;
        }

        internal static bool IsWinVista
        {
            get
            {
                return (GetWindowsVersionType() == WindowsType.WindowsVista);
            }
        }

        internal static bool IsWinXP
        {
            get
            {
                return (GetWindowsVersionType() == WindowsType.WindowsXP);
            }
        }
    }
}

