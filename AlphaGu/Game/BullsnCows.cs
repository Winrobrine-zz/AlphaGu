using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AlphaGu.Game
{
    public static class Extension
    {
        private static Random random = new Random();

        public static void Shuffle<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(array.Length);
                T temp = array[i]; array[i] = array[j]; array[j] = temp;
            }
        }

        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int j = random.Next(list.Count);
                T temp = list[i]; list[i] = list[j]; list[j] = temp;
            }
        }
    }

    public class BullsnCows
    {
        public enum Turn
        {
            Player,
            Computer,
            End,
        }

        public struct Score
        {
            public int Length;
            public int Strike;
            public int Ball;

            public Score(int length, int strike, int ball)
            {
                Length = length;
                Strike = strike;
                Ball = ball;
            }

            public bool IsNull
            {
                get { return (Strike == 0 && Ball == 0); }
            }

            public bool IsError
            {
                get { return (Strike == -1 || Ball == -1) || (Strike + Ball > Length); }
            }

            public override string ToString()
            {
                return Strike + " Strike " + Ball + " Ball";
            }
        }

        public Turn GameTurn
        {
            get { return gameTurn; }
            set
            {
                if (gameTurn != Turn.End)
                {
                    gameTurn = value;

                    if (gameTurn == Turn.Player)
                    {
                        guessButton.IsEnabled = true;
                        answerButton.IsEnabled = false;
                    }
                    else if (gameTurn == Turn.Computer)
                    {
                        Computer.ComputerTurn();

                        if (Answers.Count > 0)
                        {
                            guessButton.IsEnabled = false;
                            answerButton.IsEnabled = true;
                        }
                    }
                }
            }
        }

        public Player Player;
        public Computer Computer;
        public List<Score> Scores;
        public Turn Winner = Turn.End;
        public bool IsCheck = false;
        public string ComNumber { get; private set; }
        public List<string> Perms;
        public List<string> Answers;
        public Symbol Symbol;
        public TextBlock Message;
        public TextBlock Number;
        
        int length;
        Turn gameTurn;
        Button guessButton, answerButton;

        public BullsnCows(int length, Symbol symbol, TextBlock message, Button guessButton, Button answerButton)
        {
            this.length = length;
            this.guessButton = guessButton;
            this.answerButton = answerButton;
            
            Symbol = symbol;
            Message = message;
            Number = symbol.Number;
            Perms = Permutations(length).ToList();
            Scores = new List<Score>();
            Player = new Player(this, length);
            Computer = new Computer(this, length);
            ComNumber = RandNumber();
            InitAnswer();
            GameTurn = Turn.Player;
        }

        public void InitAnswer()
        {
            Answers = new List<string>(Perms);
            Answers.Shuffle();
        }

        private string RandNumber()
        {
            char[] nums = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            nums.Shuffle();

            string chosenNum = null;

            for (int i = 0; i < length; i++)
                chosenNum += nums[i].ToString();

            return chosenNum;
        }

        private IEnumerable<string> Permutations(int size)
        {
            if (size > 0)
            {
                foreach (string str in Permutations(size - 1))
                    for (char num = '0'; num <= '9'; num++)
                        if (!str.Contains(num))
                            yield return str + num;
            }
            else yield return "";
        }
    }
}
