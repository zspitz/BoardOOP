namespace BoardOOP;

public class Board {
    public int Height { get; }
    public int Width { get; }
    public int CurrentLevel { get; set; }

    int cursorX;
    int cursorY;
    Random rnd = new Random();

    public Wall[] Walls { get; }

    public Board(int height, int width) {
        Height = height;
        Width = width;

        Walls = new Wall[] {
            new(false, '|', true),
            new(false, ';', false),
            new(true, '-', true),
            new(true, '+', false)
        };

        /*
------------------
|                    ;
|                    ;
+++++++++++
        */

    }

    public void StartGame() {
        foreach (var wall in Walls) {
            wall.Draw(Height, Width);
        }

        cursorX = rnd.Next(0, Width);
        cursorY = rnd.Next(0, Height);

        while (true) {
            var newX = cursorX;
            var newY = cursorY;

            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.UpArrow:
                    newY -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    newY += 1;
                    break;
                case ConsoleKey.RightArrow:
                    newX += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    newX -= 1;
                    break;
            }

            bool newLevel = false;
            foreach (var wall in Walls) {
                if (wall.HasPoint(newX, newY)) {
                    // פסילה
                    newLevel = true;
                    break;
                }
            }

            if (newLevel) {
                Console.Clear();
                foreach (var wall in Walls) {
                    wall.Draw(Height, Width);
                }

                cursorX = rnd.Next(0, Width);
                cursorY = rnd.Next(0, Height);
            } else {
                cursorX = newX;
                cursorY = newY;
            }
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write('*');
        }
    }
}
