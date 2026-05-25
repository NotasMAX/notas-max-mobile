// Helpers/SimuladoHelper.cs

using NotasMax.Models;

namespace NotasMax.Helpers;

public static class SimuladoHelper
{
    public static string ObterLetraTurma(
        Turma turma,
        List<Turma> turmas)
    {
        var idx = turmas
            .Where(t => t.Serie == turma.Serie)
            .ToList()
            .IndexOf(turma);

        return ((char)('A' + idx)).ToString();
    }

    public static string NomeSimulado(Simulado simulado)
    {
        return simulado.Numero switch
        {
                1 => "Diagnóstico",
                2 => "Março",
                3 => "Abril",
                4 => "Maio",
                5 => "Junho",
                6 => "Julho",
                7 => "Agosto",
                8 => "Setembro",
                9 => "Outubro",
                10 => "Novembro",
                11 => "Final",

                _ => $"Bim {simulado.bimestre}"
            };
    }

    public static List<ResultadoSimulado> GerarResultados(
        List<Usuario> alunos,
        int totalQuestoes)
    {
        var rng = new Random();

        return alunos.Select(aluno =>
        {
            var acertos = rng.Next(0, totalQuestoes + 1);

            return new ResultadoSimulado
            {
                Aluno_Id = aluno.Id,
                Aluno = aluno,

                Acertos = acertos,

                Nota = 0
            };
        }).ToList();
    }
}