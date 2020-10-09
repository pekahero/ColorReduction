using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorReduction
{
    struct RGBColor
    {
        public Int16 r, g, b;
        public readonly int eucDistance;

        public RGBColor(Int16 r, Int16 g, Int16 b)
        {
            this.r = r;
            this.g = g;
            this.b = b;

            this.eucDistance = (int) Math.Sqrt(Math.Pow(r, 2) + Math.Pow(g, 2) + Math.Pow(b, 2));
        }

        public double distanceBetween(RGBColor other)
        {
            return Math.Sqrt(Math.Pow(r - other.r, 2) + Math.Pow(g - other.g, 2) + Math.Pow(b - other.b, 2));
        }

    }

    struct ColorBucket
    {
        public readonly int startEucDistance;
        public readonly int endEucDistance;
        RGBColor[] bucketColors;

        public ColorBucket(RGBColor[] sortedBucketColors, int startDist)
        {
            this.bucketColors = sortedBucketColors;
            this.startEucDistance = startDist;
            this.endEucDistance = sortedBucketColors.Last().eucDistance;
        }

        public RGBColor ClothestColor(RGBColor col)
        {
            var result = this.bucketColors[0];
            var minDist = result.distanceBetween(col);
            foreach (var color in bucketColors)
            {
                var dst = color.distanceBetween(col);
                if (dst < minDist)
                {
                    result = color;
                    minDist = dst;
                }
            }

            return result;
        }

        
    }

    class ClothestColorFinder
    {
        ColorBucket[] buckets;
        
        public ClothestColorFinder(IEnumerable<RGBColor> colors, int bucketsCount)
        {
            buckets = new ColorBucket[bucketsCount];
            var sorted = colors.OrderBy(c => c.eucDistance).ToArray();
            var colCount = colors.Count();
            var bucketSize = colCount / bucketsCount;
            var lastBucketSize = bucketSize + colCount % bucketsCount;

            for (int i = 0; i < bucketsCount; i++)
            {
                var bucketColors = new RGBColor[i == bucketsCount - 1 ? lastBucketSize : bucketSize];
                var startColorIndex = i * bucketSize;
                var endColorIndex = startColorIndex + bucketColors.Length;
                int counter = 0;

                for (int j = startColorIndex; j < endColorIndex; j++)
                {
                    bucketColors[counter++] = sorted[j];
                }

                buckets[i] = new ColorBucket(bucketColors, i == 0 ? 0 : buckets[i - 1].endEucDistance);
            }
        }

        public RGBColor apply(RGBColor color)
        {
            for (int i = buckets.Length - 1; i >= 0; i--)
            {
                var bucket = buckets[i];

                if (bucket.startEucDistance <= color.eucDistance)
                {
                    return bucket.ClothestColor(color);
                }
            }

            throw new Exception("WTF exception: you've performed some bullshit don't do so in the future");
            
        }
    }
}
