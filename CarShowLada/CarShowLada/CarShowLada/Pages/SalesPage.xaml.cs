using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        /// <summary>
        /// Конструктор страницы Продажи
        /// </summary>
        public SalesPage()
        {
            InitializeComponent();
            //Разграничение прав доступа
            if (App.CurrentUser.id_Role == 1)
            {
                //Установка видимости кнопки "Добавить запись продажи", при работе администратора
                BtnAddSale.Visibility = Visibility.Visible;
            }
            else
            {
                //Установка невидимости кнопки "Добавить запись продажи", при работе пользователя
                BtnAddSale.Visibility = Visibility.Collapsed;
            }
            LViewSales.ItemsSource = App.Context.Sales.ToList();
            ComboSortByDateSale.SelectedIndex = 0;
            UpdateSales();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка сортировки
        /// </summary>
        private void ComboSortByDateSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSales();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Добавить запись продажи"
        /// </summary>
        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу добавления записи продажи
            NavigationService.Navigate(new AddSalePage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Удалить"
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Подтверждение удаления текущей записи продажи
                var currentSale = (sender as Button).DataContext as Entities.Sale;
                var currentClient = App.Context.Clients.ToList().FirstOrDefault(p => p.id_Client == currentSale.Client.id_Client);
                if (MessageBox.Show($"Вы уверены, что хотите удалить запись: \n{currentSale.DateSaleText}\n{currentSale.WorkerText}\n{currentSale.ClientText}\n{currentSale.CarText}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Удаление объекта (строки) таблицы CLient (Клиент)
                    App.Context.Clients.Remove(currentClient);
                    //Удаление объекта (строки) таблицы Sale (Продажа)
                    App.Context.Sales.Remove(currentSale);
                    App.Context.SaveChanges();
                    UpdateSales();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод, который осуществляет представление элементов в зависимости от выбранной сортировки
        /// </summary>
        private void UpdateSales()
        {
            var sales = App.Context.Sales.ToList();
            if (ComboSortByDateSale.SelectedIndex == 0)
                sales = sales.OrderBy(p => p.Date_sale).ToList();
            else
                sales = sales.OrderByDescending(p => p.Date_sale).ToList();
            LViewSales.ItemsSource = sales;
        }

        /// <summary>
        /// Обработчик события "Загрузка страницы"
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSales();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Печать"
        /// </summary>
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Предустановки шрифта, размера, разметки PDF файла
                FlowDocument fd = new FlowDocument();
                fd.FontFamily = new FontFamily("Times New Roman");
                fd.FontSize = 14;
                fd.ColumnWidth = 700;
                //Занесение элементов из представления в PDF файл
                fd.Blocks.Add(new Paragraph(new Run("Список продаж\v")));
                foreach (var item in LViewSales.Items)
                {
                    fd.Blocks.Add(new Paragraph(new Run(item.ToString())));
                }
                //Открытие диалогового окна печати
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() != true) return;
                fd.PageHeight = pd.PrintableAreaHeight;
                fd.PageWidth = pd.PrintableAreaWidth;
                IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;
                pd.PrintDocument(idocument.DocumentPaginator, "Печать");
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
