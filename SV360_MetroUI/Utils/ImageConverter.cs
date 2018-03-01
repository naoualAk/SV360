using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//using DALSA.SaperaProcessing.CPro;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading;

namespace SV360.Utils
{
    [Obsolete("Plus utilisé")]
    static class ImageConverter
    {
       /* 

        [DllImport("kernel32.dll")]
        private static extern void CopyMemory(IntPtr pDest, IntPtr pSrc, int length);
       private static CProImage imageConverted;       

       static ImageConverter()
        {
           imageConverted = new CProImage();
        }

       static private bool IsWidthCompatible(CProImage image)
       {
           int mult = IntPtr.Size*8 / CProData.GetFormatNumBits(image.Format);
           return (image.Width % mult == 0);
       }

       static private Bitmap Copy8BitCProImageToBmp(CProImage image)
       {
          int Width = image.Width;
          int Height = image.Height;  

          IntPtr DataAdress = image.GetData(true);
          Bitmap bmp; 
          BitmapData bData;
     
          if (IsWidthCompatible(image))
              bmp = new Bitmap(Width, Height,Width, PixelFormat.Format8bppIndexed, DataAdress);
          else
          {
              bmp = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);
              bData = bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
              // Copy the bytes to the bitmap object
              switch (IntPtr.Size)
              {
                  case 4:
                      {
                          for (int y = 0; y < Height; y++)
                          {
                              IntPtr pDst = new IntPtr((bData.Scan0).ToInt32() + (y * bData.Stride));
                              IntPtr pSrc = new IntPtr(DataAdress.ToInt32() + y * Width);
                              CopyMemory(pDst, pSrc, Width);
                          }
                          break;
                      }
                  case 8:
                      {
                          for (int y = 0; y < Height; y++)
                          {
                              IntPtr pDst = new IntPtr((Int64)((bData.Scan0).ToInt64() + (y * bData.Stride)));
                              IntPtr pSrc = new IntPtr((Int64)(DataAdress.ToInt64() + y * Width));
                              CopyMemory(pDst, pSrc, Width);
                          }
                          break;
                      }
              }
              bmp.UnlockBits(bData);
          }

          

          //Change the palette from 256 color levels to 256 grayscale levels 
          ColorPalette pal = bmp.Palette;
          for (int i = 0; i < 256; i++)
             pal.Entries[i] = Color.FromArgb(255, i, i, i);
          bmp.Palette = pal;

          return bmp;
       }

       static private Bitmap CopyRgbCProImageToBmp(CProImage image)
       {
          int Width = image.Width;
          int Height = image.Height;

          Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format32bppRgb);
          IntPtr DataAdress = image.GetData(true);
          BitmapData bData = bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
          // Copy the bytes to the bitmap object
          CopyMemory(bData.Scan0, DataAdress, Width * Height * 4);
          bmp.UnlockBits(bData);
     
          return bmp;
       }

        static public Bitmap GetBitmapFromCProImage(CProImage img)
        {            
           switch(img.Format)
	        {  
              case CProData.FormatEnum.FormatUByte:
                 {
                    return Copy8BitCProImageToBmp(img);
                 }
              case CProData.FormatEnum.FormatUShort :
              case CProData.FormatEnum.FormatShort :
              case CProData.FormatEnum.FormatByte:
              case CProData.FormatEnum.FormatFloat:
                {
                   // realloc imageconverted if size has changed or format is RGB
                   if (img.Height != imageConverted.Height || img.Width != imageConverted.Width || 
                       imageConverted.Format == CProData.FormatEnum.FormatRgb)
                   {
                      imageConverted.Set(img.Width, img.Height, CProData.FormatEnum.FormatUByte);
                   }
                   //Graphics doesn't support those kind of images , So convert image to 8 bit 
                   imageConverted.ConvertFormat(img,CProImage.ConvertMode.ConvertBias);
                   return Copy8BitCProImageToBmp(imageConverted);
                }
              case CProData.FormatEnum.FormatRgb :
                {
                   return CopyRgbCProImageToBmp(img);
                }
              case CProData.FormatEnum.FormatLab:
              case CProData.FormatEnum.FormatHsv:
              case CProData.FormatEnum.FormatYuv:
                {
                   // realloc imageconverted if size has changed or format is Ubyte
                   if (img.Height != imageConverted.Height || img.Width != imageConverted.Width ||
                       imageConverted.Format == CProData.FormatEnum.FormatUByte)
                   {
                      imageConverted.Set(img.Width, img.Height, CProData.FormatEnum.FormatRgb);
                   }
                   //Graphics doesn't support those kind of image , So convert image to RGB image 
                   imageConverted.ConvertFormat(img, CProImage.ConvertMode.ConvertBias);
                   return CopyRgbCProImageToBmp(imageConverted);
                }
              default:
                 {                      
                     return null;
                 }

            }
        }

        static public void DisplayImage(CProImage Image, Graphics g, Rectangle dstRec, Rectangle srcRec)
        {
           if (!Image.Valid)
              return;
           // Set NearestNeigbor to be consistent with C++. Morever is faster than the default (Bilinear)
           g.InterpolationMode = InterpolationMode.NearestNeighbor;
           Bitmap bmp = GetBitmapFromCProImage(Image);
           if(bmp != null)
                g.DrawImage(bmp, dstRec, srcRec, GraphicsUnit.Pixel);
        }        */
    }
}
