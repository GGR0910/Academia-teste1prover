using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibary_entities
{
    public class Funcoes
    {

        public static int verifica()
        {
            string resposta;
            Console.WriteLine("Deseja retornar ao menu? Se sim digite 1, se não digite 2 para fechar o programa.");
            resposta = Console.ReadLine();
            int resp = int.Parse(resposta);


            return resp;
        }

        public static void verifica_numero(int numero)
        {

            if (numero> 3)
            {
                throw new ArgumentException();
            }

        }


        public static void recarga_inicio()
        {
            Console.Clear();
            Console.WriteLine("Por favor, escolha uma dentre as opções. Pressione enter para retornar.");
            Console.ReadLine();
            Console.Clear();
        }

        
    }
}
