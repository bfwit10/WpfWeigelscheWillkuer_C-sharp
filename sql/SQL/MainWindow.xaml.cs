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

namespace SQL
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

        private void fill_cb_UseDatabase()
        {
            string ShowDBQuery = "show databases;";
            MySqlConnection conDataBase = new MySqlConnection(Connection);
            MySqlCommand cmdDataBase = new MySqlCommand(ShowDBQuery, conDataBase);
            MySqlDataReader dbReader;
            try
            {
                conDataBase.Open();//Verbindung zur Datenbankverbindung öffnen
                dbReader = cmdDataBase.ExecuteReader();
                while (dbReader.Read())
                {
                    string databases_sVar = dbReader.GetString("database");
                    cb_Databases.Items.Add(databases_sVar);
                }
                conDataBase.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)//zum schliessen der gesamten Form
        {
            System.Windows.Application.Current.Shutdown(); 
        }

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e) //Speichern der Daten
        {


            
        }

        private void btnErstellen_Click(object sender, RoutedEventArgs e)
        {
            string QueryCreateDB = "create database " + txtDatenbank.Text + ";"; //String für den gewählten Command an die Datenbank

            MySqlConnection conDataBase = new MySqlConnection(Connection); //Hiermit wird die Datenbank angesprochen
            MySqlCommand cmdDataBase = new MySqlCommand(QueryCreateDB, conDataBase); //MySQL Command wird hiermit án die Datenbank übergeben

            try
            {

                Match match = Regex.Match(txtDatenbank.Text, "[^a-zA-Z]",
                RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    //conDataBase.Open();//Verbindung zur Datenbankverbindung öffnen
                    //cmdDataBase.ExecuteNonQuery(); //Verarbeiten des Befehles
                    //conDataBase.Close();//Schliessen der Datenbankverbindung
                    MessageBoxResult result = MessageBox.Show("Die Datenbank wurde erfolgreicht erstellt!", "Erfolgreich erstellt", MessageBoxButton.OK);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Es sind nur Buchstaben erlaubt!", "Fehler!", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLöschen_Click(object sender, RoutedEventArgs e)
        {
            string QueryCreateDB = "drop database " + txtDatenbank.Text + ";"; //String für den gewählten Command an die Datenbank
            MySqlConnection conDataBase = new MySqlConnection(Connection); //Hiermit wird die Datenbank angesprochen
            MySqlCommand cmdDataBase = new MySqlCommand(QueryCreateDB, conDataBase); //MySQL Command wird hiermit án die Datenbank übergeben

            try
            {
                conDataBase.Open();//Verbindung zur Datenbankverbindung öffnen
                cmdDataBase.ExecuteNonQuery(); //Verarbeiten des Befehles
                conDataBase.Close();//Schliessen der Datenbankverbindung
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string UseDBQuery = "use " + cb_Databases.Text + ";";
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

            try //höchte id auslesen
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
        }

        //Friedhof
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void BtnLaden_Click(object sender, RoutedEventArgs e) { }

    }
}
