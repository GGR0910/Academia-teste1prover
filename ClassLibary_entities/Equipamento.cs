using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibary_entities
{
    public class Equipamento
    {

        private static string Nome { get; set; }
        private static string Grupo_muscular { get; set; }
        private static double Preco { get; set; }
        private static int Idadeequipamento { get; set; }



        

        public static bool registrarequipamento()
        {

            string nome;
            string tempostr;
            string grupo_muscular;
            string precostr;
            string valor_saida = "0";



            Console.WriteLine("Qual o nome do novo aparelho?");
            nome = Console.ReadLine();
            if (valor_saida.Equals(nome))
            {
                return false;
            };
            Nome = nome;

            Console.WriteLine("Para qual grupo muscular ele serve?");
            grupo_muscular = Console.ReadLine();
            if (valor_saida.Equals(grupo_muscular))
            {
                return false;
            };
            Grupo_muscular = grupo_muscular;


        preco_aparelho:
            Console.WriteLine("Quanto ele custou?");
            precostr = Console.ReadLine();

            try { double preco = double.Parse(precostr); Preco = preco;}
            catch (FormatException) { Console.WriteLine("O valor deve ser numérico."); goto preco_aparelho; }
            if (valor_saida.Equals(precostr))
            {
                return false;
            };


            tempo_uso:
            Console.WriteLine("Qual o tempo de uso desse equipamento em anos?");
            tempostr = Console.ReadLine();

            try { int tempo = int.Parse(tempostr); Idadeequipamento = tempo; }
            catch (FormatException) { Console.WriteLine("O tempo deve ser um valor numérico."); goto tempo_uso; }

            if ( valor_saida.Equals(tempostr)) 
            { 
                return false; 
            };
            


            DAO dt = new DAO();
            dt.execute_command($"INSERT INTO proverexecises.equipamentos (Nome, Grupo_muscular, Preço,tempo_uso) VALUES('{Nome}','{Grupo_muscular}',{Preco},{Idadeequipamento})");


            return true;

        }


        


    }




}
