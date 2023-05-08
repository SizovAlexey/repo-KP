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
    /// Логика взаимодействия для PostsPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        /// <summary>
        /// Конструктор страницы Должности
        /// </summary>
        public PostsPage()
        {
            InitializeComponent();
            //Разграничение прав доступа
            if (App.CurrentUser.id_Role == 1)
            {
                //Установка видимости кнопки "Добавить должность", при работе администратора
                BtnAddPost.Visibility = Visibility.Visible;
            }
            else
            {
                //Установка невидимости кнопки "Добавить должность", при работе пользователя
                BtnAddPost.Visibility = Visibility.Collapsed;
            }
            LViewPosts.ItemsSource = App.Context.Posts.ToList();
            ComboSortBySalary.SelectedIndex = 0;
            UpdatePosts();
        }

        /// <summary>
        /// Обработчик события "Изменение выбора" выпадающего списка сортировки
        /// </summary>
        private void ComboSortBySalary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePosts();
        }

        /// <summary>
        /// Обработчик события "Изменение текста" строки поиска
        /// </summary>
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePosts();
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Редактировать"
        /// </summary>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу редактирования должности с обозначением текущей должности
            var currentPost = (sender as Button).DataContext as Entities.Post;
            NavigationService.Navigate(new AddEditPostPage(currentPost));
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Добавить должность"
        /// </summary>
        private void BtnAddPost_Click(object sender, RoutedEventArgs e)
        {
            //Переход на страницу добавления должности
            NavigationService.Navigate(new AddEditPostPage());
        }

        /// <summary>
        /// Обработчик события "Клик" кнопки "Удалить"
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка на наличие сотрудников, работающих на текущей должности
                var currentPost = (sender as Button).DataContext as Entities.Post;
                var workerWithThisPostFromDB = App.Context.Workers.ToList().FirstOrDefault(p => p.id_Post == currentPost.id_Post);
                if (workerWithThisPostFromDB == null)
                {
                    //Подтверждение удаления текущей должности
                    if (MessageBox.Show($"Вы уверены, что хотите удалить должность: " + $"{currentPost.Name_post}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        //Удаление объекта (строки) таблицы Post (Должность)
                        App.Context.Posts.Remove(currentPost);
                        App.Context.SaveChanges();
                        UpdatePosts();
                    }
                }
                else
                {
                    if (MessageBox.Show($"Сначала измените должность у сотрудников, которым присвоена эта должность.\nХотите перейти на страницу Сотрудники?", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new WorkersPage());
                    }
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
        private void UpdatePosts()
        {
            var posts = App.Context.Posts.ToList();
            if (ComboSortBySalary.SelectedIndex == 0)
                posts = posts.OrderBy(p => p.Salary).ToList();
            else
                posts = posts.OrderByDescending(p => p.Salary).ToList();
            posts = posts.Where(p => p.Name_post.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewPosts.ItemsSource = posts;
        }

        /// <summary>
        /// Обработчик события "Загрузка страницы"
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePosts();
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
                fd.Blocks.Add(new Paragraph(new Run("Список должностей\v")));
                foreach (var item in LViewPosts.Items)
                {
                    fd.Blocks.Add(new Paragraph(new Run((item as Post).ToStringIntoPDF())));
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
