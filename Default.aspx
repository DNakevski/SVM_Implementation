<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SVM</title>

    <link href="include/style.css" rel="stylesheet" />

    <script src="js/jquery-1.6.4.min.js"></script>
    <script src="js/highcharts.js"></script>
    <script src="js/modules/exporting.js"></script>
    <script src="js/themes/grid.js"></script>

    <script type="text/javascript">
        var chart;

        $(document).ready(function () {

            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'container',
                    type: 'scatter',
                    borderWidth: 0,
                    backgroundColor: {
                        linearGradient: [0, 0, 500, 500],
                        stops: [
                           [0, 'rgb(240, 240, 255)'],
                           [1, 'rgb(240, 240, 255)']
                        ]
                    },
                    margin: [70, 50, 60, 80],
                    events: {
                        click: function (e) {
                            // find the clicked values and the series
                            var x = e.xAxis[0].value,
                                y = e.yAxis[0].value,
                                series = this.series[0];    
                            
                            addPoint(x, y);

                            // Add it
                            //series.addPoint([x, y]);

                        }
                    }
                },
                title: {
                    text: 'SVM Classification'
                },
                xAxis: {
                    minPadding: 0.2,
                    maxPadding: 0.2,
                    maxZoom: 60
                },
                yAxis: {
                    title: {
                        text: 'Value'
                    },
                    minPadding: 0.2,
                    maxPadding: 0.2,
                    maxZoom: 60,
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                legend: {
                    enabled: true
                },
                exporting: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        lineWidth: 0,
                        marker: {
                            radius: 5
                        },
                        point: {
                            events: {
                                'click': function (e) {
                                    var x = e.point.x;
                                    var y = e.point.y;
                                    var name = this.series.name;

                                    if (this.series.data.length > 1) {
                                        if (removePoint(x, y, name)) {
                                            this.remove();
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                series: [{
                    name: 'Class1',
                    data: [[40, 20]]
                }, {
                    name: 'Class2',
                    data: [[10, 20]]
                }]
            });


            $("#btnClass1").click(function () {

                $("#currentClass").val("Class1");

                $("#btnClass1").removeClass("btn-class1");
                $("#btnClass2").removeClass("btn-class2");
                $("#btnClass1").addClass("btn-class1");
            });

            $("#btnClass2").click(function () {
                
                $("#currentClass").val("Class2");

                $("#btnClass2").removeClass("btn-class2");
                $("#btnClass1").removeClass("btn-class1");
                $("#btnClass2").addClass("btn-class2");
            });

            $("#btnClasify").click(function () {

                $.post("Ajax.aspx?action=clasify", { }, function (data) {

                    if (data == "success") {
                        var series = {
                            type: 'line',
                            lineWidth: 1,
                            marker: {
                                radius: 0
                            },
                            id: 'series',
                            name: 'JSON Data',
                            data: [[50,50], [20,20]]
                        }
                        chart.addSeries(series)
                    }
                    else
                        alert("Класификацијата не е успешно направена!");
                });

            });
        });


        //add new point (x,y) in the graph
        function addPoint(x, y) 
        {
            var className = $("#currentClass").val();

            $.post("Ajax.aspx?action=addPoint", { x: x, y: y, className: className }, function (data) {

                if (data == "success") {

                    if (className == "Class1") {
                        series = chart.series[0];
                        series.addPoint([x, y]);
                    }

                    if (className == "Class2") {
                        series = chart.series[1];
                        series.addPoint([x, y]);
                    }
                }
                else
                    alert("Не е дозволено нелинеарно мешање на податоците!");
            });
        }

        //removes the point from a given class
        function removePoint(x, y, className) {

            $.post("Ajax.aspx?action=removePoint", { x: x, y: y, className: className }, function (data) {

                if (data == "success") {
                }
            });

            return true;
        }

    </script>
</head>
<body>

    <input type="hidden" id="currentClass" value="Class1"/>

    <div id="header"></div>
    <div id="navigation"></div>

    <div id="content">
        <div id="control">
            <div id="control_header"><h3>Избери Класа</h3></div>

            <div id="control_classes">
                <input type="button" class="btn btn-class1" id="btnClass1" value="Класа 1"/>
                <input type="button" class="btn " id="btnClass2" value="Класа 2"/>
            </div>

            <div id="control_clasify">
                <input type="button" class="btn btn-clasify" id="btnClasify" value="Класифицирај"/>
            </div>
        </div>
        <div id="main_content">

            <form id="form1" runat="server">

            <div>
                <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto;"></div>
            </div>

        </form>

        </div>
    </div>
   
    <div id="top_footer"></div>
</body>
</html>
