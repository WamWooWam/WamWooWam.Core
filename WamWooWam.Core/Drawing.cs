#if NET35 || NET40 || NET45 || NET462

using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

#endif
using System;

namespace WamWooWam.Core
{
    public static class Drawing
    {

#if NET35 || NET40 || NET45 || NET462
        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;

            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }

            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
#endif

        public static void ScaleProportions(ref double currentWidth, ref double currentHeight, double maxWidth, double maxHeight)
        {
            if (currentWidth <= maxWidth && currentHeight <= maxHeight)
            {
                return;
            }
            else
            {
                var ratioX = maxWidth / currentWidth;
                var ratioY = maxHeight / currentHeight;
                var ratio = Math.Min(ratioX, ratioY);

                currentWidth = (currentWidth * ratio);
                currentHeight = (currentHeight * ratio);
            }
        }

        public static void Scale(ref double width, ref double height, double maxWidth, double maxHeight, StretchMode mode = StretchMode.Uniform)
        {
            if (mode == StretchMode.None)
            {
                return;
            }

            if (mode == StretchMode.Fill)
            {
                width = maxWidth;
                height = maxHeight;
                return;
            }

            var ratioX = (double)maxWidth / width;
            var ratioY = (double)maxHeight / height;
            double ratio = 0;

            if (mode == StretchMode.Uniform)
            {
                ratio = Math.Min(ratioX, ratioY);
            }

            if (mode == StretchMode.UniformToFill)
            {
                ratio = Math.Max(ratioX, ratioY);
            }

            width = (int)(width * ratio);
            height = (int)(height * ratio);
        }

        public enum StretchMode
        {
            None, Fill, Uniform, UniformToFill
        }
    }
}
