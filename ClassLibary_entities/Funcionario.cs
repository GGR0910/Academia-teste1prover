using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibary_entities
{
    public class Funcionario
    {
        public static string Nome { get; private set; }
        private static int Idade { get; set; }
        private static double Salario { get; set; }
        private static readonly string valor_saida = "sair";


        public static void registrarfuncionario()
        {

            string nome;
            string idadestr;
            string salariostr;



        pegar_nome:

            Console.WriteLine("Qual o nome do(a) novo funcionário?");
            nome = Console.ReadLine();
            if (nome == "")
            {
                Console.WriteLine("O nome não pode ser nulo, clique enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto pegar_nome;
            }

            if (nome.Equals(valor_saida.ToLower()))
            {
                throw new Exception_return_menu();
            }

            try
            {
                int valor = int.Parse(nome);
                if (valor is int)
                {
                    Console.WriteLine("O valor deve ser textual e não numérico, clique enter para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                    goto pegar_nome;
                }
            }
            catch (FormatException)
            {
                if (valor_saida.Equals(nome.ToLower()))
                {
                    throw new Exception_return_menu();
                }
                Nome = nome;
            }



        pegar_idade:
            Console.Clear();
            Console.WriteLine("Quantos anos ele(a) tem?");
            idadestr = Console.ReadLine();

            if (valor_saida.Equals(idadestr.ToLower()))
            { throw new Exception_return_menu(); };
            try { int idade = int.Parse(idadestr); Idade = idade; }

            catch (FormatException)
            {
                Console.WriteLine("A idade deve ser um valor numérico, não nulo, inteiro, tecle enter para continuar.");
                Console.ReadLine();
                ; Console.Clear();
                goto pegar_idade;
            }





        pegar_salario:
            Console.Clear();
            Console.WriteLine("Qual será o salário dele(a)?");
            salariostr = Console.ReadLine();


            if (valor_saida.Equals(salariostr.ToLower())) { throw new Exception_return_menu(); };
            try { double salario = double.Parse(salariostr); Salario = salario;

                if (salario < 3000 || salario > 10000) { Console.WriteLine("O valor deve estar entre R$3000 e R$10000. Tecle enter para continuar."); Console.ReadLine();Console.Clear(); goto pegar_salario; }
            
            }
            catch (FormatException)
            {
                Console.WriteLine("O salário deve ser um valor numérico e não nulo clique enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto pegar_salario;
            }

            try
            {
                DAO dt = new DAO();
                dt.execute_command($"INSERT INTO proverexecises.funcionarios (Nome, Idade, Salario) VALUES('{Nome}',{Idade},{Salario})");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Clique enter para retornar ao menu.");
                Console.ReadLine();
                Console.Clear();
                throw new Exception_return_menu();
            }

        }

        public static void listarfuncionarios()
        {
        listarfuncionarios:
            Console.Clear();
            Console.WriteLine("Lista de funcionários:");
            Console.WriteLine("********************");
            DAO.retdatatablefuncionario("SELECT * FROM proverexecises.funcionarios");
            Console.WriteLine("********************");
            Console.WriteLine("Selecione 1 para apagar algum registro, 2 para editar um registro ou 3 para apagar os registros.");
            Console.WriteLine("Escreva sair a qualquer momento para retornar ao menu.");
            string saida = Console.ReadLine();

            try
            {
                switch (int.Parse(saida))
                {
                    case 1:
                        Console.WriteLine("Escreva o id do funcionário que deseja apagar.");
                        string idstr = Console.ReadLine();
                        if(valor_saida.Equals(idstr.ToLower())) { throw new Exception_return_menu(); }
                        int id = int.Parse(idstr);
                        deletarfuncionario(id);
                        goto listarfuncionarios;
                    case 2:
                        editarfuncionario();
                        Console.Clear();
                        goto listarfuncionarios;
                    case 3:
                        try
                        {
                            DAO.deletartodos("DELETE FROM proverexecises.funcionarios");
                            goto listarfuncionarios;
                        }
                        catch (Exception_return_menu)
                        {
                            goto listarfuncionarios;
                        }


                    default:
                        Console.WriteLine("Por favor escolha uma das opções. Clique enter para continuar");
                        Console.ReadLine();
                        goto listarfuncionarios;
                }
            }
            catch (FormatException)
            {
                saida = saida.ToLower();
                if (valor_saida.Equals(saida.ToLower()))
                {
                    throw new Exception_return_menu();
                }
                Console.WriteLine("Por favor, escolha uma das opções. Tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto listarfuncionarios;

            }
            catch (Exception_return_menu ex)
            {
                throw ex;
            }

        }

        private static void deletarfuncionario(int id)
        {
            DAO dao = new DAO();
            dao.execute_command($"DELETE FROM proverexecises.funcionarios WHERE funcionarioid = {id}");
        }

        private static void editarfuncionario()
        {

        editarfuncionario:

            try
            {
                Console.WriteLine("Selecione o id do funcionario que deseja editar ou sair para retornar ao menu.");
                Console.WriteLine("Caso queira manter uma informação, tecle enter no campo.");
                string idstr = Console.ReadLine();

                if (valor_saida.Equals(idstr.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }


                try
                {
                    int id = int.Parse(idstr);
                    List<string> lista = DAO.retdatatabletolistfuncionario($"SELECT Nome,Idade,Salario from proverexecises.funcionarios WHERE funcionarioid={id}");
                    string nome = lista[0];
                    string idade = lista[1];
                    string salario = lista[2];
                    Console.Clear();


                pegar_nome:
                    Console.WriteLine($"Qual será o novo nome? Anterior: {nome}");
                    string nomeprovisorio = Console.ReadLine();                    
                    try
                    {
                        if (valor_saida.Equals(nomeprovisorio.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }
                        int testestr = int.Parse(nomeprovisorio);
                        if (testestr is int) { Console.WriteLine("O valor deve ser um texto. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); goto pegar_nome; }
                    }
                    catch (FormatException)
                    {
                        if (valor_saida.Equals(nomeprovisorio.ToLower())) { throw new Exception_return_menu(); }
                        if (nomeprovisorio == "" || nomeprovisorio == null) { Nome = nome; }
                        else { Nome = nomeprovisorio; }
                    }


                pegar_idade:
                    Console.WriteLine($"Qual será a nova idade? Anterior: {idade}");
                    string idadeprovisorio = Console.ReadLine();        
                    try
                    {
                        if (valor_saida.Equals(idadeprovisorio.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }
                        if (idadeprovisorio == "" || idadeprovisorio == null) { Idade = int.Parse(idade); }
                        else { Idade = int.Parse(idadeprovisorio); }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("O valor deve ser um numero inteiro. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); goto pegar_idade;
                    }



                pegar_salario:
                    Console.WriteLine($"Qual será o novo salário? Anterior: R${salario}");
                    string salario_provisorio = Console.ReadLine();                  
                    try
                    {
                        if (valor_saida.Equals(salario_provisorio.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }
                        if (salario_provisorio == "" || salario_provisorio == null) { Salario = Double.Parse(salario); }
                        else { Salario = double.Parse(salario_provisorio); }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("O valor deve ser um numero. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); goto pegar_salario;
                    }

                    DAO db = new DAO();
                    db.execute_command($"UPDATE proverexecises.funcionarios SET Nome= '{Nome}', Idade= {Idade},Salario={Salario} WHERE funcionarioid= {id}");


                }

                catch (FormatException) { Console.WriteLine("Selecione uma das opções. Tecle enter para retornar"); Console.ReadLine(); }
            }

            catch (ArgumentOutOfRangeException) { Console.WriteLine("Por favor selecione uma das ID. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); listarfuncionarios(); }








        }

        
    }
}