using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AlphaGu
{
    public class AlphaWindow : Window
    {
        static AlphaWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AlphaWindow), new FrameworkPropertyMetadata(typeof(AlphaWindow)));
        }

        #region Click events
        protected void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleClick(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion

        public override void OnApplyTemplate()
        {
            Rectangle titleRectangle = GetTemplateChild("titleRectangle") as Rectangle;
            if (titleRectangle != null)
                titleRectangle.PreviewMouseDown += TitleClick;
            Button minimizeButton = GetTemplateChild("minimizeButton") as Button;
            if (minimizeButton != null)
                minimizeButton.Click += MinimizeClick;

            Button closeButton = GetTemplateChild("closeButton") as Button;
            if (closeButton != null)
                closeButton.Click += CloseClick;

            base.OnApplyTemplate();
        }
    }
}
