<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LJTZS.aspx.cs" Inherits="LJTZS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html, body{margin:0px;height:100%;padding:0px;}
    </style>
    <script type="text/javascript" src="js/FusionCharts/JSClass/FusionCharts.js"></script>
    <script type="text/javascript">

        function ShowLJTPi() {
            if (LJTPiXml) {
                var chart = new FusionCharts("js/FusionCharts/Charts/Line.swf", "LJTPi", "100%", "100%", "0", "0");
                chart.setDataXML(LJTPiXml);
                chart.setTransparent(true);
                chart.render("LJTPi");
            }
        }
    </script>
</head>
<body>
    <div id="LJTPi" style="width:100%;height:100%"></div>
    <script type="text/javascript" src="http://www.czpi.cz.jsinfo.net/CZLJT/pjclz_pi.aspx"></script>
</body>
</html>
