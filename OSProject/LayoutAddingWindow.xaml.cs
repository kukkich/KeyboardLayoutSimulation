using System.Windows;

namespace OSProject
{
    /// <summary>
    /// Логика взаимодействия для LayoutAddingWindow.xaml
    /// </summary>
    public partial class LayoutAddingWindow : Window
    {
        public LayoutAddingWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
