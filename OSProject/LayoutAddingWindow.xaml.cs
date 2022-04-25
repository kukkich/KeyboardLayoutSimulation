using OSProject.Models;
using OSProject.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OSProject
{
    public partial class LayoutAddingWindow : Window
    {
        private readonly LayoutAddingViewModel _viewModel;

        public LayoutAddingWindow(DefaultKeyboardLayoutConfig layoutConfig)
        {
            _viewModel = new LayoutAddingViewModel(layoutConfig);

            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            nameTextField.DataContext = _viewModel;
            buttonsList.DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.CreateNewLayout();
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
