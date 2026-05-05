using System.Collections.ObjectModel;

namespace NotasMax.ViewModels;

public class ResultadoSimulado
{
    public string? Numero { get; set; }
    public double Nota { get; set; }
}

public class ViewModelExemplo
{
    public ObservableCollection<ResultadoSimulado> DadosSimulados { get; set; }

    public ViewModelExemplo()
    {
        // Gerando alguns dados de exemplo (Mock / Simulação) para o gráfico de desempenho
        DadosSimulados = new ObservableCollection<ResultadoSimulado>
        {
            new ResultadoSimulado { Numero = "1", Nota = 6.5 },
            new ResultadoSimulado { Numero = "2", Nota = 7.0 },
            new ResultadoSimulado { Numero = "3", Nota = 6.0 },
            new ResultadoSimulado { Numero = "4", Nota = 8.5 },
            new ResultadoSimulado { Numero = "5", Nota = 9.0 }
        };
    }
}
