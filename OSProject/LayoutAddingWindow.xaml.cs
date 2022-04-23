using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

            buttonsList.DataContext = ViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonsList_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
