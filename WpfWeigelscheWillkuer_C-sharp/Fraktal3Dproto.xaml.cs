using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfWeigelscheWillkuer_C_sharp
{
	/// <summary>
	/// Interaktionslogik für Fraktal3Dproto.xaml
	/// </summary>
	public partial class Fraktal3Dproto : Window
	{
		public Fraktal3Dproto()
		{
			InitializeComponent();
		}

		// Methode zur Berechnung der Rotation um die X - Achse während der Regler auf dem vertikalen Scrollbalken bewegt wird
		private void Scrollbar_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			angleContent_x.Content = "X - Achse: " + Math.Floor(Scrollbar_X.Value).ToString() + "°";
			Cube.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), Scrollbar_X.Value));
		}

		// Methode zur Berechnung der Rotation um die Y - Achse während der Regler auf dem horizontalen Skrollbalken bewegt wird
		private void Scrollbar_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			angleContent_y.Content = "Y - Achse: " + Math.Floor(Scrollbar_Y.Value).ToString() + "°";
			Cube.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Scrollbar_Y.Value));
		}

		// Methode für das Scrollen des Würfels entlang der X- und Y- Achse mit dem Mausrad
		private void Viewport3D_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			Viewport3D viewPort = sender as Viewport3D;
			Point mousePosition = e.GetPosition(viewPort); // Abfrage der aktuellen Mausposition
			HitTestResult result = VisualTreeHelper.HitTest(viewPort, mousePosition);

			if ((Keyboard.Modifiers == ModifierKeys.Alt)) // wird [Alt] mit gedrückt dann
			{
				// Wird die Methode der jeweils anderen Achse aufgerufen, setzt sich der Wert um den der 3D Körper
				// entlang der zuvor verwendeten Achse transformiert wurde, wieder zurück.
				// Bug muss noch behoben werden

				if (e.Delta > 0)
				{
					Scrollbar_X.Value = Scrollbar_X.Value + 10;
				}
				else if (e.Delta < 0)
				{
					Scrollbar_X.Value = Scrollbar_X.Value - 10;
				}
			}
			else
			{
				if (e.Delta > 0) // Mausrad nach oben
				{
					ModelVisual3D vYPlus3D = result.VisualHit as ModelVisual3D; // hier liegt noch ein Bug versteckt, der das Programm zum Absturz führen kann
					RotateTransform3D rotYPlus = (vYPlus3D.Transform as RotateTransform3D);
					(rotYPlus.Rotation as AxisAngleRotation3D).Angle += 10; // würfel nach rechts drehen
				}
				else if (e.Delta < 0) // Mausrad nach unten
				{
					ModelVisual3D vYMinus3D = result.VisualHit as ModelVisual3D; // hier liegt noch ein Bug versteckt, der das Programm zum Absturz führen kann
					RotateTransform3D rotYMinus = (vYMinus3D.Transform as RotateTransform3D);
					(rotYMinus.Rotation as AxisAngleRotation3D).Angle -= 10; //würfel nach links drehen
				}
			}
		}

		// Methode zur Skalierung des Würfels
		// bzw Entwicklung zur Positionsveränderung der Camera
		//private void Viewport3D_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		//{
		//    if (Keyboard.Modifiers != ModifierKeys.Control)
		//        return;

		//    if (e.Delta > 0)
		//    {
		//        zoom = new ScaleTransform3D();
		//        ScaleTransform3D.ScaleY + 0.2;
		//        ScaleTransform3D.ScaleY + 0.2;
		//    }
		//    //PerspectiveCamera.Position()

		//    else if (e.Delta < 0)
		//    {
		//        ScaleTransform3D.ScaleXProperty - 0.2;
		//        ScaleTransform3D.ScaleYProperty - 0.2;
		//        ScaleTransform3D.ScaleYProperty - 0.2;
		//    }

		//}
	}
}