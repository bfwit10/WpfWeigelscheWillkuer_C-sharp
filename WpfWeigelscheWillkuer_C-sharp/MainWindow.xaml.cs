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
using System.Reflection;
using System.Media;
using System.IO;


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

			//Action!

			//App.Current.MainWindow.Hide();
		}

        public Brush PickBrush() //Zufllige Farbe für die Linie
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

		public void playRandomVivaldi(bool toggleOff)
		{
			SoundPlayer simpleSound = new SoundPlayer();
			if (!toggleOff)
			{
				string Zzahl;
				string[] toVivaldi1 = { Environment.CurrentDirectory, @"vivaldi\vivaldi1.wav", @"vivaldi\vivaldi2.wav", @"vivaldi\vivaldi3.wav" };

				string vivaldi1 = System.IO.Path.Combine(toVivaldi1[0], toVivaldi1[1]);
				string vivaldi2 = System.IO.Path.Combine(toVivaldi1[0], toVivaldi1[2]);
				string vivaldi3 = System.IO.Path.Combine(toVivaldi1[0], toVivaldi1[3]);
				Random zufall = new Random();

				Zzahl = Convert.ToString(zufall.Next(1, 4));

				if (Zzahl == "1" || Zzahl == "4")
				{
					simpleSound = new SoundPlayer(vivaldi1);
					simpleSound.Play();
				}
				else if (Zzahl == "2")
				{
					simpleSound = new SoundPlayer(vivaldi2);
					simpleSound.Play();
				}
				else if (Zzahl =="3")
				{
					simpleSound = new SoundPlayer(vivaldi3);
					simpleSound.Play();
				}
			}
			else simpleSound.Stop();
			
		}

		public void stopVivaldi()
		{
			bool toggleOff = true;
			playRandomVivaldi(toggleOff);
		}

        //Ab hier beginnt die Berechnung und Darstellung des Fraktales
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            playRandomVivaldi(false);
            drawLines(true);
        }

		Line Linie1, Linie2, Linie3, Linie4, Linie5, Linie6, Linie7, Linie8;
		
		public void drawLines(bool draw)
		{
				double basisObjekte;
				int layer = 0;
				double gesamtlänge;
				double länge;
				double x = 300; //X-Wert für den Beginn
				double tmp1 = 300; //X-Wert für den Beginn
				double tmp2 = 100; //Y-Wert für den Beginn

				Double.TryParse(txtLänge.Text, out gesamtlänge);
				Int32.TryParse(txtEingabe.Text, out layer);
			
				//Linie 1 und 4 werden insgesamt nur einmal gezeichnet
				Linie1 = new Line(); Linie4 = new Line();
				Linie1.Stroke = PickBrush();
				Linie1.Fill = PickBrush();
				Linie4.Stroke = PickBrush();
				Linie4.Fill = PickBrush();
				Linie1.X1 = tmp1;//Obere Linie
				Linie1.Y1 = tmp2;
				Linie1.X2 = Linie1.X1 + gesamtlänge;
				Linie1.Y2 = Linie1.Y1;
				Linie4.X1 = Linie1.X1;//Linke Linie
				Linie4.Y1 = Linie1.Y1;
				Linie4.X2 = Linie4.X1;
				Linie4.Y2 = Linie4.Y1 + gesamtlänge;
				MainGrid.Children.Add(Linie1);
				MainGrid.Children.Add(Linie4);

				basisObjekte = Math.Pow(2, layer); //Berechnung der Basis Objekte in einer Reihe sowie Spalte
				länge = gesamtlänge / basisObjekte; //Berechnung der Länge für die Seiten des Basis Objektes

				for (int i = 0; i < basisObjekte; i++)//Schreibmaschinen Methode. Wenn die Horizontale fertig ist geht es einen schritt in die vertikale
				{

					for (int k = 0; k < basisObjekte; k++) //Schleife zum erstellen der Basis Objekte Horizontal
					{
						Linie2 = new Line();	Linie3 = new Line();
						Linie5 = new Line();	Linie6 = new Line(); 
						Linie7 = new Line();	Linie8 = new Line();

						Linie2.Stroke = PickBrush();
						Linie2.Fill = PickBrush();
						Linie3.Stroke = PickBrush();
						Linie3.Fill = PickBrush();
						Linie5.Stroke = PickBrush();
						Linie5.Fill = PickBrush();
						Linie6.Stroke = PickBrush();
						Linie6.Fill = PickBrush();
						Linie7.Stroke = PickBrush();
						Linie7.Fill = PickBrush();
						Linie8.Stroke = PickBrush();
						Linie8.Fill = PickBrush();

						Linie2.X1 = tmp1 + länge;//Rechte Linie
						Linie2.Y1 = tmp2;
						Linie2.X2 = Linie2.X1;
						Linie2.Y2 = Linie2.Y1 + länge;

						Linie3.X1 = Linie2.X2;//Untere Linie
						Linie3.Y1 = Linie2.Y2;
						Linie3.X2 = Linie3.X1 - länge;
						Linie3.Y2 = Linie3.Y1;

						Linie5.X1 = tmp1;//Horizontale Trennung
						Linie5.Y1 = tmp2 + (länge / 2);
						Linie5.X2 = Linie5.X1 + länge;
						Linie5.Y2 = Linie5.Y1;

						Linie6.X1 = tmp1 + (länge / 2);//Vertikale Trennung
						Linie6.Y1 = Linie1.Y1;
						Linie6.X2 = Linie6.X1;
						Linie6.Y2 = tmp2 + länge;

						Linie7.X1 = Linie2.X2;//Diagonale von oben links nach unten rechts
						Linie7.Y1 = Linie2.Y2;
						Linie7.X2 = Linie3.X2;
						Linie7.Y2 = Linie3.Y2 - länge;

						Linie8.X1 = Linie3.X2;//Diagonale oben rechts nach unten links
						Linie8.Y1 = Linie3.Y2;
						Linie8.X2 = Linie2.X1;
						Linie8.Y2 = Linie2.Y1;

						MainGrid.Children.Add(Linie2);
						MainGrid.Children.Add(Linie3);
						MainGrid.Children.Add(Linie5);
						MainGrid.Children.Add(Linie6);
						MainGrid.Children.Add(Linie7);
						MainGrid.Children.Add(Linie8);

						tmp1 = tmp1 + länge;//Berechnung für das nächste Basis Objekt in der Horizontalen Bewegung (X-Achse)
						
					}

					tmp1 = x;//zurücksetzen auf den Anfangs X Wert wo gezeichnet wird
					tmp2 = tmp2 + länge;//Berechnung für die nächste Zeile zum zeichnen (Y-Achse)

				}
				
		}

        // Kreissal (und hier schließen sich zwei Kgreise... oo ... Gute N8!) 
       private void BtnclearView_Click(object sender, RoutedEventArgs e)
       {
			removeLines();
			this.txtEingabe.Text = string.Empty;
			this.txtLänge.Text = string.Empty;
			stopVivaldi();
       }

		public void removeLines()
		{
			MainGrid.Children.Remove(Linie8);
			MainGrid.Children.Remove(Linie7);
			MainGrid.Children.Remove(Linie2);
			MainGrid.Children.Remove(Linie6);
			MainGrid.Children.Remove(Linie5);
			MainGrid.Children.Remove(Linie3);
			MainGrid.Children.Remove(Linie4);
			MainGrid.Children.Remove(Linie1);
			
		}	
			
		

        //Friedhof (lieber Herr Gertz, Standardkonstruktoren an diesen Ort zu verbannen tztztzt... )
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
            private void BtnLaden_Click(object sender, RoutedEventArgs e) { }
            private void BtnSpeichern_Click(object sender, RoutedEventArgs e) { }
            private void btnErstellen_Click(object sender, RoutedEventArgs e) { }
            private void BtnLöschen_Click(object sender, RoutedEventArgs e) { }
            private void cb_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
            private void fill_cb_UseDatabase() { }
	}
}
