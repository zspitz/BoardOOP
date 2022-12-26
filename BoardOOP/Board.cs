namespace BoardOOP;

public class Board {
    public int Height { get; }
    public int Width { get; }
    public int CurrentLevel { get; private set; } = 0;
    Snake snake;
    List<Shape> shapes = new();

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
        startLevel();

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

            var newHead = snake.TryGetNewHead(direction);
            if (newHead is null) {
                continue;
            }

            bool newLevel = false;
            if (snake.HasPoint(newHead)) {
                newLevel = true;
            } else {
                bool continueOuter = false;
                foreach (var wall in Walls) {
                    if (wall.HasPoint(newHead)) {
                        continueOuter = true;
                        break;
                    }
                }
                if (continueOuter) {
                    continue;
                }
            }

            if (newLevel) {
                startLevel();
                continue;
            }

            snake.Move(direction);
        }
    }

    private void startLevel() {
        CurrentLevel += 1;

        Console.Clear();
        foreach (var wall in Walls) {
            wall.Draw(Height, Width);
        }

        var shapeCount = 
            shapes.Count == 0 ?
                new Random().Next(3, 7) :
                shapes.Count + 1;

        shapes.Clear();

        bool hasCollision;
        for (int i = 0; i < shapeCount; i++) {
            Shape newShape;
            do {
                hasCollision = false;
                newShape = Shape.CreateRandom(Height, Width);
                foreach (var shape in shapes) {
                    if (Shape.HasCollision(shape,newShape)) {
                        hasCollision = true;
                        break;
                    }
                }
            } while (hasCollision);

            shapes.Add(newShape);
        }

        foreach (var shape in shapes) {
            shape.Draw();
        }

        do {
            hasCollision = false;
            snake = new('*', Height, Width);
            foreach (var shape in shapes) {
                if (Shape.HasCollision(shape, snake)) {
                    hasCollision = true;
                    break;
                }
            }    
        } while (hasCollision);

        snake.DrawHead();
    }
}
