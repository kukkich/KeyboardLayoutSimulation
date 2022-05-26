using Newtonsoft.Json;
using OSProject.Models;
using OSProject.Models.Config;
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
        public readonly LayoutsConfig LayoutsConfig;
        public readonly string layoutsDirectoryRoot;
        public event PropertyChangedEventHandler PropertyChanged;

        private string _value;
        private KeyboardLayout _currentLayout;
        private readonly DirectoryInfo _rootDirectory;

        public AppViewModel(string content, LayoutsConfig defaultLayoutConfig)
        {
            LayoutsConfig = defaultLayoutConfig;
            Value = content;
            Layouts = new ObservableCollection<KeyboardLayout>();

            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            layoutsDirectoryRoot = new DirectoryInfo(appRoot).Parent
                .GetDirectories()
                .FirstOrDefault(directory => directory.Name == "Layouts")
                ?.FullName;

            if (layoutsDirectoryRoot is null)
            {
                MessageBox.Show("Нет дирректории Layouts");
                System.Environment.Exit(1);
            }

            _rootDirectory = new DirectoryInfo(layoutsDirectoryRoot);
        }

        public void SetLayout(string name)
        {
            CurrentLayout = Layouts.First(x => x.Name == name);
        }

        public void UpdateLayouts()
        {
            EnsureExamplesCreated();
            var layoutsData = _rootDirectory.GetFiles();
            Layouts.Clear();
            foreach (var layoutFile in layoutsData)
            { 
                ReadKeyboardLayout(layoutFile);
            }
            CurrentLayout = Layouts.First();
            OnPropertyChanged(nameof(CurrentLayout));
        }

        public char GetConfiguredCharacter(int characterId)
        {
            return LayoutsConfig.DefaultLayoutCongfig.GetCharacterById(characterId);
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

        private void EnsureExamplesCreated()
        {

            if (!LayoutsConfig.LayoutsExamples
                .All(example =>
                    Layouts.Any(layout => layout.Name == example.Name)
                ))
            {
                foreach (var layoutExample in LayoutsConfig.LayoutsExamples)
                {
                    File.WriteAllText(
                        Path.Combine(_rootDirectory.FullName, layoutExample.Name + ".json"),
                        JsonConvert.SerializeObject(layoutExample, Formatting.Indented)
                    );
                }
            }
        }
    }
}
