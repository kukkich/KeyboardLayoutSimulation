using Newtonsoft.Json;
using OSProject.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OSProject.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public KeyboardLayout CurrentLayout
        {
            get => _currentLayout;
            set
            {
                _currentLayout = value;
                OnPropertyChanged("CurrentcurrentLayout");
            }
        }
        public ObservableCollection<KeyboardLayout> Layouts { get; set; }
        public readonly DefaultKeyboardLayoutConfig LayoutConfig;
        public readonly string LayoutsDirectoryRoot;
        public event PropertyChangedEventHandler PropertyChanged;

        private string _value;
        private KeyboardLayout _currentLayout;
        private readonly DirectoryInfo _rootDirectory;

        public AppViewModel(string content, DefaultKeyboardLayoutConfig defaultLayoutConfig)
        {
            LayoutConfig = defaultLayoutConfig;
            Value = content;
            Layouts = new ObservableCollection<KeyboardLayout>();

            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            LayoutsDirectoryRoot = new DirectoryInfo(appRoot).Parent.Parent
                .GetDirectories()
                .First(directory => directory.Name == "Layouts")
                .FullName;

            _rootDirectory = new DirectoryInfo(LayoutsDirectoryRoot);
        }

        public void SetLayout(string name)
        {
            CurrentLayout = Layouts.First(x => x.Name == name);
        }

        public void UpdateLayouts()
        {
            var layoutsData = _rootDirectory.GetFiles();
            Layouts.Clear();
            foreach (var layoutFile in layoutsData)
            {
                ReadKeyboardLayout(layoutFile);
            }
        }

        public char GetConfiguredCharacter(int characterId)
        {
            return LayoutConfig.GetCharacterById(characterId);
        }

        public void Clear()
        {
            Value = String.Empty;
        }

        public void RemoveLastChar()
        {
            if (!String.IsNullOrEmpty(Value))
                Value = Value.Remove(Value.Length - 1);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ReadKeyboardLayout(FileInfo file)
        {
            using (StreamReader stream = new StreamReader(file.FullName))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
                KeyboardLayout newLayout = JsonConvert.DeserializeObject<KeyboardLayout>(stream.ReadToEnd(), settings);
                Layouts.Add(newLayout);
            }
        }
    }
}
