using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#nullable enable

public class BitmapModelAdapter {

    public static BitmapModel getBitmapModel(Bitmap bitmap) {
        //var greyBitmap = convertRGBtoGreyscale(bitmap);
        byte[] array;
        convertRGBtoGreyscale(bitmap, out array);
        BitmapModel result = new BitmapModel(array, bitmap.Width, bitmap.Width*bitmap.Height);
        return result;
    }

    //see https://stackoverflow.com/questions/17352061/fastest-way-to-convert-image-to-byte-array
    private static byte[] imageToByteArray(Image image)
    {
        using(var ms = new MemoryStream())
        {
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }

    //see https://stackoverflow.com/questions/2265910/convert-an-image-to-grayscale
    private static void convertRGBtoGreyscale(Bitmap c, out byte[] array) {
             int x, y;

            array = new byte[c.Width*c.Height];
             // Loop through the images pixels to reset color.
             for (y = 0; y < c.Height; y++)
             {
                 for (x = 0; x < c.Width; x++)
                 {
                     Color pixelColor = c.GetPixel(x, y);
                     Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                     array[y*c.Width + x] = pixelColor.R;
                     //c.SetPixel(x, y, newColor); // Now greyscale
                 }
             }
            //d = c;   // d is grayscale version of c  
    }

}

    