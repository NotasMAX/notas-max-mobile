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

public class NotaAluno
{
    public string? Nome { get; set; }
    public double Nota { get; set; }
}

public class DistribuicaoNotas
{
    public string? Faixa { get; set; }
    public int Quantidade { get; set; }
}

public class ViewModelExemplo
{
    public List<NotaSimulado> NotaSimulados { get; set; }
    public List<NotaMateria> NotaMaterias { get; set; }
    public List<NotaAluno> NotaAlunos { get; set; }

    public List<DistribuicaoNotas> DistribuicaoNotas { get; set; }

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

        NotaAlunos = new List<NotaAluno>
        {
            new NotaAluno { Nome = "João", Nota = 7.5 },
            new NotaAluno { Nome = "Maria", Nota = 8.0 },
            new NotaAluno { Nome = "Pedro", Nota = 6.5 },
            new NotaAluno { Nome = "Ana", Nota = 7.0 },
            new NotaAluno { Nome = "Lucas", Nota = 8.5 },
            new NotaAluno { Nome = "Sofia", Nota = 3.5 },
            new NotaAluno { Nome = "Rafael", Nota = 8.0 },
            new NotaAluno { Nome = "Isabela", Nota = 6.5 }
        };

        DistribuicaoNotas = new List<DistribuicaoNotas>
        {
            new DistribuicaoNotas { Faixa = "<5", Quantidade = 1 },
            new DistribuicaoNotas { Faixa = "5-7", Quantidade = 2 },
            new DistribuicaoNotas { Faixa = ">7", Quantidade = 5 },
        };
    }
}
