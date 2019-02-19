using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfWeigelscheWillkuer_C_sharp
{
	class GeoObject: IDisposable 
	{
		
		private Line Linie1, Linie2, Linie3, Linie4, Linie5, Linie6, Linie7, Linie8;

        //	TODO: Hier ein mal Zeichen und dann Transformieren damit anzahl der
        //	Operationen reduzieren & ggf. Objekte freigeben

        
        

        //Methode zum zeichnen der Linien
        public void drawLines(bool draw)
		{
            double x = 300; //X-Wert für den Beginn
            double tmp1 = 300; //X-Wert für den Beginn
            double tmp2 = 100; //Y-Wert für den Beginn

            

			//Linie 1 und 4 werden insgesamt nur einmal gezeichnet
			Linie1 = new Line(); Linie4 = new Line();
			Linie1.Stroke = zarterPastelBrush();
			Linie1.Fill = zarterPastelBrush();
			Linie4.Stroke = zarterPastelBrush();
			Linie4.Fill = zarterPastelBrush();
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

					Linie2.Stroke = zarterPastelBrush();
					Linie3.Stroke = zarterPastelBrush();
					Linie5.Stroke = zarterPastelBrush();
					Linie6.Stroke = zarterPastelBrush();
					Linie7.Stroke = zarterPastelBrush();
					Linie8.Stroke = zarterPastelBrush();
					Linie2.Fill = zarterPastelBrush();
					Linie3.Fill = zarterPastelBrush();
					Linie5.Fill = zarterPastelBrush();
					Linie6.Fill = zarterPastelBrush();
					Linie7.Fill = zarterPastelBrush();
					Linie8.Fill = zarterPastelBrush();

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
			//rm lineX = null;
			//rm gc.
		}

		~GeoObject()
		{
			Line[] lineArray = new Line[8];
			
			{
			Linie1, Linie2, Linie3, Linie4, Linie5, Linie6, Linie7, Linie8 
			};

			foreach (var line in lineArray)
			{
				if ( line != null)
				{
				   line .Dispose();
				   line =null;
				}

			}
		}
	}
}
