using NotasMax.ViewModels;
using System.Globalization;

namespace NotasMax.ContentsViews;

public partial class CalendarioItemContentView : ContentView
{
    public CalendarioItemContentView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (!(this.BindingContext is CalendarioByAluno.CalendarioSimulado simulado))
            return;

        string dataFormatada = simulado.DataRealizacao.ToString("dd 'de' MMMM", new CultureInfo("pt-BR"));
        label_data_realizacao.Text = dataFormatada;
        label_numero_simulado.Text = "Simulado " + simulado.Numero;

        var tipo = simulado.Tipo;
        if (tipo == "objetivo")
        {
            border_tipo.BackgroundColor = Color.FromArgb("#e7ecf4");
            border_icon.BackgroundColor = Color.FromArgb("#e7ecf4");
        }
        else
        {
            border_tipo.BackgroundColor = Color.FromArgb("#fef5da");
            border_icon.BackgroundColor = Color.FromArgb("#fef5da");
        }
        label_tipo.Text = char.ToUpper(tipo[0]) + tipo.Substring(1).ToLower();
    }
}