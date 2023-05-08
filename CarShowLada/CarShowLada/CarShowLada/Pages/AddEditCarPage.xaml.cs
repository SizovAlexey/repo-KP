using CarShowLada.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using TextCorrectLib;
using Color = CarShowLada.Entities.Color;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditCarPage.xaml
    /// </summary>
    public partial class AddEditCarPage : Page
    {
        private Entities.Car _currentCar = null; //Создание объекта (строки) таблицы Car (Автомобиль) для обозначения текущего автомобиля
        private byte[] _imageData; //Создание массива байтов для заполнения картинки автомобиля
        public List<Color> ListColor { get; set; } //Создание списка элементов таблицы Color (Цвет) для заполнения значений выпадающего списка
        public List<Body> ListBody { get; set; } //Создание списка элементов таблицы Body (Кузов) для заполнения значений выпадающего списка
        public List<Drive> ListDrive { get; set; } //Создание списка элементов таблицы Drive (Привод) для заполнения значений выпадающего списка

        /// <summary>
        /// Метод, который заполняет элементы Выпадающий список значениями из базы данных
        /// </summary>
        private void SetComboBoxData()
        {
            CarShowLadaEntities dc = new CarShowLadaEntities();
            var itemColor = dc.Colors.ToList();
            ListColor = itemColor;
            var itemBody = dc.Bodies.ToList();
            ListBody = itemBody;
            var itemDrive = dc.Drives.ToList();
            ListDrive = itemDrive;
            DataContext = this;
        }

        /// <summary>
        /// Конструктор страницы добавления автомобиля
        /// </summary>
        public AddEditCarPage()
        {
            InitializeComponent();
            SetComboBoxData();
        }

        /// <summary>
        /// Конструктор страницы редактирования текущего автомобиля
        /// </summary>
        public AddEditCarPage(Entities.Car currentCar)
        {
            try
            {
                //Заполнение полей данными текущего автомобиля
                SetComboBoxData();
                InitializeComponent();
                _currentCar = currentCar;
                Title = "Редактирование автомобиля";
                TBoxModel.Text = _currentCar.Model;
                TBoxCost.Text = _currentCar.Cost.ToString("N2");
                for (int i = 0; i < ListColor.Count; i++)
                {
                    if (ListColor[i].id_Color == _currentCar.id_Color)
                    {
                        ChangeColor.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < ListBody.Count; i++)
                {
                    if (ListBody[i].id_Body == _currentCar.id_Body)
                    {
                        ChangeBody.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < ListDrive.Count; i++)
                {
                    if (ListDrive[i].id_Drive == _currentCar.id_Drive)
                    {
                        ChangeDrive.SelectedIndex = i;
                        break;
                    }
                }
                TBoxEnginePower.Text = _currentCar.Engine_power.ToString();
                TBoxCountCar.Text = _currentCar.CountCar.ToString();
                if (_currentCar.Image != null)
                    ImageCar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_currentCar.Image);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Выбрать фотографию"
        /// </summary>
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            //Диалоговое окно открытие файла с расширением: .png, .jpg или .jpeg
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.ipg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _imageData = File.ReadAllBytes(ofd.FileName);
                ImageCar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_imageData);
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
                    //Если текущий автомобиль не обозначен, значит идёт операция добавления автомобиля. Создание нового объекта (строки) таблицы Car (Автомобиль)
                    if (_currentCar == null)
                    {
                        var car = new Entities.Car
                        {
                            Model = TBoxModel.Text,
                            Cost = decimal.Parse(TBoxCost.Text),
                            id_Color = ListColor[ChangeColor.SelectedIndex].id_Color,
                            id_Body = ListBody[ChangeBody.SelectedIndex].id_Body,
                            id_Drive = ListDrive[ChangeDrive.SelectedIndex].id_Drive,
                            Engine_power = int.Parse(TBoxEnginePower.Text),
                            CountCar = int.Parse(TBoxCountCar.Text),
                            Image = _imageData
                        };
                        App.Context.Cars.Add(car);
                        App.Context.SaveChanges();
                        MessageBox.Show("Запись успешно добавлена");
                    }
                    else
                    {
                        //Редактирование текущего автомобиля. Изменение данных строки в таблице Car (Автомобиль)
                        _currentCar.Model = TBoxModel.Text;
                        _currentCar.Cost = decimal.Parse(TBoxCost.Text);
                        _currentCar.id_Color = ListColor[ChangeColor.SelectedIndex].id_Color;
                        _currentCar.id_Body = ListBody[ChangeBody.SelectedIndex].id_Body;
                        _currentCar.id_Drive = ListDrive[ChangeDrive.SelectedIndex].id_Drive;
                        _currentCar.Engine_power = int.Parse(TBoxEnginePower.Text);
                        _currentCar.CountCar = int.Parse(TBoxCountCar.Text);
                        if (_imageData != null)
                            _currentCar.Image = _imageData;
                        App.Context.SaveChanges();
                        MessageBox.Show("Запись успешно отредактирована");
                    }
                    NavigationService.GoBack();
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

            if (string.IsNullOrWhiteSpace(TBoxModel.Text))
                errorBuilder.AppendLine("Название модели обязательно для заполнения;");

            var carFromDB = App.Context.Cars.ToList().FirstOrDefault(p => p.Model.ToLower() == TBoxModel.Text.ToLower()
                && p.Cost.ToString() == TBoxCost.Text
                && p.id_Color == ListColor[ChangeColor.SelectedIndex].id_Color
                && p.id_Body == ListBody[ChangeBody.SelectedIndex].id_Body
                && p.id_Drive == ListDrive[ChangeDrive.SelectedIndex].id_Drive
                && p.Engine_power.ToString() == TBoxEnginePower.Text);
            if (carFromDB != null && carFromDB != _currentCar)
                errorBuilder.AppendLine("Такой автомобиль уже есть в базе данных;");

            if (!string.IsNullOrWhiteSpace(TBoxCost.Text))
            {
                decimal cost = 0;
                if (decimal.TryParse(TBoxCost.Text, out cost) == false || cost <= 0)
                    errorBuilder.AppendLine("Цена автомобиля должна быть положительным числом;");
            }
            else
            {
                errorBuilder.AppendLine("Цена автомобиля обязательна для заполнения;");
            }

            if (ChangeColor.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор цвета автомобиля обязателен;");

            if (ChangeBody.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор типа кузова автомобиля обязателен;");

            if (ChangeBody.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор привода автомобиля обязателен;");

            if (!string.IsNullOrWhiteSpace(TBoxEnginePower.Text))
            {
                int power = 0;
                if (int.TryParse(TBoxEnginePower.Text, out power) == false || power <= 0)
                    errorBuilder.AppendLine("Мощность двигателя автомобиля должна быть положительным числом;");
            }
            else
            {
                errorBuilder.AppendLine("Мощность двигателя автомобиля обязательна для заполнения;");
            }

            if (!string.IsNullOrWhiteSpace(TBoxCountCar.Text))
            {
                int count = 0;
                if (int.TryParse(TBoxCountCar.Text, out count) == false || count < 0)
                    errorBuilder.AppendLine("Количество автомобилей должно быть положительным числом;");
            }
            else
            {
                errorBuilder.AppendLine("Количество автомобилей обязательно для заполнения;");
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
