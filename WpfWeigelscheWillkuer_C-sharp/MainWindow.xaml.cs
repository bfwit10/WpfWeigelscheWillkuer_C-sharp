using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient; //Sehr wichtig!
using System.Text.RegularExpressions;


namespace WpfWeigelscheWillkuer_C_sharp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            string Connection = GlobalVariables.GlobalMySqlCon; //Für die Verbindung an unsere Datenbank
            public MainWindow()
            {
                InitializeComponent();
                fill_cb_UseDatabase(); //Auskommentiert weil die Methode auf dem Friehof (s.u.) gelandet ist...
            }
            private void Button_Click(object sender, RoutedEventArgs e)//zum schliessen der gesamten Form
            {
                System.Windows.Application.Current.Shutdown();
            }

            private void btnladen_Click(object sender, RoutedEventArgs e)
            {
                //lade button
            }

            private void BtnAbspeichern_Click(object sender, RoutedEventArgs e)
            {
                int pin, ebene, id = 0;

                string maxid = "use Fraktal; select max(id) from WWUser;";
                MySqlConnection conDataBase = new MySqlConnection(Connection); //Connection zur Datenbank
                MySqlCommand cmdDataBase = new MySqlCommand(maxid, conDataBase);
                MySqlDataReader dbReader;

                try //höchste id auslesen
                {
                    conDataBase.Open();
                    dbReader = cmdDataBase.ExecuteReader();
                    while (dbReader.Read())
                    {
                        string strid = dbReader.GetString("max(id)");
                        int.TryParse(strid, out id);
                    }
                    conDataBase.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                id = id + 1; //zum erhöhen des PK

                if (txtBenutzername.Text == "" || txtPin.Text == "" || txtEbene.Text == "") //Überprüfung, ob die Eingaben vom User nicht leer sind
                {
                    MessageBoxResult result = MessageBox.Show("Bitte geben Sie Ihren Benutzernamen, Pin und ihre lieblings Ebene an!", "Fehler", MessageBoxButton.OK);
                }
                else
                {

                    Match match = Regex.Match(txtBenutzername.Text, "[^a-zA-Z]",
                    RegexOptions.IgnoreCase);
                    if (!match.Success)
                    {
                        if (int.TryParse(txtPin.Text, out pin) || int.TryParse(txtPin.Text, out ebene)) //Überprüfung ob Pin und Ebene jeweils eine Zahl sind
                        {
                            try
                            {
                                string Query = "use Fraktal; insert into WWUser Values (" + id + ",'" + txtBenutzername.Text + "'," + txtPin.Text + "," + txtEbene.Text + ");"; //Befehl zum erzeugen eines neuen Benutzers
                                MySqlCommand cmdQuery = new MySqlCommand(Query, conDataBase);
                                conDataBase.Open();//Verbindung zur Datenbankverbindung öffnen
                                cmdQuery.ExecuteNonQuery(); //Verarbeiten des Befehles
                                conDataBase.Close();//Schliessen der Datenbankverbindung
                                MessageBoxResult result = MessageBox.Show("Ihr Profil wurde erfolgreich gespeichert", "Speichervorgang abgeschlossen", MessageBoxButton.OK);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("Ihr Pin bzw. Ihre Ebene besteht nicht nur aus Ziffern", "Speichern fehlgeschlagen", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Der Benutzername darf nur aus Buchstaben bestehen!", "Fehler!", MessageBoxButton.OK);
                    }
                }
            }

		private void BT_Fraktal3Dproto_Click(object sender, RoutedEventArgs e)
		{
			Fraktal3Dproto Fraktal3dPROTO = new Fraktal3Dproto()
			{
				Title = "Fraktal3dPROTO",
				Topmost = true,
				ResizeMode = ResizeMode.NoResize,
				ShowInTaskbar = false,
				Owner = this
			};
			Fraktal3dPROTO.ShowDialog();

			//Action!

			//App.Current.MainWindow.Hide();
		}

        //Ab hier beginnt die Berechnung des Fraktales
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            double basisObjekte;
            int layer = 0;
            double gesamtlänge;
            double länge;
            double x = 300;
            double tmp1 = 300;
            double tmp2 = 100;


            Double.TryParse(txtLänge.Text, out gesamtlänge);
            Int32.TryParse(txtEingabe.Text, out layer);

            basisObjekte = Math.Pow(2, layer);
            länge = gesamtlänge / basisObjekte; //Berechnung der Länge für die Seiten des Quadrates

            for (int i = 0; i < basisObjekte; i++)
            {


                for (int k = 0; k < basisObjekte; k++) //Schleife zum erstellen der Basis Objekte Horizontal
                {
                    Line Linie1 = new Line(); Line Linie2 = new Line(); Line Linie3 = new Line(); Line Linie4 = new Line();
                    Line Linie5 = new Line(); Line Linie6 = new Line(); Line Linie7 = new Line(); Line Linie8 = new Line();


                    //Farbcodes müssen noch integriert werden
                    Linie1.Stroke = System.Windows.Media.Brushes.Red;
                    Linie1.Fill = System.Windows.Media.Brushes.Red;
                    Linie2.Stroke = System.Windows.Media.Brushes.Red;
                    Linie2.Fill = System.Windows.Media.Brushes.Red;
                    Linie3.Stroke = System.Windows.Media.Brushes.Red;
                    Linie3.Fill = System.Windows.Media.Brushes.Red;
                    Linie4.Stroke = System.Windows.Media.Brushes.Red;
                    Linie4.Fill = System.Windows.Media.Brushes.Red;
                    Linie5.Stroke = System.Windows.Media.Brushes.Red;
                    Linie5.Fill = System.Windows.Media.Brushes.Red;
                    Linie6.Stroke = System.Windows.Media.Brushes.Red;
                    Linie6.Fill = System.Windows.Media.Brushes.Red;
                    Linie7.Stroke = System.Windows.Media.Brushes.Red;
                    Linie7.Fill = System.Windows.Media.Brushes.Red;
                    Linie8.Stroke = System.Windows.Media.Brushes.Red;
                    Linie8.Fill = System.Windows.Media.Brushes.Red;

                    Linie1.X1 = tmp1;
                    Linie1.Y1 = tmp2;

                    Linie1.X2 = Linie1.X1 + länge;
                    Linie1.Y2 = Linie1.Y1;//Linke Linie
                    Linie2.X1 = Linie1.X2;
                    Linie2.Y1 = Linie1.Y2;
                    Linie2.X2 = Linie2.X1;

                    Linie2.Y2 = Linie2.Y1 + länge;//Rechte Linie
                    Linie3.X1 = Linie2.X2;
                    Linie3.Y1 = Linie2.Y2;
                    Linie3.X2 = Linie3.X1 - länge;

                    Linie3.Y2 = Linie3.Y1; //Untere Linie
                    Linie4.X1 = Linie3.X2;
                    Linie4.Y1 = Linie3.Y2;
                    Linie4.X2 = Linie4.X1;

                    Linie4.Y2 = Linie4.Y1 - länge; //Linke Linie
                    Linie5.X1 = tmp1;
                    Linie5.Y1 = tmp2 + (länge / 2);
                    Linie5.X2 = Linie5.X1 + länge;

                    Linie5.Y2 = Linie5.Y1; //Horizontale Trennung
                    Linie6.X1 = tmp1 + (länge / 2);
                    Linie6.Y1 = Linie1.Y1;
                    Linie6.X2 = Linie6.X1;
                    Linie6.Y2 = tmp2 + länge; //Vertikale Trennung
                    Linie7.X1 = Linie1.X1;
                    Linie7.Y1 = Linie1.Y1;
                    Linie7.X2 = Linie2.X2;
                    Linie7.Y2 = Linie2.Y2; //Vertical von oben links nach unten rechts
                    Linie8.X1 = Linie2.X1;
                    Linie8.Y1 = Linie2.Y1;
                    Linie8.X2 = Linie4.X1;
                    Linie8.Y2 = Linie4.Y1; //Vertical oben rechts nach unten links

                    MainGrid.Children.Add(Linie1); MainGrid.Children.Add(Linie2); MainGrid.Children.Add(Linie3); MainGrid.Children.Add(Linie4);
                    MainGrid.Children.Add(Linie5); MainGrid.Children.Add(Linie6); MainGrid.Children.Add(Linie7); MainGrid.Children.Add(Linie8);

                    tmp1 = tmp1 + länge;

                }
                tmp1 = x;
                tmp2 = tmp2 + länge;
            }
        }




        //Friedhof
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
            private void BtnLaden_Click(object sender, RoutedEventArgs e) { }
            private void BtnSpeichern_Click(object sender, RoutedEventArgs e) { }
            private void btnErstellen_Click(object sender, RoutedEventArgs e) { }
            private void BtnLöschen_Click(object sender, RoutedEventArgs e) { }
            private void cb_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
            private void fill_cb_UseDatabase() { }


    }
}
