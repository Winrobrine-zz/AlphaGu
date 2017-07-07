using System;
using System.Linq;

namespace AlphaGu.Game
{
    public class Player
    {
        BullsnCows game;
        int length;

        public Player(BullsnCows game, int length)
        {
            this.game = game;
            this.length = length;
        }

        public BullsnCows.Score CalcPerm(string number, string guess)
        {
            if (number.Length != length || !game.Perms.Contains(guess))
                return new BullsnCows.Score(length, -1, -1);

            BullsnCows.Score score = new BullsnCows.Score();
            score.Length = length;

            for (int i = 0; i < length; i++)
            {
                char curGuess = guess[i];

                if (curGuess == number[i])
                    score.Strike++;
                else if (number.Contains(curGuess))
                    score.Ball++;
            }

            return score;
        }

        public BullsnCows.Score PlayerTurn(string guess)
        {
            if (game.GameTurn != BullsnCows.Turn.Player || !game.Perms.Contains(guess))
                return new BullsnCows.Score(length, -1, -1);

            BullsnCows.Score score = CalcPerm(game.ComNumber, guess);

            if (score.Strike == length)
            {
                game.Symbol.Emotion = Symbol.Emotions.Abashed;
                game.Message.Text = "Oh My Goodness......." + Environment.NewLine + "What is your number?";
                game.IsCheck = true;
            }
            else
            {
                game.Symbol.Emotion = Symbol.RandomEmotion();
            }

            return score;
        }
    }
}
