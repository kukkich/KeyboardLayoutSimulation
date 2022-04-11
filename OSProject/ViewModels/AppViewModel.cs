﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OSProject.Models;
using System.IO;
using System.Collections.ObjectModel;

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

        private string _value;
        private KeyboardLayout _currentLayout;
        private DirectoryInfo _rootDirectory;
        private string _layoutsDirectoryRoot = @"C:\Users\vitia\source\repos\C#\WPF\OSProject\OSProject\Layouts\";
        private string _layoutsFileExtension = ".txt";

        public AppViewModel(string content)
        {
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
            foreach (var layoutFile in layoutsData)
            {
                ReadKeyboardLayout(layoutFile.Name.Remove(
                    layoutFile.Name.Length - _layoutsFileExtension.Length
                ));
            }
        }

        private void ReadKeyboardLayout(string layoutName)
        {
            using (StreamReader stream = new StreamReader(_layoutsDirectoryRoot + layoutName + _layoutsFileExtension))
            {
                Layouts.Add(new KeyboardLayout(layoutName, stream));
            }
        }

        private void ChangeLayout(string layoutName)
        {
            CurrentLayout = Layouts.First(x => x.Name ==layoutName);
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}