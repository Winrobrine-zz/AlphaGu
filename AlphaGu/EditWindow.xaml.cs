using AlphaGu.Game;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AlphaGu
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : AlphaWindow
    {
        MainWindow main;

        public EditWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void AlphaWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= main.LEN; i++)
            {
                strikeBox.Items.Add(i);
                ballBox.Items.Add(i);
            }

            strikeBox.SelectedIndex = 0;
            ballBox.SelectedIndex = 0;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int strike, ball;

            if (int.TryParse(strikeBox.Text, out strike) && int.TryParse(ballBox.Text, out ball))
            {
                BullsnCows.Score score = new BullsnCows.Score(main.LEN, strike, ball);

                if (score.IsError)
                    return;

                main.Game.InitAnswer();
                main.Game.Scores.RemoveAt(main.Game.Scores.Count - 1);
                main.Game.Scores.Add(score);

                for (int i = 0; i < main.Game.Scores.Count; i++)
                {
                    BullsnCows.Score item = main.Game.Scores[i];
                    string guess = ((ListBoxItem)main.computerLog.Items[i]).Content.ToString().Split(new string[] { ": " }, StringSplitOptions.None)[0];
                    main.Game.Computer.CalcScore(item, guess);

                    ListBoxItem lbxItem = new ListBoxItem()
                    {
                        Content = guess + ": " + item.ToString(),
                    };
                    main.computerLog.Items[i] = lbxItem;
                }
            }

            Close();
        }
    }
}
