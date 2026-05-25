using NotasMax.Models;
using NotasMax.Services.Simulados;
using NotasMax.ViewModels;
using Syncfusion.Maui.Toolkit.Calendar;
using System.Diagnostics;
using System.Globalization;

namespace NotasMax.Views.Aluno;

public partial class CalendarioAlunoView : ContentPage
{
    private readonly ISimuladoService _simuladoService;
    private CalendarioByAluno? calendario;
    public CalendarioAlunoView(ISimuladoService simuladoService)
    {
        InitializeComponent();
        _simuladoService = simuladoService;
    }

    private async Task FecthCalendario()
    {
        try
        {
            calendario = await _simuladoService.GetCalendarioByAluno();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);   
        }
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await InicializarCalendario();

    }

    private void PersonalizacaoCalendario()
    {
        CalendarTextStyle textStyle = new CalendarTextStyle()
        {
            TextColor = Colors.Black,
            FontSize = 12,  
        };

        CalendarTextStyle TrailingLeadingDatesTextStyle = new CalendarTextStyle()
        {
            TextColor = Colors.Gray,
            FontSize = 12,
        };
        calendar.SelectionBackground = Colors.Transparent;
        calendar.TodayHighlightBrush = Color.FromArgb("1685F3");


        calendar.MonthView = new CalendarMonthView()
        {
            WeekendDays = new List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Saturday,
            },
            TextStyle = textStyle,
            TodayTextStyle = textStyle,
            SelectionTextStyle = textStyle,           
            SpecialDatesTextStyle = textStyle,
            WeekendDatesTextStyle = textStyle,
            TrailingLeadingDatesTextStyle = TrailingLeadingDatesTextStyle,
        };
    }

    private async Task InicializarCalendario()
    {
        PersonalizacaoCalendario();
        var dataAtual = DateTime.Now;
        calendar.MinimumDate = dataAtual.AddYears(-1);
        calendar.MaximumDate = dataAtual.AddYears(1);

        await FecthCalendario();
        var simuladoMesSelecionado = calendario?.Simulados.Where(s => s.DataRealizacao.Month == dataAtual.Month);
        BindableLayout.SetItemsSource(layout_eventos, simuladoMesSelecionado);

        if (calendario == null)
            return;

        calendar.MonthView.SpecialDayPredicate = (date) =>

        {
            var simuladodentificado = calendario.Simulados?.FirstOrDefault(e => e.DataRealizacao.Date == date);

            if (simuladodentificado == null)
                return null;

            CalendarIconDetails iconDetails = new CalendarIconDetails();
            iconDetails.Icon = CalendarIcon.Dot;

            if (simuladodentificado.Tipo == "objetivo")
            {
                iconDetails.Fill = Color.FromArgb("#1685F3");
            }
            else
            {
                iconDetails.Fill = Color.FromArgb("#FFBB0F");
            }
            return iconDetails;
        };
    }

    private void calendar_ViewChanged(object sender, CalendarViewChangedEventArgs e)
    {
        int meioMes = e.NewVisibleDates.Count / 2;
        var novoMes = e.NewVisibleDates[meioMes].Month;

        var simuladoMesSelecionado = calendario?.Simulados.Where(s => s.DataRealizacao.Month == novoMes);
        BindableLayout.SetItemsSource(layout_eventos, simuladoMesSelecionado);
    }
}