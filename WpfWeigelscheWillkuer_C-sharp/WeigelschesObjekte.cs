using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace WpfWeigelscheWillkuer_C_sharp
{
	class WeigelschesObjekte : DependencyObject
	{

		public static readonly DependencyProperty X_SizeProperty;
		public static readonly DependencyProperty Y_SizeProperty;

		static WeigelschesObjekte() //C# mit VS Studio 2015 Kapitel 22.3.1
		{
			X_SizeProperty = DependencyProperty.Register("X_Size", typeof(double), typeof(WeigelschesObjekte));
		}

		
	}
	
	class WeigelscheLinie : DependencyObject
	{
		public static readonly DependencyProperty Line_SizeProperty;

		static WeigelscheLinie()
		{
			Line_SizeProperty = DependencyProperty.Register("Line_Size", typeof(double), typeof(Line));
		}
	}

}

