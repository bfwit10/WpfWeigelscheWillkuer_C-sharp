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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

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
			// here you can drop your code ;-)
		}
		
		private void Scrollbar_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            angleContent_x.Content = "X - Achse: " + Math.Floor(Scrollbar_X.Value).ToString() + "°";
            Cube.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), Scrollbar_X.Value));
        }

        private void Scrollbar_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            angleContent_y.Content = "Y - Achse: " + Math.Floor(Scrollbar_Y.Value).ToString() + "°";
            Cube.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Scrollbar_Y.Value));
        }

		public void Viewport3D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Viewport3D viewPort = sender as Viewport3D;
			Point mousePosition = e.GetPosition(viewPort);
			HitTestResult result = VisualTreeHelper.HitTest(viewPort, mousePosition);
			if (result.VisualHit.GetType() == typeof(ModelVisual3D))
			{
				ModelVisual3D v3D = result.VisualHit as ModelVisual3D;
				RotateTransform3D rot = (v3D.Transform as RotateTransform3D);
				(rot.Rotation as AxisAngleRotation3D).Angle += 10;
			}
		}
	}
}

