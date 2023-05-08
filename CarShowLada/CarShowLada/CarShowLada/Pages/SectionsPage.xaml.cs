using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для SectionsPage.xaml
    /// </summary>
    public partial class SectionsPage : Page
    {
        /// <summary>
        /// Конструктор страницы с разделами
        /// </summary>
        public SectionsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Автомобили"
        /// </summary>
        private void BtnCars_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу Автомобили
            NavigationService.Navigate(new CarsPage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Продажи"
        /// </summary>
        private void BtnSales_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу Продажи
            NavigationService.Navigate(new SalesPage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Сотрудники"
        /// </summary>
        private void BtnWorkers_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу Сотрудники
            NavigationService.Navigate(new WorkersPage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Должности"
        /// </summary>
        private void BtnPosts_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу Должности
            NavigationService.Navigate(new PostsPage());
        }
    }
}
