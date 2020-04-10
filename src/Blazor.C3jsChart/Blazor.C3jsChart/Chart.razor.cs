using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.C3js.Chart
{
    public partial class Chart<TItem> : ComponentBase
    {
        #region fields

        ElementReference elementRef;
        string elementId;
        List<DataSet<TItem>> datasets = new List<DataSet<TItem>>();
        private BarOptions barOptions;

        [Inject]
        IJSRuntime JSRuntime { get; set; }
        #endregion

        #region inicialização

        #endregion

        /// <summary>
        /// Ciclo de vida do componente, padrão da plataforma blazor.
        /// Usado definir valores iniciais para o componente
        /// </summary>
        protected override void OnInitialized()
        {
            elementId = "id_" + Guid.NewGuid().ToString();

            base.OnInitialized();
        }

        /// <summary>
        /// Ciclo de vida do componente, padrão da plataforma blazor.
        /// Algumas chamadas só podem ser feitas após o componente estar renderizado
        /// </summary>
        /// <param name="firstRender">controle se é a primeira vez que o componene esta sendo renderizado</param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Initialize();

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task Initialize()
        {
            //serializa os dados em uma forma que c3js possa entender
            var dados = datasets.ToDictionary(d => d.Name, d => d.Datasouce);

            // inicializa o componente via javascript 
            await JSRuntime.InvokeVoidAsync("C3jsChart.inicializar",
                elementId, dados, elementRef, ObterChartType(Type),
                ShowLegend, ObterLegendPosition(LegendPosition),
                Zoom, ObterZoomType(ZoomType), Width, Height, ShowToolTip,
                ShowDataLabels, ShowGridX, ShowGridY, XCategory,
                XCategoryType.ToString().ToLower(), RotateTickText,
                XLabel, ObterXLabelPosition(XLabelPosition), YLabel,
                ObterYLabelPosition(YLabelPosition),
                MultilineMax, PaddingTop, PaddingLeft,
                PaddingBotttom, PaddingRight, ColorPattern,
                BarOptions, PieOptions, DonutOptions, GaugeOptions,
                ToolTipFormatValue);
        }

        #region Methods
        /// <summary>
        /// Adiciona um dataset para o gráfico.
        /// </summary>
        /// <param name="dataset">O dataset a ser adicionado</param>
        public void AddDataset(DataSet<TItem> dataset)
        {
            if (!datasets.Contains(dataset))
            {
                datasets.Add(dataset);
            }
        }

        public async void Update()
        {
            await Initialize();
        }
        public void AddDataset(IEnumerable<DataSet<TItem>> items)
        {
            foreach (var dataset in items)
                if (!datasets.Contains(dataset))
                    datasets.Add(dataset);
                else
                    throw new DatasetAlreadyInDatasetsException("Dataset already exists");
        }

        /// <summary>
        /// Criar um dataset, para que não seja necessário criar dataset manualmente;
        /// </summary>
        /// <param name="name">nome do dataset, descrição que vai aparecer na legenda</param>
        /// <param name="valores"> dados do dataset para segar o gráfico</param>
        /// <returns></returns>
        public DataSet<TItem> CreateDataset(string name, params TItem[] valores)
        {
            return new DataSet<TItem>
            {
                Name = name,
                Datasouce = new List<TItem>(valores)
            };
        }

        public void ClearDataset()
        {
            datasets.Clear();
        }
        /// <summary>
        /// Converte o tipo para o formato do c3
        /// </summary>
        /// <param name="type">O tipo definido do gráfico</param>
        /// <returns></returns>
        string ObterChartType(ChartType type)
        {
            switch (type)
            {
                case ChartType.Line:
                    return "line";
                case ChartType.Pie:
                    return "pie";
                case ChartType.Bar:
                    return "bar";
                case ChartType.Area:
                    return "area";
                case ChartType.Spline:
                    return "spline";
                case ChartType.Dunot:
                    return "donut";
                case ChartType.StackedBar:
                    return "stackedBar";
                default:
                    return "line";
            }
        }
        /// <summary>
        /// Converte o tipo para o formato do c3
        /// </summary>
        /// <param name="zoomType">A maneira de realizar o zoom no gráfico</param>
        /// <returns></returns>
        string ObterZoomType(ZoomType zoomType)
        {
            switch (zoomType)
            {
                case ZoomType.Drag:
                    return "drag";
                case ZoomType.Scrool:
                    return "scroll";
                default:
                    return "scroll";

            }
        }

        /// <summary>
        /// Converte o tipo para o formato do c3
        /// </summary>
        /// <param name="legendPosition">Define o lado da legenda no gráfico.</param>
        /// <returns></returns>
        string ObterLegendPosition(LegendPosition legendPosition)
        {
            string result = null;
            switch (legendPosition)
            {
                case LegendPosition.Right:
                    result = "right";
                    break;
                case LegendPosition.Inset:
                    result = "inset";
                    break;
                case LegendPosition.Bottom:
                    result = "bottom";
                    break;
            }
            return result;
        }

        string ObterXLabelPosition(XLabelPosition xLabelPosition)
        {
            switch (xLabelPosition)
            {
                case XLabelPosition.InnerCenter:
                    return "inner-center";
                case XLabelPosition.InnerLeft:
                    return "inner-left";
                case XLabelPosition.InnerRight:
                    return "inner-right";
                case XLabelPosition.OuterCenter:
                    return "outer-center";
                case XLabelPosition.OuterLeft:
                    return "outer-left";
                case XLabelPosition.OuterRight:
                    return "outer-right";
                default:
                    return "inner-right";
            }
        }

        string ObterYLabelPosition(YLabelPosition yLabelPosition)
        {
            switch (yLabelPosition)
            {
                case YLabelPosition.InnerTop:
                    return "inner-top";
                case YLabelPosition.InnerMiddle:
                    return "inner-middle";
                case YLabelPosition.InnerBottom:
                    return "inner-bottom";
                case YLabelPosition.OuterTop:
                    return "outer-top";
                case YLabelPosition.OuterBottom:
                    return "outer-bottom";
                case YLabelPosition.OuterMiddle:
                    return "outer-middle";
                default:
                    return "inner-top";
            }
        }

        public void AddBarOptions(BarOptions barOptions)
        {
            if (Type == ChartType.Bar)
                this.barOptions = barOptions;
            else
                throw new InvalidChartOptions($"This options is not valid to this chart type:{Type.ToString()}");
        }
        #endregion

        #region properties

        public IEnumerable<DataSet<TItem>> Datasets => datasets;
        /// <summary>
        /// Tipo do gráfico
        /// </summary>
        [Parameter] public ChartType Type { get; set; } = ChartType.Line;

        /// <summary>
        /// Define a largura do gráfico
        /// </summary>
        [Parameter] public int? Width { get; set; } = null;
        /// <summary>
        /// Define a latura do gráfico
        /// </summary>
        [Parameter] public int? Height { get; set; } = null;
        /// <summary>
        /// Define se o gráfico possibilita realizar zoom
        /// </summary>
        [Parameter] public bool Zoom { get; set; } = false;
        /// <summary>
        /// Define a maneira que o usuário pode realizar o zoom no gráfico
        /// </summary>
        [Parameter] public ZoomType ZoomType { get; set; } = ZoomType.Scrool;
        /// <summary>
        /// Define se o tootip dos valores do gráfico deve ser apresentado
        /// </summary>
        [Parameter] public bool ShowToolTip { get; set; } = true;
        /// <summary>
        /// Define se a legenda vai ser apresentada
        /// </summary>
        [Parameter] public bool ShowLegend { get; set; } = true;
        /// <summary>
        /// define a posição da lengenda
        /// </summary>
        [Parameter] public LegendPosition LegendPosition { get; set; }

        /// <summary>
        /// define se o gráfico vai mostrar o label dos valores do dataset no gráfico
        /// </summary>
        [Parameter] public bool ShowDataLabels { get; set; }

        /// <summary>
        /// Define titulo do gráfico
        /// </summary>
        [Parameter] public string Title { get; set; }

        /// <summary>
        /// Mostrar grid no eixo X
        /// </summary>
        [Parameter] public bool ShowGridX { get; set; }

        /// <summary>
        /// Mostrar grid no eixo Y
        /// </summary>
        [Parameter] public bool ShowGridY { get; set; }

        /// <summary>
        /// Nome do dataset com os dados para mostrar no eixo X
        /// </summary>
        [Parameter] public string DatasetNameExisX { get; set; }
        /// <summary>
        /// Container para os dataset a serem utilizado
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Dados a serem mostrado no eixo X
        /// </summary>
        [Parameter] public IEnumerable<object> XCategory { get; set; }

        /// <summary>
        /// Tipo da categoria do eixo X
        /// </summary>
        [Parameter] public XType XCategoryType { get; set; }
        /// <summary>
        /// Rotate tick text no eixo X
        /// </summary>
        [Parameter] public int RotateTickText { get; set; }
        /// <summary>
        /// X label caption 
        /// </summary>
        [Parameter] public string XLabel { get; set; }
        /// <summary>
        /// The x axis label position
        /// </summary>
        [Parameter] public XLabelPosition XLabelPosition { get; set; }
        /// <summary>
        /// The Y Label caption
        /// </summary>
        [Parameter] public string YLabel { get; set; }
        /// <summary>
        /// The posistion of the Y axies label
        /// </summary>
        [Parameter] public YLabelPosition YLabelPosition { get; set; }
        /// <summary>
        /// When using the x category type, define the max number of line
        /// </summary>
        [Parameter] public int? MultilineMax { get; set; } = null;
        /// <summary>
        /// Padding left of the chart
        /// </summary>
        [Parameter] public int? PaddingLeft { get; set; } = null;
        /// <summary>
        /// Padding right of the chart
        /// </summary>
        [Parameter] public int? PaddingRight { get; set; } = null;
        /// <summary>
        /// Padding top of the chart
        /// </summary>
        [Parameter] public int? PaddingTop { get; set; } = null;
        /// <summary>
        /// Padding bomttom of the chart
        /// </summary>
        [Parameter] public int? PaddingBotttom { get; set; } = null;
        /// <summary>
        /// Colors
        /// </summary>
        [Parameter] public string[] ColorPattern { get; set; }
        /// <summary>
        /// Options to chart when Type is Bar
        /// </summary>
        [Parameter] public BarOptions BarOptions { get; set; }
        /// <summary>
        /// Options to chart when Type is Pie
        /// </summary>
        [Parameter] public PieOptions PieOptions { get; set; }
        /// <summary>
        /// Options to chat when Type is Donut
        /// </summary>
        [Parameter] public DonutOptions DonutOptions { get; set; }
        /// <summary>
        /// Options to chart when Type  is Gauge
        /// </summary>
        [Parameter] public GaugeOptions GaugeOptions { get; set; }

        [Parameter] public TooTipFormatValue ToolTipFormatValue { get; set; }

        #endregion
    }
}

