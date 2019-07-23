using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace DirectUI.Win32
{
    internal static class ShellHelper
    {
        private static readonly Regex r = new Regex(
            @"c=\[c:(?<key>.+)\]", RegexOptions.Compiled);

        internal static void ApplicationExit()
        {
            ApplicationExit(0);
        }

        internal static void ApplicationExit(int exitCode)
        {
            try
            {
                Environment.Exit(exitCode);
            }
            catch(Exception ex)
            {
                Log.DUILog.GettingLog(ex);
            }
        }

        internal static string ParseUrl(string url, IDictionary<string, string> credentials)
        {
            if (credentials != null)
            {
                Match match = r.Match(url);
                if (!match.Success)
                {
                    return url;
                }
                string str = "";
                string str2 = match.Result("${key}");
                if (string.IsNullOrEmpty(str2))
                {
                    return url;
                }
                if (credentials.ContainsKey(str2))
                {
                    str = HttpWebRequestHelper.UrlEncode(credentials[str2]);
                }
                url = r.Replace(url, "c=" + str);
            }
            return url;
        }

        internal static void StartUrl(string url)
        {
            try
            {
                StringBuilder builder = new StringBuilder(
                    ParseString((string)Registry.ClassesRoot.OpenSubKey(
                    @"HTTP\shell\open\command", false).GetValue(string.Empty, string.Empty)));
                builder.Append(" ");
                builder.Append(url);
                NativeMethods.WinExec(builder.ToString(), 5);
            }
            catch(Exception ex)
            {
                Log.DUILog.GettingLog(ex);
                try
                {
                    StringBuilder builder2 = new StringBuilder(
                        ParseString((string)Registry.ClassesRoot.OpenSubKey(
                        @"Applications\iexplore.exe\shell\open\command", false).GetValue(
                        string.Empty, string.Empty)));
                    builder2.Append(" ");
                    builder2.Append(url);
                    NativeMethods.WinExec(builder2.ToString(), 5);
                }
                catch(Exception ex1)
                {
                    Log.DUILog.GettingLog(ex1);
                }
            }
        }

        internal static void StartUrl(string url, IDictionary<string, string> credentials)
        {
            StartUrl(ParseUrl(url, credentials));
        }

        private static string ParseString(string value)
        {
            if (value.Substring(0, 1) == "\"")
            {
                int index = value.IndexOf("\"", 1);
                return value.Substring(0, index + 1);
            }
            return value.Split(new char[] { ' ' })[0];
        }
    }
}
