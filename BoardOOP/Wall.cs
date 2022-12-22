namespace BoardOOP;
public class Wall {
    readonly bool isHorizontal;
    readonly char filler;
    readonly bool atStart;

    List<(int x, int y)> points = new();

    public Wall(bool isHorizontal, char filler, bool atStart) {
        this.isHorizontal = isHorizontal;
        this.filler = filler;
        this.atStart = atStart;
    }

    public void Draw(int height, int width) {
        if (isHorizontal && atStart) {
            for (int i = 0; i <= width; i++) {
                points.Add((i, 0));
            }
        } else if (isHorizontal) {
            for (int i = 0; i <= width; i++) {
                points.Add((i, height));
            }
        } else if (!isHorizontal && atStart) {
            for (int i = 0; i <= height; i++) {
                points.Add((0, i));
            }
        } else {
            for (int i = 0; i <= height; i++) {
                points.Add((width, i));
            } 
        }

        foreach (var (x,y) in points) {
            Console.SetCursorPosition(x, y);
            Console.Write(filler);
        }
    }

    public bool HasPoint((int x, int y)? point) {
        if (point is null) { return false; }
        return points.Contains(point.Value);
    }
}
