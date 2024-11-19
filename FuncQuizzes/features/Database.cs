using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace FuncQuizzes.features
{
    internal class Database
    {
        /* <summary>
         * 
         ---------------------------------------------------
            
            features.Database mydb = new features.Database();

            DataTable tb = mydb.SelectQuery("Select Category from tbl_category");

            foreach (DataRow dr in tb.Rows)
            {
                this.Hello.Children.Add(new TextBlock { Text = $"Name : {dr[0]}" });
            }

            
            // Insert with parameters

                    db.Insert("tbl_category", "id_Category, Category", "'3', 'C#'");

                    // Update with parameters
                         db.Update("tbl_category", "Name = 'Home Electronics'", "ID = 1");

                    // Select all from a specific table with parameter
                        DataTable categories = db.SelectFromTable("tbl_category");

                    // Delete with parameters
                        db.Delete("tbl_category", "ID = 1");


        </summary> */

        private readonly SQLiteConnection _connection;

        public Database(string dataBase_Path)
        {
            _connection = new SQLiteConnection($"Data Source={dataBase_Path}");
        }

        public DataTable SelectQuery(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                _connection.Open();

                using (SQLiteCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = query;

                    using (SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd))
                    {
                        ad.Fill(dataTable);
                    }
                }
            }

            catch (SQLiteException err)
            {
                //MessageBox.Show($"Connection Error: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                _connection.Close();
            }

            return dataTable;
        }

        public DataTable SelectFromTable(string tableName)
        {

            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Please input column name to operation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return SelectQuery($"SELECT * FROM {tableName}");
        }

        public bool Insert(string tableName, string columnName, string values)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(values))
            {
                MessageBox.Show("TableName, Columns, and Values must be set for an insert operation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                _connection.Open();

                using (SQLiteCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {tableName} ({columnName}) VALUES ({values})";
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch (SQLiteException err)
            {
                MessageBox.Show($"Insert Error: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            finally
            {
                _connection.Close();
            }
        }

        public bool Update(string tableName, string updateValue, string condition)
        {

            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(updateValue) || string.IsNullOrEmpty(condition))
            {
                MessageBox.Show("TableName, Updates, and Condition must be set for an update operation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                _connection.Open();

                using (SQLiteCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE {tableName} SET {updateValue} WHERE {condition}";
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch (SQLiteException err)
            {
                MessageBox.Show($"Update Error: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            finally
            {
                _connection.Close();
            }
        }

        public bool Delete(string tableName, string condition)
        {

            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(condition))
            {
                MessageBox.Show("TableName and Condition must be set for a delete operation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                _connection.Open();
                using (SQLiteCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {tableName} WHERE {condition}";
                    cmd.ExecuteNonQuery();
                }
                return true;
            }

            catch (SQLiteException err)
            {
                MessageBox.Show($"Delete Error: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            finally
            {
                _connection.Close();
            }
        }
    }
}
