using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
        const char _emptySymbol = '-';
        const int _lineSize = 3;
        static Player[] _players = new Player[2];
        static char[] _cells = Enumerable.Repeat(_emptySymbol, _lineSize ^ 2).ToArray();

        static void ShowCells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{_cells[0]}-|-{_cells[1]}-|-{_cells[2]}-");
            Console.WriteLine($"-{_cells[3]}-|-{_cells[4]}-|-{_cells[5]}-");
            Console.WriteLine($"-{_cells[6]}-|-{_cells[7]}-|-{_cells[8]}-");        
        }

        static bool IsCellInRange(int cellIndex)
        {
            return cellIndex <= _cells.Length && cellIndex > 0;
        }

        static bool IsCellOwned(int cellIndex)
        {
            return _players.Any(p => p.PlayerSymbol == _cells[cellIndex - 1]);
        }

        static void MakeMove(Player player)
        {
            string raw_cell;
            int cell;
            Console.Write(player.PlayerName);
            Console.Write(",введите номер ячейки,сделайте свой ход:");

            raw_cell = Console.ReadLine();
            while (!int.TryParse(raw_cell, out cell)
                || !IsCellInRange(cell)
                || IsCellOwned(cell))
            {
                Console.Write("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                raw_cell = Console.ReadLine();
                Console.WriteLine();
            }
            _cells[cell - 1] = player.PlayerSymbol;            
        }

        static char GetCompletedLineSymbol()
        {
            for (int i = 0; i < 3; i++)
                if (IsHorizontalLineCompleted(i) || IsVerticalLineCompleted(i))
                    return _cells[i];

            int centerPointIndex = _cells.Length / 2;
            if (IsDiagonalLineCompleted())
                return _cells[centerPointIndex];

            return '-';
        }

        static bool IsDiagonalLineCompleted()
        {
            return (_cells[2] == _cells[4] && _cells[4] == _cells[6]) || (_cells[0] == _cells[4] && _cells[4] == _cells[8]);
        }

        static bool IsVerticalLineCompleted(int rowIndex)
        {
            return _cells[rowIndex] == _cells[rowIndex + 3] && _cells[rowIndex + 3] == _cells[rowIndex + 6];
        }

        static bool IsHorizontalLineCompleted(int columnIndex)
        {
            return _cells[columnIndex * 3] == _cells[columnIndex * 3 + 1] && _cells[columnIndex * 3 + 1] == _cells[columnIndex * 3 + 2];
        }

        static void PrintResult(char winningSymbol)
        {
            var winner = _players.First(p => p.PlayerSymbol == winningSymbol);
            Console.WriteLine($"{winner.PlayerName} вы  выиграли поздравляем, {string.Join(", ", _players.Where(p => p != winner))} -  а вы проиграли...");
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                var firstPlayerName = Console.ReadLine();
                _players[0] = new Player(firstPlayerName, 'X');

                Console.Write("Введите имя второго игрока: ");
                var secondPlayerName = Console.ReadLine();
                _players[1] = new Player(secondPlayerName, 'O');
                Console.WriteLine();
            } while (_players[0].PlayerName == _players[1].PlayerName);

            ShowCells();

            int minMovesToCompleteLine = (_lineSize - 1) * _players.Length;
            for (int move = 0; move < _cells.Length; move++)
            {
                int currentPlayerIndex = move % _players.Length;
                MakeMove(_players[currentPlayerIndex]);

                ShowCells();

                if (move >= minMovesToCompleteLine)
                {
                    var completedLineSymbol = GetCompletedLineSymbol();
                    if (completedLineSymbol != _emptySymbol)
                    {
                        PrintResult(completedLineSymbol);
                        return;
                    }
                }

            }
        }
    }
}
