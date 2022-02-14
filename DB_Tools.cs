﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DB_Task1
{
    class DB_Tools // class for work with DB
    {
        public SqlConnection Connection { get; private set; }
        public SqlDataAdapter Adapter { get; private set; }
        string cs;

        public DB_Tools()
        {
            cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString.ToString(); // get connection string from app.config
            Adapter = new SqlDataAdapter();
        }

        public void Fill(string cmd, DataTable table) // Method for filling dataset table 
        {
            Connection = new SqlConnection(cs);
            Adapter.SelectCommand = new SqlCommand(cmd, Connection);
            table.Columns.Clear();
            table.Clear();
            Adapter.Fill(table);
        }

        public async void Query(string cmd) // async Method for queries to the database 
        {
            using (Connection = new SqlConnection(cs))
            {
                await Connection.OpenAsync();
                SqlCommand command = new SqlCommand(cmd, Connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
