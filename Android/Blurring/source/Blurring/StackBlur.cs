/*
 Copyright (c) 2010, Sungjin Han <meinside@gmail.com>
 All rights reserved.
 
 Redistribution and use in source and binary forms, with or without
 modification, are permitted provided that the following conditions are met:

  * Redistributions of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above copyright
    notice, this list of conditions and the following disclaimer in the
    documentation and/or other materials provided with the distribution.
  * Neither the name of meinside nor the names of its contributors may be
    used to endorse or promote products derived from this software without
    specific prior written permission.

 THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Runtime.CompilerServices;
using Android.Graphics;

namespace Blurring
{
    /// <summary>
    /// This class is a variety of a Gaussian blur. It's slow - use RenderScript if you can.
    /// </summary>
    /// <remarks>
    /// Stack Blur Algorithm by Mario Klingemann - mario@quasimondo.com
    /// 
    /// This code is a C# port of the Java code which can be found at:
    /// http://www.java2s.com/Code/Android/2D-Graphics/Generateablurredbitmapfromgivenone.htm
    ///
    /// The following code is another example (Java based):
    /// http://incubator.quasimondo.com/processing/stackblur.pde
    /// </remarks>
    internal static class StackBlur
    {
        public static void Blur(Bitmap original, Bitmap blurred, int radius)
        {
            if (radius < 1)
            {
                throw new ArgumentOutOfRangeException("radius", "Radius must be > 0.");
            }

            int width = original.Width;
            int height = original.Height;
            int wm = width - 1;
            int hm = height - 1;
            int wh = width * height;
            int div = radius + radius + 1;
            int[] a = new int[wh];
            int[] r = new int[wh];
            int[] g = new int[wh];
            int[] b = new int[wh];
            int asum, rsum, gsum, bsum;
            int x, y, i;
            int p1, p2;
            int[] vmin = new int[Math.Max(width, height)];
            int[] vmax = new int[Math.Max(width, height)];
            int[] dv = new int[256 * div];
            for (i = 0; i < 256 * div; i++)
            {
                dv[i] = i / div;
            }

            int[] blurredBitmap = new int[wh];
            original.GetPixels(blurredBitmap, 0, width, 0, 0, width, height);
            Premultiply(blurredBitmap);

            int yw = 0;
            int yi = 0;

            for (y = 0; y < height; y++)
            {
                asum = 0;
                rsum = 0;
                gsum = 0;
                bsum = 0;
                for (i = -radius; i <= radius; i++)
                {
                    int p = blurredBitmap[yi + Math.Min(wm, Math.Max(i, 0))];
                    asum += A(p);
                    rsum += R(p);
                    gsum += G(p);
                    bsum += B(p);
                }
                for (x = 0; x < width; x++)
                {
                    a[yi] = dv[asum];
                    r[yi] = dv[rsum];
                    g[yi] = dv[gsum];
                    b[yi] = dv[bsum];

                    if (y == 0)
                    {
                        vmin[x] = Math.Min(x + radius + 1, wm);
                        vmax[x] = Math.Max(x - radius, 0);
                    }
                    p1 = blurredBitmap[yw + vmin[x]];
                    p2 = blurredBitmap[yw + vmax[x]];

                    asum += A(p1) - A(p2);
                    rsum += R(p1) - R(p2);
                    gsum += G(p1) - G(p2);
                    bsum += B(p1) - B(p2);

                    yi++;
                }
                yw += width;
            }

            for (x = 0; x < width; x++)
            {
                asum = rsum = gsum = bsum = 0;
                int yp = -radius * width;
                for (i = -radius; i <= radius; i++)
                {
                    yi = Math.Max(0, yp) + x;
                    asum += a[yi];
                    rsum += r[yi];
                    gsum += g[yi];
                    bsum += b[yi];
                    yp += width;
                }
                yi = x;
                for (y = 0; y < height; y++)
                {
                    blurredBitmap[yi] = (dv[asum] << 24) | (dv[rsum] << 16) | (dv[gsum] << 8) | (dv[bsum]);
                    if (x == 0)
                    {
                        vmin[y] = Math.Min(y + radius + 1, hm) * width;
                        vmax[y] = Math.Max(y - radius, 0) * width;
                    }
                    p1 = x + vmin[y];
                    p2 = x + vmax[y];

                    asum += a[p1] - a[p2];
                    rsum += r[p1] - r[p2];
                    gsum += g[p1] - g[p2];
                    bsum += b[p1] - b[p2];

                    yi += width;
                }
            }

            Unpremultiply(blurredBitmap);
            blurred.SetPixels(blurredBitmap, 0, width, 0, 0, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Premultiply(int[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                int a = A(v[i]);
                var r = R(v[i]);
                int g = G(v[i]);
                int b = B(v[i]);
                v[i] = (C(a) << 24) | (C(r * a / 255f) << 16) | (C(g * a / 255f) << 8) | (C(b * a / 255f));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Unpremultiply(int[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                int a = A(v[i]);
                var r = R(v[i]);
                int g = G(v[i]);
                int b = B(v[i]);
                v[i] = (C(a) << 24) | (C(r * 255f / a) << 16) | (C(g * 255f / a) << 8) | (C(b * 255f / a));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int C(float v)
        {
            if (v > 255) return 255;
            if (v < 0) return 0;
            return (int)v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int B(int p)
        {
            return (p & 0x000000ff);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int G(int p)
        {
            return (p & 0x0000ff00) >> 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int R(int p)
        {
            return (p & 0x00ff0000) >> 16;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int A(int p)
        {
            return (int)((p & 0xff000000) >> 24);
        }
    }
}
