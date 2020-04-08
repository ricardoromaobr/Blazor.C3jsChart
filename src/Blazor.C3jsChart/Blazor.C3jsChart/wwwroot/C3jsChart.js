window.C3jsChart = {
    _instances: [],

    inicializar: (id, dados, element, chartType, showLegend, legendPosition, zoom,
        zoomType, width, height, showToolTip, labels, showGridX, showGridY, xcategory,
        xcategorytype, rotateTickText, xlabel, xlabelPosition, ylabel, ylabelPosition,
        multilineMax, paddingTop, paddingLeft, paddingBottom, paddingRight,
        colorPattern) => {

        chart = C3jsChart._instances[id];

        chartObj = {};


        if (paddingTop || paddingLeft || paddingBottom || paddingRight)
            chartObj.padding = {
                top: paddingTop,
                left: paddingLeft,
                bottom: paddingBottom,
                right: paddingRight
            };

        if (colorPattern)
            chartObj.color = { pattern: colorPattern };

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

        chartObj.grid = {
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

            //define se o eixo x é multiline
        } else
            chartObj.axis.x.categories = undefined;

        if (multilineMax) {
            if (chartObj.axis.x.tick) {
                chartObj.axis.x.tick.multilineMax = multilineMax;
                chartObj.axis.x.tick.multiline = true;
            } else {
                chartObj.axis.x.tick = {
                    multilineMax: multilineMax,
                    multiline: true
                };
            }
        }

        if (rotateTickText) {
            chartObj.axis.x.tick = {
                rotate: rotateTickText,
                multiline: false
            };
        }

        if (xlabel) {
            chartObj.axis.x.label = {
                text: xlabel,
                position: xlabelPosition
            };
        }

        if (ylabel) {
            chartObj.axis.y.label = {
                text: ylabel,
                position: ylabelPosition
            };
        }

        if (!chart) {
            chartObj.bindto = '#' + id;
            chart = c3.generate(chartObj);
        } else
            chart.load(chartObj);
    }
}