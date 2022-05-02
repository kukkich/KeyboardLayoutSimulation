using Microsoft.Extensions.Configuration;
using OSProject.Models;
using OSProject.Models.Config;
using OSProject.Models.UI;
using OSProject.ViewModels;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OSProject
{
    public partial class MainWindow : Window
    {
        private readonly AppViewModel _viewModel;
        private static readonly Size _buttonSize = new Size(40, 40);
        private static readonly double _buttonHorizontalSpaceBetween = 10;
        private static readonly double _buttonVerticalSpaceBetween = 10;
        private SolidColorBrush _keyboardButtonColor;
        private SolidColorBrush _keyboardButtonforegroud;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            LayoutsConfig layoutsConfig = config.GetRequiredSection(nameof(LayoutsConfig)).Get<LayoutsConfig>();
            
            _viewModel = new AppViewModel("Ваш текст", layoutsConfig);

            textBlock.DataContext = _viewModel;
            keyboardLayoutsPanel.DataContext = _viewModel;

            _viewModel.UpdateLayouts();
            _viewModel.SetLayout("Eng");

            SetColors();

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

        private void SetColors()
        {
            _keyboardButtonColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#1c6dd0");
            _keyboardButtonforegroud = (SolidColorBrush)new BrushConverter().ConvertFromString("#ffffff");
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

                UIbutton.AddOrChangeValue("BindedValue", button.Value);
                UIbutton.AddOrChangeValue("Id", button.Id);

                UIbutton.Foreground = _keyboardButtonforegroud;
                UIbutton.Background = _keyboardButtonColor;
                UIbutton.FontSize += 2;
                UIbutton.FontFamily = new FontFamily("Arial Rounded MT Bold");

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
                _viewModel.Value += _viewModel.CurrentLayout.GetBottonValue((int)button.GetData("Id"));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Clear();
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RemoveLastChar();
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
            LayoutAddingWindow layoutAddingWindow = new LayoutAddingWindow(_viewModel.LayoutsConfig.DefaultLayoutCongfig, _viewModel.layoutsDirectoryRoot);

            var previousLayout = _viewModel.CurrentLayout;
            if (layoutAddingWindow.ShowDialog() == true)
                _viewModel.UpdateLayouts();

        }

        private void textBlock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
