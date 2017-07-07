using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AlphaGu
{
    public partial class Symbol : UserControl
    {
        #region Emotion

        public static readonly DependencyProperty EmotionProperty = DependencyProperty.Register("Emotion", typeof(Emotions), typeof(Symbol), new PropertyMetadata(Emotions.Calm));
        Storyboard storyboard;

        public enum Emotions
        {
            Calm,
            Optimistic,
            Abashed,
        }

        public Emotions Emotion
        {
            get { return (Emotions)GetValue(EmotionProperty); }
            set
            {
                SetValue(EmotionProperty, value);
                BeginStoryboard(value.ToString());
            }
        }

        public static Emotions RandomEmotion()
        {
            return (Emotions)new Random().Next(2);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BeginStoryboard(Emotion.ToString());
        }

        private void BeginStoryboard(string name)
        {
            if (storyboard != null)
            {
                storyboard.Seek(TimeSpan.Zero);
                storyboard.Stop();
            }
            storyboard = FindResource(name) as Storyboard;
            storyboard.Begin();
        }

        #endregion

        public Symbol()
        {
            InitializeComponent();
        }
    }
}
