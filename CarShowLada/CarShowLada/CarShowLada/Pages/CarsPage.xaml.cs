using CarShowLada.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using Color = CarShowLada.Entities.Color;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        public List<Color> ListColor { get; set; } //Создание списка элементов таблицы Color (Цвет) для заполнения значений выпадающего списка
        public List<Body> ListBody { get; set; } //Создание списка элементов таблицы Body (Кузов) для заполнения значений выпадающего списка
        public List<Drive> ListDrive { get; set; } //Создание списка элементов таблицы Drive (Привод) для заполнения значений выпадающего списка

        /// <summary>
        /// Метод, который заполняет элементы Выпадающий список значениями из базы данных
        /// </summary>
        private void SetComboBoxData()
        {
            ListColor = App.Context.Colors.ToList();
            ListBody = App.Context.Bodies.ToList();
            ListDrive = App.Context.Drives.ToList();
            //Перед заполнением значений из базы данных во все выпадающие списки вносится значение "Все"
            ComboFilterBody.Items.Add("Все");
            ComboFilterColor.Items.Add("Все");
            ComboFilterDrive.Items.Add("Все");
            foreach (var body in ListBody)
            {
                ComboFilterBody.Items.Add(body.Name_body);
            }
            foreach (var color in ListColor)
            {
                ComboFilterColor.Items.Add(color.Name_color);
            }
            foreach (var drive in ListDrive)
            {
                ComboFilterDrive.Items.Add(drive.Name_drive);
            }
        }

        /// <summary>
        /// Конструктор страницы Автомобили
        /// </summary>
        public CarsPage()
        {
            InitializeComponent();
            SetComboBoxData();
            //Разграничение прав доступа
            if (App.CurrentUser.id_Role == 1)
            {
                //Установка видимости кнопки "Добавить автомобиль" и флажка "Отображать автомобили, которых нет в наличии", при работе администратора
                BtnAddCar.Visibility = Visibility.Visible;
                CheckAvailabilityCar.Visibility = Visibility.Visible;
            }
            else
            {
                //Установка невидимости кнопки "Добавить автомобиль" и флажка "Отображать автомобили, которых нет в наличии", при работе пользователя
                BtnAddCar.Visibility = Visibility.Collapsed;
                CheckAvailabilityCar.Visibility = Visibility.Collapsed;
            }
            LViewCars.ItemsSource = App.Context.Cars.ToList();
            ComboSort.SelectedIndex = 0;
            ComboFilterBody.SelectedIndex = 0;
            ComboFilterColor.SelectedIndex = 0;
            ComboFilterDrive.SelectedIndex = 0;
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка сортировки
        /// </summary>
        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Изменение текста" строки поиска
        /// </summary>
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка фильтрации по кузову
        /// </summary>
        private void ComboFilterBody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка фильтрации по цвету
        /// </summary>
        private void ComboFilterColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка фильтрации по приводу
        /// </summary>
        private void ComboFilterDrive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Редактировать"
        /// </summary>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу редактирования автомобиля с обозначением текущего автомобиля
            var currentCar = (sender as Button).DataContext as Entities.Car;
            NavigationService.Navigate(new AddEditCarPage(currentCar));
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Добавить автомобиль"
        /// </summary>
        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу добавления автомобиля
            NavigationService.Navigate(new AddEditCarPage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Удалить"
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Подтверждение удаления текущего автомобиля
                var currentCar = (sender as Button).DataContext as Entities.Car;
                if (MessageBox.Show($"Вы уверены, что хотите удалить автомобиль: " + $"{currentCar.Model} {currentCar.Body.Name_body} {currentCar.Color}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Удаление объекта (строки) таблицы Car (Автомобиль)
                    App.Context.Cars.Remove(currentCar);
                    App.Context.SaveChanges();
                    UpdateCars();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Купить"
        /// </summary>
        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу добавления записи продажи с обозначением текущего автомобиля
            var currentCar = (sender as Button).DataContext as Entities.Car;
            NavigationService.Navigate(new AddSalePage(currentCar));
        }

        /// <summary>
        /// Обработчик события "Установлен" флажка "Отображать автомобили, которых нет в наличии"
        /// </summary>
        private void CheckAvailabilityCar_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Обработчик события "Неустановлен" флажка "Отображать автомобили, которых нет в наличии"
        /// </summary>
        private void CheckAvailabilityCar_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateCars();
        }

        /// <summary>
        /// Метод, который осуществляет представление элементов в зависимости от выбранной сортировки, фильтрации или поиска
        /// </summary>
        private void UpdateCars()
        {
            var cars = App.Context.Cars.ToList();
            if (ComboSort.SelectedIndex == 0)
                cars = cars.OrderBy(p => p.Cost).ToList();
            if (ComboSort.SelectedIndex == 1)
                cars = cars.OrderByDescending(p => p.Cost).ToList();
            if (ComboSort.SelectedIndex == 2)
                cars = cars.OrderBy(p => p.CountCar).ToList();
            if (ComboSort.SelectedIndex == 3)
                cars = cars.OrderByDescending(p => p.CountCar).ToList();
            if (CheckAvailabilityCar.IsChecked == false)
                cars = cars.Where(p => p.CountCar > 0).ToList();
            if (ComboFilterBody.SelectedIndex > 0)
                cars = cars.Where(p => p.Body.Name_body == ComboFilterBody.SelectedValue.ToString()).ToList();
            if (ComboFilterColor.SelectedIndex > 0)
                cars = cars.Where(p => p.Color.Name_color == ComboFilterColor.SelectedValue.ToString()).ToList();
            if (ComboFilterDrive.SelectedIndex > 0)
                cars = cars.Where(p => p.Drive.Name_drive == ComboFilterDrive.SelectedValue.ToString()).ToList();
            cars = cars.Where(p => p.Model.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewCars.ItemsSource = cars;
        }

        /// <summary>
        /// Обработчик события "Загрузка страницы"
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCars();
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
                fd.Blocks.Add(new Paragraph(new Run("Список автомобилей\v")));
                foreach (var item in LViewCars.Items)
                {
                    fd.Blocks.Add(new Paragraph(new Run((item as Car).ToStringIntoPDF())));
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
