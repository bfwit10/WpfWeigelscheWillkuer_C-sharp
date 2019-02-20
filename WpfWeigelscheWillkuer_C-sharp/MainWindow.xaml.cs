using MySql.Data.MySqlClient; //Sehr wichtig!
using System;
using System.Media;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

// GeometryDrawing &&
// https://stackoverflow.com/questions/7366574/how-to-draw-graphics-as-efficiently-as-possible-in-wpf?rq=1
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawinggroup

namespace WpfWeigelscheWillkuer_C_sharp
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string Connection = GlobalVariables.GlobalMySqlCon; //Für die Verbindung an unsere Datenbank

		public MainWindow()
		{
			InitializeComponent();
			Fill_cb_UseDatabase(); //Auskommentiert weil die Methode auf dem Friehof (s.u.) gelandet ist...
		}

		private void _btnExit_Click(object sender, RoutedEventArgs e) //zum schliessen der gesamten Form
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

		private void _btnladen_Click(object sender, RoutedEventArgs e)
		{
			//lade button
			// Configure message box
			string message = "Diese Funktion ist noch nicht implementiert!";
			// Show message box
			MessageBoxResult result = MessageBox.Show(message);
		}

		private void _btnAbspeichern_Click(object sender, RoutedEventArgs e)
		{
			int id = 0;

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
					if (int.TryParse(txtPin.Text, out int pin) || int.TryParse(txtPin.Text, out int ebene)) //Überprüfung ob Pin und Ebene jeweils eine Zahl sind
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

		private void _btnFraktal3Dproto_Click(object sender, RoutedEventArgs e)
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

		//	Pastel Random Color
		//	Pastel: 87CEEB,32CD32,BA55D3,F08080,4682B4,9ACD32,40E0D0,FF69B4,
		//	F0E68C,D2B48C,8FBC8B,6495ED,DDA0DD,5F9EA0,FFDAB9,FFA07A

		public Brush RandomPastelBrush()
		{
			Brush[] pastels = new Brush[16]
			{
				Brushes.SkyBlue,
				Brushes.LimeGreen,
				Brushes.MediumOrchid,
				Brushes.LightCoral,
				Brushes.SteelBlue,
				Brushes.YellowGreen,
				Brushes.Turquoise,
				Brushes.HotPink,
				Brushes.Khaki,
				Brushes.Tan,
				Brushes.DarkSeaGreen,
				Brushes.CornflowerBlue,
				Brushes.Plum,
				Brushes.CadetBlue,
				Brushes.PeachPuff,
				Brushes.LightSalmon
			};
			Random random = new Random();
			Brush brush = pastels[random.Next(pastels.Length)];
			return brush;
		}

		//	zarte Pastel Random Color
		public Brush ZarterPastelBrush()
		{
			return new SolidColorBrush(_zartePasteldefinieren()); //geiler scheiß!
		}

		//	BrightPastel: 418CF0,FCB441,DF3A02,056492,BFBFBF,1A3B69,FFE382,
		//	129CDD,CA6B4B,005CDB,F3D288,506381,F1B9A8,E0830A,7893BE
		private Color _zartePasteldefinieren()
		{
			Color[] zartePastelfarben = new Color[15]
			{
				Color.FromRgb(41,140,240),
				Color.FromRgb(252,180,41),
				Color.FromRgb(223,58,2),
				Color.FromRgb(05,64,92),
				Color.FromRgb(191,191,191),
				Color.FromRgb(26,59,69),
				Color.FromRgb(255,227,82),
				Color.FromRgb(12,156,221),
				Color.FromRgb(202,107,75),
				Color.FromRgb(0,92,219),
				Color.FromRgb(243,210,88),
				Color.FromRgb(50,63,81),
				Color.FromRgb(241,185,168),
				Color.FromRgb(224,83,10), //warum ist 14 * 16 < 15 * 15 und die differenz 16...
				Color.FromRgb(78,93,190)
			};
			Random random = new Random();
			Color zartesPastel = zartePastelfarben[random.Next(zartePastelfarben.Length)];
			return zartesPastel;
		}

		//public Brush PickBrush() //Zufällige Farbe für die Linie
		//{
		//	Brush result = Brushes.Transparent;

		//	Random rnd = new Random();

		//	Type brushesType = typeof(Brushes);

		//	PropertyInfo[] properties = brushesType.GetProperties();

		//	int random = rnd.Next(properties.Length);
		//	result = (Brush)properties[random].GetValue(null, null);

		//	return result;
		//}

		//Methode die den Soundplayer zum Abspielen der vivaldi.wav auffordert
		public void PlayRandomVivaldi(bool toggleOff)
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
				else if (Zzahl == "3")
				{
					simpleSound = new SoundPlayer(vivaldi3);
					simpleSound.Play();
				}
			}
			else simpleSound.Stop();
		}

		//Methode die das Abspielen der Musik anhält.
		public void StopVivaldi()
		{
			bool toggleOff = true;
			PlayRandomVivaldi(toggleOff);
		}

		//Ab hier beginnt die Berechnung und Darstellung des Fraktales
		private void _btnStart_Click(object sender, RoutedEventArgs e)
		{
			//playRandomVivaldi(false);
			// Aufruf der Zeichnen Methode!
			DrawLines(true);
		}

		private Line Linie1, Linie2, Linie3, Linie4, Linie5, Linie6, Linie7, Linie8;

        //	TODO: Hier ein mal Zeichen und dann Transformieren damit anzahl der
        //	Operationen reduzieren & ggf. Objekte freigeben

        double basisObjekte;
        int layer = 0;
        double gesamtlänge;
        double länge;
        

        //Methode zum zeichnen der Linien
        public void DrawLines(bool draw)
		{
            double x = 300; //X-Wert für den Beginn
            double tmp1 = 300; //X-Wert für den Beginn
            double tmp2 = 100; //Y-Wert für den Beginn

            Double.TryParse(txtLänge.Text, out gesamtlänge);
			Int32.TryParse(txtEingabe.Text, out layer);

			//Linie 1 und 4 werden insgesamt nur einmal gezeichnet
			Linie1 = new Line(); Linie4 = new Line();
			Linie1.Stroke = ZarterPastelBrush();
			Linie1.Fill = ZarterPastelBrush();
			Linie4.Stroke = ZarterPastelBrush();
			Linie4.Fill = ZarterPastelBrush();
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

			for (int i = 0; i < basisObjekte; i++)//Schreibmaschinen Methode.
												  //Wenn die Horizontale fertig ist geht es einen schritt in die vertikale
			{
				for (int k = 0; k < basisObjekte; k++) //Schleife zum erstellen der Basis Objekte Horizontal
				{
					Linie2 = new Line(); Linie3 = new Line();
					Linie5 = new Line(); Linie6 = new Line();
					Linie7 = new Line(); Linie8 = new Line();

					Linie2.Stroke = ZarterPastelBrush();
					Linie3.Stroke = ZarterPastelBrush();
					Linie5.Stroke = ZarterPastelBrush();
					Linie6.Stroke = ZarterPastelBrush();
					Linie7.Stroke = ZarterPastelBrush();
					Linie8.Stroke = ZarterPastelBrush();
					Linie2.Fill = ZarterPastelBrush();
					Linie3.Fill = ZarterPastelBrush();
					Linie5.Fill = ZarterPastelBrush();
					Linie6.Fill = ZarterPastelBrush();
					Linie7.Fill = ZarterPastelBrush();
					Linie8.Fill = ZarterPastelBrush();

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
			Linie1 = null;
			Linie2 = null;
			Linie3 = null;
			Linie4 = null;
			Linie5 = null;
			Linie6 = null;
			Linie7 = null;
			Linie8 = null;
		}

		// TODO: ´hier noch eine Variante zum leeren vor dem Neu zeichnen... (ohne Textboxen zu leeren...)
		// & gc() 
		//

		// Event zum leeren der Anzeige, wenn der Clear Button gedrückt wurde
		private void BtnclearView_Click(object sender, RoutedEventArgs e)
		{
			//removeLines(); // Remove Lines entfernt (momentan) nur das zuletzt gezeichnete Objekt!
			//this.txtEingabe.Text = string.Empty;
			//this.txtLänge.Text = string.Empty;
			//stopVivaldi();

			//überzeichnen der Rechtecke (doof)
			Rectangle theRect = new Rectangle
			{

				Height = gesamtlänge * 1.2,
				Width = gesamtlänge * 1.2,
				Fill = new SolidColorBrush(Colors.White)
			};

			PanelCollectionContent();
			//MainGrid.Children.Add(theRect);
			GC.Collect();
		}
		//// Methode zum Löschen der Gezeichneten Objekte (löscht bisher nur das letzte Objekt)
		//public void removeLines()
		//{
		//	MainGrid.Children.Remove(Linie8);
		//	MainGrid.Children.Remove(Linie7);
		//	MainGrid.Children.Remove(Linie2);
		//	MainGrid.Children.Remove(Linie6);
		//	MainGrid.Children.Remove(Linie5);
		//	MainGrid.Children.Remove(Linie3);
		//	MainGrid.Children.Remove(Linie4);
		//	MainGrid.Children.Remove(Linie1);
		//}

		// Metode zum Debuggen der panel collection.
		public void PanelCollectionContent()
		{
			// MainGrid.Children.CopyTo();
			var Counter = MainGrid.Children.Count;
			
			MainGrid.Children.RemoveRange(15,Counter-15);
			var Counter2 = MainGrid.Children.Count;
			var Counter3 = MainGrid.Children.IndexOf(Linie3);
			
			// Configure message box
			string message =	$"In MainGrid.Children befinden sich: {Counter},dann {Counter2} Elemente! \n" +
								$"Index of last line Obj.: {Counter3}";
			// Show message box
			MessageBoxResult result = MessageBox.Show(message);

		}

		//Friedhof (lieber Herr Gertz, Standardkonstruktoren an diesen Ort zu verbannen tztztzt... )
		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

		private void BtnLaden_Click(object sender, RoutedEventArgs e)
		{
		}

		private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
		{
		}

		private void BtnErstellen_Click(object sender, RoutedEventArgs e)
		{
		}

		private void BtnLöschen_Click(object sender, RoutedEventArgs e)
		{
		}

		private void Cb_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void Fill_cb_UseDatabase()
		{
		}
	}
}