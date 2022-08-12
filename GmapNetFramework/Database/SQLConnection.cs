using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GmapNetFramework
{
    class SQLConnection
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-U4MN4UD\SQLEXPRESS;Initial Catalog=TestTask_Gmap;Integrated Security=True");

        public void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public void GetItems(List<Item> items)
        {
            items.Clear();
            string sqlExpression = "SELECT * FROM markers";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        items.Add(new Item
                        {
                            Id = (int)reader.GetValue(0),
                            Name = (string)reader.GetValue(1),
                            lng = (double)reader.GetValue(2),
                            lat = (double)reader.GetValue(3)
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Сохраняем изменения в бд при перетаскивании маркера
        /// </summary>
        /// <param name="item"></param>
        public void SaveChange(Item item)
        {
            ///в float ms sql используется точка как разделитель, в c# запятая из-за региональных особенностей
            var p = Math.Truncate((float)item.lng);
            string t = (item.lng - p).ToString();
            string lng = p.ToString() + "." + t.Substring(2);

            p = Math.Truncate((float)item.lat);
            t = (item.lat - p).ToString();
            string lat = p.ToString() + "." + t.Substring(2);

            string sqlExpression = "UPDATE markers " +
                                    $"SET longitude = {lng} , latitude= {lat} " +
                                    $"WHERE id = {(int)item.Id} ";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            Console.WriteLine(command.ExecuteNonQuery());
        }
    }
}

