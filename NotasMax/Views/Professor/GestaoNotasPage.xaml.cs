using Microsoft.Maui.Controls.Shapes;
using NotasMax.Helpers;
using NotasMax.Models;
using NotasMax.ViewModels;

namespace NotasMax.Views.Professor;

public partial class GestaoNotasPage : ContentPage
{
    private Turma _turmaSelecionada;
    private Simulado _simuladoSelecionado;

    private readonly List<Turma> _turmas;
    private readonly List<Simulado> _simulados;

    public GestaoNotasPage()
    {
        InitializeComponent();

        _turmas = CriarTurmasSeed();
        _simulados = CriarSimuladosSeed(_turmas);

        _turmaSelecionada = _turmas.First();
        _simuladoSelecionado = _simulados.First();

        RenderizarChipsTurma();
        RenderizarChipsSimulado();
        AtualizarLista();
    }

    // SEEDS
    
    private static List<Turma> CriarTurmasSeed()
    {
        var turma9A = new Turma
        {
            Id = Guid.NewGuid(),
            Serie = 9,
            Ano = 2025,

            Alunos = new List<Usuario>
            {
                new() { Id = Guid.NewGuid(), Nome = "Lucas Oliveira", Email = "lucas@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Ana Clara Santos", Email = "ana@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Pedro Henrique Lima", Email = "pedro@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Maria Eduarda Costa", Email = "maria@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "João Gabriel Souza", Email = "joao@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Isabela Ferreira", Email = "isabela@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Rafael Almeida", Email = "rafael@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Sophia Rodrigues", Email = "sophia@escola.com" },
            }
        };

        var turma9B = new Turma
        {
            Id = Guid.NewGuid(),
            Serie = 9,
            Ano = 2025,

            Alunos = new List<Usuario>
            {
                new() { Id = Guid.NewGuid(), Nome = "Beatriz Cunha", Email = "beatriz@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Carlos Mendes", Email = "carlos@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Diana Fonseca", Email = "diana@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Eduardo Vasconcelos", Email = "edu@escola.com" },
            }
        };

        var turma8A = new Turma
        {
            Id = Guid.NewGuid(),
            Serie = 8,
            Ano = 2025,

            Alunos = new List<Usuario>
            {
                new() { Id = Guid.NewGuid(), Nome = "Felipe Torres", Email = "felipe@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Gabriela Nunes", Email = "gabi@escola.com" },
                new() { Id = Guid.NewGuid(), Nome = "Henrique Barros", Email = "henrique@escola.com" },
            }
        };

        return new List<Turma>
        {
            turma9A,
            turma9B,
            turma8A
        };
    }

  private static List<Simulado> CriarSimuladosSeed(List<Turma> turmas)
{
    var matPortugues = new Materia
    {
        Id = Guid.NewGuid(),
        Nome = "Português",
        Cor = "#1565C0"
    };

    var matMatematica = new Materia
    {
        Id = Guid.NewGuid(),
        Nome = "Matemática",
        Cor = "#2E9E5B"
    };

    var matCiencias = new Materia
    {
        Id = Guid.NewGuid(),
        Nome = "Ciências",
        Cor = "#8E24AA"
    };

    var simulados = new List<Simulado>();

    foreach (var turma in turmas)
    {
        var tdPort = new TurmaDisciplina
        {
            Turma_Id = turma.Id,
            Turma = turma,
            Professor_Id = Guid.NewGuid(),
            Materia_Id = matPortugues.Id,
            Materia = matPortugues
        };

        var tdMat = new TurmaDisciplina
        {
            Turma_Id = turma.Id,
            Turma = turma,
            Professor_Id = Guid.NewGuid(),
            Materia_Id = matMatematica.Id,
            Materia = matMatematica
        };

        var tdCiencias = new TurmaDisciplina
        {
            Turma_Id = turma.Id,
            Turma = turma,
            Professor_Id = Guid.NewGuid(),
            Materia_Id = matCiencias.Id,
            Materia = matCiencias
        };

        // SIMULADO 1
        simulados.Add(new Simulado
        {
            Id = Guid.NewGuid(),
            Numero = 1,
            bimestre = 1,
            DataRealidacao = new DateTime(2025, 3, 10),

            Turma_Id = turma.Id,
            Turma = turma,

            Conteudos = new List<ConteudoSimulado>
            {
                new()
                {
                    TurmaDisciplina_Id = tdPort.Turma_Id,
                    TurmaDisciplina = tdPort,
                    QuantidadeQuestoes = 20,
                    Peso = 1.0,
                     Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)
                },

                new()
                {
                    TurmaDisciplina_Id = tdMat.Turma_Id,
                    TurmaDisciplina = tdMat,
                    QuantidadeQuestoes = 20,
                    Peso = 1.0,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                }
            }
        });

        // SIMULADO 2
        simulados.Add(new Simulado
        {
            Id = Guid.NewGuid(),
            Numero = 2,
            bimestre = 1,
            DataRealidacao = new DateTime(2025, 3, 28),

            Turma_Id = turma.Id,
            Turma = turma,

            Conteudos = new List<ConteudoSimulado>
            {
                new()
                {
                    TurmaDisciplina_Id = tdPort.Turma_Id,
                    TurmaDisciplina = tdPort,
                    QuantidadeQuestoes = 25,
                    Peso = 1.0,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                }
            }
        });

        // SIMULADO 3
        simulados.Add(new Simulado
        {
            Id = Guid.NewGuid(),
            Numero = 3,
            bimestre = 1,
            DataRealidacao = new DateTime(2025, 4, 15),

            Turma_Id = turma.Id,
            Turma = turma,

            Conteudos = new List<ConteudoSimulado>
            {
                new()
                {
                    TurmaDisciplina_Id = tdMat.Turma_Id,
                    TurmaDisciplina = tdMat,
                    QuantidadeQuestoes = 30,
                    Peso = 1.2,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                },

                new()
                {
                    TurmaDisciplina_Id = tdCiencias.Turma_Id,
                    TurmaDisciplina = tdCiencias,
                    QuantidadeQuestoes = 15,
                    Peso = 0.8,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                }
            }
        });

        // SIMULADO 4
        simulados.Add(new Simulado
        {
            Id = Guid.NewGuid(),
            Numero = 4,
            bimestre = 2,
            DataRealidacao = new DateTime(2025, 5, 12),

            Turma_Id = turma.Id,
            Turma = turma,

            Conteudos = new List<ConteudoSimulado>
            {
                new()
                {
                    TurmaDisciplina_Id = tdPort.Turma_Id,
                    TurmaDisciplina = tdPort,
                    QuantidadeQuestoes = 18,
                    Peso = 1.0,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                },

                new()
                {
                    TurmaDisciplina_Id = tdMat.Turma_Id,
                    TurmaDisciplina = tdMat,
                    QuantidadeQuestoes = 22,
                    Peso = 1.3,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                },

                new()
                {
                    TurmaDisciplina_Id = tdCiencias.Turma_Id,
                    TurmaDisciplina = tdCiencias,
                    QuantidadeQuestoes = 10,
                    Peso = 0.7,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                }
            }
        });

        // SIMULADO 5
        simulados.Add(new Simulado
        {
            Id = Guid.NewGuid(),
            Numero = 5,
            bimestre = 2,
            DataRealidacao = new DateTime(2025, 6, 3),

            Turma_Id = turma.Id,
            Turma = turma,

            Conteudos = new List<ConteudoSimulado>
            {
                new()
                {
                    TurmaDisciplina_Id = tdMat.Turma_Id,
                    TurmaDisciplina = tdMat,
                    QuantidadeQuestoes = 40,
                    Peso = 1.5,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                }
            }
        });

        // SIMULADO 6
        simulados.Add(new Simulado
        {
            Id = Guid.NewGuid(),
            Numero = 6,
            bimestre = 2,
            DataRealidacao = new DateTime(2025, 7, 20),

            Turma_Id = turma.Id,
            Turma = turma,

            Conteudos = new List<ConteudoSimulado>
            {
                new()
                {
                    TurmaDisciplina_Id = tdPort.Turma_Id,
                    TurmaDisciplina = tdPort,
                    QuantidadeQuestoes = 35,
                    Peso = 1.1,
 Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                },

                new()
                {
                    TurmaDisciplina_Id = tdCiencias.Turma_Id,
                    TurmaDisciplina = tdCiencias,
                    QuantidadeQuestoes = 20,
                    Peso = 1.0,
        Resultados = SimuladoHelper.GerarResultados(
                            turma.Alunos,
                            25)                }
            }
        });
    }

    return simulados;
}
    private void RenderizarChipsTurma()
    {
        TurmasContainer.Children.Clear();

        foreach (var turma in _turmas)
        {
            var label =
                $"{turma.Serie}º Ano " +
                $"{SimuladoHelper.ObterLetraTurma(turma, _turmas)}";

            var chip = CriarChip(
                label,
                turma == _turmaSelecionada);

            chip.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    // animaçãozinha
                    await chip.ScaleToAsync(0.96, 70, Easing.CubicOut);
                    await chip.ScaleToAsync(1.0, 70, Easing.CubicIn);

                    _turmaSelecionada = turma;

                    RenderizarChipsTurma();
                    RenderizarChipsSimulado();
                    AtualizarLista();
                })
            });

            TurmasContainer.Children.Add(chip);
        }
    }

    private void RenderizarChipsSimulado()
    {
        SimuladosContainer.Children.Clear();

        var simuladosDaTurma = _simulados
            .Where(s => s.Turma_Id == _turmaSelecionada.Id)
            .ToList();

        if (!simuladosDaTurma.Any(s =>
            s.Id == _simuladoSelecionado.Id))
        {
            _simuladoSelecionado = simuladosDaTurma.First();
        }

        foreach (var simulado in simuladosDaTurma)
        {
            var label =
                $"Sim {simulado.Numero} - " +
                $"{SimuladoHelper.NomeSimulado(simulado)}";

            var chip = CriarChip(
                label,
                simulado.Id == _simuladoSelecionado.Id);

            chip.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    _simuladoSelecionado = simulado;

                    RenderizarChipsSimulado();
                    AtualizarLista();
                })
            });

            SimuladosContainer.Children.Add(chip);
        }
    }

    private static Border CriarChip(
        string texto,
        bool selecionado)
    {
           return new Border
           {
               Background = selecionado
            ? new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb("#033363"), 0.0f),
                    new GradientStop(Color.FromArgb("#1685F3"), 1.0f)
                },
                new Point(0, 0),
                new Point(1, 0))
            : new SolidColorBrush(Colors.White),

            StrokeShape = new RoundRectangle
            {
                CornerRadius = 20
            },

            Stroke = selecionado
                ? Color.FromArgb("#1565C0")
                : Color.FromArgb("#E5E7EB"),

            StrokeThickness = 1,

            Padding = new Thickness(14, 6),

            Content = new Label
            {
                Text = texto,

                FontSize = 13,

                FontAttributes = selecionado
                    ? FontAttributes.Bold
                    : FontAttributes.None,

                TextColor = selecionado
                    ? Colors.White
                    : Color.FromArgb("#1A1A2E")
            }
        };
    }


    private void AtualizarLista()
    {
        var itens = _simuladoSelecionado.Conteudos

            .SelectMany(c =>
                c.Resultados.Select(r => new
                {
                    Resultado = r,
                    Questoes = c.QuantidadeQuestoes,
                    Peso = c.Peso
                }))

            .GroupBy(x => x.Resultado.Aluno_Id)

            .Select(g =>
            {
                var totalQuestoes =
                    g.Sum(x => x.Questoes);

                var pesoMedio =
                    g.Average(x => x.Peso);

                var resultadoAgrupado =
                    new ResultadoSimulado
                    {
                        Aluno_Id = g.Key,

                        Aluno = g
                            .First()
                            .Resultado
                            .Aluno,

                        Acertos = g
                            .Sum(x => x.Resultado.Acertos)
                    };

                return AlunoNotaItemViewModel.From(
                    resultadoAgrupado,
                    totalQuestoes,
                    pesoMedio);
            })

            .OrderBy(x => x.NomeCompleto)

            .ToList();

        AlunosCollection.ItemsSource = itens;
    }

    // SALVAR

    private async void OnSalvarClicked(
        object sender,
        EventArgs e)
    {
        var itens = (AlunosCollection.ItemsSource
            as IEnumerable<AlunoNotaItemViewModel>)?.ToList();

        if (itens == null || !itens.Any())
            return;

        var linhas = itens.Select(i =>
            $"{i.Iniciais}: " +
            $"{i.Acertos} acertos -> " +
            $"nota {i.Nota:F1}");

        var resumo = string.Join("\n", linhas);

        await DisplayAlertAsync(
            $"Simulado {_simuladoSelecionado.Numero} Notas salvas",
            resumo,
            "OK");
    }

    private void OnAcertosTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry)
            return;

        var apenasNumeros = new string(
            (e.NewTextValue ?? "")
            .Where(char.IsDigit)
            .ToArray());

        if (apenasNumeros.Length > 2)
            apenasNumeros = apenasNumeros[..2];

        if (entry.Text != apenasNumeros)
            entry.Text = apenasNumeros;
    }
}