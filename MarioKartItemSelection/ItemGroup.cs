using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MarioKartItemSelection
{
	public class ItemGroup : INotifyPropertyChanged
	{
		ObservableCollection<MKItem> _items = new ObservableCollection<MKItem>();
		public ObservableCollection<MKItem> items
		{
			get { return _items; }
			set
			{
				_items = value;
				CallPropertyChanged("items");
			}
		}

		string _name = "UNSET";
		public string name
		{
			get { return _name; }
			set
			{
				_name = value;
				CallPropertyChanged("name");
			}
		}

		MKItem _selected_item = null;
		public MKItem selected_item
		{
			get { return _selected_item; }
			set
			{
				_selected_item = value;
				CallPropertyChanged("selected_item");
				CallPropertyChanged("stats");
			}
		}

		public ObservableCollection<double> stats
		{
			get
			{
				if (selected_item == null)
					return new ObservableCollection<double>(GList.MakeList<double>(MKItem.stat_names.Count()));
				else
					return selected_item.stats;
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
