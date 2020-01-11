using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class FileOutput {

    public FileOutput(Bitmap bitmap) {
        bitmap.Save("out.png");
    }

    public FileOutput(LinkedList<Geo> pathList, BitmapModel bitmapModel) {
        Bitmap bitmapFromlist = new Bitmap(bitmapModel.width, bitmapModel.height);
        Color printedColor = Color.Red;
        foreach (Geo item in pathList) {
            bitmapFromlist.SetPixel(item.x, item.y, printedColor);
        }
        bitmapFromlist.Save("out.png");
    }
}