using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using RadioPlayer.RadioBrowser;
using System.Windows;
using System.Diagnostics;

namespace RadioPlayer
{
    public partial class MainWindow : MetroWindow
    {
        public string LeftText
        {
            get { return (string)GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }
        public static readonly DependencyProperty LeftTextProperty = DependencyProperty.Register("LeftText", typeof(string), typeof(MainWindow));
        public string CenterText
        {
            get { return (string)GetValue(CenterTextProperty); }
            set { SetValue(CenterTextProperty, value); }
        }
        public static readonly DependencyProperty CenterTextProperty = DependencyProperty.Register("CenterText", typeof(string), typeof(MainWindow));



        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            var c = Enum.GetValues(typeof(Country));

            foreach (Country country in c)
                CountryList.Items.Add(country.ToString().Replace("_", " "));

            if (CountryList.Items.Contains(RegionInfo.CurrentRegion.EnglishName))
                CountryList.SelectedIndex = CountryList.Items.IndexOf(RegionInfo.CurrentRegion.EnglishName);

            Library.Load();
        }

        public void SetStatusBarText(StatusBarText textField, string text)
        {
            // TODO: Use propdp instead?
            if (textField == StatusBarText.Center)
            {
                CenterTextSB.Text = text;
            } else {
                LeftTextSB.Text = text;
            }
        }

        public void AddHistoryEntry()
        {
            HistoryDG.Items.Refresh();
        }

        private async void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ObservableCollection<RadioStation> stations = new();

            // Country + Tags
            if (CountryList.SelectedIndex != -1 && !String.IsNullOrWhiteSpace(SearchTagsTB.Text))
                if((string)CountryList.SelectedItem != "NONE")
                    stations = await API.URLManager.AdvancedSearch((string)CountryList.SelectedItem, SearchTagsTB.Text);
                else
                    stations = await API.URLManager.TagSearch(SearchTagsTB.Text);

            // Country
            if (CountryList.SelectedIndex != -1 && String.IsNullOrWhiteSpace(SearchTagsTB.Text))
                stations = await API.URLManager.CountrySearch((string)CountryList.SelectedItem);

            // Tags
            if (CountryList.SelectedIndex == -1 && !String.IsNullOrWhiteSpace(SearchTagsTB.Text))
                stations = await API.URLManager.TagSearch(SearchTagsTB.Text);

            // Empty (Full Search)
            //if (CountryList.SelectedIndex == -1 && String.IsNullOrWhiteSpace(SearchTagsTB.Text))
                //stations = await API.URLManager.TagSearch(SearchTagsTB.Text);

            if (stations != null && stations.Count > 0)
                SearchResultDG.ItemsSource = stations;
        }

        public void AppendWindowTitle(string newTitle)
        {
            newTitle = $"Radio Player - ({newTitle})";
            Title = newTitle;
        }

        public void ResetWindowTitle()
        {
            Title = "Radio Player";
        }

        private void GitHubButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://github.com/Dealman/RadioPlayer");
        }
    }
}
