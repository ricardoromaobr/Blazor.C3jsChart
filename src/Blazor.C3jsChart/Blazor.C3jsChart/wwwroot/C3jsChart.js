window.C3jsChart = {
    _instances: [],

    inicializar: (id, dados, element, chartType, showLegend, legendPosition, zoom,
        zoomType, width, height, showToolTip, labels, showGridX, showGridY, xcategory,
        xcategorytype, rotateTickText, xlabel, xlabelPosition, ylabel, ylabelPosition,
        multilineMax, paddingTop, paddingLeft, paddingBottom, paddingRight,
        colorPattern, barOptions, pieOptions, donutOptions, gaugeOptions,
        tooTipFormatValue) => {

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

        if (barOptions) {
            chartObj.bar = {}
            if (barOptions.width)
                chartObj.bar.width = barOptions.Width;
            if (barOptions.ratioWidth)
                chartObj.bar.width = {
                    ratio: barOptions.ratioWidth
                };
            if (barOptions.zeroBase)
                chartObj.bar.zerobase = barOptions.ZeroBase;
        }

        if (pieOptions) {
            chartObj.pie = {
                label: {}
            };
            chartObj.pie.label.show = pieOptions.showLabel;

            if (pieOptions.labelFormat === 0)  // value
                chartObj.pie.label.format = C3jsChart.formatLabelReturnValue;
            else if (pieOptions.labelFormat === 1) // ratio
                chartObj.pie.label.format = C3jsChart.formatLabelReturnRatio;
            else if (pieOptions.labelFormat === 2) // id
                chartObje.pie.label.format = C3jsChart.formatLabelReturnId;

            chartObj.pie.threshold = pieOptions.labelThreshold;
            chartObj.pie.expand = pieOptions.expand;
        }

        if (donutOptions) {
            chartObj.donut = {
                label: {}
            };

            chartObj.donut.label.show = donutOptions.showLabel;
            if (donutOptions.labelFormat === 0)  // value
                chartObj.donut.label.format = C3jsChart.formatLabelReturnValue;
            else if (donutOptions.labelFormat === 1) // ratio
                chartObj.donut.label.format = C3jsChart.formatLabelReturnRatio;
            else if (donutOptions.labelFormat === 2) // id
                chartObje.donut.label.format = C3jsChart.formatLabelReturnId;

            chartObj.donut.label.donut.threshold = donutOptions.labelThreshold;
            chartObj.donut.width = donutOptions.width;
            chartObj.donut.title = donutOptions.title;
        }

        if (gaugeOptions) {
            chartObj.gauge = {
                label: {}
            };

            chartObj.gauge.label.show = donutOptions.showLabel;
            if (gaugeOptions.labelFormat === 0)  // value
                chartObj.gauge.label.format = C3jsChart.formatLabelReturnValue;
            else if (gaugeOptions.labelFormat === 1) // ratio
                chartObj.gauge.label.format = C3jsChart.formatLabelReturnRatio;
            else if (gaugeOptions.labelFormat === 2) // id
                chartObje.gauge.label.format = C3jsChart.formatLabelReturnId;

            chartObj.gauge.label.donut.threshold = donutOptions.labelThreshold;
            chartObj.gauge.min = gaugeOptions.min;
            chartObj.gauge.max = gaugeOptions.max;
            chartObj.gauge.width = gaugeOptions.width;
        }

        if (typeof(tooTipFormatValue) !== "undefined") {
            chartObj.tooltip.format = {};
            if (tooTipFormatValue === 0)  // value
                chartObj.tooltip.format.value = C3jsChart.formatLabelReturnValue;
            else if (tooTipFormatValue === 1) // ratio
                chartObj.tooltip.format.value = C3jsChart.formatLabelReturnRatio;
            else if (tooTipFormatValue === 2) // id
                chartObj.tooltip.format.value = C3jsChart.formatLabelReturnId;
        }

        
        if (!chart) {
            chartObj.bindto = '#' + id;
            chart = c3.generate(chartObj);
        } else
            chart.load(chartObj);
    },

    formatLabelReturnValue: (value, ratio, id) => {
        return d3.format(",")(value);
    },

    formatLabelReturnRatio: (value, ratio, id) => {
        return ratio;
    },

    formatLabelReturnId: (value, ratio, id) => {
        return id;
    }, 

    destroy: (id) => {                
        delete _instances[id];
    }

}