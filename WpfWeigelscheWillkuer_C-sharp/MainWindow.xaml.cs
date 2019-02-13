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
                fill_cb_UseDatabase();
				
            }

			private void btnExit_Click(object sender, RoutedEventArgs e) //zum schliessen der gesamten Form
			{
				System.Windows.Application.Current.Shutdown();
			}

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                // Configure message box
				string message = "Bitte diesen Button unter Eigenschaften benennen!";
				// Show message box
				MessageBoxResult result = MessageBox.Show(message);
            }

            private void btnladen_Click(object sender, RoutedEventArgs e)
            {
                //lade button
            }

            private void btnAbspeichern_Click(object sender, RoutedEventArgs e)
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
		/*
		 *	Hier folgt die Methode die durch das Klicken des Buttons hervorgezaubert wird.
		 *	An hier können für jeden weiteren Button oder auch andere Events die eine Änderung
		 *	der Darstellung zur Folge haben oder das s.u. aufrufen eines neuen Fensters
		 *	durchführen, eingegefügt werden (bzw. da VS unten weiter automatisch einfügt -
		 *	hin verschben werden. Im weiteren Verlauf des Projekts dürfte diese 
		 *	Funktioinalität in eine andere Klasse im selben Namespace verschoben werden. Nicht
		 *	zuletzt um den nach wie vor angestrebten M)
		 */
		private void btnFraktal3Dproto_Click(object sender, RoutedEventArgs e)
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

			//hier die View mit dem Würfel!

			//App.Current.MainWindow.Hide();
		}

        //Ab hier beginnt die Berechnung des Fraktales
        private void btnStart_Click(object sender, RoutedEventArgs e)
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
			
			Line[] lineArray = new Line[Convert.ToInt32(layer +1 )];
			for (int k = 0; k <= Convert.ToInt32(layer) ; k++)
			
			{
				lineArray[k] = new Line();
			}
			


			foreach (Line _linie in lineArray)
			{
				MainGrid.Children.Add(_linie);
			}		
			

				for (int i = 0; i < basisObjekte; i++)
				{
					for (int j = 0; j < basisObjekte; j++) //Schleife zum erstellen der Basis Objekte Horizontal
					{
						{
						/* TODO: Farbcodes müssen noch integriert werden 
						 * 
						 * hier wird der Code im ersten Schritt etwas knapper und 
						 * portabler die Schleife(n) dazu waren ja schon da... ;-)
						 * Die Zahlen der Linien waren zuvor bei eins beginnend!
						 * #NO BRAINFUCK!
						 * 
						 * @author: AGAB
						 */
						 }
						
							{
								lineArray[j].Stroke = Brushes.Red;
								lineArray[j].Fill = Brushes.Red;
							}
					
						{
							lineArray[0].X1 = tmp1;	//Initiale linie				
							lineArray[0].Y1 = tmp2;
						}
						tmp1 += länge; 
					}
					tmp1 = x;		
					tmp2 += länge;	
				}
				



					{
					lineArray[0].X2 = lineArray[0].X1 + länge;
					lineArray[0].Y2 = lineArray[0].Y1;          //Linke Linie
					lineArray[1].X1 = lineArray[0].X2;
					lineArray[1].Y1 = lineArray[0].Y2;
					lineArray[1].X2 = lineArray[1].X1;
					}

					{
						lineArray[1].Y2 = lineArray[1].Y1 + länge;  //Rechte Linie
						lineArray[2].X1 = lineArray[1].X2;
						lineArray[2].Y1 = lineArray[1].Y2;
						lineArray[2].X2 = lineArray[2].X1 - länge;
					}

					{ 
						lineArray[2].Y2 = lineArray[2].Y1;          //Untere Linie
						lineArray[3].X1 = lineArray[2].X2;
						lineArray[3].Y1 = lineArray[2].Y2;
						lineArray[3].X2 = lineArray[3].X1;
					}
					
					
					{
						lineArray[2].Y2 = lineArray[3].Y1 - länge; //Linke Linie
						lineArray[3].X1 = tmp1;
						lineArray[3].Y1 = tmp2 + (länge / 2);
						lineArray[3].X2 = lineArray[4].X1 + länge;
					}

					{ 
						lineArray[4].Y2 = lineArray[4].Y1;          //Horizontale Trennung
						lineArray[5].X1 = tmp1 + (länge / 2);
						lineArray[5].Y1 = lineArray[0].Y1;
						lineArray[5].X2 = lineArray[5].X1;
						lineArray[5].Y2 = tmp2 + länge;     //Vertikale Trennung
						lineArray[6].X1 = lineArray[0].X1;
						lineArray[6].Y1 = lineArray[0].Y1;
						lineArray[6].X2 = lineArray[1].X2;
						lineArray[6].Y2 = lineArray[1].Y2;          //Vertical von oben links nach unten rechts
						lineArray[7].X1 = lineArray[1].X1;
						lineArray[7].Y1 = lineArray[1].Y1;
						lineArray[7].X2 = lineArray[3].X1;
						lineArray[7].Y2 = lineArray[3].Y1;          //Vertical oben rechts nach unten links
					}

        }

		public static void InitializeValues()
		{
			//Nothing to initialize yet!
		}


        //Friedhof (lieber Herr Gertz, Standardkonstruktoren an diesen Ort zu verbannen tztztzt... )
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
            private void BtnLaden_Click(object sender, RoutedEventArgs e) { }
            private void BtnSpeichern_Click(object sender, RoutedEventArgs e) { }
            private void btnErstellen_Click(object sender, RoutedEventArgs e) { }
            private void BtnLöschen_Click(object sender, RoutedEventArgs e) { }
            private void cb_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
            private void fill_cb_UseDatabase() { }
		
		// Kreissal (und hier schließen sich zwei Kgreise... oo ... Gute N8!)

		private void BtnclearView_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Anzeigen leeren, Zeichenebene leeren, Objektreferenzen aufheben,
			//Garbage collector... und Zeicheneben leeren...

			MainWindow.InitializeValues();
		}
	}
}
