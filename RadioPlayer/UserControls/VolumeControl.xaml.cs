using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadioPlayer.UserControls
{
    public partial class VolumeControl : UserControl
    {
        public double Value { get { return (double)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(VolumeControl), new PropertyMetadata(50.0));

        // TODO: Store value in settings, apply on launch
        public VolumeControl()
        {
            InitializeComponent();
        }

        private void CaptureGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CaptureGrid.IsMouseOver)
            {
                CaptureGrid.CaptureMouse();
                Point mousePos = e.GetPosition(CaptureGrid);
                ST.ScaleX = Math.Clamp(mousePos.X, 0.0, CaptureGrid.ActualWidth);
                SetNewValue(Math.Round((ST.ScaleX / CaptureGrid.ActualWidth) * 100.0));
            }
        }

        private void CaptureGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (CaptureGrid.IsMouseCaptured)
            {
                Point mousePos = e.GetPosition(CaptureGrid);
                ST.ScaleX = Math.Clamp(mousePos.X, 0.0, CaptureGrid.ActualWidth);
                SetNewValue(Math.Round((ST.ScaleX / CaptureGrid.ActualWidth) * 100.0));
            }
        }

        private void SetNewValue(double newValue)
        {
            Value = Math.Clamp(newValue, 0.0, 100.0);
            StreamManager.Volume = (int)Value;
        }

        private void CaptureGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CaptureGrid.ReleaseMouseCapture();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            double width = (Value / 100.0) * CaptureGrid.ActualWidth;
            ST.ScaleX = width;
        }
    }
}
