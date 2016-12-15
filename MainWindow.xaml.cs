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

namespace PercentageCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private int[] CalcPercentage( int[] values )
		{
			if( values == null ) return null;

			// count
			int c = values.Length;

			// calc total number of votes
			int total = 0;
            for( int i = 0; i < c; ++i )
			{
				total += values[ i ];
			}

			// calc percentages for each
			float[] perc = new float[c];
			for( int i = 0; i < c; ++i )
			{
				perc[ i ] = 100.0f * ((float)values[ i ] / (float)total);
			}

			// break those numbers
			int[] res = new int[c];
			float[] frac = new float[c];
			for( int i = 0; i < c; ++i )
			{
				res[ i ] = (int)Math.Floor( perc[ i ] );
				frac[ i ] = perc[ i ] - res[ i ];
			}

			// calc remainder
			int r = 100;
			for( int i = 0; i < c; ++i )
			{
				r -= res[ i ];
			}

			// got a remainder to 
			if( r > 0 )
			{
				// index array for sorting
				int[] idx = new int[c];
				for( int i = 0; i < c; ++i )
				{
					idx[ i ] = i;
				}
				
				// sort the index array
				for( int write = 0; write < c; write++ )
				{
					for( int sort = 0; sort < c - 1; sort++ )
					{
						if( frac[ idx[ sort ] ] < frac[ idx[ sort + 1 ] ] )
						{
							int temp = idx[ sort + 1 ];
							idx[ sort + 1 ] = idx[ sort ];
							idx[ sort ] = temp;
						}
					}
				}

				// distribute remainder
				int l = 0;
				while( r > 0 )
				{
					++res[ idx[ l ] ];
					++l;
					--r;
				}
			}

			return res;
		}

		private void Slider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
		{
			if( ( sldOne != null ) && ( sldTwo != null ) && ( sldThree != null ) && (txtOne != null) && ( txtTwo != null ) && ( txtThree != null ) && ( txtTotal != null ) )
            {

				int[] values = new int[3];
				values[ 0 ] = (int)sldOne.Value;
				values[ 1 ] = (int)sldTwo.Value;
				values[ 2 ] = (int)sldThree.Value;

				int[] res = CalcPercentage( values );

				txtOne.Content = string.Format( "{0} -> {1}", values[ 0 ], res[ 0 ] );
				txtTwo.Content = string.Format( "{0} -> {1}", values[ 1 ], res[ 1 ] );
				txtThree.Content = string.Format( "{0} -> {1}", values[ 2 ], res[ 2 ] );
				txtTotal.Content = string.Format( "Total: {0}", res[ 0 ] + res[ 1 ] + res[ 2 ] );
			}
		}
	}
}
