using System;
using System.Windows;
using WpfApp5.ViewModel;


namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;

            if (vm.CloseAction == null) 
            {
                vm.CloseAction = new Action(this.Close);
            }               
        }
    }
}
