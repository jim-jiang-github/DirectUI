using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;

namespace DirectUI.Win32
{
    internal class HttpWebRequestHelper
    {
        private Encoding encoding = Encoding.UTF8;

        internal HttpWebRequestHelper()
        {
        }

        internal string Boundary
        {
            get
            {
                string[] strArray2 = ContentType.Split(new char[] { ';' });
                if (strArray2[0].Trim().ToLower() == "multipart/form-data")
                {
                    string[] strArray = strArray2[1].Split(new char[] { '=' });
                    return ("--" + strArray[1]);
                }
                return null;
            }
        }

        internal string ContentType
        {
            get
            {
                return "multipart/form-data; boundary=---------------------------7d5b915500cee";
            }
        }

        internal Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        internal byte[] CreateFieldData(string fieldName, string fieldValue)
        {
            string s = string.Format(
                Boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", 
                fieldName, fieldValue);
            return encoding.GetBytes(s);
        }

        internal byte[] CreateFieldData(
            string fieldName, string filename, string contentType, byte[] fileBytes)
        {
            string s = "\r\n";
            string str3 = string.Format(
                Boundary + 
                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", 
                fieldName, filename, contentType);
            byte[] bytes = encoding.GetBytes(str3);
            byte[] buffer2 = encoding.GetBytes(s);
            byte[] array = new byte[(bytes.Length + fileBytes.Length) + buffer2.Length];
            bytes.CopyTo(array, 0);
            fileBytes.CopyTo(array, bytes.Length);
            buffer2.CopyTo(array, bytes.Length + fileBytes.Length);
            return array;
        }

        internal Encoding GetEncoding(HttpWebResponse response)
        {
            string contentEncoding = response.ContentEncoding;
            Encoding encoding = Encoding.Default;
            if (contentEncoding == "")
            {
                string contentType = response.ContentType;
                if (contentType.ToLower().IndexOf("charset") != -1)
                {
                    contentEncoding = contentType.Substring(
                        contentType.ToLower().IndexOf("charset=") + "charset=".Length);
                }
            }
            if (contentEncoding != "")
            {
                try
                {
                    encoding = Encoding.GetEncoding(contentEncoding);
                }
                catch(Exception ex)
                {
                    Log.DUILog.GettingLog(ex);
                }
            }
            return encoding;
        }

        private static int HexToInt(char h)
        {
            if ((h >= '0') && (h <= '9'))
            {
                return (h - '0');
            }
            if ((h >= 'a') && (h <= 'f'))
            {
                return ((h - 'a') + 10);
            }
            if ((h >= 'A') && (h <= 'F'))
            {
                return ((h - 'A') + 10);
            }
            return -1;
        }

        internal static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 0x30);
            }
            return (char)((n - 10) + 0x61);
        }

        internal static bool IsSafe(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) 
                || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '\'':
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

        internal byte[] JoinBytes(ArrayList byteArrays)
        {
            int num = 0;
            int index = 0;
            string s = Boundary + "--\r\n";
            byte[] bytes = encoding.GetBytes(s);
            byteArrays.Add(bytes);
            foreach (byte[] buffer2 in byteArrays)
            {
                num += buffer2.Length;
            }
            byte[] array = new byte[num];
            foreach (byte[] buffer4 in byteArrays)
            {
                buffer4.CopyTo(array, index);
                index += buffer4.Length;
            }
            return array;
        }

        internal string TextContent(HttpWebResponse response)
        {
            string str2;
            string str = "";
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, GetEncoding(response));
            while ((str2 = reader.ReadLine()) != null)
            {
                str = str + str2 + "\r\n";
            }
            responseStream.Close();
            return str;
        }

        internal static string UrlDecode(string str)
        {
            if (str == null)
            {
                return null;
            }
            return UrlDecode(str, Encoding.UTF8);
        }

        internal static string UrlDecode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return UrlDecodeStringFromStringInternal(str, e);
        }

        private static string UrlDecodeStringFromStringInternal(string s, Encoding e)
        {
            int length = s.Length;
            UrlDecoder decoder = new UrlDecoder(length, e);
            for (int i = 0; i < length; i++)
            {
                char ch = s[i];
                if (ch == '+')
                {
                    ch = ' ';
                }
                else if ((ch == '%') && (i < (length - 2)))
                {
                    if ((s[i + 1] == 'u') && (i < (length - 5)))
                    {
                        int num3 = HexToInt(s[i + 2]);
                        int num4 = HexToInt(s[i + 3]);
                        int num5 = HexToInt(s[i + 4]);
                        int num6 = HexToInt(s[i + 5]);
                        if (((num3 < 0) || (num4 < 0)) || ((num5 < 0) || (num6 < 0)))
                        {
                            goto decoderCH;
                        }
                        ch = (char)((((num3 << 12) | (num4 << 8)) | (num5 << 4)) | num6);
                        i += 5;
                        decoder.AddChar(ch);
                        continue;
                    }
                    int num7 = HexToInt(s[i + 1]);
                    int num8 = HexToInt(s[i + 2]);
                    if ((num7 >= 0) && (num8 >= 0))
                    {
                        byte b = (byte)((num7 << 4) | num8);
                        i += 2;
                        decoder.AddByte(b);
                        continue;
                    }
                }

            decoderCH:
                if ((ch & 0xff80) == 0)
                {
                    decoder.AddByte((byte)ch);
                }
                else
                {
                    decoder.AddChar(ch);
                }
            }
            return decoder.GetString();
        }

        internal static string UrlEncode(string str)
        {
            if (str == null)
            {
                return null;
            }
            return UrlEncode(str, Encoding.UTF8);
        }

        internal static string UrlEncode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));
        }

        private static byte[] UrlEncodeBytesToBytesInternal(
            byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
        {
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsSafe(ch))
                {
                    num2++;
                }
            }
            if ((!alwaysCreateReturnValue && (num == 0)) && (num2 == 0))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num2 * 2)];
            int num4 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num6 = bytes[offset + j];
                char ch2 = (char)num6;
                if (IsSafe(ch2))
                {
                    buffer[num4++] = num6;
                }
                else if (ch2 == ' ')
                {
                    buffer[num4++] = 0x2b;
                }
                else
                {
                    buffer[num4++] = 0x25;
                    buffer[num4++] = (byte)IntToHex((num6 >> 4) & 15);
                    buffer[num4++] = (byte)IntToHex(num6 & 15);
                }
            }
            return buffer;
        }

        internal static byte[] UrlEncodeToBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            return UrlEncodeToBytes(bytes, 0, bytes.Length);
        }

        internal static byte[] UrlEncodeToBytes(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            byte[] bytes = e.GetBytes(str);
            return UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
        }

        internal static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
        {
            if ((bytes == null) && (count == 0))
            {
                return null;
            }
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if ((offset < 0) || (offset > bytes.Length))
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((count < 0) || ((offset + count) > bytes.Length))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            return UrlEncodeBytesToBytesInternal(bytes, offset, count, true);
        }

        private class UrlDecoder
        {
            private int _bufferSize;
            private byte[] _byteBuffer;
            private char[] _charBuffer;
            private Encoding _encoding;
            private int _numBytes;
            private int _numChars;

            internal UrlDecoder(int bufferSize, Encoding encoding)
            {
                _bufferSize = bufferSize;
                _encoding = encoding;
                _charBuffer = new char[bufferSize];
            }

            internal void AddByte(byte b)
            {
                if (_byteBuffer == null)
                {
                    _byteBuffer = new byte[_bufferSize];
                }
                _byteBuffer[_numBytes++] = b;
            }

            internal void AddChar(char ch)
            {
                if (_numBytes > 0)
                {
                    FlushBytes();
                }
                _charBuffer[_numChars++] = ch;
            }

            private void FlushBytes()
            {
                if (_numBytes > 0)
                {
                    _numChars += _encoding.GetChars(
                        _byteBuffer, 0, _numBytes, _charBuffer, _numChars);
                    _numBytes = 0;
                }
            }

            internal string GetString()
            {
                if (_numBytes > 0)
                {
                    FlushBytes();
                }
                if (_numChars > 0)
                {
                    return new string(_charBuffer, 0, _numChars);
                }
                return string.Empty;
            }
        }
    }
}