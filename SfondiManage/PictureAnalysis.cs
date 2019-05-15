using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfondiManage
{
    public class PictureAnalysis
    {
        public List<Color> TenMostUsedColors { get; private set; }
        public List<int> TenMostUsedColorIncidences { get; private set; }
        public Color MostUsedColor { get; private set; }
        public int MostUsedColorIncidence { get; private set; }

        private int pixelColor;

        private Dictionary<int, int> dctColorIncidence;

        public static PictureAnalysis GetMostUsedColor(System.Drawing.Image img)
        {
            var theBitMap = new Bitmap(img);

            var ret = new PictureAnalysis();

            ret.TenMostUsedColors = new List<Color>();
            ret.TenMostUsedColorIncidences = new List<int>();

            ret.MostUsedColor = Color.Empty;
            ret.MostUsedColorIncidence = 0;

            // does using Dictionary<int,int> here
            // really pay-off compared to using
            // Dictionary<Color, int> ?

            // would using a SortedDictionary be much slower, or ?

            ret.dctColorIncidence = new Dictionary<int, int>();

            // this is what you want to speed up with unmanaged code
            for (int row = 0; row < theBitMap.Size.Width; row++)
            {
                for (int col = 0; col < theBitMap.Size.Height; col++)
                {
                    ret.pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                    if (ret.dctColorIncidence.Keys.Contains(ret.pixelColor))
                    {
                        ret.dctColorIncidence[ret.pixelColor]++;
                    }
                    else
                    {
                        ret.dctColorIncidence.Add(ret.pixelColor, 1);
                    }
                }
            }

            // note that there are those who argue that a
            // .NET Generic Dictionary is never guaranteed
            // to be sorted by methods like this
            var dctSortedByValueHighToLow = ret.dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // this should be replaced with some elegant Linq ?
            foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
            {
                ret.TenMostUsedColors.Add(Color.FromArgb(kvp.Key));
                ret.TenMostUsedColorIncidences.Add(kvp.Value);
            }

            ret.MostUsedColor = Color.FromArgb(dctSortedByValueHighToLow.First().Key);
            ret.MostUsedColorIncidence = dctSortedByValueHighToLow.First().Value;

            return ret;
        }

    }
}
