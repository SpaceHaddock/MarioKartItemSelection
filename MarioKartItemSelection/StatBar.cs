using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;

namespace MarioKartItemSelection
{
	public class StatBar : INotifyPropertyChanged
	{
		//Event caller
		public event PropertyChangedEventHandler PropertyChanged;

		void CallPropertyChanged(string property_name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(property_name));
		}

		//Variables
		double _thickness_left = 0;
		public double thickness_left
		{
			get { return _thickness_left; }
			set
			{
				_thickness_left = value;
				CallPropertyChanged("thickness_left");
			}
		}

		double _thickness_right = 0;
		public double thickness_right
		{
			get { return _thickness_right; }
			set
			{
				_thickness_right = value;
				CallPropertyChanged("thickness_right");
			}
		}

		Brush _color_right = Brushes.Red;
		public Brush color_right
		{
			get { return _color_right; }
			set
			{
				_color_right = value;
				CallPropertyChanged("color_right");
			}
		}

		string _name = "NAME";
		public string name
		{
			get { return _name; }
			set
			{
				_name = value;
				CallPropertyChanged("name");
			}
		}

		string _disp_info = "DISP_INFO";
		public string disp_info
		{
			get { return _disp_info; }
			set
			{
				_disp_info = value;
				CallPropertyChanged("disp_info");
			}
		}
	}
}
