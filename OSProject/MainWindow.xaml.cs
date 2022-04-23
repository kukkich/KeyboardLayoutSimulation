using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OSProject.Models;
using OSProject.Models.UI;
using OSProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OSProject
{
    public partial class MainWindow : Window
    {
        // Добавить ViewModel для добавления новой раскладки
        private AppViewModel _viewModel;
        private static readonly Size _buttonSize = new Size(40, 40);
        private static readonly double _buttonHorizontalSpaceBetween = 10;
        private static readonly double _buttonVerticalSpaceBetween = 10;

        public MainWindow()
        {
            #region Serializing and Derialize
            var layout = new KeyboardLayout(
                "бебра",
                new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>() { new KeyboardButton(1, 'x'), new KeyboardButton(2, 'l')},
                new List<KeyboardButton>() { new KeyboardButton(3, 'g') }
            });

            string json = JsonConvert.SerializeObject(layout, Formatting.Indented);
            Console.WriteLine(json);

            JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented };

            var newLayout = JsonConvert.DeserializeObject<KeyboardLayout>(json, settings);
            #endregion



            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            DefaultKeyboardLayoutConfig layoutConfig = config
                .GetSection(nameof(DefaultKeyboardLayoutConfig))
                .Get<DefaultKeyboardLayoutConfig>();


            _viewModel = new AppViewModel("Зелибиба", layoutConfig);

            textBlock.DataContext = _viewModel;
            keyboardLayoutsPanel.DataContext = _viewModel;

            // Считать сразу всё из файла и подставлять нужную раскладку
            // в зависимости от выбранного чекбокса
            _viewModel.UpdateLayouts();
            _viewModel.SetLayout("Eng");
            ShowKeyboard();
        }

        private void ShowKeyboard()
        {
            keyboardCanvas.Children.Clear();
            int lineNumber = 0;
            foreach (List<KeyboardButton> line in _viewModel.CurrentLayout)
            {
                ShowKeyboardLine(line, lineNumber);
                lineNumber++;
            }
        }

        private void ShowKeyboardLine(List<KeyboardButton> line, int lineNumber)
        {
            int lineButtonNumber = 0;
            int buttonsNumber = line.Count();
            double lineLength =
                buttonsNumber * _buttonSize.Width +
                (buttonsNumber - 1) * _buttonHorizontalSpaceBetween;
            double linePadding = (1270 * 0.7 - lineLength) / 2;

            foreach (KeyboardButton button in line)
            {

                DataButton UIbutton = new DataButton
                {
                    Content = _viewModel.GetConfiguredCharacter(button.Id),
                    Width = _buttonSize.Width,
                    Height = _buttonSize.Height
                };

                // it migth need in fixing 
                UIbutton.AddOrChangeValue("BindedValue", button.Value);
                UIbutton.AddOrChangeValue("Id", button.Id);

                UIbutton.SetValue(
                    Canvas.LeftProperty,
                    lineButtonNumber * (_buttonHorizontalSpaceBetween + _buttonSize.Width) +
                    linePadding
                );
                UIbutton.SetValue(
                    Canvas.TopProperty,
                    lineNumber * (_buttonVerticalSpaceBetween + _buttonSize.Height) + 35
                );

                keyboardCanvas.Children.Add(UIbutton);
                lineButtonNumber++;
            }
        }

        private void KeyboardCanvas_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is DataButton button)
            {
                _viewModel.Value += _viewModel.CurrentLayout.GetBottonValue((int)button.GetData("Id"));
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Clear();
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RemoveLastChar();
        }

        private void ReadLayouts()
        {

        }

        private void ShowLayoutsPanel()
        {
        }

        private void KeyboardLayoutsPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is Selector selectedItem &&
                selectedItem.SelectedItem is KeyboardLayout layout)
            {
                _viewModel.SetLayout(layout.Name);
                ShowKeyboard();
            }
        }

        private void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            LayoutAddingWindow layoutAddingWindow = new LayoutAddingWindow(_viewModel.LayoutConfig);

            if (layoutAddingWindow.ShowDialog() == true)
            {
                MessageBox.Show("Ультра харош!");

            }
            else
            {
                MessageBox.Show("Вы мистер лох");
            }

        }
    }
}
