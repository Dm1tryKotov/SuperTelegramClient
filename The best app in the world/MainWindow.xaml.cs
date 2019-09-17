using System.Windows;
using System.Windows.Input;
using The_best_app_in_the_world.VM;

namespace The_best_app_in_the_world
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyContactsListBox.PreviewMouseDoubleClick += MyContactsListBox_PreviewMouseDoubleClick;
        }

        private void MyContactsListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MyContactsListBox.SelectedItem == null) return;

            (DataContext as MainViewModel).OpenChat(MyContactsListBox.SelectedItem as UserViewModel);
        }
    }
}
