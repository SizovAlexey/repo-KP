using CarShowLada.Entities;
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
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        /// <summary>
        /// Конструктор страницы Сотрудники
        /// </summary>
        public WorkersPage()
        {
            InitializeComponent();
            //Разграничение прав доступа
            if (App.CurrentUser.id_Role == 1)
            {
                //Установка видимости кнопки "Добавить сотрудника", при работе администратора
                BtnAddWorker.Visibility = Visibility.Visible;
            }
            else
            {
                //Установка невидимости кнопки "Добавить сотрудника", при работе пользователя
                BtnAddWorker.Visibility = Visibility.Collapsed;
            }
            LViewWorkers.ItemsSource = App.Context.Workers.ToList();
            ComboSortByBirthdateWorker.SelectedIndex = 0;
            UpdateWorkers();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка сортировки
        /// </summary>
        private void ComboSortByBirthdateWorker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateWorkers();
        }

        /// <summary>
        /// Обработчик события "Изменение текста" строки поиска
        /// </summary>
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateWorkers();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Редактировать"
        /// </summary>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу редактирования сотрудника с обозначением текущего сотрудника
            var currentWorker = (sender as Button).DataContext as Entities.Worker;
            NavigationService.Navigate(new AddEditWorkerPage(currentWorker));
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Добавить сотрудника"
        /// </summary>
        private void BtnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу добавления сотрудника
            NavigationService.Navigate(new AddEditWorkerPage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Удалить"
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Подтверждение удаления текущего сотрудника
                var currentWorker = (sender as Button).DataContext as Entities.Worker;
                if (MessageBox.Show($"Вы уверены, что хотите удалить сотрудника: " + $"{currentWorker.Surname_worker} {currentWorker.Name_worker} {currentWorker.Patronymic_worker}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Удаление объекта (строки) таблицы Worker (Сотрудник)
                    App.Context.Workers.Remove(currentWorker);
                    App.Context.SaveChanges();
                    UpdateWorkers();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод, который осуществляет представление элементов в зависимости от выбранной сортировки или поиска
        /// </summary>
        private void UpdateWorkers()
        {
            var workers = App.Context.Workers.ToList();
            if (ComboSortByBirthdateWorker.SelectedIndex == 0)
                workers = workers.OrderBy(p => p.Birthdate_worker).ToList();
            else
                workers = workers.OrderByDescending(p => p.Birthdate_worker).ToList();
            workers = workers.Where(p => p.Surname_worker.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Name_worker.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Patronymic_worker.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewWorkers.ItemsSource = workers;
        }

        /// <summary>
        /// Обработчик события "Загрузка страницы"
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateWorkers();
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
                fd.Blocks.Add(new Paragraph(new Run("Список сотрудников\v")));
                foreach (var item in LViewWorkers.Items)
                {
                    fd.Blocks.Add(new Paragraph(new Run((item as Worker).ToStringIntoPDF())));
                }
                //Открытие диалогового окна печати
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() != true) return;
                fd.PageHeight = pd.PrintableAreaHeight;
                fd.PageWidth = pd.PrintableAreaWidth;
                IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;
                pd.PrintDocument(idocument.DocumentPaginator, "Печать");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
