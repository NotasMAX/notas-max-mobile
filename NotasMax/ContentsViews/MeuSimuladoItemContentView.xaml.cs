using NotasMax.ViewModels;
using System.Globalization;

namespace NotasMax.ContentsViews;

public partial class MeuSimuladoItemContentView : ContentView
{
    bool isRealizado = false;
    public MeuSimuladoItemContentView()
	{
		InitializeComponent();
	}

    private void Button_Tapped(object sender, TappedEventArgs e)
    {
        if (BindingContext is not SimuladosByAluno.Simulado simulado)
            return;

        if (!isRealizado)
            return;

        //lógica para ir para a view de simulado
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is not SimuladosByAluno.Simulado simulado)
            return;

        isRealizado = simulado.DataRealizacao <= DateTime.Now;

        if (isRealizado)
        {
            border_icon.BackgroundColor = Color.FromArgb("#eaf8f0"); 
            image_icon.Source = "done.png";
            label_valor_media.TextColor = Helpers.ColorHelper.DefinirCor(simulado.Media);
            label_valor_media.Text = simulado.Media.ToString("N2");
        }
        else
        {
            border_icon.BackgroundColor = Color.FromArgb("#eff0f5");
            image_icon.Source = "clock.png";
            container_media.IsVisible = false;
            image_arrow.IsVisible = false;
        }

        label_numero_simulado.Text = $"Simulado {simulado.Numero}";
        string dataFormatada = simulado.DataRealizacao.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR"));
        label_data_realizacao.Text = dataFormatada;

    }
}