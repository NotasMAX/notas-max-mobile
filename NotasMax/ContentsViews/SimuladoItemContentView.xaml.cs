using NotasMax.Helpers;
using NotasMax.ViewModels;

namespace NotasMax.ContentsViews;

public partial class SimuladoItemContentView : ContentView
{
    public SimuladoItemContentView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is not NotaSimulado nota)
            return;

        label_nome_simulado.Text = "Simulado " + nota.Numero;
        label_data.Text = nota.Data.ToString("dd 'de' MMM", new System.Globalization.CultureInfo("pt-BR"));
        label_valor_nota.Text = nota.Nota.ToString("N2");
        label_valor_nota.TextColor = ColorHelper.DefinirCor(nota.Nota);
    }
}