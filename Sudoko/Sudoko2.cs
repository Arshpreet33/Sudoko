using System;
namespace Sudoko
{
    public class Sudoko2
    {
        public Dictionary<int, Dictionary<int, char>> dictHorizontal { get; set; }
        public Dictionary<int, Dictionary<int, char>> dictVertical { get; set; }
        public Dictionary<string, Dictionary<string, char>> dictBox { get; set; }
        public Dictionary<string, Dictionary<int, char>> dictBoard { get; set; }
        public char[][] board { get; set; }

        public Sudoko2(char[][] Board)
        {
            board = Board;
            dictHorizontal = new Dictionary<int, Dictionary<int, char>>();
            dictVertical = new Dictionary<int, Dictionary<int, char>>();
            dictBox = new Dictionary<string, Dictionary<string, char>>();
            dictBoard = new Dictionary<string, Dictionary<int, char>>();
        }

        public char[][] StartGame()
        {
            BoardInitialize();
            bool result = SolveSudoku();
            if (result)
            {

            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = dictHorizontal[i][j];
                }
            }
            return board;
        }

        public void BoardInitialize()
        {
            for (int i = 0; i < 9; i++)
            {
                dictHorizontal.Add(i, new Dictionary<int, char>());

                for (int j = 0; j < 9; j++)
                {
                    if (!dictVertical.ContainsKey(j))
                        dictVertical.Add(j, new Dictionary<int, char>());
                    if (!dictBox.ContainsKey((i / 3).ToString() + (j / 3).ToString()))
                        dictBox.Add((i / 3).ToString() + (j / 3).ToString(), new Dictionary<string, char>());
                    if (board[i][j] == '.') continue;
                    dictHorizontal[i].Add(j, board[i][j]);
                    dictVertical[j].Add(i, board[i][j]);
                    dictBox[(i / 3).ToString() + (j / 3).ToString()].Add((i % 3).ToString() + (j % 3).ToString(), board[i][j]);
                }
            }
        }

        public bool SolveSudoku()
        {
            bool solved = true;

            for (int i = 0; i < 9; i++)
            {
                if (dictHorizontal[i].Count == 9) continue;

                bool rowSolved = true;
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] != '.') continue;

                    string dictKey = i.ToString() + j.ToString();

                    if (dictBoard.ContainsKey(dictKey))
                    {
                        if (dictBoard[dictKey].Count == 1)
                        {
                            InsertNumber(i, j, dictKey);
                            continue;
                        }
                    }
                    else
                    {
                        dictBoard[dictKey] = new Dictionary<int, char>();
                        CheckInsertPossibility(i, j, dictKey);
                    }

                    rowSolved = false;
                }

                if (!rowSolved) solved = false;
            }
            if (solved) return true;
            else return SolveSudoku();
        }

        public void CheckInsertPossibility(int x, int y, string dictKey)
        {
            for (int i = 1; i <= 9; i++)
            {
                if (dictBoard[dictKey].ContainsKey(i)) continue;

                char num = Convert.ToChar((i).ToString());

                if (dictHorizontal[x].ContainsValue(num)) continue;
                if (dictVertical[y].ContainsValue(num)) continue;
                if (dictBox[(x / 3).ToString() + (y / 3).ToString()].ContainsValue(num)) continue;

                dictBoard[dictKey].Add(i, num);
            }
            if (dictBoard[dictKey].Count == 1)
            {
                InsertNumber(x, y, dictKey);
            }
        }

        public void InsertNumber(int x, int y, string dictKey)
        {
            var dictValue = dictBoard[dictKey].FirstOrDefault();
            char num = dictValue.Value;
            int number = dictValue.Key;
            board[x][y] = num;
            dictHorizontal[x][y] = num;
            dictVertical[y][x] = num;
            dictBox[(x / 3).ToString() + (y / 3).ToString()][(x % 3).ToString() + (y % 3)] = num;

            dictBoard.Remove(dictKey);


            //Remove number from other possibility matrices.

            //Remove number from Horizontal possibility matrices.
            for (int i = 0; i < 9; i++)
            {
                if (y == i) continue;
                string dictKeyCurrent = x.ToString() + i.ToString();
                if (dictBoard.ContainsKey(dictKeyCurrent))
                {
                    dictBoard[dictKeyCurrent].Remove(number);
                    if (dictBoard[dictKeyCurrent].Count == 1)
                    {
                        InsertNumber(x, i, dictKeyCurrent);
                    }
                }
            }

            //Remove number from Vertical possibility matrices.
            for (int i = 0; i < 9; i++)
            {
                if (x == i) continue;
                string dictKeyCurrent = i.ToString() + y.ToString();
                if (dictBoard.ContainsKey(dictKeyCurrent))
                {
                    dictBoard[dictKeyCurrent].Remove(number);
                    if (dictBoard[dictKeyCurrent].Count == 1)
                    {
                        InsertNumber(i, y, dictKeyCurrent);
                    }
                }
            }

            //Remove number from Box possibility matrices.
            int minX = (x - (x % 3));
            int minY = (y - (y % 3));
            for (int i = minX; i < minX + 3; i++)
            {
                for (int j = minY; j < minY + 3; j++)
                {
                    if (x == i && y == j) continue;
                    string dictKeyCurrent = i.ToString() + j.ToString();
                    if (dictBoard.ContainsKey(dictKeyCurrent))
                    {
                        dictBoard[dictKeyCurrent].Remove(number);
                        if (dictBoard[dictKeyCurrent].Count == 1)
                        {
                            InsertNumber(i, j, dictKeyCurrent);
                        }
                    }
                }
            }

        }
    }
}