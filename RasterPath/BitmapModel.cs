using System;
public class BitmapModel {

    public readonly int width, height;
    private byte[] input;

    public BitmapModel(byte[] input, int width, int totalArraySize) {
        this.width = width;
        this.height = totalArraySize/width;
        if (totalArraySize%width != 0)
            throw new ArgumentException();
        this.input = input;
    }

    public byte this[int x, int y] {
        get { return input[y * this.width + x]; }
        set { input[y * this.width + x] = value; }
    }
}