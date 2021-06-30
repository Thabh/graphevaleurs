using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.IO.Ports;

namespace MesurePotentiomètre
{
    public partial class Form1 : Form
    {
        List<double> xValues = new List<double>();
        List<double> yValues = new List<double>();
        string fichierDepose;
        int nbValeurs;
        int delay; //Ici vous pouvez recopier l'écart temporel en millisecondes entre deux valeurs de la mesure
        double nbMesuresParSecondes;
        public Form1()
        {
            InitializeComponent();

        }     

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //On récupère l'information du chemin d'accès au fichier
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            fichierDepose = files[0];
            this.label1.Text = "Le fichier déposé est " + fichierDepose;
            nbValeurs = File.ReadLines(fichierDepose).Count();
            /*delay = Convert.ToInt32(numericUpDown1.Value);
            nbMesuresParSecondes = 1000 / delay;*/

            //Définition du format et du style du graphique

            chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Red;
            chart1.ChartAreas[0].AxisY.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Red;
            chart1.Series["Mesures"].Color = Color.Blue;
            chart1.ChartAreas[0].AxisX.Title = "Temps de mesure";
            chart1.ChartAreas[0].AxisY.Title = "Valeur de la mesure";
            chart1.ChartAreas[0].AxisX.Interval = nbValeurs / 20;


            xValues.Clear();
            yValues.Clear();

            //On crée une instance d'un objet qui lit les données du fichier déposé
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader monStreamReader = new StreamReader(fichierDepose, encoding);

            //On lit les données du fichier et on les ajoute à la liste des données qui sera affichée par le graphique
            int col = 1;
            string ligne = monStreamReader.ReadLine();
            while (ligne != null)
            {
                int value = int.Parse(ligne);
                double temps = col * 20;
                xValues.Add(temps);
                yValues.Add(value);
                ligne = monStreamReader.ReadLine();
                col++;
                MessageBox.Show("Tests");
            }

            monStreamReader.Close();

            //Tracé du grahique
            chart1.Series["Mesures"].Points.DataBindXY(xValues, yValues);
            chart1.Invalidate();

        }

        //Cette fonction permet de ne garder que les chiffres de la sortie Arduino dans le cas où des caractères spéciaux se seraient glissés
        static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series["Mesures"].Points.DataBindXY(xValues, yValues);
        }
    }
}
