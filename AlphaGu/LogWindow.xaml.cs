using AlphaGu.Game;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlphaGu
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : AlphaWindow
    {
        MainWindow main;
        string realNum;

        public LogWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            realNum = main.guessText.Text;
        }

        private void AlphaWindow_Closed(object sender, EventArgs e)
        {
            main.EnableSet();
        }

        private void AlphaWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < main.computerLog.Items.Count; i++)
            {
                string guess = ((ListBoxItem)main.computerLog.Items[i]).Content.ToString().Split(new string[] { ": " }, StringSplitOptions.None)[0];
                BullsnCows.Score score = main.Game.Scores[i];
                BullsnCows.Score check = main.Game.Player.CalcPerm(realNum, guess);
                ListBoxItem lbxItem;

                if (!score.Equals(check))
                {
                    if (main.Game.Winner != BullsnCows.Turn.Computer)
                        main.Game.Winner = BullsnCows.Turn.Computer;

                    lbxItem = new ListBoxItem()
                    {
                        Content = score.ToString() + " → " + check.ToString(),
                        Foreground = Brushes.Red,
                    };
                }
                else
                {
                    lbxItem = new ListBoxItem()
                    {
                        Content = score,
                        Foreground = Brushes.Green,
                    };
                }

                logList.Items.Add(lbxItem);
            }
            main.guessText.IsEnabled = false;
            main.guessButton.IsEnabled = false;

            if (main.Game.Winner == BullsnCows.Turn.End)
                main.Game.Winner = BullsnCows.Turn.Player;

            if (main.Game.Winner == BullsnCows.Turn.Player)
            {
                main.message.Text = "Oh, I lost....... You are very smart!";
                Close();
            }
            else if (main.Game.Winner == BullsnCows.Turn.Computer)
            {
                main.message.Text = "I won this game because you made mistake.";
            }
        }
    }
}
