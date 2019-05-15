using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace SfondiManage
{
    public class TEMA
    {

        public TEMA(string _nome)
        {
            NOME = _nome.ToUpper();
        }

        public string NOME { get; private set; }
        public Color BTN { get; set; }
        public Color MAIN { get; set; }
        public Color ITEM { get; set; }
        public Color PANEL { get; set; }
    }

    public enum Style
    {
        Tiled,
        Centered,
        Stretched
    }

    public class RuleBG
    {
        public int Ora { get; private set; }
        public int Minuti { get; private set; }
        public string IMGPath { get; private set; }
        public Style STYLE { get; private set; }
        public TimeSpan MinutiOra {
            get { return new DateTime(1, 1, 1, Ora, Minuti, 0).TimeOfDay; }
        }

        public string FormatStile {
            get { return _stile(); }
        }

        public static RuleBG Attuale(List<RuleBG> _list)
        {
            if (_list == null || _list.Count <= 0)
                return null;
            List<RuleBG> papabili = new List<RuleBG>();
            foreach (var v in _list.OrderBy(k => k.MinutiOra))
            {
                if (v.Ora <= DateTime.Now.Hour)
                {
                    if (v.Ora == DateTime.Now.Hour)
                        if (v.Minuti > DateTime.Now.Minute)
                            continue;
                    papabili.Add(v);
                }
            }
            if (papabili.Count > 0)
                return papabili.Last();
            return _list.OrderBy(k => k.MinutiOra).Last();

        }

        public RuleBG(int h, int m, string _IMGPath, string _style)
        {
            Ora = h;
            Minuti = m;
            getIMAGE(_IMGPath);
            _stile(_style[0]);

        }
        public RuleBG(RuleBG rule)
        {
            Ora = rule.Ora;
            Minuti = rule.Minuti;
            getIMAGE(rule.IMGPath);
            STYLE = rule.STYLE;
        }

        public RuleBG(string toString)
        {
            var SplitTot = toString.Split('~');
            var SplitOra = SplitTot[0].Split(':');
            Ora = int.Parse(SplitOra[0]);
            Minuti = int.Parse(SplitOra[1]);

            getIMAGE(SplitTot[1]);
            _stile(SplitTot[2][0]);
        }

        private void getIMAGE(string img)
        {
            if (Out.IMGAvalable(img))
                IMGPath = img;
        }

        private string FormatTime(int num)
        {
            string add = "0";
            if (num >= 10)
                add = "";
            return add + num;
        }

        private void _stile(char s)
        {
            if (s == 'A')
            {
                STYLE = Style.Stretched;
            }

            if (s == 'C')
            {
                STYLE = Style.Centered;
            }

            if (s == 'R')
            {
                STYLE = Style.Tiled;
            }
        }

        public void Set()
        {

            if (Out.IsLocalFile(IMGPath))
                if (IMGPath.EndsWith("bmp"))
                    Out.SfondoManage.SetWallpaper(IMGPath, STYLE);
                else
                {
                    Out.SfondoManage.SetWallpaper(System.Drawing.Image.FromFile(IMGPath), STYLE);
                }
            else
            {
                if (Out.URLExists(IMGPath))
                {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        Out.SfondoManage.SetWallpaper(
                            System.Drawing.Image.FromStream(
                                new MemoryStream(
                                    wc.DownloadData(IMGPath))),
                            STYLE);
                    }
                }
            }
        }

        private string _stile()
        {
            string ret = "";
            if (STYLE == Style.Stretched)
            {
                ret = "Adatta";
            }

            if (STYLE == Style.Centered)
            {
                ret = "Centrato";
            }

            if (STYLE == Style.Tiled)
            {
                ret = "Ripetuto";
            }
            return ret;
        }

        private string _stile(Style styl)
        {
            string ret = "";
            if (styl == Style.Stretched)
            {
                ret = "Adatta";
            }

            if (styl == Style.Centered)
            {
                ret = "Centrato";
            }

            if (styl == Style.Tiled)
            {
                ret = "Ripetuto";
            }
            return ret;
        }

        public string ToTime()
        {

            return FormatTime(Ora) + ":" + FormatTime(Minuti);
        }

        public override string ToString()
        {
            return ToTime() + "~" + IMGPath + "~" + _stile()[0];
        }
    }

    class Out
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".PNG" };

        public static bool IMGAvalable(string img, bool NOT_THROW = false)
        {
            if (string.IsNullOrEmpty(img))
                if (!NOT_THROW)
                    throw new Exception("Immagine Assente");
                else
                    return false;

            if (IsLocalFile(img))
            {
                if (!File.Exists(img))
                    if (!NOT_THROW)
                        throw new Exception("File '" + img + "' non esistente");
                    else
                        return false;

                if (!ImageExtensions.Contains(Path.GetExtension(img).ToUpperInvariant()))
                    if (!NOT_THROW)
                        throw new Exception("Non è un immagine");
                    else
                        return false;
            }
            else
            if (!URLExists(img))
            {
                if (IS_ON_NET)
                    if (!NOT_THROW)
                        throw new Exception("URL '" + img + "' non valido");
                    else
                        return false;
                else
                    if (!NOT_THROW)
                    throw new Exception("Connessione internet assente");
                else
                    return false;
            }
            return true;
        }

        public static bool IS_ON_NET {
            get {
                {
                    try
                    {
                        using (var client = new System.Net.WebClient())
                        using (client.OpenRead("http://clients3.google.com/generate_204"))
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsUrlValid(string url)
        {
            return new Regex
                (@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase)
                .IsMatch(url);
        }

        public static bool URLExists(string url)
        {
            if (!IsUrlValid(url))
                return false;
            try
            {
                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "HEAD";

                try
                {
                    using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse())
                    {
                        if (response.StatusCode.ToString() == "OK")
                        {
                            return response.ContentType.ToLower().StartsWith("image/");
                        }
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception eee)
            {
                throw new Exception("Connessione internet assente");
            }
        }

        public static bool IsLocalFile(string _file)
        {
            return new Uri(_file).IsFile;
        }

        public static string DirRuleBG = Path.Combine(Path.GetTempPath(), "RuleBG");
        public static string FileRuleBG = Path.Combine(DirRuleBG, "RuleBG.txt");
        public static string FileTEMA = Path.Combine(DirRuleBG, "t.t");
        public static string FileGrandezzaItem = Path.Combine(DirRuleBG, "g.t");

        public static List<string> leggiTXT(string fil)
        {
            List<string> lines = new List<string>();
            if (File.Exists(fil))
            {
                using (StreamReader r = new StreamReader(fil))
                {
                    lines.AddRange(leggiTXT(r));
                }
            }
            return lines;
        }

        public static List<string> leggiTXT(StreamReader stream)
        {
            List<string> lines = new List<string>();
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;

        }

        public static List<string> leggiURL(string URL)
        {
            return leggiTXT(new StreamReader(new System.Net.WebClient().OpenRead(URL)));
        }

        public static class SfondoManage
        {

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, string vParam, UInt32 winIni);

            //const uint SPI_SETDESKWALLPAPER = 0x14;
            //const uint SPIF_UPDATEINIFILE = 0x01;
            //const uint SPIF_SENDWININICHANGE = 0x02;

            private static readonly UInt32 SPI_SETDESKWALLPAPER = 20;
            private static readonly UInt32 SPIF_UPDATEINIFILE = 0x1;
            private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;
            private static readonly UInt32 SPI_GETDESKWALLPAPER = 0x73;
            private static readonly int MAX_PATH = 260;

            public static void SetWallpaper(Uri uri, Style style)
            {
                var x = new System.Net.WebClient().OpenRead(uri.ToString());

                System.Drawing.Image img = System.Drawing.Image.FromStream(x);

                SetWallpaper(img, style);
            }

            public static void SetWallpaper(string path, Style style)
            {
                if (path.Equals(GetWallpaper()))
                    return;

                setStyle(style);
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE);
            }

            public static void SetWallpaper(System.Drawing.Image img, Style style)
            {
                string tempPath = "";
                string tempPath1 = Path.Combine(DirRuleBG, "wallpaper1.bmp"); ;
                string tempPath2 = Path.Combine(DirRuleBG, "wallpaper2.bmp"); ;
                if (GetWallpaper().Equals(tempPath2))
                {
                    File.Delete(tempPath2);
                    tempPath = tempPath1;
                }
                else
                {
                    File.Delete(tempPath1);
                    tempPath = tempPath2;
                }

                File.Delete(tempPath);

                img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

                SetWallpaper(tempPath, style);

            }

            public static void setStyle(Style _style)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (_style == Style.Stretched)
                {
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (_style == Style.Centered)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (_style == Style.Tiled)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                }
            }

            public static string GetWallpaper()
            {
                string wallpaper = new string('\0', MAX_PATH);
                SystemParametersInfo(SPI_GETDESKWALLPAPER,
                    (UInt32)wallpaper.Length, wallpaper, 0);
                wallpaper = wallpaper.Substring(0, wallpaper.IndexOf('\0'));
                return wallpaper;
            }
        }
    }

    public static class Utility
    {
        static string AppName = "SfondiManage";

        public static void avviasempre(bool val = true)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (val)
            {
                var old = rk.GetValue(AppName);
                if (old != null)
                {
                    if (!System.Windows.Forms.Application.ExecutablePath.Equals(old.ToString()))
                        rk.SetValue(AppName, System.Windows.Forms.Application.ExecutablePath);
                }
                else
                {
                    rk.SetValue(AppName, System.Windows.Forms.Application.ExecutablePath);
                }
            }
            else
            {
                rk.DeleteValue(AppName, false);
            }
        }

        public static bool isAvviaSempre()
        {
            return Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)
                .GetValue(AppName) != null;
        }


        public static dynamic RadioScreen {
            get {
                var X = Convert.ToDouble(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width);
                var Y = Convert.ToDouble(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

                // Calculate the greatest common divisor
                var GCD = GetGreatestCommonDivisor(Convert.ToInt32(X), Convert.ToInt32(Y));

                // Calculate the display aspect ratio
                return new
                {
                    X = (int)X / GCD,
                    Y = (int)Y / GCD
                };
            }
        }

        private static int GetGreatestCommonDivisor(Int32 a, Int32 b)
        {
            return b == 0 ? a : GetGreatestCommonDivisor(b, a % b);
        }
    }

}

