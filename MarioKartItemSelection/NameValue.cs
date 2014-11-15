using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MarioKartItemSelection
{
	public class NameValue : INotifyPropertyChanged
	{
		string _name;
		public string name
		{
			get { return _name; }
			set
			{
				_name = value;
				CallPropertyChanged("name");
			}
		}

		double _value;
		public double value
		{
			get { return _value; }
			set
			{
				_value = value;
				CallPropertyChanged("value");
			}
		}

		//Event caller
		public event PropertyChangedEventHandler PropertyChanged;

		void CallPropertyChanged(string property_name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(property_name));
		}
	}
}