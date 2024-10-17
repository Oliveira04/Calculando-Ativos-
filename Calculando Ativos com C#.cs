using System;

class Program
{
    static void Main()
    {
        // Recebe a entrada do número de ativos
        int N = int.Parse(Console.ReadLine());
        
        // Recebe e divide os valores de mercado em um array de strings
        string[] valoresMercadoStr = Console.ReadLine().Split(',');
        double[] valoresMercado = Array.ConvertAll(valoresMercadoStr, double.Parse);
        
        // Recebe o valor total investido
        double valorTotalInvestido = double.Parse(Console.ReadLine());
        
        // Recebe e divide as alocações mínimas em um array de strings
        string[] alocacoesMinimasStr = Console.ReadLine().Split(',');
        double[] alocacoesMinimas = Array.ConvertAll(alocacoesMinimasStr, double.Parse);
        
        // Recebe e divide as alocações máximas em um array de strings
        string[] alocacoesMaximasStr = Console.ReadLine().Split(',');
        double[] alocacoesMaximas = Array.ConvertAll(alocacoesMaximasStr, double.Parse);
        
        // Calcula o total do mercado
        double totalMercado = 0;
        for (int i = 0; i < N; i++)
        {
            totalMercado += valoresMercado[i];
        }

        // Calcula as alocações proporcionais e ajustando aos limites mínimos e máximos
        double[] alocacoes = new double[N];
        double totalAlocacoesProporcionais = 0;

        for (int i = 0; i < N; i++)
        {
            // Calcula a alocação proporcional
            double proporcional = (valoresMercado[i] / totalMercado) * valorTotalInvestido;
            
            // Ajusta a alocação respeitando os limites mínimo e máximo
            alocacoes[i] = Math.Max(alocacoesMinimas[i], Math.Min(alocacoesMaximas[i], proporcional));
            
            totalAlocacoesProporcionais += alocacoes[i]; // Acumula a alocação total
        }

        // Ajusta a alocação caso o total alocado não seja igual ao valor total investido
        double diferenca = valorTotalInvestido - totalAlocacoesProporcionais;

        // Distribui a diferença de forma proporcional dentro dos limites
        if (Math.Abs(diferenca) > 0.01) // Tolerância para erros de arredondamento
        {
            for (int i = 0; i < N; i++)
            {
                if (diferenca > 0 && alocacoes[i] < alocacoesMaximas[i])
                {
                    double capacidadeExtra = alocacoesMaximas[i] - alocacoes[i];
                    double adicionado = Math.Min(diferenca, capacidadeExtra);
                    alocacoes[i] += adicionado;
                    diferenca -= adicionado;
                }
                else if (diferenca < 0 && alocacoes[i] > alocacoesMinimas[i])
                {
                    double capacidadeReduzida = alocacoes[i] - alocacoesMinimas[i];
                    double subtraido = Math.Min(-diferenca, capacidadeReduzida);
                    alocacoes[i] -= subtraido;
                    diferenca += subtraido;
                }

                // Se a diferença já é zero, podemos parar
                if (Math.Abs(diferenca) < 0.01)
                {
                    break;
                }
            }
        }

        // Imprime as alocações formatadas com duas casas decimais
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine($"{alocacoes[i]:F2}"); // Mostra cada alocação formatada com duas casas decimais
        }
    }
}
