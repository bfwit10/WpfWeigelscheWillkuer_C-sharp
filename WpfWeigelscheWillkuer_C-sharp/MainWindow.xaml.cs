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
		private int myVar;
		private int myVar;
		private int myVar;
		private int myVar;

		public int MyProperty
		{
			get { return myVar; }
			set { myVar = value; }
		}

		public int MyProperty
		{
			get { return myVar; }
			set { myVar = value; }
		}

		public int MyProperty
		{
			get { return myVar; }
			set { myVar = value; }
		}

		public int MyProperty
		{
			get { return myVar; }
			set { myVar = value; }
		}


		private string Connection = GlobalVariables.GlobalMySqlCon; //Für die Verbindung an unsere Datenbank

		public MainWindow()
		{
			InitializeComponent();
			fill_cb_UseDatabase(); //Auskommentiert weil die Methode auf dem Friehof (s.u.) gelandet ist...


        //	TODO: Hier ein mal Zeichen und dann Transformieren damit anzahl der
        //	Operationen reduzieren & ggf. Objekte freigeben

			double basisObjekte;
			int layer = 0;
			double gesamtlänge;
			double länge;

			Double.TryParse(txtLänge.Text, out gesamtlänge);
			Int32.TryParse(txtEingabe.Text, out layer);
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
			// Configure message box
			string message = "Diese Funktion ist noch nicht implementiert!";
			// Show message box
			MessageBoxResult result = MessageBox.Show(message);
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
				else if (Zzahl == "3")
				{
					simpleSound = new SoundPlayer(vivaldi3);
					simpleSound.Play();
				}
			}
			else simpleSound.Stop();
		}

		//Methode die das Abspielen der Musik anhält.
		public void stopVivaldi()
		{
			bool toggleOff = true;
			playRandomVivaldi(toggleOff);
		}

		//Ab hier beginnt die Berechnung und Darstellung des Fraktales
		private void btnStart_Click(object sender, RoutedEventArgs e)
		{
			//playRandomVivaldi(false);
			// Aufruf der Zeichnen Methode!
			drawLines(true);
		}

		

		

		// TODO: ´hier noch eine Variante zum leeren vor dem Neu zeichnen... (ohne Textboxen zu leeren...)
		// & gc() 
		//

		// Event zum leeren der Anzeige, wenn der Clear Button gedrückt wurde
		private void BtnclearView_Click(object sender, RoutedEventArgs e)
		{
			removeLines(); // Remove Lines entfernt (momentan) nur das zuletzt gezeichnete Objekt!
			this.txtEingabe.Text = string.Empty;
			this.txtLänge.Text = string.Empty;
			//stopVivaldi();

			//überzeichnen der Rechtecke (doof)
			Rectangle theRect = new Rectangle
			{

				Height = gesamtlänge*1.2,
				Width = gesamtlänge*1.2,
				Fill = new SolidColorBrush(Colors.White)
			};

			MainGrid.Children.Add(theRect);
		}

		// Methode zum Löschen der Gezeichneten Objekte (löscht bisher nur das letzte Objekt)
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

		private void BtnLaden_Click(object sender, RoutedEventArgs e)
		{
		}

		private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
		{
		}

		private void btnErstellen_Click(object sender, RoutedEventArgs e)
		{
		}

		private void BtnLöschen_Click(object sender, RoutedEventArgs e)
		{
		}

		private void cb_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void fill_cb_UseDatabase()
		{
		}
	}
}