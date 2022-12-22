using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BoardOOP; 
public class Snake {
    private static Random rnd = new();
    public char Char { get; }

    public (int x, int y) Head => Body[^1];
    //public (int x, int y) Head {
    //    get {
    //        return Body[Body.Count - 1];
    //    }
    //}

    public List<(int x, int y)> Body { get; } = new();

    public Directions? LastDirection { get; private set; } = null;

    public Snake(char character, int boardHeight, int boardWidth) {
        Char = character;
        // TODO handle wall overlap outside of Snake class
        Body.Add((
            rnd.Next(1, boardWidth),
            rnd.Next(1, boardHeight)
        ));
    }

    public void DrawHead() {
        Console.SetCursorPosition(Head.x, Head.y);
        Console.Write(Char);
    }

    private (int x, int y) getNewHead(Directions direction) {
        (int x, int y) newHead = Head;
        switch (direction) {
            case Directions.Left:
                newHead.x -= 1;
                break;
            case Directions.Right:
                newHead.x += 1;
                break;
            case Directions.Up:
                newHead.y -= 1;
                break;
            case Directions.Down:
                newHead.y += 1;
                break;
        }
        return newHead;
    }

    public (int x, int y)? CanMove(Directions direction) {
        if (
            LastDirection == Directions.Up && direction == Directions.Down ||
            LastDirection == Directions.Down && direction == Directions.Up ||
            LastDirection == Directions.Right && direction == Directions.Left ||
            LastDirection == Directions.Left && direction == Directions.Right
        ) {
            return null;
        }

        return getNewHead(direction);
    }

    public void Move(Directions direction) {
        var newHead = getNewHead(direction);
        Body.Add(newHead);
        DrawHead();
        LastDirection = direction;
    }

    public bool HasPoint((int x, int y)? newHead) {
        if (newHead is null) { return false; }
        return Body.Contains(newHead.Value);
    }
}
