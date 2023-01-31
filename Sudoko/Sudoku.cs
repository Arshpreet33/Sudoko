using System;
using System.Linq;

namespace Sudoko
{
    public class Sudoku
    {
        public static char[][] SolveSudoku(char[][] board)
        {
            bool solved = true;
            for (int i = 0; i < 9; i++)
            {
                var EmptyPlaces = new List<int>();
                bool rowSolved = true;
                var misssingNumbers = new Dictionary<int, char> { { 1, '1' }, { 2, '2' }, { 3, '3' }, { 4, '4' }, { 5, '5' }, { 6, '6' }, { 7, '7' }, { 8, '8' }, { 9, '9' } };
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '.')
                    {
                        rowSolved = false;
                        EmptyPlaces.Add(j);
                    }
                    char num = Convert.ToChar((j + 1).ToString());
                    if (board[i].Contains(num)) misssingNumbers.Remove(j + 1);
                }
                if (rowSolved) continue;

                if (!FillProbabilityNumber(board, i, EmptyPlaces, misssingNumbers)) solved = false;
            }

            if (solved) return board;
            else return SolveSudoku(board);
        }

        public static bool FillProbabilityNumber(char[][] board, int x, List<int> EmptyPlaces, Dictionary<int, char> misssingNumbers)
        {
            bool solved = true;

            foreach (KeyValuePair<int, char> num in misssingNumbers)
            {
                int possibilityCount = 0;
                int possibilityIndex = -1;
                foreach (var j in EmptyPlaces)
                {
                    if (SudokuCheckNumberExists.CheckNumberInsertPossibility(board, x, j, num.Value))
                    {
                        possibilityCount++;
                        possibilityIndex = j;
                    }
                }

                if (possibilityCount == 1 && possibilityIndex > -1)
                {
                    board[x][possibilityIndex] = num.Value;
                }
                else
                {
                    solved = false;
                }
            }
            return solved;
        }

        //public static char[][] SolveSudoku(char[][] board)
        //{
        //    bool solved = true;
        //    for (int i = 0; i < 9; i++)
        //    {
        //        bool rowSolved = true;
        //        for (int k = 0; k < 9; k++)
        //        {
        //            int possibilityCount = 0;
        //            int possibilityIndex = -1;
        //            bool alreadySolved = false;
        //            char num = Convert.ToChar((k + 1).ToString());
        //            for (int j = 0; j < 9; j++)
        //            {
        //                if (board[i][j] == num)
        //                {
        //                    alreadySolved = true;
        //                    break;
        //                }
        //                if (board[i][j] != '.')
        //                {
        //                    continue;
        //                }
        //                if (SudokuCheckNumberExists.CheckNumberInsertPossibility(board, i, j, num))
        //                {
        //                    possibilityCount++;
        //                    possibilityIndex = j;
        //                }
        //            }

        //            if (alreadySolved) continue;
        //            else if (possibilityCount == 1 && possibilityIndex > -1)
        //            {
        //                board[i][possibilityIndex] = num;
        //            }
        //            else
        //            {
        //                rowSolved = false;
        //            }
        //        }
        //        if (!rowSolved) solved = false;
        //    }

        //    if (solved) return board;
        //    else return SolveSudoku(board);
        //}

        //public static bool FillProbabilityNumber(char[][] board, int x)
        //{
        //    //int[][] ProbabilityMatrix = new int[9][];
        //    bool solved = true;

        //    for (int i = 0; i < 9; i++)
        //    {
        //        int possibilityCount = 0;
        //        int possibilityIndex = -1;
        //        bool alreadySolved = false;
        //        //ProbabilityMatrix[i] = new int[9];
        //        char num = Convert.ToChar((i + 1).ToString());
        //        for (int j = 0; j < 9; j++)
        //        {
        //            if (board[x][j] == num)
        //            {
        //                alreadySolved = true;
        //                break;
        //            }
        //            if (board[x][j] != '.')
        //            {
        //                continue;
        //            }
        //            if (SudokuCheckNumberExists.CheckNumberInsertPossibility(board, x, j, num))
        //            {
        //                possibilityCount++;
        //                possibilityIndex = j;
        //            }
        //        }

        //        if (alreadySolved) continue;
        //        else if (possibilityCount == 1 && possibilityIndex > -1)
        //        {
        //            board[x][possibilityIndex] = num;
        //        }
        //        //else if (possibilityCount == 0)
        //        //{
        //        //    int rowEmptyCount = 0;
        //        //    int rowEmptyIndex = -1;
        //        //    for (int j = 0; j < 9; j++)
        //        //    {
        //        //        if (board[x][j] == '.')
        //        //        {
        //        //            rowEmptyCount++;
        //        //            rowEmptyIndex = j;
        //        //        }
        //        //    }
        //        //    if (rowEmptyCount > 0)
        //        //    {
        //        //        if (rowEmptyCount == 1 && rowEmptyIndex > -1)
        //        //        {
        //        //            board[x][rowEmptyIndex] = num;
        //        //        }
        //        //        else
        //        //        {
        //        //            solved = false;
        //        //            Console.WriteLine("something is wrong");
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        Console.WriteLine("something is definitely very wrong");
        //        //    }
        //        //}
        //        else
        //        {
        //            solved = false;
        //        }
        //    }
        //    return solved;
        //}
    }
}
