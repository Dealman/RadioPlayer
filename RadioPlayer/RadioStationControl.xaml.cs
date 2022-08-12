using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace RadioPlayer
{
    public partial class RadioStationControl : UserControl
    {
        public RadioStation Station 
        { 
            get { return (RadioStation)GetValue(StationProperty); } 
            set { SetValue(StationProperty, value); } 
        }
        public static readonly DependencyProperty StationProperty = DependencyProperty.Register("Station", typeof(RadioStation), typeof(RadioStationControl), new PropertyMetadata(null));

        public Color OutlineColor
        {
            get { return (Color)GetValue(OutlineColorProperty); }
            set { SetValue(OutlineColorProperty, value); }
        }
        public static readonly DependencyProperty OutlineColorProperty = DependencyProperty.Register("OutlineColor", typeof(Color), typeof(RadioStationControl));

        public Thickness OutlineThickness //BorderThickness="2,2,0,2"
        {
            get { return (Thickness)GetValue(OutlineThicknessProperty); }
            set { SetValue(OutlineThicknessProperty, value); }
        }
        public static readonly DependencyProperty OutlineThicknessProperty = DependencyProperty.Register("OutlineThickness", typeof(Thickness), typeof(RadioStationControl));




        public RadioStationControl()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void Testeroni_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (Station is null) return;
            //Debug.WriteLine($"{Station.name}");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RadioStation radioStation = this.DataContext as RadioStation;

            if (radioStation != null)
                Station = radioStation;

            //if (!String.IsNullOrWhiteSpace(Station.Favicon))
            //    Border = Colors.Gold;

            if (OutlineColor == Colors.Transparent)
                DasBorder.BorderThickness = new Thickness(0);

            if (Library.FavouriteStations.Count > 0)
                if (Library.Contains(Station))
                    MarkAsFavourite(true);

            if (!String.IsNullOrWhiteSpace(Station.Favicon))
                SetStationImage();
                //StationImage.Source = Station.Homepage;
        }

        void SetStationImage()
        {
            try
            {
                var bmpImg = new BitmapImage();
                bmpImg.BeginInit();
                bmpImg.UriSource = new Uri(Station.Favicon);
                bmpImg.EndInit();

                StationImage.Source = bmpImg;
            } catch (Exception e) {
                Debug.WriteLine(e.InnerException);
                return;
            }

            FallbackImage.Visibility = Visibility.Hidden;
        }

        public void MarkAsFavourite(bool newState)
        {
            if (newState)
            {
                FavImage.Icon = FontAwesome5.EFontAwesomeIcon.Solid_Star;
                FavImage.Foreground = Brushes.Gold;
                OutlineThickness = new Thickness(0, 2, 0, 2);
                OutlineColor = Colors.Gold;
            }
        }

        private void ControlContainer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Station is null) return;

            StreamManager.StreamURL(Station.URL, Station.Name);
        }

        private void FavImage_MouseEnter(object sender, MouseEventArgs e)
        {
            FavImage.Width = 34;
            FavImage.Height = 34;

            switch (FavImage.Icon)
            {
                case FontAwesome5.EFontAwesomeIcon.Regular_Star:
                    FavImage.Icon = FontAwesome5.EFontAwesomeIcon.Solid_Star;
                    break;
            }
        }

        private void FavImage_MouseLeave(object sender, MouseEventArgs e)
        {
            FavImage.Width = 32;
            FavImage.Height = 32;

            switch (FavImage.Icon)
            {
                case FontAwesome5.EFontAwesomeIcon.Solid_Star:
                    if (FavImage.Foreground != Brushes.Gold)
                        FavImage.Icon = FontAwesome5.EFontAwesomeIcon.Regular_Star;
                    break;
            }
        }

        private void FavImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!Library.Contains(Station))
            {
                Debug.WriteLine("Add");
                Library.AddStation(Station, true);
                FavImage.Icon = FontAwesome5.EFontAwesomeIcon.Solid_Star;
                FavImage.Foreground = Brushes.Gold;
                OutlineThickness = new Thickness(0, 2, 0, 2);
            } else {
                Library.RemoveStation(Station, true);
                FavImage.Icon = FontAwesome5.EFontAwesomeIcon.Regular_Star;
                FavImage.Foreground = Brushes.White;
                OutlineThickness = new Thickness(0, 0, 0, 0);
            }
        }
    }
}
