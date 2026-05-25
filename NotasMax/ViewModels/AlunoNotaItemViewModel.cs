// ViewModels/AlunoNotaItemViewModel.cs

using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotasMax.Models;

namespace NotasMax.ViewModels;

public class AlunoNotaItemViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string nome = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

    // Dados do aluno
    public Guid AlunoId { get; init; }

    public string NomeCompleto { get; init; } = "";

    public string Iniciais { get; init; } = "";

    // Dados do simulado
    public int TotalQuestoes { get; init; }

    public double Peso { get; init; } = 1.0;

   
    private int _acertos;

    public int Acertos
    {
        get => _acertos;
        set
        {
            var novoValor = Math.Max(0, Math.Min(value, TotalQuestoes));

            if (_acertos == novoValor)
                return;

            _acertos = novoValor;

            OnPropertyChanged();
            OnPropertyChanged(nameof(Nota));
            OnPropertyChanged(nameof(CorNota));
        }
    }


    public decimal Nota
    {
        get
        {
            if (TotalQuestoes <= 0)
                return 0m;

            var notaCalculada =
                (decimal)Acertos / TotalQuestoes * 10 * (decimal)Peso;

            return Math.Min(Math.Round(notaCalculada, 1), 10m);
        }
    }


    public Color CorNota =>
        Nota >= 7 ? Color.FromArgb("#2E9E5B")
        : Nota >= 5 ? Color.FromArgb("#E6A817")
        : Color.FromArgb("#E53935");

  
    public static AlunoNotaItemViewModel From(
        ResultadoSimulado resultado,
        int totalQuestoes,
        double peso)
    {
        var partes = (resultado.Aluno.Nome ?? "")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var iniciais = partes.Length switch
        {
            >= 2 => $"{partes[0][0]}{partes[^1][0]}".ToUpper(),
            1 => $"{partes[0][0]}".ToUpper(),
            _ => "?"
        };

        return new AlunoNotaItemViewModel
        {
            AlunoId = resultado.Aluno_Id,
            NomeCompleto = resultado.Aluno.Nome ?? "",
            Iniciais = iniciais,
            TotalQuestoes = totalQuestoes,
            Peso = peso,
            _acertos = resultado.Acertos
        };
    }
}