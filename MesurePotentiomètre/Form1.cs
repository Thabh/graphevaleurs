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
        string destinationFichier;
        string fichierValeurs = "C:\\Users\\Hugo\\Documents\\Cours\\Stage\\valeurs.txt";
        int enrValeurs;
        int nbValeurs;
        SerialPort COM;
        bool depotFichier = false;

        public Form1()
        {
            InitializeComponent();
            
            COM = new SerialPort("COM5", 9600);
        }

        private void genValeurs_Click(object sender, EventArgs e)
        {
            depotFichier = false;
            enrValeurs = 0;
            nbValeurs = Convert.ToInt32(numericUpDown1.Value) * 20;
            try
            {
                // Vérifiez si le fichier existe déjà. Si oui, supprimez-le.    
                if (File.Exists(fichierValeurs))
                {
                    File.Delete(fichierValeurs);
                }

                // Créer un nouveau fichier   
                StreamWriter monStreamWriter = File.CreateText(fichierValeurs);
                
                COM.Open();

                // Ecrire dans le fichier 
                while (enrValeurs < nbValeurs)
                {
                    COM.Open();
                    string oneLine = COM.ReadLine();
                    oneLine = RemoveSpecialCharacters(oneLine);
                    if (oneLine != null)
                    {
                        monStreamWriter.WriteLine(oneLine);
                    }
                    enrValeurs++;
                    COM.Close();
                }

                // Fermeture du StreamWriter (attention très important) 
                monStreamWriter.Close();
                MessageBox.Show("Fin de l'enregistrement");
            }
            catch (Exception ex)
            {
                Console.Write("Une erreur est survenue au cours de l'opération :");
                Console.WriteLine(ex.ToString());
            }
        }

        private void traceGraph_Click(object sender, EventArgs e)
        {
            if(!depotFichier)
            {
                destinationFichier = fichierValeurs;
            }
            else
            {
                destinationFichier = fichierDepose;
            }

            //Définition du format du graphique
            chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Red;
            chart1.ChartAreas[0].AxisY.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Red;
            chart1.Series["Mesures"].Color = Color.Blue;
            chart1.ChartAreas[0].AxisX.Title = "Temps de mesure";
            chart1.ChartAreas[0].AxisY.Title = "Valeur de la mesure";
            chart1.ChartAreas[0].AxisX.Interval = nbValeurs/20 ;

            xValues.Clear();
            yValues.Clear();

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader monStreamReader = new StreamReader(destinationFichier, encoding);

            int col = 1;
            string ligne = monStreamReader.ReadLine();
            while (ligne != null)
            {
                int value = int.Parse(ligne);
                if (value > 1023) value = 1023;
                double temps = col * 20;
                xValues.Add(temps);
                yValues.Add(value);
                ligne = monStreamReader.ReadLine();
                col++;
            }

            monStreamReader.Close();

            //Tracé du grahique
            chart1.Series["Mesures"].Points.DataBindXY(xValues, yValues);
            chart1.Invalidate();
            
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            fichierDepose = files[0];
            depotFichier = true;

        }
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
    }
}
