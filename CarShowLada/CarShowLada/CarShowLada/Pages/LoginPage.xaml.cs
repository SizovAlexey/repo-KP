using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CarShowLada.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        /// <summary>
        /// Конструктор страницы Авторизация
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            //Проверка подключения к базе данных
            try
            {
                App.Context.Database.Connection.Open();
                App.Context.Database.Connection.Close();
            }
            catch (System.Data.SqlClient.SqlException error)
            {
                MessageBox.Show($"Ошибка подключения к базе данных:\n{error.Message}\nПриложение будет закрыто.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{error.Message}\nПриложение будет закрыто.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Войти"
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка на пустые поля
                if (TBoxLogin.Text != "" && PBoxPassword.Password != "")
                {
                    //Проверка на существование пользователя
                    var currentUser = App.Context.Users.FirstOrDefault(p => p.Login == TBoxLogin.Text);
                    if (currentUser != null)
                    {
                        //Проверка на правильность пароля
                        if (currentUser.Password == PBoxPassword.Password)
                        {
                            App.CurrentUser = currentUser;
                            NavigationService.Navigate(new SectionsPage());
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        var result = MessageBox.Show("Пользователь с такими данными не найден. Хотите зарегистрироваться?", "Ошибка авторизации", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            NavigationService.Navigate(new RegistrationPage());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введите данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
            {
                MessageBox.Show($"Ошибка подключения к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Зарегистрироваться"
        /// </summary>
        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу Регистрация
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
