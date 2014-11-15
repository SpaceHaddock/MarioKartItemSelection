using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using MvvmExample.Helpers;
using System.Windows.Input;

namespace MarioKartItemSelection
{
	public class MKBuild : INotifyPropertyChanged
	{
		//Public variables
		void ItemGroups_Changed(object sennder, PropertyChangedEventArgs e)
		{
			CallPropertyChanged("final_stats");
		}

		string _name = "No Name";
		public string name
		{
			get { return _name; }
			set
			{
				_name = value;
				CallPropertyChanged("name");
			}
		}

		ObservableCollection<ItemGroup> _item_groups = new ObservableCollection<ItemGroup>();
		public ObservableCollection<ItemGroup> item_groups
		{
			get { return _item_groups; }
			set
			{
				_item_groups = value;
				CallPropertyChanged("item_groups");
			}
		}

		public ObservableCollection<double> final_stats
		{
			get
			{
				ObservableCollection<double> result = new ObservableCollection<double>(GList.MakeList<double>(MKItem.stat_names.Count()));
				foreach (ItemGroup group in item_groups)
					for (int i = 0; i < result.Count; i++)
						result[i] += group.stats[i];
				return result;
			}
		}

		ObservableCollection<NameValue> _stat_weights = new ObservableCollection<NameValue>();
		public ObservableCollection<NameValue> stat_weights
		{
			get { return _stat_weights; }
			set
			{
				_stat_weights = value;
				CallPropertyChanged("stat_weights");
			}
		}

		public string[] stat_names { get { return MKItem.stat_names; } }

		//Event caller
		public event PropertyChangedEventHandler PropertyChanged;

		void CallPropertyChanged(string property_name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(property_name));
		}

		//Constructor
		public MKBuild()
		{
			for (int i = 0; i < stat_names.Length - 1; i++)
				stat_weights.Add(new NameValue() { name = stat_names[i], value = 1 });

			//Enter stats
			string[][][] names = new string[][][]
			{
				new string[][] {
					new string[] { "Baby Mario", "Baby Luigi", "Baby Peach", "Baby Daisy", "Baby Rosalina", "Lemmy", "Mii Light" },
					new string[] { "Toad", "Shy Guy", "Koopa Troopa", "Lakitu", "Wendy", "Larry", "Toadette" },
					new string[] { "Peach", "Daisy", "Yoshi" },
					new string[] { "Mario", "Luigi", "Iggy", "Ludwig", "Mii Medium" },
					new string[] { "Donkey Kong", "Waluigi", "Rosalina", "Roy" },
					new string[] { "Metal Mario", "Pink Gold Peach" },
					new string[] { "Wario", "Bowser", "Morton", "Mii Heavy" }
				},

				new string[][] {
					new string[] { "Standard Kart", "Prancer", "Cat Cruiser", "Sneaker" },
					new string[] { "Gold Standard", "Mach 8", "Circuit Special", "Sports Coupe" },
					new string[] { "Badwagon", "TriSpeeder", "Steel Driver" },
					new string[] { "Biddybuggy", "Landship" },
					new string[] { "Pipe Frame" },
					new string[] { "The Duke" },
					new string[] { "Mr Scooty"},
					new string[] { "Standard Bike", "Flame Rider", "Varmit" },
					new string[] { "Sports Bike", "Jet Bike", "Comet", "Yoshi Bike" },
					new string[] { "Teddy Buggy" },
					new string[] { "Wild Wiggler" },
					new string[] { "Standard ATV" }
				},

				new string[][] {
					new string[] { "Standard", "Blue Standard", "Offroad", "Retro Offroad" },
					new string[] { "Monster", "Hot Monster" },
					new string[] { "Slick", "Cyber Slick" },
					new string[] { "Roller", "Azure Roller", "Button" },
					new string[] { "Slim", "Crimson Slim" },
					new string[] { "Metal", "Gold Tires" },
					new string[] { "Wood", "Sponge", "Cushion" }
				},

				new string[][] {
					new string[] { "Super", "Waddle Wing", "Plane", "Wario Wing", "Gold" },
					new string[] { "Flower", "Peach Parasol", "MKTV Parafoil", "Bowser Kite", "Cloud" }
				}
			};

			//Iterate through names and load into viewmodel
			for (int i = 0; i < names.Length; i++)
			{
				item_groups.Add(new ItemGroup());
				for (int j = 0; j < names[i].Length; j++)
				{
					foreach (string name in names[i][j])
						item_groups.Last().items.Add(new MKItem(name, j + i * 100));
				}
				item_groups.Last().PropertyChanged += ItemGroups_Changed;
			}

			//Give each item group a name
			string[] item_group_names = { "Characters", "Karts", "Wheels", "Gliders" };
			for (int i = 0; i < item_groups.Count; i++)
				item_groups[i].name = item_group_names[i];
		}

		//Functions
		public void UpdateStatWeight()
		{
			List<double> weights = new List<double>();
			foreach (NameValue nv in stat_weights)
				weights.Add(nv.value);

			foreach(ItemGroup group in item_groups)
			{
				foreach(MKItem item in group.items)
					item.UpdateStatWeight(weights);
			}
		}

		public void SuggestedBuild()
		{
			UpdateStatWeight();
			foreach(ItemGroup group in item_groups)
				group.selected_item = group.items.OrderBy(i => i.stats.Last()).Last();
		}

		//Commands
		RelayCommand _UpdateStatWeightButton;
		public ICommand UpdateStatWeightButton
		{
			get
			{
				if(_UpdateStatWeightButton == null)
					_UpdateStatWeightButton = new RelayCommand(param => this.UpdateStatWeight());
				return _UpdateStatWeightButton;
			}
		}

		RelayCommand _SuggestBuildButton;
		public ICommand SuggestBuildButton
		{
			get
			{
				if(_SuggestBuildButton == null)
					_SuggestBuildButton = new RelayCommand(param => this.SuggestedBuild());
				return _SuggestBuildButton;
			}
		}
	}
}