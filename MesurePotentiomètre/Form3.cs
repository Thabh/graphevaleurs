using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;

namespace MesurePotentiomètre
{
    public partial class Form3 : Form
    {
        bool end = false;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            mesuresBindingSource.DataSource = new List<Mesures>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Temps"
            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Valeur",
                LabelFormatter = value => value + ""
            }) ;
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Init data
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var annees = (from o in mesuresBindingSource.DataSource as List<Mesures>
                         select new { Annee = o.Annee }).Distinct();
            foreach (var annee in annees)
            {
                List<double> values = new List<double>();
                for(int mois = 1; mois <= 12; mois++)
                {
                    double value = 0;
                    var data = from o in mesuresBindingSource.DataSource as List<Mesures>
                               where o.Annee.Equals(annee.Annee)&&o.Temps.Equals(mois)
                               orderby o.Temps ascending
                               select new { o.Valeur, o.Temps };
                    if (data.SingleOrDefault() != null)
                        value = data.SingleOrDefault().Valeur;
                    values.Add(value);
                }
                series.Add(new LineSeries() { Title = annee.Annee.ToString(), Values = new ChartValues<double>(values)});
            }
            cartesianChart1.Series = series;
        }
    }
}