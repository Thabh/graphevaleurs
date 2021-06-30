using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.IO.Ports;

namespace MesurePotentiomètre
{
    public partial class Form3 : Form
    {
        List<double> xValues = new List<double>();
        List<double> yValues = new List<double>();
        //Ici, vous pouvez entrer le nom du fichier dans lequel vous souhaitez que vos valeurs soient stockées
        string fichierValeurs = "valeurs.txt";
        int enrValeurs;
        double nbValeurs;
        SerialPort COM;
        int delay = 50; //Ici vous pouvez recopier la valeur de délai que vous avez introduit dans votre code Arduino
        double nbMesuresParSecondes; //Cette variable nous permet de garder une trace de l'instant auquel la mesure a été faite

        public Form3()
        {
            InitializeComponent();

            //On crée l'instance du port USB et on définit sa vitesse de transfert
            COM = new SerialPort("COM5", 9600);
            nbMesuresParSecondes = 1000 / delay;
        }

        private void genValeurs_Click(object sender, EventArgs e)
        {

            enrValeurs = 0;
            //Le calcul ici dépend du temps entre deux valeurs dans votre programme Arduino
            nbValeurs = Convert.ToInt32(numericUpDown1.Value) * nbMesuresParSecondes;
            try
            {
                // Vérifiez si le fichier existe déjà. Si oui, supprimez-le. Si vous souhaitez conserver un fichier de mesure, pensez à changer son nom ou à le déplacer  
                if (File.Exists(fichierValeurs))
                {
                    File.Delete(fichierValeurs);
                }

                // Créer un nouveau fichier   
                StreamWriter monStreamWriter = File.CreateText(fichierValeurs);
                
                //Ouverture du port USB
                COM.Open();

                // Ecrire dans le fichier
                while (enrValeurs < nbValeurs+1)
                {
                    string oneLine = COM.ReadLine();
                    oneLine = RemoveSpecialCharacters(oneLine);
                    if (oneLine != null)
                    {
                        monStreamWriter.WriteLine(oneLine);
                    }
                    enrValeurs++;
                    Thread.Sleep(delay);
                }

                // Fermeture du StreamWriter et du port USB (attention très important) 
                monStreamWriter.Close();
                COM.Close();
                MessageBox.Show("Fin de l'enregistrement");
            }
            catch (Exception ex)
            {
                Console.Write("Une erreur est survenue au cours de l'opération :");
                Console.WriteLine(ex.ToString());
            }
            
            //Définition du format et du style du graphique

            chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Red;
            chart1.ChartAreas[0].AxisY.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Red;
            chart1.Series["Mesures"].Color = Color.Blue;
            chart1.ChartAreas[0].AxisX.Title = "Temps de mesure";
            chart1.ChartAreas[0].AxisY.Title = "Valeur de la mesure";
            chart1.ChartAreas[0].AxisX.Interval = nbValeurs / Convert.ToInt32(numericUpDown1.Value);

            xValues.Clear();
            yValues.Clear();

            //On crée une instance d'un objet qui lit les données du fichier déposé
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader monStreamReader = new StreamReader(fichierValeurs, encoding);

            //On lit les données du fichier et on les ajoute à la liste des données qui sera affichée par le graphique
            int col = 0;
            string ligne = monStreamReader.ReadLine();
            for (int i = 1; i < enrValeurs; i++)
            {
                ligne = monStreamReader.ReadLine();
                int value = int.Parse(ligne);
                double temps = col * delay;
                xValues.Add(temps);
                yValues.Add(value);
                col++;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists("C:\\Sauvegardes"))
            {
                System.IO.Directory.CreateDirectory("C:\\Sauvegardes");
            }
            string nomFichier = "C:\\Sauvegardes\\" + DateTime.Now.ToString("dd_MM_yy") + "_" + DateTime.Now.ToString("hh_mm_ss") + ".txt";
            StreamWriter monStreamWriter = File.CreateText(nomFichier);
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader monStreamReader = new StreamReader(fichierValeurs, encoding);
            int copie = File.ReadLines(fichierValeurs).Count();

            for (int i = 0; i < copie; i++)
            {
                string oneLine = monStreamReader.ReadLine();
                if (oneLine != null)
                {
                    monStreamWriter.WriteLine(oneLine);
                }
            }
            monStreamReader.Close();
            monStreamWriter.Close();
            MessageBox.Show("Série sauvegardée");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            chart1.Series["Mesures"].Points.DataBindXY(xValues, yValues);
        }
    }
}
