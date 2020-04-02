window.C3jsChart = {
    _instances: [],

    inicializar: (id, dados, element, chartType, showLegend, legendPosition, zoom,
        zoomType, width, height, showToolTip, labels, showGridX, showGridY, xcategory,
        xcategorytype, rotateTickText, xlabel, xlabelPosition, ylabel, ylabelPosition) => {

        chart = C3jsChart._instances[id];

        chartObj = {};

        dataObj = {
            json: dados,
            type: chartType
        };

        if (labels)
            dataObj.labels = true;

        chartObj.data = dataObj;
        chartObj.size = {};

        if (width)
            charObj.size.width = width;

        if (height)
            chartObj.size.height = height;

        chartObj.legend = {
            show: showLegend,
            position: legendPosition
        };

        chartObj.tooltip = {
            show: showToolTip
        };

        if (zoom) {
            chartObj.zoom = {
                enabled: true,
                type: zoomType
            };
        }

        chartObj.Grid = {
            x: {
                show: showGridX
            },
            y: {
                show: showGridY
            }
        }


        chartObj.axis = {
            x: {},
            y: {}
        };

        if (xcategory) {
            chartObj.axis.x.categories = xcategory;
            chartObj.axis.x.type = xcategorytype;
        }

        if (rotateTickText) {
            chartObj.axis.x.tick = {
                rotate: rotateTickText,
                multiline: false
            };
        }


        if (xlabel) {
            chartObj.axis.x.label = {
                text : xlabel,
                position: xlabelPosition
            };
        }

        if (ylabel) {
            chartObj.axis.y.label = {
                text : ylabel,
                position: ylabelPosition
            };
        }


        if (!chart) {
            chartObj.bindto = '#' + id;
            chart = c3.generate(chartObj);
        }
    }
}