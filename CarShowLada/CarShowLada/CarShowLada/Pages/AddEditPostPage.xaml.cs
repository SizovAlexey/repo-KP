using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TextCorrectLib;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPostPage.xaml
    /// </summary>
    public partial class AddEditPostPage : Page
    {
        private Entities.Post _currentPost = null; //Создание объекта (строки) таблицы Post (Должность) для обозначения текущей должности

        /// <summary>
        /// Конструктор страницы добавления должности
        /// </summary>
        public AddEditPostPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор страницы редактирования текущей должности
        /// </summary>
        public AddEditPostPage(Entities.Post currentPost)
        {
            try
            {
                //Заполнение полей данными текущей должности
                InitializeComponent();
                _currentPost = currentPost;
                Title = "Редактирование должности";
                TBoxPostName.Text = _currentPost.Name_post;
                TBoxSalary.Text = _currentPost.Salary.ToString();
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
                    //Если текущая должность не обозначена, значит идёт операция добавления должности. Создание нового объекта (строки) таблицы Post (Должность)
                    if (_currentPost == null)
                    {
                        var post = new Entities.Post
                        {
                            Name_post = TBoxPostName.Text,
                            Salary = decimal.Parse(TBoxSalary.Text),
                        };
                        App.Context.Posts.Add(post);
                        App.Context.SaveChanges();
                        MessageBox.Show("Запись успешно добавлена");
                    }
                    else
                    {
                        //Редактирование текущей должности. Изменение данных строки в таблице Post (Должность)
                        _currentPost.Name_post = TBoxPostName.Text;
                        _currentPost.Salary = decimal.Parse(TBoxSalary.Text);
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

            if (!string.IsNullOrWhiteSpace(TBoxPostName.Text))
            {
                if (!TextCorrect.IsTextCorrect(TBoxPostName.Text))
                    errorBuilder.AppendLine("Некорректно введено название должности;");
            }
            else
            {
                errorBuilder.AppendLine("Название должности обязательно для заполнения;");
            }

            var postFromDB = App.Context.Posts.ToList().FirstOrDefault(p => p.Name_post.ToLower() == TBoxPostName.Text.ToLower());
            if (postFromDB != null && postFromDB != _currentPost)
                errorBuilder.AppendLine("Такая должность уже есть в базе данных;");

            if (!string.IsNullOrWhiteSpace(TBoxSalary.Text))
            {
                decimal salary = 0;
                if (decimal.TryParse(TBoxSalary.Text, out salary) == false || salary <= 0)
                    errorBuilder.AppendLine("Зарплата сотрудника должна быть положительным числом;");
            }
            else
            {
                errorBuilder.AppendLine("Зарплата сотрудника обязательна для заполнения;");
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
