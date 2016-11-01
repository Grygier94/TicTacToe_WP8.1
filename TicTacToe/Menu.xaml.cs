using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TicTacToe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        DifficultyLevel difficultyLevel;
        public Menu()
        {
            this.InitializeComponent();
            difficultyLevel = DifficultyLevel.Medium;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetupModel model = e.Parameter as SetupModel;
            if(model != null)
            {
                tbPlayerNameX.Text = model.PlayerNameX;
                difficultyLevel = model.Level;

                if (difficultyLevel == DifficultyLevel.Multiplayer)
                {
                    tbPlayerNameO.Text = model.PlayerNameO;
                    ShowSinglePlayerControls(false);
                    ShowMulitPlayerControls(true);
                } else
                {
                    CheckDifficultyLevel();
                }
            }
        }

        private void ChangePlayer(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Content.ToString() == "1 Player")
            {
                ShowSinglePlayerControls(true);
                ShowMulitPlayerControls(false);
                difficultyLevel = DifficultyLevel.Medium;
                CheckDifficultyLevel();
            }
            else
            {
                ShowSinglePlayerControls(false);
                ShowMulitPlayerControls(true);
                difficultyLevel = DifficultyLevel.Multiplayer;
            }
        }

        private void ChangeDifficultyLevel(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "btnEasy")
                difficultyLevel = DifficultyLevel.Easy;
            else if (btn.Name == "btnMedium")
                difficultyLevel = DifficultyLevel.Medium;
            else if (btn.Name == "btnHard")
                difficultyLevel = DifficultyLevel.Hard;

            CheckDifficultyLevel();
        }

        void CheckDifficultyLevel()
        {
            foreach (Button control in MenuGrid.Children.Where(c => c is Button))
            {
                if (control.Tag != null && control.Tag.ToString() == "SinglePlayerControl")
                    control.IsEnabled = true;
            }

            if (difficultyLevel == DifficultyLevel.Easy)
                btnEasy.IsEnabled = false;
            else if (difficultyLevel == DifficultyLevel.Medium)
                btnMedium.IsEnabled = false;
            else if(difficultyLevel == DifficultyLevel.Hard)
                btnHard.IsEnabled = false;
        }
        void ShowSinglePlayerControls(bool show)
        {
            foreach (Button control in MenuGrid.Children.Where(c => c is Button))
            {
                if (control.Tag != null && control.Tag.ToString() == "SinglePlayerControl")
                    control.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            }
            btnSinglePlayer.IsEnabled = !show;
        }
        void ShowMulitPlayerControls(bool show)
        {
            tbPlayerNameO.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            btnMultiPlayer.IsEnabled = !show;
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            SetupModel model = new SetupModel
            {
                PlayerNameX = tbPlayerNameX.Text,
                PlayerNameO = difficultyLevel != DifficultyLevel.Multiplayer ? "Computer" : tbPlayerNameO.Text,
                Level = difficultyLevel
            };

            Frame.Navigate(typeof(Game), model);
        }

        private async void Author(object sender, RoutedEventArgs e)
        {
            MessageDialog msgBox = new MessageDialog("Author: Marcin Grygierczyk");
            await msgBox.ShowAsync();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
