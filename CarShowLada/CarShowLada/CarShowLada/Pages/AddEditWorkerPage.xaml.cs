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

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditWorkerPage.xaml
    /// </summary>
    public partial class AddEditWorkerPage : Page
    {
        private Entities.Worker _currentWorker = null; //Создание объекта (строки) таблицы Worker (Сотрудник) для обозначения текущего сотрудника
        private byte[] _imageData; //Создание массива байтов для заполнения картинки сотрудника
        public List<Post> ListPost { get; set; } //Создание списка элементов таблицы Post (Должность) для заполнения значений выпадающего списка

        /// <summary>
        /// Метод, который заполняет элемент Выпадающий список значениями из базы данных
        /// </summary>
        private void SetComboBoxData()
        {
            CarShowLadaEntities dc = new CarShowLadaEntities();
            var itemPost = dc.Posts.ToList();
            ListPost = itemPost;
            DataContext = this;
        }

        /// <summary>
        /// Конструктор страницы добавления сотрудника
        /// </summary>
        public AddEditWorkerPage()
        {
            InitializeComponent();
            SetComboBoxData();
        }

        /// <summary>
        /// Конструктор страницы редактирования текущего сотрудника
        /// </summary>
        public AddEditWorkerPage(Entities.Worker currentWorker)
        {
            try
            {
                //Заполнение полей данными текущего сотрудника
                SetComboBoxData();
                InitializeComponent();
                _currentWorker = currentWorker;
                Title = "Редактирование сотрудника";
                TBoxSurname.Text = _currentWorker.Surname_worker;
                TBoxName.Text = _currentWorker.Name_worker;
                TBoxPatronymic.Text = _currentWorker.Patronymic_worker;
                DPickerBirthdateWorker.SelectedDate = _currentWorker.Birthdate_worker;
                for (int i = 0; i < ListPost.Count; i++)
                {
                    if (ListPost[i].id_Post == _currentWorker.id_Post)
                    {
                        ChangePost.SelectedIndex = i;
                        break;
                    }
                }
                TBoxNumberPhoneWorker.Text = _currentWorker.Telephone_number_worker;
                if (_currentWorker.Image_worker != null)
                    ImageWorker.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_currentWorker.Image_worker);
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
                ImageWorker.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_imageData);
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
                    //Если текущий сотрудник не обозначен, значит идёт операция добавления сотрудника. Создание нового объекта (строки) таблицы Worker (Сотрудник)
                    if (_currentWorker == null)
                    {
                        var worker = new Entities.Worker
                        {
                            Surname_worker = TBoxSurname.Text,
                            Name_worker = TBoxName.Text,
                            Patronymic_worker = TBoxPatronymic.Text,
                            Birthdate_worker = DateTime.Parse(DPickerBirthdateWorker.Text),
                            id_Post = ListPost[ChangePost.SelectedIndex].id_Post,
                            Telephone_number_worker = TBoxNumberPhoneWorker.Text,
                            Image_worker = _imageData
                        };
                        App.Context.Workers.Add(worker);
                        App.Context.SaveChanges();
                        MessageBox.Show("Запись успешно добавлена");
                    }
                    else
                    {
                        //Редактирование текущего сотрудника. Изменение данных строки в таблице Worker (Сотрудник)
                        _currentWorker.Surname_worker = TBoxSurname.Text;
                        _currentWorker.Name_worker = TBoxName.Text;
                        _currentWorker.Patronymic_worker = TBoxPatronymic.Text;
                        _currentWorker.Birthdate_worker = DateTime.Parse(DPickerBirthdateWorker.Text);
                        _currentWorker.id_Post = ListPost[ChangePost.SelectedIndex].id_Post;
                        _currentWorker.Telephone_number_worker = TBoxNumberPhoneWorker.Text;
                        if (_imageData != null)
                            _currentWorker.Image_worker = _imageData;
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

            if (!string.IsNullOrWhiteSpace(TBoxSurname.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxSurname.Text))
                    errorBuilder.AppendLine("Некорректно введена фамилия сотрудника;");
            }
            else
            {
                errorBuilder.AppendLine("Фамилия сотрудника обязательна для заполнения;");
            }

            if (!string.IsNullOrWhiteSpace(TBoxName.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxName.Text))
                    errorBuilder.AppendLine("Некорректно введено имя сотрудника;");
            }
            else
            {
                errorBuilder.AppendLine("Имя сотрудника обязательно для заполнения;");
            }

            if (!string.IsNullOrWhiteSpace(TBoxPatronymic.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxPatronymic.Text))
                    errorBuilder.AppendLine("Некорректно введено отчество сотрудника;");
            }
            else
            {
                errorBuilder.AppendLine("Отчество сотрудника обязательно для заполнения;");
            }

            var workerFromDB = App.Context.Workers.ToList().FirstOrDefault(p => p.Surname_worker.ToLower() == TBoxSurname.Text.ToLower() 
                && p.Name_worker.ToLower() == TBoxName.Text.ToLower() 
                && p.Patronymic_worker.ToLower() == TBoxPatronymic.Text.ToLower()
                && p.Birthdate_worker == DateTime.Parse(DPickerBirthdateWorker.Text)
                && p.Telephone_number_worker == TBoxNumberPhoneWorker.Text);
            if (workerFromDB != null && workerFromDB != _currentWorker)
                errorBuilder.AppendLine("Такой сотрудник уже есть в базе данных;");

            if (string.IsNullOrWhiteSpace(DPickerBirthdateWorker.Text))
                errorBuilder.AppendLine("Дата рождения сотрудника обязательна для заполнения;");

            if (ChangePost.SelectedIndex < 0)
                errorBuilder.AppendLine("Выбор должности обязателен;");

            if (!string.IsNullOrWhiteSpace(TBoxNumberPhoneWorker.Text))
            {
                if (!TextCorrect.IsNumberPhoneCorrect(TBoxNumberPhoneWorker.Text))
                    errorBuilder.AppendLine("Некорректно введен номер телефона сотрудника (11 цифр без пробелов);");
            }
            else
            {
                errorBuilder.AppendLine("Номер телефона сотрудника обязателен для заполнения;");
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
