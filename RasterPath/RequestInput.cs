using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#nullable enable

public class RequestInput {
    public readonly Geo start = new Geo(47, 105);
    public readonly Geo finish = new Geo(130, 252);
    public readonly String filePath = "arielLowRes.bmp";
    public readonly Bitmap image;
    public readonly CoefficientCost coefficientCost = new CoefficientCost(1);

    public RequestInput() {
        image = (Bitmap)Image.FromFile(filePath);
    }

    private List<Image> GetAllPages(string file)
    {
        List<Image> images = new List<Image>();
        Bitmap bitmap = (Bitmap)Image.FromFile(file);
        int count = bitmap.GetFrameCount(FrameDimension.Page);
        for (int idx = 0; idx < count; idx++)
        {
            // save each frame to a bytestream
            bitmap.SelectActiveFrame(FrameDimension.Page, idx);
            System.IO.MemoryStream byteStream = new System.IO.MemoryStream();
            bitmap.Save(byteStream, ImageFormat.Tiff);

            // and then create a new Image from it
            images.Add(Image.FromStream(byteStream));
        }
        return images;
    }
}

public struct CoefficientCost {
    public double color;

    public CoefficientCost(double color) {
        this.color = color;
    }
}