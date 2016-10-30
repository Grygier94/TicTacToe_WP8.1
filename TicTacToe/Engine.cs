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
    public enum Mark { X, O }

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
        public bool Win()
        {
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
            if (currentTurn == Mark.X)
            {
                if (difficultyLevel == DifficultyLevel.Multiplayer)
                    ShowMessage(playerX + " won! Congratulations!");
                else
                    ShowMessage("You won! Congratulations!");
                ScoreX++;
            }
            else
            {
                if(difficultyLevel == DifficultyLevel.Multiplayer)
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
