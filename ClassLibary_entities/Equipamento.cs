using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibary_entities
{
    public class Equipamento
    {

        public static string Nome { get; private set; }
        private static string Grupo_muscular { get; set; }
        private static double Preco { get; set; }
        private static int Idadeequipamento { get; set; }
        private static readonly string valor_saida = "sair";




        public static void registrarequipamento()
        {

            string nome;
            string tempostr;
            string grupo_muscular;
            string precostr;



        pegar_nome:
            Console.WriteLine("Qual o nome do novo aparelho?");
            nome = Console.ReadLine();
            if (nome == ""|| nome== null)
            {
                Console.WriteLine("O nome não pode ser nulo, tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto pegar_nome;
            }

            int valor;
            try
            {
                valor = int.Parse(nome);
                if (valor is int)
                {
                    Console.WriteLine("O valor deve ser textual e não numérico, tecle enter pra continuar.");
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
                };
                Nome = nome;
            }



        pegar_grupo:
            Console.Clear();
            Console.WriteLine("Para qual grupo muscular ele serve?");
            grupo_muscular = Console.ReadLine();
            if (grupo_muscular == ""|| grupo_muscular== null)
            {
                Console.WriteLine("O grupo não pode ser nulo,tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto pegar_grupo;
            }

            try
            {
                valor = int.Parse(grupo_muscular);
                if (valor is int)
                {

                    Console.WriteLine("O valor deve ser textual e não numérico,tecle enter para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                    goto pegar_grupo;
                }
            }
            catch (FormatException)
            {
                if (valor_saida.Equals(grupo_muscular.ToLower()))
                {
                    throw new Exception_return_menu();
                };
                Grupo_muscular = grupo_muscular;
            }


        preco_aparelho:
            Console.Clear();
            Console.WriteLine("Quanto ele custou?");
            precostr = Console.ReadLine();

            try { double preco = double.Parse(precostr); Preco = preco; }
            catch (FormatException)
            {
                if (valor_saida.Equals(precostr.ToLower()))
                {
                    throw new Exception_return_menu();
                };
                Console.WriteLine("O valor deve ser numérico e não nulo, tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto preco_aparelho;
            }

        tempo_uso:
            Console.Clear();
            Console.WriteLine("Qual o tempo de uso desse equipamento em anos?");
            tempostr = Console.ReadLine();

            try { int tempo = int.Parse(tempostr); Idadeequipamento = tempo; }
            catch (FormatException)
            {
                if (valor_saida.Equals(tempostr.ToLower()))
                {
                    throw new Exception_return_menu();
                };
                Console.WriteLine("O tempo deve ser um valor numérico e não nulo, tecle enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                goto tempo_uso;
            }           
                DAO dt = new DAO();
                dt.execute_command($"INSERT INTO proverexecises.equipamentos (Nome, Grupo_muscular, Preço,tempo_uso) VALUES('{Nome}','{Grupo_muscular}',{Preco},{Idadeequipamento})");

        }

        public static void listarequipamentos()
        {
        listarequipamentos:
            Console.Clear();
            Console.WriteLine("Lista de equipamentos:");
            Console.WriteLine("********************");
            DAO.retdatatableequipamento("SELECT * FROM proverexecises.equipamentos");
            Console.WriteLine("********************");
            Console.WriteLine("Selecione 1 para apagar algum registro, 2 para editar um registro, 3 para apagar todos os registros ou 4 para verificar equipamentos por tempo de uso.");
            Console.WriteLine("Escreva sair a qualquer momento para retornar ao menu.");
            string saida = Console.ReadLine();

            try
            {
                switch (int.Parse(saida))
                {
                    case 1:
                        Console.WriteLine("Escreva o id do equipamento que deseja apagar.");
                        string idstr = Console.ReadLine();
                        if (valor_saida.Equals(idstr.ToLower())) { throw new Exception_return_menu(); }
                        int id = int.Parse(idstr);
                        deletarequipamento(id);
                        goto listarequipamentos;

                    case 2:

                        editarequipamento();
                        Console.Clear();
                        goto listarequipamentos;
                    case 3:
                        try
                        {
                            DAO.deletartodos("DELETE FROM proverexecises.equipamentos");
                            goto listarequipamentos;
                        }

                        catch (Exception_return_menu) { goto listarequipamentos; }

                    case 4:

                        Console.WriteLine("A partir de quantos anos de uso deseja ver os equipamentos?");
                        string anosstr = Console.ReadLine();

                        if (valor_saida.Equals(anosstr.ToLower())) { throw new Exception_return_menu(); } 

                        int anos = int.Parse(anosstr);
                        Console.Clear();
                        DAO.retdatatableequipamento($"SELECT equipamentoid, Nome, Grupo_Muscular, Preço, tempo_uso FROM proverexecises.equipamentos where tempo_uso >= {anos} ORDER BY tempo_uso");
                        decisao:
                        Console.WriteLine("Para apagar todos os registros tecle 1, para apagar todos os registros e comprar novos tecle 2. Para sair digite sair.");
                        string respostastr = Console.ReadLine();

                        if (valor_saida.Equals(respostastr.ToLower())) { throw new Exception_return_menu(); }


                        try
                        {
                            int resposta = int.Parse(respostastr);

                            switch (resposta)
                            {
                                case 1:
                                    DAO.deletartodos($"DELETE FROM proverexecises.equipamentos WHERE tempo_uso >= {anos}");
                                    goto listarequipamentos;
                                case 2:
                                    DAO.comprarnovos(anos);
                                    goto listarequipamentos;
                                default:
                                    throw new FormatException();                                  

                            }
                        }
                        catch (FormatException){
                            
                            Console.WriteLine("Por favor escolha uma das opções. Clique enter para continuar");
                            Console.ReadLine();
                            Console.Clear();
                            goto decisao;
                        }

                        


                    default:
                        Console.WriteLine("Por favor escolha uma das opções. Clique enter para continuar");
                        Console.ReadLine();
                        goto listarequipamentos;
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
                goto listarequipamentos;

            }
            catch (Exception_return_menu ex) { throw ex; }



        }

        private static void deletarequipamento(int id)
        {
            DAO dao = new DAO();
            dao.execute_command($"DELETE FROM proverexecises.equipamentos WHERE equipamentoid = {id}");
        }

        private static void editarequipamento()
        {

            Console.WriteLine("Selecione o id do funcionario que deseja editar ou sair para retornar ao menu.");
            string idstr = Console.ReadLine();
            if (valor_saida.Equals(idstr.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }

            try { int id = int.Parse(idstr);
                List<string> lista = DAO.retdatatabletolistequipamento($"SELECT Nome,Grupo_Muscular,Preço,tempo_uso from proverexecises.equipamentos WHERE equipamentoid={id}");
                string nome = lista[0];
                string grupo = lista[1];
                string preco= lista[2];
                string tempo = lista[3];
                Console.Clear();

                try
                {
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


                pegar_grupo:
                    Console.WriteLine($"Qual será o novo grupo? Anterior: {grupo}");
                    string grupoprovisorio = Console.ReadLine();
                    try
                    {
                        if (valor_saida.Equals(grupoprovisorio.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }
                        int testestr = int.Parse(grupoprovisorio);
                        if (testestr is int) { Console.WriteLine("O valor deve ser um texto. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); goto pegar_grupo; }
                    }
                    catch (FormatException)
                    {
                        if (valor_saida.Equals(grupoprovisorio.ToLower())) { throw new Exception_return_menu(); }
                        if (grupoprovisorio == "" || grupoprovisorio == null) {  Grupo_muscular= grupo; }
                        else { Grupo_muscular = grupoprovisorio; }
                    }



                pegar_preco:
                    Console.WriteLine($"Qual será o novo preço? Anterior: R${preco}");
                    string precoprovisorio = Console.ReadLine();
                    try
                    {
                        if (valor_saida.Equals(precoprovisorio.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }
                        if (precoprovisorio == "" || precoprovisorio == null) { Preco = double.Parse(preco); }
                        else { Preco = double.Parse(precoprovisorio); }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("O valor deve ser um numero. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); goto pegar_preco;
                    }



                pegar_tempo:
                    Console.WriteLine($"Qual será o novo tempo de uso em anos? Anterior: {tempo}");
                    string tempoprovisorio = Console.ReadLine();
                    try
                    {
                        if (valor_saida.Equals(tempoprovisorio.ToLower())) { Console.Clear(); throw new Exception_return_menu(); }
                        if (tempoprovisorio == "" || tempoprovisorio == null) { Idadeequipamento = int.Parse(tempo); }
                        else { Idadeequipamento = int.Parse(tempoprovisorio); }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("O valor deve ser um numero. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); goto pegar_tempo;
                    }

                }
                catch (FormatException) { Console.WriteLine("Selecione uma das opções. Tecle enter para retornar"); Console.ReadLine(); }

                DAO dao = new DAO();
                dao.execute_command($"UPDATE proverexecises.equipamentos SET Nome= '{Nome}', Grupo_Muscular= '{Grupo_muscular}' , Preço={Preco}" +
                    $",tempo_uso= {Idadeequipamento} WHERE equipamentoid= {id}");
              
            }
            catch (ArgumentOutOfRangeException) { Console.WriteLine("Por favor selecione uma das ID. Tecle enter para continuar."); Console.ReadLine(); Console.Clear(); listarequipamentos(); }
        }

        public static void registrarpordb(string nome, string grupo, string preco)
        {

                Nome = nome;
                Grupo_muscular = grupo;

                string precostr = preco;
                Preco = double.Parse(precostr);

            
                Idadeequipamento = 0;

                DAO dao = new DAO();
                dao.execute_command($"Insert into proverexecises.equipamentos (Nome,Grupo_Muscular,Preço,tempo_uso) values('{Nome}','{Grupo_muscular}',{Preco}, {Idadeequipamento})");

        }


    }
}


    

