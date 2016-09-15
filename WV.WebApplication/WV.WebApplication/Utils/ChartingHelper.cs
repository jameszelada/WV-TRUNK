using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.DataVisualization.Charting;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace WV.WebApplication.Utils
{
    public class ChartingHelper
    {
        private Dictionary<string, object> _Data;
        private Dictionary<string, object> _ChartParameters;
        
        public Dictionary<string, object> ChartParameters
        {
            get { return _ChartParameters; }
            set { _ChartParameters = value; }
        }
        public Dictionary<string, object> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public const String CHARTS_THEME = @"<Chart BackColor=""#FFFFFF"" BackGradientStyle=""TopBottom"" BorderColor=""#A0A0A0"" Palette=""None"" PaletteCustomColors=""#376894"" >
        <ChartAreas>
        <ChartArea Name=""Default"" _Template_=""All"" BackColor=""Transparent"" BackSecondaryColor=""White"" BorderWidth=""1"" BorderColor=""#A0A0A0"" BorderDashStyle=""Solid"" >
        <AxisY>
        <MajorGrid Interval=""Auto"" LineColor=""64, 64, 64, 64"" />    
        <LabelStyle Font=""Verdana, 100pt"" />
        </AxisY>
        <AxisX LineColor=""#000000"">
        <MajorGrid Interval=""Auto"" LineColor=""64, 64, 64, 64"" />
        <LabelStyle Font=""Verdana, 100pt"" />
        </AxisX>
        </ChartArea>
        </ChartAreas>
        <Legends>
        <Legend _Template_=""All"" BackColor=""Transparent"" Docking=""Bottom"" Font=""Verdana, 100pt, style=Plain"" LegendStyle=""Row"">
        </Legend>
            </Legends>                          
        </Chart>";

        public byte[] GetChart()
        {
            int Width = 0, Height = 0;
            string chartTitle = "", xTitle = "", yTitle = "",chartType="Column" ;
            List<string> xValues = new List<string>();
            List<string> yValues= new List<string>();

            if (_ChartParameters.ContainsKey("width"))
            {
                Width = Convert.ToInt32(_ChartParameters["width"]);
            }
            if (_ChartParameters.ContainsKey("height"))
            {
                Height = Convert.ToInt32(_ChartParameters["height"]);
            }
            if (_ChartParameters.ContainsKey("chartTitle"))
            {
                chartTitle = Convert.ToString(_ChartParameters["chartTitle"]);
            }
            if (_ChartParameters.ContainsKey("xTitle"))
            {
                xTitle = Convert.ToString(_ChartParameters["xTitle"]);
            }
            if (_ChartParameters.ContainsKey("yTitle"))
            {
                yTitle = Convert.ToString(_ChartParameters["yTitle"]);
            }
            if (_ChartParameters.ContainsKey("chartType"))
            {
                chartType = Convert.ToString(_ChartParameters["chartType"]);
            }


            foreach (KeyValuePair<string, object> data in _Data)
            {
                xValues.Add(data.Key);
                yValues.Add(data.Value.ToString());
               
            }

            
            var myChart = new System.Web.Helpers.Chart(width: Width, height: Height,theme:CHARTS_THEME)
                .SetXAxis(title:xTitle)
                .SetYAxis(title:yTitle)
                
            .AddTitle(chartTitle)
            .AddSeries(
            chartType:chartType,
            xValue: xValues,xField:xTitle,
            yValues: yValues,yFields: yTitle
                
                );

            return myChart.GetBytes();
            
        }

        public List<int> GetAgeRanges(string programType)
        {
            List<int> ranges = new List<int>();

            switch (programType.ToLower())
            {
                case "cbsn":

                    ranges.Add(3);
                    ranges.Add(2);
                    ranges.Add(1);
                    break;
                case "primera infancia":
                    ranges.Add(3);
                    ranges.Add(2);
                    ranges.Add(1);

                    break;
                case "cdic":
                    ranges.Add(6);
                    ranges.Add(4);
                    ranges.Add(3);
                    break;
                case "cic":
                    ranges.Add(12);
                    ranges.Add(10);
                    ranges.Add(7);
                    break;
                case "caj":
                    ranges.Add(20);
                    ranges.Add(15);
                    ranges.Add(13);
                    break;
                case "other":
                    ranges.Add(15);
                    ranges.Add(5);
                    ranges.Add(3);
                    break;
                default:
                    break;
            }
            return ranges;
        
        }
    }
}