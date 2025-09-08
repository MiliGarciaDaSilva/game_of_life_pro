using System;
using System.Threading;

namespace Ucu.Poo.GameOfLife
{
    class Programgit
    {
        static void Main(string[] args)
        {
            string path = "Board.txt";  
            
            BoardLoader loader = new BoardLoader();
            BoardPrinter printer = new BoardPrinter();
            Rules rules = new Rules();
            
            Board board = loader.LoadFromFile(path);
            
            for (int generation = 0; generation < 10; generation++)
            {
                Console.WriteLine($"Generación {generation}");
                printer.Print(board);
                rules.NextGeneration(board);
                Thread.Sleep(500);
            }
        }
    }
}