using FuncQuizzes.components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuncQuizzes.pages
{
    /// <summary>
    /// Interaction logic for selectcategory.xaml
    /// </summary>
    public partial class selectcategory : Page
    {
        public selectcategory()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            StackPanel[] stacks = new StackPanel[] { stack01, stack02, stack03, stack04 };
            string connectionString = "Data Source=DATA\\FuncQuizzes.sqlite";
            string query = "SELECT category FROM tbl_category ORDER BY category ASC";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        int index = 0;
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader.GetString(0);

                                Button categoryButton = new Button
                                {
                                    Content = categoryName,
                                    Height = 40,
                                    VerticalContentAlignment = VerticalAlignment.Center,
                                    HorizontalContentAlignment = HorizontalAlignment.Center,
                                    Style = (Style)Application.Current.Resources["newbuttonstyle"],
                                    Margin = new Thickness(15),
                                };
                                categoryButton.Click += buttoncategory_Click;
                                int col = index % 4;
                                switch (col)
                                {
                                    case 0:
                                        stack01.Children.Add(categoryButton);
                                        break;
                                    case 1:
                                        stack02.Children.Add(categoryButton);
                                        break;
                                    case 2:
                                        stack03.Children.Add(categoryButton);
                                        break;
                                    case 3:
                                        stack04.Children.Add(categoryButton);
                                        break;
                                }
                                index++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while loading categories: " + ex.Message);
                    }
                }
            }
        }
        public void loadpage()
        {
            App.SwitchPage(new pages.chooselevel());
        }
        private void buttoncategory_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data source=DATA\\FuncQuizzes.sqlite"))
            {
                con.Open();
                string questionQuery = "SELECT id_category FROM tbl_category WHERE category = @category";
                Button button = sender as Button;

                if (button != null)
                {
                    string categoryName = button.Content.ToString();

                    SQLiteCommand cmd = new SQLiteCommand(questionQuery, con);
                    cmd.Parameters.AddWithValue("@category", categoryName);

                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int categoryId))
                    {
                        ((App)Application.Current).GlobalCategoryId = categoryId;
                        loadpage();
                    }
                    else
                    {
                        MessageBox.Show("Category not found.");
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.SwitchPage(new pages.HomeScreen());
        }
    }
}
