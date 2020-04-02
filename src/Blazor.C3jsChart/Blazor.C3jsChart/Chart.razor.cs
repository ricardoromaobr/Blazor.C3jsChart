using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.C3jsChart
{
    public partial class Chart<TItem> : ComponentBase
    {
        #region fields

        ElementReference elementRef;
        string elementId;
        List<DataSet<TItem>> datasets = new List<DataSet<TItem>>();

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
            //serializa os dados em uma forma que c3js possa entender
            var dados = datasets.ToDictionary(d => d.Name, d => d.Datasouce);

            // inicializa o componente via javascript 
            await JSRuntime.InvokeVoidAsync("C3jsChart.inicializar",
                elementId, dados, elementRef, ObterChartType(Type),
                ShowLegend, ObterLegendPosition(LengendPosition),
                Zoom, ObterZoomType(ZoomType), Width, Height, ShowToolTip,
                ShowDataLabels, ShowGridX, ShowGridY, XCategory,
                XCategoryType.ToString().ToLower(), RotateTickText,
                XLabel, ObterXLabelPosition(XLabelPosition), YLabel,
                ObterYLabelPosition(YLabelPosition));

            await base.OnAfterRenderAsync(firstRender);
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
            switch (legendPosition)
            {
                case LegendPosition.Left:
                    return "left";
                case LegendPosition.Right:
                    return "right";
                case LegendPosition.Top:
                    return "top";
                case LegendPosition.Bottom:
                    return "bottom";
                default:
                    return "bottom";
            }
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
        #endregion

        #region properties
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
        [Parameter] public LegendPosition LengendPosition { get; set; }

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
        #endregion
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
        [Parameter] public string XLabel { get; set; }
        [Parameter] public XLabelPosition XLabelPosition { get; set; }
        [Parameter] public string YLabel { get; set; }
        [Parameter] public YLabelPosition YLabelPosition { get; set; }
    }
}

