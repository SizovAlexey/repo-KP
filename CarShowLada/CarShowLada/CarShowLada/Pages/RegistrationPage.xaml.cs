using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using PasswordLib;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        /// <summary>
        /// Конструктор страницы Регистрация
        /// </summary>
        public RegistrationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Зарегистрироваться"
        /// </summary>
        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка на уже существующего пользователя
                var currentUser = App.Context.Users.Where(p => p.Login.ToLower().Equals(TBoxLogin.Text.ToLower())).FirstOrDefault();
                if (currentUser != null)
                {
                    MessageBox.Show("Такой пользователь уже есть в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //Проверка на заполнение логина
                else if (string.IsNullOrWhiteSpace(TBoxLogin.Text))
                {
                    MessageBox.Show("Введите логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //Проверка на заполнение пароля
                else if (!(string.IsNullOrEmpty(TBoxLogin.Text)) && (string.IsNullOrEmpty(PBoxPassword.Password)))
                {
                    MessageBox.Show("Введите пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //Проверки на длину логина
                    if (TBoxLogin.Text.Length < 3)
                    {
                        MessageBox.Show("Слишком короткий логин.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (TBoxLogin.Text.Length > 20)
                    {
                        MessageBox.Show("Слишком длинный логин.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        //Проверка на сложность пароля
                        if ((TBoxPasswordComplexity.Text == "Хороший") || (TBoxPasswordComplexity.Text == "Сложный"))
                        {
                            //Проверка на соответствие паролей
                            if (PBoxPassword.Password == PBoxPasswordConfirm.Password)
                            {
                                //Создание нового объекта (строки) таблицы User (Пользователь)
                                Entities.User user = new Entities.User();
                                user.Login = TBoxLogin.Text;
                                user.Password = PBoxPassword.Password;
                                user.id_Role = 2;
                                App.Context.Users.Add(user);
                                App.Context.SaveChanges();
                                MessageBox.Show("Регистрация прошла успешно");
                                NavigationService.GoBack();
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен быть сложнее.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события "Изменение пароля" строки Пароль
        /// </summary>
        private void PBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Изменение строки Сложность пароля в соответствии с проверкой вводимого пароля
            if (string.IsNullOrWhiteSpace(PBoxPassword.Password))
            {
                TBoxPasswordComplexity.Text = "";
            }
            else
            {
                var passwordCheck = Password.PasswordComplexity(PBoxPassword.Password);
                TBoxPasswordComplexity.Text = passwordCheck;
            }
        }
    }
}
