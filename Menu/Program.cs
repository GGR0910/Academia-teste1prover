using ClassLibary_entities;
using ConsoleTables;
using MySqlX.XDevAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
            Console.WriteLine("3-Listar aparelhos.");
            Console.WriteLine("4-Listar funcionários.");
            Console.WriteLine("0-Fechar menu.");

            string escolha;
            escolha = Console.ReadLine();
            try
            {
                int result = int.Parse(escolha);

                switch (result)
                {
                    case 1:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("Registrando funcionário, para retornar ao menu digite sair:");
                            Funcionario.registrarfuncionario();
                            Console.Clear();
                            Console.Clear();
                            Console.WriteLine($"Funcionario {Funcionario.Nome} registrado! Tecle enter para continuar.");
                            Console.ReadLine();
                            Funcionario.listarfuncionarios();
                        }
                        catch
                        {
                            Console.Clear();
                            goto Menuinicio;
                        }
                        break;

                    case 2:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("Registrando aparelho, para retornar ao menu digite sair:");
                            Equipamento.registrarequipamento();
                            Console.Clear();
                            Console.WriteLine($"Aparelho {Equipamento.Nome} registrado! Tecle enter para retornar.");
                            Console.ReadLine();
                            Equipamento.listarequipamentos();
                        }
                        catch (Exception_return_menu)
                        {
                            Console.Clear();
                            goto Menuinicio;
                        }
                        break;
                    case 3:
                        try { Equipamento.listarequipamentos(); }
                        catch (Exception_return_menu) { Console.Clear(); goto Menuinicio; }
                        break;

                    case 4:
                        try
                        {Funcionario.listarfuncionarios();}
                        catch (Exception_return_menu)
                        { Console.Clear(); goto Menuinicio;}
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Até mais.");
                        Environment.Exit(0);
                        break;
                    default:
                        throw new Exception_return_menu();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Por favor, selecione uma das opções. Tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto Menuinicio;
            }
            catch (Exception_return_menu) {
                Console.WriteLine("Por favor, selecione uma das opções. Tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto Menuinicio;
            }


        }
    }
}