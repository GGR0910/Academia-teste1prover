using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using ConsoleTables;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibary_entities
{
    internal class DAO
    {

        string str = @"server= localhost;database=proverexecises;Uid=root;password=Gogoll90@";
        MySqlConnection conn;
        MySqlConnection reader = null;



        public DAO()
        {
            try
            {
                conn = new MySqlConnection(str);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no banco de dados, por favor tente de novo mais tarde.");
                throw ex;
            }

        }

        public void execute_command(string sql)
        {

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

        }

        public static List<string> retdatatabletolistfuncionario(string sql)
        {

            DAO dao = new DAO();
            MySqlCommand command = new MySqlCommand(sql, dao.conn);
            var reader = command.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                var nome = reader.GetString(0);
                var idade = reader.GetString(1);
                var salario = reader.GetString(2);
                list.Add(nome);
                list.Add(idade);
                list.Add(salario);

            }


            return list;
        }

        public static List<string> retdatatabletolistequipamento(string sql)
        {

            DAO dao = new DAO();
            MySqlCommand command = new MySqlCommand(sql, dao.conn);
            var reader = command.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                var nome = reader.GetString(0);
                var grupo = reader.GetString(1);
                var preco = reader.GetString(2);
                var tempo = reader.GetString(3);
                Console.WriteLine();
                list.Add(nome);
                list.Add(grupo);
                list.Add(preco);
                list.Add(tempo);
            }
           
            return list;
        }

        public static void retdatatablefuncionario(string sql)
        {

            DAO dao = new DAO();
            MySqlCommand command = new MySqlCommand(sql, dao.conn);
            var reader = command.ExecuteReader();

            ConsoleTable table = new ConsoleTable("ID","Nome", "Idade", "Salario");
            while (reader.Read())
            {
                var nome = reader.GetString(0);
                var idade = reader.GetString(1);
                var salario = reader.GetString(2);
                var id = reader.GetString(3);

                table.AddRow( nome, idade, salario,id);

            }
            table.Write();



        }

        public static void retdatatableequipamento(string sql)
        {

            DAO dao = new DAO();
            MySqlCommand command = new MySqlCommand(sql, dao.conn);
            var reader = command.ExecuteReader();

            ConsoleTable table = new ConsoleTable("ID","Nome", "Grupo Muscular", "Preço", "Tempo de uso(anos)");
            while (reader.Read())
            {
                var nome = reader.GetString(0);
                var grupo_muscular = reader.GetString(1);
                var preco = reader.GetString(2);
                var idade = reader.GetString(3);
                var id = reader.GetString(4);

                table.AddRow(nome, grupo_muscular, preco, idade,id);

            }

            table.Write();


        }

        public static void deletartodos(string sql)
        {
        confimacao:
            try
            {
                Console.WriteLine("Tem certeza que deseja fazer isso? Digite 1 para sim ou 2 para não.");
                string confirmacao = Console.ReadLine();
                int resposta = int.Parse(confirmacao);
                switch (resposta)
                {
                    case 1:
                        {
                            DAO dao = new DAO();
                            dao.execute_command(sql);
                            break;
                        }
                    case 2: { throw new Exception_return_menu(); }
                    default:
                        {
                            Console.WriteLine("Por favor, selecione uma das opções. Tecle enter para retornar.");
                            Console.ReadLine();
                            Console.Clear();
                            goto confimacao;
                        }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Por favor, selecione uma das opções. Tecle enter para retornar.");
                Console.ReadLine();
                Console.Clear();
                goto confimacao;
            }
            catch (Exception_return_menu ex) { throw ex; }
                           
        }

        public static void comprarnovos(int anos)
        {

            DAO dao = new DAO();
            MySqlCommand command = new MySqlCommand($"SELECT Nome,Grupo_Muscular, Preço from proverexecises.equipamentos where tempo_uso>= {anos}", dao.conn);
            var reader = command.ExecuteReader();
            List<string> list = new List<string>();
            

            while (reader.Read())
            {
                var nome = reader.GetString(0);
                var grupo = reader.GetString(1);
                var preco = reader.GetString(2);
                Equipamento.registrarpordb(nome,grupo,preco);
            }

            deletartodos($"DELETE FROM proverexecises.equipamentos WHERE tempo_uso >= {anos}");
            Equipamento.listarequipamentos();


        }
    }
}



