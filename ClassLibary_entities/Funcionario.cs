namespace ClassLibary_entities
{
    public class Funcionario
    {
        private static string Nome { get; set; } 
        private static int Idade { get; set; }
        private static double Salario { get; set; }


        public static bool registrarfuncionario()
        {

            string nome;
            string idadestr;
            string salariostr;
            string valor_saida= "0";
            

            Console.WriteLine("Qual o nome do novo funcionário?");
            nome = Console.ReadLine();
            if (valor_saida.Equals(nome)) 
            {
                return false;
            }
            Nome = nome;

            pegar_idade:
            Console.WriteLine("Quantos anos ele tem?");
            idadestr = Console.ReadLine();
            try { int idade = int.Parse(idadestr); Idade = idade; }
            catch (FormatException) { Console.WriteLine("A idade deve ser um valor numérico."); goto pegar_idade; }
            if (valor_saida.Equals(idadestr))
            {
                return false;
            }
            


            pegar_salario:
            Console.WriteLine("Qual será o salário dele?");
            salariostr = Console.ReadLine();
            try { double salario = double.Parse(salariostr); Salario = salario; }
            catch (FormatException) { Console.WriteLine("O salário deve ser um valor numérico."); goto pegar_salario; }
            if (valor_saida.Equals(salariostr))
            {
                return false;
            }
            return true;
            



            DAO dt = new DAO();
            dt.execute_command($"INSERT INTO proverexecises.funcionarios (Nome, Idade, Salario) VALUES('{Nome}',{Idade},{Salario})");
            return true;




        }


    }
}