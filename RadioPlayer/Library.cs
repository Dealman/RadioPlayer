using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace RadioPlayer
{
    public static class Library
    {
        static string LibraryPath;
        public static ObservableCollection<RadioStation> FavouriteStations = new();

        static Library()
        {
            var userConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            LibraryPath = Path.Combine(Path.GetDirectoryName(userConfig), "Library.json");
        }

        public static bool Contains(RadioStation target)
        {
            if (target is null) return false;

            if (FavouriteStations.Count > 0)
                foreach (RadioStation station in FavouriteStations)
                    if (station.Equals(target))
                        return true;

            return false;
        }

        public static void AddStation(RadioStation target, bool shouldSave = false)
        {
            if (!Contains(target))
                FavouriteStations.Add(target);

            if (shouldSave)
                Save();
        }

        public static void RemoveStation(RadioStation target, bool shouldSave = false)
        {
            if (Contains(target))
            {
                foreach (RadioStation station in FavouriteStations)
                {
                    if (station.Equals(target))
                    {
                        FavouriteStations.Remove(station);
                        break;
                    }
                }
            }

            if (shouldSave)
                Save();
        }

        public static void AddStationRange(List<RadioStation> stations, bool shouldSave = false)
        {
            foreach (RadioStation station in stations)
                AddStation(station);

            if (shouldSave)
                Save();
        }        

        public static void RemoveStationRange(List<RadioStation> stations, bool shouldSave = false)
        {
            foreach (RadioStation station in stations)
                RemoveStation(station);

            if (shouldSave)
                Save();
        }

        public static void Clear(bool shouldSave = false)
        {
            if (FavouriteStations.Count > 0)
                FavouriteStations.Clear();

            if (shouldSave)
                Save();
        }

        static void UpdateProperty()
        {

        }

        public static bool Exists()
        {
            if (File.Exists(LibraryPath))
                return true;

            return false;
        }

        public static bool Create()
        {
            try
            {
                var file = new FileInfo(LibraryPath).Directory;
                file.Create();
                File.Create(LibraryPath).Close();

                return true;
            } catch (Exception e) {
                MessageBox.Show($"An error occurred while trying to create the library!\n\nMessage:\n{e.Message}");
            }

            return false;
        }

        public static bool IsEmpty()
        {
            if (Exists() && new FileInfo(LibraryPath).Length > 0)
                return false;

            return true;
        }

        public static bool Save()
        {
            if (FavouriteStations.Count > 0)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(FavouriteStations);

                if (!String.IsNullOrWhiteSpace(json))
                {
                    if (!Exists())
                        if (!Create())
                            return false;

                    File.WriteAllText(LibraryPath, json);

                    if (Exists() && new FileInfo(LibraryPath).Length > 0)
                        return true;
                    else
                        MessageBox.Show($"An error occurred while trying to write to the library! File doesn't exist, or received no data to write.");
                }
            } else {
                Debug.WriteLine("Empty Library");
                if (Exists())
                {
                    Debug.WriteLine("Library exists");
                    var file = File.Open(LibraryPath, FileMode.Open);
                    file.SetLength(0);
                    file.Close();
                }
            }

            return false;
        }

        public static bool Load()
        {
            if (!IsEmpty())
            {
                var json = File.ReadAllText(LibraryPath);
                if (!String.IsNullOrWhiteSpace(json))
                {
                    ObservableCollection<RadioStation> stations = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<RadioStation>>(json);

                    if (stations.Count > 0)
                    {
                        Clear();

                        foreach (RadioStation station in stations)
                            if (!FavouriteStations.Contains(station))
                                AddStation(station);

                        if (FavouriteStations.Count > 0)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
