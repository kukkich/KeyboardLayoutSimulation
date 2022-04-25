using OSProject.Models;
using OSProject.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OSProject
{
    public partial class ButtonValueChangingWindow : Window
    {
        private readonly ButtonValueChangingViewModel _viewModel;

        public ButtonValueChangingWindow(ButtonSetting buttonSetting)
        {
            _viewModel = new ButtonValueChangingViewModel(buttonSetting);

            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            textField.Focus();

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
