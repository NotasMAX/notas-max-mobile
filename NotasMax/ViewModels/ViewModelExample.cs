using System.Collections.ObjectModel;

namespace NotasMax.ViewModels;

public class NotaSimulado
{
    public string? Numero { get; set; }
    public double Nota { get; set; }
}

public class NotaMateria
{
    public string? Materia { get; set; }
    public double Nota { get; set; }
}

public class ViewModelExemplo
{
    public List<NotaSimulado> NotaSimulados { get; set; }
    public List<NotaMateria> NotaMaterias { get; set; }
    public ViewModelExemplo()
    {
        // Gerando alguns dados de exemplo (Mock / Simulação) para o gráfico de desempenho
        NotaSimulados = new List<NotaSimulado>
        {
            new NotaSimulado { Numero = "1", Nota = 6.54 },
            new NotaSimulado { Numero = "2", Nota = 7.45 },
            new NotaSimulado { Numero = "3", Nota = 6.30 },
            new NotaSimulado { Numero = "4", Nota = 8.56 },
            new NotaSimulado { Numero = "5", Nota = 9.14 }
        };

        NotaMaterias = new List<NotaMateria>
        {
            new NotaMateria { Materia = "Matemática", Nota = 7.5 },
            new NotaMateria { Materia = "Português", Nota = 8.0 },
            new NotaMateria { Materia = "Química", Nota = 6.5 },
            new NotaMateria { Materia = "História", Nota = 7.0 },
            new NotaMateria { Materia = "Geografia", Nota = 8.5 },
            new NotaMateria { Materia = "Inglês", Nota = 3.5 },
            new NotaMateria { Materia = "Física", Nota = 8.0 },
            new NotaMateria { Materia = "Biologia", Nota = 6.5 }
        };


    }
}
