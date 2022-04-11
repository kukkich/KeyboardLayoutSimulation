﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OSProject.Models;
using OSProject.ViewModels;

namespace OSProject
{
    public partial class MainWindow : Window
    {
        // Добавить ViewModel для добавления новой раскладки
        private AppViewModel ViewModel { get; set; }

        private static readonly Size _buttonSize = new Size(40, 40);
        private static readonly double _buttonHorizontalSpaceBetween = 10;
        private static readonly double _buttonVerticalSpaceBetween = 10;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            ViewModel = new AppViewModel("Зелибиба");

            textBlock.DataContext = ViewModel;
            keyboardLayoutsPanel.DataContext = ViewModel;

            // Считать сразу всё из файла и подставлять нужную раскладку
            // в зависимости от выбранного чекбокса
            ViewModel.UpdateLayouts();
            ViewModel.SetLayout("Eng");
            ShowKeyboardButtons();
        }

        private void ShowKeyboardButtons()
        {
            keyboardCanvas.Children.Clear();
            int lineNumber = 0;
            foreach (List<KeyboardButton> line in ViewModel.CurrentLayout)
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
                Button UIbutton = new Button
                {
                    Content = button.Value,
                    Width = _buttonSize.Width,
                    Height = _buttonSize.Height
                };

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

        private void Canvas_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button button)
            {
                ViewModel.Value += button.Content;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Clear();
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveLastChar();
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
                ViewModel.SetLayout(layout.Name);
                ShowKeyboardButtons();
            }
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LayoutAddingWindow layoutAddingWindow = new LayoutAddingWindow(ViewModel);

            if (layoutAddingWindow.ShowDialog() == true)
            {
                // добавлять новую раскладку в коллекцию + добавлять новый файл
                ViewModel.Layouts.Add(layoutAddingWindow.viewModel.NewLayout);
            }
            else
            {
                MessageBox.Show("BibleTum :(");
            }
        }

    }
}