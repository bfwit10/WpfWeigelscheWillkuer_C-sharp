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
