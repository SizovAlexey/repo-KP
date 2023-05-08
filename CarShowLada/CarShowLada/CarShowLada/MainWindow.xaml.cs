using System.Windows;

namespace CarShowLada
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Pages.LoginPage());
        }

        /// <summary>
        /// Обработчик события Click кнопки "Назад". Переход на предыдущую страницу
        /// </summary>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameMain.CanGoBack)
                FrameMain.GoBack();
        }
    }
}
