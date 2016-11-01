using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace TicTacToe
{
    public enum Mark { X = -10, O = 1 }

    class Engine
    {
        private Mark currentTurn;
        private int scoreX, scoreO;
        private string playerX, playerO;
        private DifficultyLevel difficultyLevel;
        private Mark?[,] board;

        public int ScoreX
        {
            get { return scoreX; }
            private set { scoreX = value; }
        }
        public int ScoreO
        {
            get { return scoreO; }
            private set { scoreO = value; }
        }
        public string PlayerX { get { return playerX; } }
        public string PlayerO { get { return playerO; } }
        public Mark CurrentTurn { get { return currentTurn; } }
        public Mark?[,] Board { get { return board; } }

        public Engine(SetupModel setup)
        {
            currentTurn = Mark.X;
            board = new Mark?[3, 3];
            ScoreX = ScoreO = 0;
            playerX = setup.PlayerNameX;
            playerO = setup.PlayerNameO;
            difficultyLevel = setup.Level;
        }

        private async void ShowMessage(string msg)
        {
            MessageDialog msgBox = new MessageDialog(msg);
            await msgBox.ShowAsync();
        }

        public void ChangeTurn()
        {
            currentTurn = currentTurn == Mark.X ? Mark.O : Mark.X;
        }

        public bool IsBlockForkAvailable(int filledFields, out string position)
        {
            position = "";

            if(filledFields == 3)
            {
                if ((Board[0, 0] == Mark.X && Board[2, 2] == Mark.X) ||
                (Board[0, 2] == Mark.X && Board[2, 0] == Mark.X))
                {
                    position = "10";
                    return true;
                }
                else if (Board[0, 0] == Mark.X && Board[1, 2] == Mark.X)
                {
                    position = "22";
                    return true;
                }
                else if (Board[0, 2] == Mark.X && Board[1, 0] == Mark.X)
                {
                    position = "20";
                    return true;
                }
                else if (Board[2, 2] == Mark.X && Board[1, 0] == Mark.X)
                {
                    position = "00";
                    return true;
                }
                else if (Board[2, 0] == Mark.X && Board[1, 2] == Mark.X)
                {
                    position = "02";
                    return true;
                }
            }

            return false;
        }

        public bool IsBlockAvailable(out string position)
        {
            int sum;

            for (int i = 0; i < 3; i++)
            {
                //check horizontal
                sum = 0;
                position = "";
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == null)
                        position = i.ToString() + j.ToString();
                    else
                        sum += (int)board[i, j];

                    if (sum == -20 && j == 2)
                        return true;
                }

                //check vertical
                sum = 0;
                position = "";
                for (int j = 0; j < 3; j++)
                {
                    if (board[j, i] == null)
                        position = j.ToString() + i.ToString();
                    else
                        sum += (int)board[j, i];

                    if (sum == -20 && j == 2)
                        return true;
                }
            }

            //tilt
            sum = 0;
            position = "";
            for (int j = 0; j < 3; j++)
            {
                if (board[j, j] == null)
                    position = j.ToString() + j.ToString();
                else
                    sum += (int)board[j, j];

                if (sum == -20 && j == 2)
                    return true;
            }

            sum = 0;
            position = "";
            for (int j = 0; j < 3; j++)
            {
                if (board[2 - j, j] == null)
                    position = (2 - j).ToString() + j.ToString();
                else
                    sum += (int)board[2 - j, j];

                if (sum == -20 && j == 2)
                    return true;
            }

            return false;
        }

        public bool IsLastMoveAvailable(out string position)
        {
            int sum;

            for (int i = 0; i < 3; i++)
            {
                //check horizontal
                sum = 0;
                position = "";
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == null)
                        position = i.ToString() + j.ToString();
                    else
                        sum += (int)board[i, j];

                    if (sum == 2 && j == 2)
                        return true;
                }

                //check vertical
                sum = 0;
                position = "";
                for (int j = 0; j < 3; j++)
                {
                    if (board[j, i] == null)
                        position = j.ToString() + i.ToString();
                    else
                        sum += (int)board[j, i];

                    if (sum == 2 && j == 2)
                        return true;
                }
            }

            //tilt
            sum = 0;
            position = "";
            for (int j = 0; j < 3; j++)
            {
                if (board[j, j] == null)
                    position = j.ToString() + j.ToString();
                else
                    sum += (int)board[j, j];

                if (sum == 2 && j == 2)
                    return true;
            }

            sum = 0;
            position = "";
            for (int j = 0; j < 3; j++)
            {
                if (board[2 - j, j] == null)
                    position = (2 - j).ToString() + j.ToString();
                else
                    sum += (int)board[2 - j, j];

                if (sum == 2 && j == 2)
                    return true;
            }

            return false;
        }

        public bool Win()
        {
            #region first method
            //int sum;
            //for(int i = 0; i < 3; i++)
            //{
            //    //check horizontal
            //    sum = 0;
            //    for (int j = 0; j < 3; j++)
            //    {
            //        sum += (int)(board[i, j] ?? 0);
            //        if (sum == 3 || sum == -3)
            //            return true;
            //    }

            //    //check vertical
            //    sum = 0;
            //    for (int j = 0; j < 3; j++)
            //    {
            //        sum += (int)(board[j, i] ?? 0);
            //        if (sum == 3 || sum == -3)
            //            return true;
            //    }
            //}

            ////tilt
            //sum = 0;
            //for (int j = 0; j < 2; j++)
            //{
            //    sum += (int)(board[j, j] ?? 0);
            //    if (sum == 3 || sum == -3)
            //        return true;
            //}
            //sum = 0;
            //for (int j = 0; j < 2; j++)
            //{
            //    sum += (int)(board[2-j, j] ?? 0);
            //    if (sum == 3 || sum == -3)
            //        return true;
            //}
            #endregion

            if ((board[0, 0] == Mark.X && board[0, 1] == Mark.X && board[0, 2] == Mark.X) ||
                (board[0, 0] == Mark.O && board[0, 1] == Mark.O && board[0, 2] == Mark.O) ||
                (board[1, 0] == Mark.X && board[1, 1] == Mark.X && board[1, 2] == Mark.X) ||
                (board[1, 0] == Mark.O && board[1, 1] == Mark.O && board[1, 2] == Mark.O) ||
                (board[2, 0] == Mark.X && board[2, 1] == Mark.X && board[2, 2] == Mark.X) ||
                (board[2, 0] == Mark.O && board[2, 1] == Mark.O && board[2, 2] == Mark.O) ||

                (board[0, 0] == Mark.X && board[1, 0] == Mark.X && board[2, 0] == Mark.X) ||
                (board[0, 0] == Mark.O && board[1, 0] == Mark.O && board[2, 0] == Mark.O) ||
                (board[0, 1] == Mark.X && board[1, 1] == Mark.X && board[2, 1] == Mark.X) ||
                (board[0, 1] == Mark.O && board[1, 1] == Mark.O && board[2, 1] == Mark.O) ||
                (board[0, 2] == Mark.X && board[1, 2] == Mark.X && board[2, 2] == Mark.X) ||
                (board[0, 2] == Mark.O && board[1, 2] == Mark.O && board[2, 2] == Mark.O) ||

                (board[0, 0] == Mark.X && board[1, 1] == Mark.X && board[2, 2] == Mark.X) ||
                (board[0, 0] == Mark.O && board[1, 1] == Mark.O && board[2, 2] == Mark.O) ||
                (board[2, 0] == Mark.X && board[1, 1] == Mark.X && board[0, 2] == Mark.X) ||
                (board[2, 0] == Mark.O && board[1, 1] == Mark.O && board[0, 2] == Mark.O))
            {
                return true;
            }

            return false;
        }

        public void GameWon()
        {
            if (currentTurn == Mark.O)
            {
                if (difficultyLevel == DifficultyLevel.Multiplayer)
                    ShowMessage(playerX + " won! Congratulations!");
                else
                    ShowMessage("You won! Congratulations!");
                ScoreX++;
            }
            else
            {
                if (difficultyLevel == DifficultyLevel.Multiplayer)
                    ShowMessage(playerO + " won! Congratulations!");
                else
                    ShowMessage(playerO + " won, you lost!");
                ScoreO++;
            }
        }
        public void Draw()
        {
            ShowMessage("Tie!");
        }
        public void Restart()
        {
            Array.Clear(board, 0, board.Length);
        }
        public bool PlaceMark(int row, int column, Button sender)
        {
            if (row > 2 || row < 0 || column > 2 || column < 0 || board[row, column] != null)
                return false;

            if (currentTurn == Mark.X)
            {
                board[row, column] = Mark.X;
                sender.Content = "X";
            }
            else
            {
                board[row, column] = Mark.O;
                sender.Content = "O";
            }
            return true;
        }
        public bool IsBoardFilled()
        {
            foreach (Mark? field in board)
            {
                if (field == null)
                    return false;
            }
            return true;
        }
    }
}
