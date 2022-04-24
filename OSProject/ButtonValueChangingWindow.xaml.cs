using OSProject.Models;
using OSProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OSProject
{
    public partial class ButtonValueChangingWindow : Window
    {
        private ButtonValueChangingViewModel _viewModel { get; set; }

        public ButtonValueChangingWindow(ButtonSetting buttonSetting)
        {
            _viewModel = new ButtonValueChangingViewModel(buttonSetting);
            
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.DataContext = _viewModel;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Confirm();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            Confirm();
        }

        private void TextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textField.Text.Length > 1) textField.Text = textField.Text.Remove(1);
            else _viewModel.NewValue = textField.Text;
        }

        private void Confirm()
        {
            _viewModel.AcceptСhanges();
            this.DialogResult = true;
        }
    }
}
