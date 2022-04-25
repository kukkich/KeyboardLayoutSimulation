using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System;
using OSProject.Models;
using OSProject.ViewModels;

namespace OSProject
{
    public partial class LayoutAddingWindow : Window
    {
        public LayoutAddingViewModel ViewModel;

        public LayoutAddingWindow(DefaultKeyboardLayoutConfig layoutConfig)
        {
            ViewModel = new LayoutAddingViewModel(layoutConfig);
            
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            nameTextField.DataContext = ViewModel;
            buttonsList.DataContext = ViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.CreateNewLayout();
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeValueButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button button &&
                button.DataContext is ButtonSetting buttonSetting)
            {
                ButtonValueChangingWindow valueChangingWindow = new ButtonValueChangingWindow(buttonSetting);
                valueChangingWindow.Owner = this;
                valueChangingWindow.ShowDialog();
            }
        }
    }
}
