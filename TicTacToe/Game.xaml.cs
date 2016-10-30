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

        //Events
        private void FieldClick(object sender, RoutedEventArgs e)
        {
            Button field = sender as Button;
            int row = Convert.ToInt32(field.Name.Substring(5, 1)) - 1;
            int column = Convert.ToInt32(field.Name.Substring(7, 1)) - 1;

            if (engine.PlaceMark(row, column, field))
            {
                if (engine.Win())
                {
                    engine.GameWon();
                    UpdateScores();
                    TriggerButtons(false);
                }
                if (engine.IsBoardFilled())
                {
                    engine.Draw();
                    TriggerButtons(false);
                }
                engine.ChangeTurn();
                tbScore1.FontWeight = tbScore1.FontWeight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold;
                tbScore2.FontWeight = tbScore1.FontWeight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold;
            }
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            CleanBoard();
            engine.Restart();
            TriggerButtons(true);
        }

        private void Menu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Menu));
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetupModel model = e.Parameter as SetupModel;
            engine = new Engine(model);
            UpdateScores();
            CleanBoard();
            TriggerButtons(true);
            tbScore1.FontWeight = FontWeights.Bold;
            tbScore2.FontWeight = FontWeights.Normal;
        }
    }
}
