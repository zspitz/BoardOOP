namespace BoardOOP;

public class Board {
    public int Height { get; }
    public int Width { get; }
    public int CurrentLevel { get; set; }

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

        Snake snake = new('*', Height, Width);
        snake.DrawHead();

        while (true) {
            Directions direction;
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.UpArrow:
                    direction = Directions.Up;
                    break;
                case ConsoleKey.DownArrow:
                    direction = Directions.Down;
                    break;
                case ConsoleKey.RightArrow:
                    direction = Directions.Right;
                    break;
                case ConsoleKey.LeftArrow:
                    direction = Directions.Left;
                    break;
                default:
                    continue;
            }

            var newHead = snake.CanMove(direction);
            if (newHead is null) {
                continue;
            }

            bool newLevel = false;
            if (snake.HasPoint(newHead)) {
                newLevel = true;
            } else {
                foreach (var wall in Walls) {
                    if (wall.HasPoint(newHead)) {
                        // פסילה
                        newLevel = true;
                        break;
                    }
                }
            }

            if (newLevel) {
                Console.Clear();
                foreach (var wall in Walls) {
                    wall.Draw(Height, Width);
                }

                snake = new('*', Height, Width);
                snake.DrawHead();
                continue;
            }

            snake.Move(direction);
        }
    }
}
