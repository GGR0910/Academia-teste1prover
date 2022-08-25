using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClassLibary_entities
{
    internal class DAO
    {

        string str = @"server= localhost;database=proverexecises;Uid=root;password=Gogoll90@";
        MySqlConnection conn = null;
        MySqlConnection reader = null;



        public DAO()
        {
            conn = new MySqlConnection(str);
            conn.Open();
        }

        public void execute_command(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

        }

        public DataTable retdatatable(string sql)
        {

            DataTable data = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(data);
            return data;
        }








    }
}



