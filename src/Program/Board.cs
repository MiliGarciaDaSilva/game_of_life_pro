using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Ucu.Poo.GameOfLife;

//-------------------------------------------Crear tablero------------------------------------------------------------//
public class Board
{
    private bool[,] cells;

    public int Width => cells.GetLength(0);
    public int Height => cells.GetLength(1);

    public Board(int width, int height)
    {
        cells = new bool[width, height];
    }
    

    public bool[,] GetCells()
    {
        return cells;
    }

    public void SetCells(bool[,] newCells)
    {
        cells = newCells;
    }
}

// La clase Board tiene una única razón de cambio, que es cómo se representa el tablero
// Board es "experto" en las dimensiones del tablero, debe conocer Width y Height

//-------------------------------------------Reglas del juego---------------------------------------------------------//
public class Rules
{
    public void NextGeneration(Board board)
    {
        bool[,] gameBoard = board.GetCells();
        int boardWidth = board.Width;
        int boardHeight = board.Height;

        bool[,] cloneboard = new bool[boardWidth, boardHeight];
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                int aliveNeighbors = 0;
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < boardWidth && j >= 0 && j < boardHeight && gameBoard[i, j])
                        {
                            aliveNeighbors++;
                        }
                    }
                }

                if (gameBoard[x, y])
                {
                    aliveNeighbors--;
                }

                if (gameBoard[x, y] && aliveNeighbors < 2)
                {
                    // Celula muere por baja población
                    cloneboard[x, y] = false;
                }
                else if (gameBoard[x, y] && aliveNeighbors > 3)
                {
                    // Celula muere por sobrepoblación
                    cloneboard[x, y] = false;
                }
                else if (!gameBoard[x, y] && aliveNeighbors == 3)
                {
                    // Celula nace por reproducción
                    cloneboard[x, y] = true;
                }
                else
                {
                    // Celula mantiene el estado que tenía
                    cloneboard[x, y] = gameBoard[x, y];
                }
            }
        }
        board.SetCells(cloneboard);
    }
}

// La clase rules tiene una única razón de cambio, que sería el caso de que cambiaran las reglas del juego
// Rules es experta únicamente en decidir si una célula está viva o muerta, evaluando el estado de sus vecinos

//-------------------------------------------Inicializar tablero------------------------------------------------------//
public class BoardLoader
{
    public Board LoadFromFile(string url)
    {
        string content = File.ReadAllText(url);
        string[] contentLines = content.Split('\n');
        bool[,] board = new bool[contentLines.Length, contentLines[0].Length];
        for (int  y=0; y<contentLines.Length;y++)
        {
            for (int x=0; x<contentLines[y].Length; x++)
            {
                if(contentLines[y][x]=='1')
                {
                    board[y,x]=true;
                }
            }
        }
        var newBoard = new Board(board.GetLength(0), board.GetLength(1));
        newBoard.SetCells(board);
        return newBoard;
    }
}

// La clase BoardLoader tiene una única razón de cambio, el formato del archivo
// Es experto en cómo interpretar el archivo y transformarlo en una matriz

//-------------------------------------------Imprimir tablero---------------------------------------------------------//
public class BoardPrinter
{
    public void Print(Board board)
    {
        int width = board.Width;
        int height = board.Height;
        bool[,] b = board.GetCells();

        Console.Clear();
        StringBuilder s = new StringBuilder();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (b[x, y])
                {
                    s.Append("|X|");
                }
                else
                {
                    s.Append("___");
                }
            }
            s.Append("\n");
        }
        Console.WriteLine(s.ToString());
    }
}

// La clase BordPrinter tiene una única razón de cambio, el estilo de impresión del tablero
// Se especializa en renderizar el tablero 