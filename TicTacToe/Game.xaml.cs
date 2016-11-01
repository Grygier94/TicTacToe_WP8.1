using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

//RESET DISABLED !

namespace TicTacToe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        private Engine engine;
        private SetupModel model;

        public Game()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        private void TriggerButtons(bool b)
        {
            foreach (object field in grid.Children)
            {
                if (field is Button)
                {
                    if ((field as Button).Tag != null)
                        (field as Button).IsEnabled = b;
                }
            }
            btnRestart.IsEnabled = !b;
        }
        private void CleanBoard()
        {
            foreach (object field in grid.Children)
            {
                if (field is Button)
                {
                    if ((field as Button).Tag != null)
                        (field as Button).Content = string.Empty;
                }
            }
        }
        private void UpdateScores()
        {
            tbScore1.Text = string.Format("[X] {0}: {1}", engine.PlayerX, engine.ScoreX);
            tbScore2.Text = string.Format("[O] {0}: {1}", engine.PlayerO, engine.ScoreO);
        }

        private void FieldClick(object sender, RoutedEventArgs e)
        {
            Button field = sender as Button;
            int row = Convert.ToInt32(field.Name.Substring(5, 1)) - 1;
            int column = Convert.ToInt32(field.Name.Substring(7, 1)) - 1;

            if (engine.PlaceMark(row, column, field))
            {
                ChangeTurn();
                LookForWinner();
            }
        }

        #region ComputerLogic
        private void ComputerMove(DifficultyLevel level)
        {
            int filledFields = grid.Children.Where(o => o is Button)
                .Where(b => (b as Button).Tag != null && (b as Button).Tag.ToString() == "field")
                .Where(f => (f as Button).Content != null)
                .Where(f => (f as Button).Content.ToString() == "X" || (f as Button).Content.ToString() == "O")
                .Count();

            if (filledFields == 0 || filledFields == 1)
            {
                if (!engine.PlaceMark(1, 1, field2_2))
                    engine.PlaceMark(2, 2, field3_3);
            }

            else
            {
                string pos;
                switch (level)
                {
                    case DifficultyLevel.Easy:
                        if (engine.IsLastMoveAvailable(out pos))
                            Move(Int32.Parse(pos[0].ToString()), Int32.Parse(pos[1].ToString()));
                        else
                            RandomMove();
                        break;
                    case DifficultyLevel.Medium:
                        if (engine.IsLastMoveAvailable(out pos))
                            Move(Int32.Parse(pos[0].ToString()), Int32.Parse(pos[1].ToString()));
                        else if (engine.IsBlockAvailable(out pos))
                            Move(Int32.Parse(pos[0].ToString()), Int32.Parse(pos[1].ToString()));
                        else
                            RandomMove();
                        break;

                    case DifficultyLevel.Hard:
                        if (engine.IsLastMoveAvailable(out pos))
                            Move(Int32.Parse(pos[0].ToString()), Int32.Parse(pos[1].ToString()));
                        else if (engine.IsBlockAvailable(out pos))
                            Move(Int32.Parse(pos[0].ToString()), Int32.Parse(pos[1].ToString()));
                        else if (engine.IsBlockForkAvailable(filledFields, out pos))
                            Move(Int32.Parse(pos[0].ToString()), Int32.Parse(pos[1].ToString()));
                        else
                            RandomMove();
                        break;
                }
            }
            ChangeTurn();
            LookForWinner();
        }

        private void Move(int row, int col)
        {
            Button field = FindName(String.Format("field{0}_{1}", row + 1, col + 1)) as Button;
            engine.PlaceMark(row, col, field);
        }

        private void RandomMove()
        {
            Random rand = new Random();
            int row;
            int col;
            Button field;

            do
            {
                row = rand.Next(0, 3);
                col = rand.Next(0, 3);
                field = FindName(String.Format("field{0}_{1}", row + 1, col + 1)) as Button;
            }
            while (!engine.PlaceMark(row, col, field));
        }
        #endregion

        private void LookForWinner()
        {
            if (engine.Win())
            {
                engine.GameWon();
                UpdateScores();
                TriggerButtons(false);
            }
            else if (engine.IsBoardFilled())
            {
                engine.Draw();
                TriggerButtons(false);
            }
            else if (model.Level != DifficultyLevel.Multiplayer && engine.CurrentTurn == Mark.O)
                ComputerMove(model.Level);
        }

        private void ChangeTurn()
        {
            engine.ChangeTurn();
            tbScore1.FontWeight = tbScore1.FontWeight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold;
            tbScore2.FontWeight = tbScore1.FontWeight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold;
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            CleanBoard();
            engine.Restart();
            TriggerButtons(true);

            if (model.Level != DifficultyLevel.Multiplayer && engine.CurrentTurn == Mark.O)
                ComputerMove(model.Level);
        }

        private void Menu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Menu), model);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            model = e.Parameter as SetupModel;
            engine = new Engine(model);
            UpdateScores();
            CleanBoard();
            TriggerButtons(true);
            tbScore1.FontWeight = FontWeights.Bold;
            tbScore2.FontWeight = FontWeights.Normal;
        }
    }
}
