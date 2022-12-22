namespace BoardOOP;
public abstract class Shape {
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

    public Shape(char filler) {
        this.filler = filler;
    }
}
