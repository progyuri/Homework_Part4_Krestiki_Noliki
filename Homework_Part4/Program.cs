using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Part4
{
    internal class Program
    {
        static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char currentPlayer = 'X';
        static bool isPlayingAgainstComputer = true;
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру 'Крестики-Нолики'!");
            Console.WriteLine("Выберите режим игры:");
            Console.WriteLine("1. Играть против компьютера");
            Console.WriteLine("2. Играть против другого игрока");

            var choice = Console.ReadLine();
            if (choice == "2")
            {
                isPlayingAgainstComputer = false;
            }

            // Определяем, кто ходит первым
            currentPlayer = random.Next(0, 2) == 0 ? 'X' : 'O';
            Console.WriteLine($"Первым ходит: {currentPlayer}");

            // Основной цикл игры
            while (true)
            {
                DrawBoard();
                if (currentPlayer == 'X' || !isPlayingAgainstComputer)
                {
                    PlayerMove();
                }
                else
                {
                    ComputerMove();
                }

                if (CheckWin())
                {
                    DrawBoard();
                    Console.WriteLine($"Победитель: {currentPlayer}");
                    break;
                }

                if (CheckDraw())
                {
                    DrawBoard();
                    Console.WriteLine("Ничья!");
                    break;
                }

                // Переключение игрока
                currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
            }
        }

        static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("-------------");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"| {board[i * 3]} | {board[i * 3 + 1]} | {board[i * 3 + 2]} |");
                Console.WriteLine("-------------");
            }
        }

        static void PlayerMove()
        {
            int move;
            while (true)
            {
                Console.WriteLine($"Игрок {currentPlayer}, введите номер клетки (1-9):");
                if (int.TryParse(Console.ReadLine(), out move) && move >= 1 && move <= 9 && board[move - 1] != 'X' && board[move - 1] != 'O')
                {
                    board[move - 1] = currentPlayer;
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ход, попробуйте снова.");
                }
            }
        }

        static void ComputerMove()
        {
            int move;
            while (true)
            {
                move = random.Next(1, 10);
                if (board[move - 1] != 'X' && board[move - 1] != 'O')
                {
                    board[move - 1] = 'O';
                    break;
                }
            }
        }

        static bool CheckWin()
        {
            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8},
                {0, 3, 6},
                {1, 4, 7},
                {2, 5, 8},
                {0, 4, 8},
                {2, 4, 6}
            };

            for (int i = 0; i < 8; i++)
            {
                if (board[winningCombinations[i, 0]] == currentPlayer &&
                    board[winningCombinations[i, 1]] == currentPlayer &&
                    board[winningCombinations[i, 2]] == currentPlayer)
                {
                    return true;
                }
            }

            return false;
        }

        static bool CheckDraw()
        {
            foreach (char cell in board)
            {
                if (cell != 'X' && cell != 'O')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
