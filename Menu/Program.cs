using ClassLibary_entities;
using MySqlX.XDevAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace Menu
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Seja bem vindo ao Menu da Academia Prover");

        Menuinicio:
            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1-Cadastrar funcionário.");
            Console.WriteLine("2-Cadastrar novo aparelho.");
            Console.WriteLine("0-Fechar menu.");

            string escolha;
            escolha = Console.ReadLine();
            int result;
            


            try
            {
                result = int.Parse(escolha);
                Funcoes.verifica_numero(result);
            }
            catch (ArgumentException)
            {
                Funcoes.recarga_inicio();
                goto Menuinicio;
            }
            catch(FormatException)
            {
                Funcoes.recarga_inicio();
                goto Menuinicio;
            }




            if (result == 1)
            {
                Console.Clear();
                Console.WriteLine("Registrando funcionário, para retornar ao menu digite 0:");
                bool resultado = Funcionario.registrarfuncionario();
                Console.Clear();
                if (resultado == false)
                {
                    Console.Clear();
                    goto Menuinicio;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Funcionario registrado!");
                    int resposta = Funcoes.verifica();
                    if (resposta == 1)
                    {
                        Console.Clear();
                        goto Menuinicio;
                    }
                    if (resposta == 0)
                    {
                        Console.WriteLine("Encerrando");
                        Environment.Exit(0);
                    }
                }
            }


            if (result == 2)
            {
                Console.Clear();
                Console.WriteLine("Registrando aparelho, para retornar ao menu digite 0:");
                bool resultado = Equipamento.registrarequipamento();
                

                if  (resultado == false) {
                    Console.Clear();
                    goto Menuinicio; }

                else {
                    Console.Clear();
                    Console.WriteLine("Aparelho registrado!");
                    int resposta = Funcoes.verifica();
                    if (resposta == 1)
                    {
                        Console.Clear();
                        goto Menuinicio;
                    }
                    if (resposta == 0)
                    {
                        Console.WriteLine("Encerrando");
                        Environment.Exit(0);
                    } }

                if(result == 0)
                { }
                    Environment.Exit(0);
                }



            }


        }
    }
