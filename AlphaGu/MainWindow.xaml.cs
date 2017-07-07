using AlphaGu.Game;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlphaGu
{
    public partial class MainWindow : AlphaWindow
    {
        public BullsnCows Game;
        public readonly int LEN = 4;

        public MainWindow()
        {
            InitializeComponent();
            Game = new BullsnCows(LEN, symbol, message, guessButton, answerButton);
        }

        private void AlphaWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= LEN; i++)
            {
                strikeBox.Items.Add(i);
                ballBox.Items.Add(i);
            }
        }

        private void Restart()
        {
            Game = new BullsnCows(LEN, symbol, message, guessButton, answerButton);
            symbol.Emotion = Symbol.Emotions.Calm;
            computerLog.Items.Clear();
            playerLog.Items.Clear();
        }

        public void EnableSet()
        {
            answerButton.IsEnabled = false;
            guessButton.IsEnabled = false;
            guessText.IsEnabled = true;
        }

        private void answerButton_Click(object sender, RoutedEventArgs e)
        {
            int strike, ball;

            if (int.TryParse(strikeBox.Text, out strike) && int.TryParse(ballBox.Text, out ball))
            {
                Game.Computer.PlayerScore = new BullsnCows.Score(LEN, strike, ball);
                if (Game.Computer.PlayerScore.IsError)
                    return;

                ListBoxItem item = new ListBoxItem()
                {
                    Content = symbol.Number.Text + ": " + Game.Computer.PlayerScore.ToString(),
                };
                computerLog.Items.Add(item);

                strikeBox.SelectedIndex = 0;
                ballBox.SelectedIndex = 0;

                Game.GameTurn = BullsnCows.Turn.Player;

                if (Game.GameTurn == BullsnCows.Turn.End)
                    EnableSet();
                symbol.Number.Text = null;
            }
            else
            {
                symbol.Emotion = Symbol.Emotions.Abashed;
                message.Text = "Not a valid answer.";
            }
        }

        private void guessButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Game.IsCheck)
            {
                BullsnCows.Score score = Game.Player.PlayerTurn(guessText.Text);
                if (score.IsError)
                {
                    symbol.Emotion = Symbol.Emotions.Abashed;
                    message.Text = "Not a valid guess.";
                    guessText.Text = null;
                    return;
                }

                ListBoxItem item = new ListBoxItem()
                {
                    Content = guessText.Text + ": " + score.ToString(),
                };
                playerLog.Items.Add(item);

                if (score.Strike != LEN)
                    Game.GameTurn = BullsnCows.Turn.Computer;
            }
            else if (Game.Perms.Contains(guessText.Text))
            {
                new LogWindow(this).Show();
            }

            guessText.Text = null;
        }

        private void guessText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Game.GameTurn != BullsnCows.Turn.Computer)
                guessButton_Click(null, null);
        }

        private void computerLog_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ListBoxItem && e.ClickCount == 2 && Game.GameTurn == BullsnCows.Turn.Player)
            {
                ListBoxItem item = e.Source as ListBoxItem;
                if (computerLog.Items.IndexOf(item) == computerLog.Items.Count - 1)
                {
                    if (MessageBox.Show("Do you really want to edit the score?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        new EditWindow(this).Show();
                }
            }
        }
    }
}
