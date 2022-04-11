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
using OSProject.ViewModels;

namespace OSProject
{
    public partial class LayoutAddingWindow : Window
    {
        public LayoutAddingViewModel viewModel;
        public LayoutAddingWindow(AppViewModel appViewModel)
        {
            viewModel = new LayoutAddingViewModel(appViewModel, String.Empty, String.Empty);

            InitializeComponent();
        }

        private void AddLayout_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TryUpdate(name.Text, layout.Text);
            if (viewModel.IsValid())
                this.DialogResult = true;
            else
                MessageBox.Show(viewModel.Problem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
