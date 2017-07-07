using System;
using System.Linq;

namespace AlphaGu.Game
{
    public class Computer
    {
        BullsnCows game;
        BullsnCows.Score playerScore;
        int length;

        public BullsnCows.Score PlayerScore
        {
            get { return playerScore; }
            set
            {
                playerScore = value;
                game.Scores.Add(PlayerScore);
                CalcScore(PlayerScore, game.Number.Text);
            }
        }

        public Computer(BullsnCows game, int length)
        {
            this.game = game;
            this.length = length;
        }

        public void CalcScore(BullsnCows.Score score, string guess)
        {
            if (score.IsError)
            {
                game.Symbol.Emotion = Symbol.Emotions.Abashed;
                game.Message.Text = "Not a valid answer.";
                return;
            }

            if (score.Strike == length)
            {
                game.Symbol.Emotion = Symbol.Emotions.Optimistic;
                game.Message.Text = "Hooray! I win!" + Environment.NewLine + "My number is " + game.ComNumber + "!";
                game.GameTurn = BullsnCows.Turn.End;
                game.Winner = BullsnCows.Turn.Computer;
                return;
            }

            game.Symbol.Emotion = Symbol.RandomEmotion();
            game.Message.Text = "I see.";

            if (game.Answers.Count > 0)
            {
                for (int idx = game.Answers.Count - 1; idx >= 0; idx--)
                {
                    string answer = game.Answers[idx];
                    int strike = 0, ball = 0;
                    for (int i = 0; i < length; i++)
                    {
                        if (answer[i] == guess[i])
                            strike++;
                        else if (answer.Contains(guess[i]))
                            ball++;
                    }

                    if ((strike != score.Strike) || (ball != score.Ball))
                        game.Answers.RemoveAt(idx);
                }
            }
        }

        public void ComputerTurn()
        {
            if (game.GameTurn != BullsnCows.Turn.Computer)
                return;

            if (game.Answers.Count == 0)
            {
                game.Symbol.Emotion = Symbol.Emotions.Abashed;
                game.Message.Text = "No possible answer fits the scores you gave." + Environment.NewLine + "What is your number?";
                game.GameTurn = BullsnCows.Turn.Player;
                game.IsCheck = true;
                return;
            }

            game.Answers.Shuffle();
            game.Number.Text = game.Answers[0];
            game.Symbol.Emotion = Symbol.RandomEmotion();
            game.Message.Text = "I guess.......";
        }
    }
}
