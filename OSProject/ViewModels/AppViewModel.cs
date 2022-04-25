using OSProject.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using OSProject.Extensions;

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
        private string _value;
        private KeyboardLayout _currentLayout;
        private DirectoryInfo _rootDirectory;
        private string _layoutsDirectoryRoot = @"C:\Users\vitia\source\repos\C#\WPF\OSProject\OSProject\Layouts\";
        private string _layoutsFileExtension = ".txt";

        public AppViewModel(string content, DefaultKeyboardLayoutConfig defaultLayoutConfig)
        {
            LayoutConfig = defaultLayoutConfig;
            Value = content;
            Layouts = new ObservableCollection<KeyboardLayout>();
            _rootDirectory = new DirectoryInfo(_layoutsDirectoryRoot);
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ReadKeyboardLayout(FileInfo file)
        {
            string layoutName = file.NameWithoutExtension();
            if (!file.Extension.Equals(".json"))
            {
                using (StreamReader stream = new StreamReader(_layoutsDirectoryRoot + layoutName + _layoutsFileExtension))
                {
                    Layouts.Add(new KeyboardLayout(layoutName, stream));
                }
            }
            else
            {
                // обработка Json
            }
        }

        private void ChangeLayout(string layoutName)
        {
            CurrentLayout = Layouts.First(x => x.Name == layoutName);
        }
    }
}
