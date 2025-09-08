using System;
using System.Threading;

namespace Ucu.Poo.GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "board.txt";  
            
            BoardLoader loader = new BoardLoader();
            BoardPrinter printer = new BoardPrinter();
            Rules rules = new Rules();
            
            Board board = loader.LoadFromFile(path);
            
            for (int generation = 0; generation < 100; generation++)
            {
                printer.Print(board);
                rules.NextGeneration(board);
                Thread.Sleep(300);
            }
        }
    }
}