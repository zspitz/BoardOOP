using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace BoardOOP;
public class Shape {
    protected readonly char filler;
    protected List<(int x, int y)> points = new();

    public bool HasPoint((int x, int y)? point) {
        if (point is null) { return false; }
        return points.Contains(point.Value);
    }

    public void Draw() {
        foreach (var (x, y) in points) {
            Console.SetCursorPosition(x, y);
            Console.Write(filler);
        }
    }

    public Shape(char filler, IEnumerable<(int x, int y)>? points = default) {
        this.filler = filler;

        if (points is not null) {
            foreach (var point in points) {
                this.points.Add(point);
            }
        }
    }

    public static Shape CreateRandom(int boardHeight, int boardWidth) {
        Random rnd = new Random();
        List<(int x, int y)> points = new();
        char filler = ' ';

        while (true) {
            var top = rnd.Next(0, boardHeight);
            var left = rnd.Next(0, boardWidth);

            switch (rnd.Next(0, 2)) {
                case 0:
                    // קו
                    filler = '=';

                    var length = rnd.Next(2, 11);
                    if (length + left > boardWidth) { continue; }

                    for (int i = 0; i < length; i++) {
                        points.Add((i + left, top));
                    }
                    break;

                case 1:
                    // משולש
                    filler = '#';

                    var height = rnd.Next(2, 10);
                    if (height + top > boardHeight || height + left > boardWidth) {
                        continue;
                    }

                    for (int relativeX = 0; relativeX < height; relativeX++) {
                        for (int relativeY = 0; relativeY <= relativeX; relativeY++) {
                            points.Add((relativeX + left, relativeY + top));
                        }
                    }

                    //for (int y = top; y < height + top; y++) {
                    //    for (int x = left; x <= y; x++) {
                    //        points.Add((x, y));
                    //    }
                    //}
                    break;
            }

            break;
        }

        return new Shape(filler, points);
    }

    public static bool HasCollision(Shape shape1, Shape shape2) {
        foreach (var (x1,y1) in shape1.points) {
            foreach (var (x2,y2) in shape2.points) {
                if (x1 == x2 && y1 == y2) {
                    return true;
                }
            }
        }
        return false;
    }
}
