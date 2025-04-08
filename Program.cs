using System;
using System.Text.Json;

namespace Teste_Tecnico {
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Questão 1");
            Soma();

            Console.WriteLine("\nQuestão 2");
            Faturamentos();

            Console.WriteLine("\nQuestão 3");
            Fibonacci(35);

            Console.WriteLine("\nQuestão 4");
            PercentualPorEstado();

            Console.WriteLine("\nQuestão 5");
            InverterString("Teste");
        }

        //Questão 1
        public static void Soma()
        {
            int indice = 13;
            int soma = 0;

            for(int k =0; k < indice; k++) {
                soma += k;
            }

            Console.WriteLine("O valor da soma é: " +soma);
        }

        //Questão 2
        public static bool Fibonacci(int numero)
        {
            int numeroAtual = 1;
            int numeroAnterior = 0;

            //Calcula a sequência de Fibonacci até que passe o número informado
            while (numeroAnterior <= numero)
            {
                int auxiliar = numeroAtual; //Segura o número atual

                numeroAtual = numeroAtual + numeroAnterior;
                numeroAnterior = auxiliar;

                //Se o número é igual ao informado
                if (numeroAnterior == numero)
                {
                    Console.WriteLine("O número " + numero + " pertence à sequência de Fibonacci.");
                    return true;
                }
            }

            Console.WriteLine("O número " + numero + " não pertence à sequência de Fibonacci!");
            return false;
        }

        //Questão 3
        public static void Faturamentos()
        {
            string caminhoArquivo = "dados.json";
            string jsonString = File.ReadAllText(caminhoArquivo);

            //Passa os arquivos para o format
            List<Faturamento> faturamento = JsonSerializer.Deserialize<List<Faturamento>>(jsonString);

            //Maior e menor valor
            double menorValor = faturamento.Where(f => f.valor > 0).Min(f => f.valor);
            double maiorValor = faturamento.Max(f => f.valor);

            //Faturamentos acima da média mensal
            double mediaMensal = faturamento.Where(f => f.valor > 0) //Remove dias sem faturamento
                                            .Average(f => f.valor);  //Média mensal

            var faturamentosDiarios = faturamento.GroupBy(f => f.dia); //Agrupa por dia

            //Calcula os dias em que o faturamento ultrapassou a média
            int diasComFaturamentoAcimaDaMedia = faturamentosDiarios.Where(f => f.Sum(fd => fd.valor) > mediaMensal).Count();


            //Saída
            Console.WriteLine("Menor faturamento do mês: " + menorValor.ToString("F2"));
            Console.WriteLine("Maior faturamento do mês: " + maiorValor.ToString("F2"));
            Console.WriteLine("n° de dias com faturamento superior à media do mês: " + diasComFaturamentoAcimaDaMedia);
        }

        //Questão 4
        private static void PercentualPorEstado()
        {
            Dictionary<string, double> faturamentosPorEstado = new Dictionary<string, double>
            {
                { "SP", 67836.43 },
                { "RJ", 36678.66 },
                { "MG", 29229.88 },
                { "ES", 27165.48 },
                { "Outros", 19849.53 }
            };

            double faturamentoTotal = faturamentosPorEstado.Sum(f => f.Value);

            foreach (var faturamento in faturamentosPorEstado)
            {
                double percentualDoEstado = (faturamento.Value / faturamentoTotal) * 100;
                Console.WriteLine(faturamento.Key +": " +percentualDoEstado.ToString("F2") +"%");
            }
        }

        //Questão 5
        private static string InverterString(string str)
        {
            string novaString = "";
            char[] caracteres = str.ToCharArray();

            //Monta
            for (int i = caracteres.Length - 1; i >= 0; i--)
            {
                novaString += caracteres[i];
            }

            Console.WriteLine(novaString);
            return novaString;
        }
    }
}