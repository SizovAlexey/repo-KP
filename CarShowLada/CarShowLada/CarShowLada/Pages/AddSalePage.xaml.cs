using CarShowLada.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TextCorrectLib;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditSalePage.xaml
    /// </summary>
    public partial class AddSalePage : Page
    {
        public List<Branch> ListBranch { get; set; } //Создание списка элементов таблицы Branch (Филиал) для заполнения значений выпадающего списка
        public List<Worker> ListWorker { get; set; } //Создание списка элементов таблицы Worker (Сотрудник) для заполнения значений выпадающего списка
        public List<Car> ListCar { get; set; } //Создание списка элементов таблицы Car (Автомобиль) для заполнения значений выпадающего списка

        /// <summary>
        /// Метод, который заполняет элементы Выпадающий список значениями из базы данных
        /// </summary>
        private void SetComboBoxData()
        {
            CarShowLadaEntities dc = new CarShowLadaEntities();
            var itemBranch = dc.Branches.ToList();
            ListBranch = itemBranch;
            var itemWorker = dc.Workers.ToList();
            ListWorker = itemWorker;
            var itemCar = dc.Cars.ToList();
            ListCar = itemCar;
            DataContext = this;
        }

        /// <summary>
        /// Конструктор страницы добавления записи продажи
        /// </summary>
        public AddSalePage()
        {
            InitializeComponent();
            SetComboBoxData();
            DPickerDateSale.SelectedDate = DateTime.Today;
        }

        /// <summary>
        /// Конструктор страницы добавления записи продажи с уже вбранным автомобилем
        /// </summary>
        public AddSalePage(Entities.Car currentCar)
        {
            try
            {
                //Заполнение даты продажи текущей датой, выбор текущего автомобиля и запрет на изменение выбора автомобиля
                InitializeComponent();
                SetComboBoxData();
                DPickerDateSale.SelectedDate = DateTime.Today;
                for (int i = 0; i < ListCar.Count; i++)
                {
                    if (ListCar[i].id_Car == currentCar.id_Car)
                    {
                        ChangeCar.SelectedIndex = i;
                        break;
                    }
                }
                ChangeCar.IsEnabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Сохранить"
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка на то, есть ли ошибки в введённых данных
                var errorMessage = CheckErrors();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //Создание нового объекта (строки) таблицы Client (Клиент)
                    var client = new Entities.Client
                    {
                        Surname_client = TBoxSurnameClient.Text,
                        Name_client = TBoxNameClient.Text,
                        Patronymic_client = TBoxPatronymicClient.Text,
                        Birthdate_client = DateTime.Parse(DPickerBirthdateClient.Text),
                        Telephone_number_client = TBoxNumberPhoneClient.Text
                    };
                    //Создание нового объекта (строки) таблицы Sale (Продажа)
                    var sale = new Entities.Sale
                    {
                        Date_sale = DateTime.Parse(DPickerDateSale.Text),
                        id_Branch = ListBranch[ChangeBranch.SelectedIndex].id_Branch,
                        id_Worker = ListWorker[ChangeWorker.SelectedIndex].id_Worker,
                        id_Client = client.id_Client,
                        id_Car = ListCar[ChangeCar.SelectedIndex].id_Car
                    };
                    //Проверка на то, есть ли в наличии выбранный автомобиль
                    var carFromDB = App.Context.Cars.ToList().FirstOrDefault(p => p.id_Car == sale.id_Car);
                    if (carFromDB.CountCar > 0)
                    {
                        carFromDB.CountCar--;
                        App.Context.Clients.Add(client);
                        App.Context.Sales.Add(sale);
                        App.Context.SaveChanges();
                        MessageBox.Show("Запись успешно добавлена");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Данного автомобиля нет в наличии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод, который проверят введённые пользователем данные на корректность и возвращает найденные ошибки в виде последовательности строк
        /// </summary>
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(DPickerDateSale.Text))
                errorBuilder.AppendLine("Дата продажи обязательна для заполнения;");

            var saleFromDB = App.Context.Sales.ToList().FirstOrDefault(p => p.Date_sale == DateTime.Parse(DPickerDateSale.Text) 
                && p.id_Branch == ListBranch[ChangeBranch.SelectedIndex].id_Branch 
                && p.id_Worker == ListWorker[ChangeWorker.SelectedIndex].id_Worker
                && p.Client.Surname_client == TBoxSurnameClient.Text
                && p.Client.Name_client == TBoxNameClient.Text
                && p.Client.Patronymic_client == TBoxPatronymicClient.Text
                && p.Client.Birthdate_client == DateTime.Parse(DPickerBirthdateClient.Text)
                && p.Client.Telephone_number_client == TBoxNumberPhoneClient.Text
                && p.id_Car == ListCar[ChangeCar.SelectedIndex].id_Car);
            if (saleFromDB != null)
                errorBuilder.AppendLine("Такая запись продажи уже есть в базе данных;");

            if (ChangeBranch.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор филиала обязателен;");

            if (ChangeWorker.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор сотрудника обязателен;");

            if (!string.IsNullOrWhiteSpace(TBoxSurnameClient.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxSurnameClient.Text))
                    errorBuilder.AppendLine("Некорректно введена фамилия клиента;");
            }
            else
            {
                errorBuilder.AppendLine("Фамилия клиента обязательна для заполнения;");
            }

            if (!string.IsNullOrWhiteSpace(TBoxNameClient.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxNameClient.Text))
                    errorBuilder.AppendLine("Некорректно введено имя клиента;");
            }
            else
            {
                errorBuilder.AppendLine("Имя клиента обязательно для заполнения;");
            }

            if (!string.IsNullOrWhiteSpace(TBoxPatronymicClient.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxPatronymicClient.Text))
                    errorBuilder.AppendLine("Некорректно введено отчество клиента;");
            }
            else
            {
                errorBuilder.AppendLine("Отчество клиента обязательно для заполнения;");
            }

            if (string.IsNullOrEmpty(DPickerBirthdateClient.Text))
                errorBuilder.AppendLine("Дата рождения клиента обязательна для заполнения;");

            if (!string.IsNullOrWhiteSpace(TBoxNumberPhoneClient.Text))
            {
                if (!TextCorrect.IsNumberPhoneCorrect(TBoxNumberPhoneClient.Text))
                    errorBuilder.AppendLine("Некорректно введен номер телефона клиента (11 цифр без пробелов);");
            }
            else
            {
                errorBuilder.AppendLine("Номер телефона клиента обязателен для заполнения;");
            }

            if (ChangeCar.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор автомобиля обязателен;");

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
