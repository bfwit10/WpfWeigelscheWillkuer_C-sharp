using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfWeigelscheWillkuer_C_sharp
{
	class Pastel
	{
	//	Pastel Random Color
		//	Pastel: 87CEEB,32CD32,BA55D3,F08080,4682B4,9ACD32,40E0D0,FF69B4,
		//	F0E68C,D2B48C,8FBC8B,6495ED,DDA0DD,5F9EA0,FFDAB9,FFA07A

		public Brush randomPastelBrush()
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
		public Brush zarterPastelBrush()
		{
			return new SolidColorBrush(zartePasteldefinieren()); //geiler scheiß!
		}

		//	BrightPastel: 418CF0,FCB441,DF3A02,056492,BFBFBF,1A3B69,FFE382,
		//	129CDD,CA6B4B,005CDB,F3D288,506381,F1B9A8,E0830A,7893BE
		private Color zartePasteldefinieren()
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
				Color.FromRgb(224,83,10),
				Color.FromRgb(78,93,190)
			};
			Random random = new Random();
			Color zartesPastel = zartePastelfarben[random.Next(zartePastelfarben.Length)];
			return zartesPastel;
		}
	}
}
