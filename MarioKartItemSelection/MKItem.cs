using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace MarioKartItemSelection
{
	public class MKItem : INotifyPropertyChanged
	{
		static ObservableCollection<MKItem> all_items = new ObservableCollection<MKItem>();

		public string name { get; set; }
		public string image { get { return string.Format("Images/{0}.png", name.ToLower().Replace(" ", "")); } }

		private ObservableCollection<double> _stats;
		public ObservableCollection<double> stats
		{
			get { return _stats; }
			set
			{
				_stats = value;
				CallPropertyChanged("stats");
			}
		}

		public Brush bg_color
		{
			get { return is_selected ? Brushes.Red : Brushes.Gray; }
		}

		private bool _is_selected = false;
		public bool is_selected
		{
			get { return _is_selected; }
			set
			{
				_is_selected = value;
				CallPropertyChanged("is_selected");
				CallPropertyChanged("bg_color");
			}
		}

		public static string[] stat_names = { "Speed", "Speed (Water)", "Speed (Air)", "Speed (Anti-Gravity)",
			"Acceleration", "Weight", "Handling", "Handling (Water)", "Handling (Air)", "Handling (Anti-Gravity)",
			"Traction", "MiniTurbo", "Average", "Weighted Average" };

		//Event caller
		public event PropertyChangedEventHandler PropertyChanged;

		void CallPropertyChanged(string property_name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(property_name));
		}

		//Functions
		public void UpdateStatWeight(List<double> stat_weights)
		{
			double weighted_average = 0;
			for (int i = 0; i < stat_names.Length - 2; i++)
				weighted_average += stats[i] * stat_weights[i];
			stats[stats.Count - 1] = weighted_average / (stat_names.Length - 2);
			CallPropertyChanged("stats");
		}

		//Constructor
		public MKItem(string in_name, int stat_class)
		{
			name = in_name;
			switch (stat_class)
			{
				//Weight classes
				case 0:
					stats = new ObservableCollection<double> { 2.25, 2.25, 2.25, 2.5, 3.25, 2.25, 4.75, 5.25, 4.5, 5, 4.5, 3 };
					break;
				case 1:
					stats = new ObservableCollection<double> { 2.75, 2.75, 2.75, 3, 3, 2.75, 4.25, 4.75, 4, 4.5, 4.25, 2.75 };
					break;
				case 2:
					stats = new ObservableCollection<double> { 3.25, 3.25, 3.25, 3.5, 2.75, 3.25, 3.75, 4.25, 3.5, 4, 4, 2.5 };
					break;
				case 3:
					stats = new ObservableCollection<double> { 3.75, 3.75, 3.75, 4, 2.5, 3.75, 3.25, 3.75, 3, 3.5, 3.75, 2.25 };
					break;
				case 4:
					stats = new ObservableCollection<double> { 4.25, 4.25, 4.25, 4.5, 2.25, 4.25, 2.75, 3.25, 2.5, 3, 3.5, 2 };
					break;
				case 5:
					stats = new ObservableCollection<double> { 4.25, 4.25, 4.25, 4.5, 2, 4.75, 2.75, 3.25, 2.5, 3, 3.25, 1.75 };
					break;
				case 6:
					stats = new ObservableCollection<double> { 4.75, 4.75, 4.75, 5, 2, 4.75, 2.25, 2.75, 2, 2.5, 3.25, 1.75 };
					break;

				//Karts
				case 100:
					stats = new ObservableCollection<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
					break;
				case 101:
					stats = new ObservableCollection<double> { 0.5, 0.25, 0.5, -0.25, -0.25, 0.25, 0, 0, 0, -0.25, -1, -0.5 };
					break;
				case 102:
					stats = new ObservableCollection<double> { 0, 0.5, 0, -0.25, -0.5, 0.5, -0.5, 0.75, -0.25, -0.75, 0.5, -0.75 };
					break;
				case 103:
					stats = new ObservableCollection<double> { -0.75, 0.5, 0.5, -1, 1.25, -0.5, 0.5, 0.75, 0.75, 0, -0.25, 1 };
					break;
				case 104:
					stats = new ObservableCollection<double> { 0, 0.25, 0.25, -0.25, 0.25, -0.25, 0.5, 0.5, 0.5, 0.25, -0.5, 0.25 };
					break;
				case 105:
					stats = new ObservableCollection<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
					break;
				case 106:
					stats = new ObservableCollection<double> { -0.75, 0.5, 0.5, -1, 1.25, -0.5, 0.5, 0.75, 0.75, 0, -0.25, 1 };
					break;
				case 107:
					stats = new ObservableCollection<double> { 0, 0.25, 0.25, -0.25, 0.25, -0.25, 0.5, 0.5, 0.5, 0.25, -0.5, 0.25 };
					break;
				case 108:
					stats = new ObservableCollection<double> { 0, 0, 0, -0.25, 0.75, -0.25, 0.75, 0.75, 0.75, 0.5, -1.25, 0.5 };
					break;
				case 109:
					stats = new ObservableCollection<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
					break;
				case 110:
					stats = new ObservableCollection<double> { 0, 0.25, 0.25, -0.25, 0.25, -0.25, 0.5, 0.5, 0.5, 0.25, -0.5, 0.25 };
					break;
				case 111:
					stats = new ObservableCollection<double> { 0, 0.5, 0, -0.25, -0.5, 0.5, -0.5, 0.75, -0.25, -0.75, 0.5, -0.75 };
					break;

				//Wheels
				case 200:
					stats = new ObservableCollection<double> { -0.5, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
					break;
				case 201:
					stats = new ObservableCollection<double> { -0.5, 0, -0.5, 0, -0.5, 0.5, -0.75, -0.75, -0.75, -0.75, 0.75, 0 };
					break;
				case 202:
					stats = new ObservableCollection<double> { 0, -0.5, 0.5, 0.5, -0.25, 0.25, 0, 0, 0, 0, -1, 0.25 };
					break;
				case 203:
					stats = new ObservableCollection<double> { -1, 0.5, 0.5, -0.5, 1, -0.5, 0.25, 0.25, 0.25, 0.25, -0.25, 1.5 };
					break;
				case 204:
					stats = new ObservableCollection<double> { -0.25, 0.25, 0.25, 0.25, -0.25, 0, 0.25, 0.25, 0.25, 0.25, -0.5, 0.25 };
					break;
				case 205:
					stats = new ObservableCollection<double> { -0.25, 0.25, 0.25, 0.25, -0.5, 0.5, 0, 0, 0, 0, -0.5, 0 };
					break;
				case 206:
					stats = new ObservableCollection<double> { -0.75, -0.5, 0.25, -0.25, 0.25, -0.25, -0.25, -0.5, 0, -0.25, 0.5, 0.75 };
					break;

				//Gliders
				case 300:
					stats = new ObservableCollection<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
					break;
				case 301:
					stats = new ObservableCollection<double> { 0, 0, -0.25, 0, 0.25, -0.25, 0, 0, 0.25, 0, 0, 0.25 };
					break;

				default:
					stats = new ObservableCollection<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
					break;
			}

			//Add average stats
			all_items.Add(this);
			double average = 0;
			foreach (double s in stats)
				average += s;
			average /= stats.Count;
			stats.Add(average);
			stats.Add(average);
		}
	}
}