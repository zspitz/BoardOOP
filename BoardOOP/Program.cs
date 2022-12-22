namespace BoardOOP;

public class Program {
    static void Main(string[] args) {
        Console.SetWindowSize(81, 26);
        Console.SetBufferSize(81, 26);

        var board = new Board(25, 80);
        board.StartGame();
    }
}
