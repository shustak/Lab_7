using Lab_7;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace Lab_7
{
    public partial class MainWindow : Window
    {
        EntityContext context;

        public MainWindow()
        {
            context = new EntityContext("TestDbConnection");
            InitializeComponent();
            InitStudentList();
        }

        private void InitStudentList()
        {
            context.Students.Load();
            dGrid.DataContext = context.Students.Local;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var stud = new Student();
            EditWindow ew = new EditWindow(stud);
            var result = ew.ShowDialog();
            if (result == true)
            {
                context.Students.Add(stud);
                context.SaveChanges();
                ew.Close();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Student stud = dGrid.SelectedItem as Student;

            EditWindow ew = new EditWindow(stud);
            var result = ew.ShowDialog();
            if (result == true)
            {
                context.SaveChanges();
                ew.Close();
            }
            else
            {
                context.Entry(stud).Reload();
                dGrid.DataContext = null;
                dGrid.DataContext = context.Students.Local;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Student stud = dGrid.SelectedItem as Student;
                context.Students.Remove(stud);
                context.SaveChanges();
            }
        }

        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
