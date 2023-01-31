using System;
namespace Sudoko
{
    public class SudokuCheckNumberExists
    {
        public static bool CheckNumberInsertPossibility(char[][] board, int horizontal, int vertical, char num)
        {
            if (Box(board, horizontal, vertical, num)) return false;
            //if (Horizontal(board, horizontal, num)) return false;
            if (Vertical(board, vertical, num)) return false;
            //if (horizontal == vertical) if (Diagonal(board, num)) return false;
            return true;
        }

        public static bool Box(char[][] board, int horizontal, int vertical, char num)
        {
            int x = horizontal / 3;
            int y = vertical / 3;
            for (int i = (x * 3); i < ((x + 1) * 3); i++)
            {
                for (int j = (y * 3); j < ((y + 1) * 3); j++)
                {
                    if (num == board[i][j]) return true;
                }
            }
            return false;
        }

        public static bool Horizontal(char[][] board, int horizontal, char num)
        {
            for (int i = 0; i < 9; i++) if (num == board[horizontal][i]) return true;
            return false;
        }

        public static bool Vertical(char[][] board, int vertical, char num)
        {
            for (int i = 0; i < 9; i++) if (num == board[i][vertical]) return true;
            return false;
        }

        public static bool Diagonal(char[][] board, char num)
        {
            for (int i = 0; i < 9; i++) if (num == board[i][i]) return true;
            return false;
        }
    }
}

