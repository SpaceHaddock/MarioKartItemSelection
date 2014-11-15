using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MvvmExample.Helpers;
using System.Windows.Input;
using System.IO;
using System;
using Microsoft.Win32;

namespace MarioKartItemSelection
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ViewModel vm = new ViewModel();

		public MainWindow()
		{
			DataContext = vm;
			vm.builds.Add(new MKBuild());
			InitializeComponent();
		}
	}

	public class ViewModel : INotifyPropertyChanged
	{
		ObservableCollection<MKBuild> _builds = new ObservableCollection<MKBuild>();
		public ObservableCollection<MKBuild> builds
		{
			get { return _builds; }
			set
			{
				_builds = value;
				CallPropertyChanged("builds");
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

		//FUNCTIONS
		string saved_file_name = "";
		void NewFile()
		{
			saved_file_name = "";
			builds = new ObservableCollection<MKBuild>();
			builds.Add(new MKBuild());
		}

		void SaveFile()
		{
			if (string.IsNullOrEmpty(saved_file_name))
				SaveFileAs();
			else
				File.WriteAllLines(saved_file_name, ExportToText());
		}

		void SaveFileAs()
		{
			SaveFileDialog save_file = new SaveFileDialog();
			save_file.FileName = "MarioKartBuilds";
			save_file.DefaultExt = ".txt";
			save_file.Filter = "Text documents (.txt)|*.txt";

			bool? result = save_file.ShowDialog();
			if (result == true)
			{
				saved_file_name = save_file.FileName;
				File.WriteAllLines(saved_file_name, ExportToText());
			}
		}

		string[] ExportToText()
		{
			List<string> result = new List<string>();
			foreach(MKBuild build in builds)
			{
				List<string> build_strings = new List<string>();

				//Get index of selected items
				foreach(ItemGroup group in build.item_groups)
					build_strings.Add(group.items.IndexOf(group.selected_item).ToString());

				//Get stat weights
				foreach(NameValue nv in build.stat_weights)
					build_strings.Add(nv.value.ToString());

				build_strings.Add(build.name);

				result.Add(string.Join("`", build_strings));
			}
			return result.ToArray();
		}

		void OpenFile()
		{
			OpenFileDialog open_file = new OpenFileDialog();
			open_file.FileName = "";
			open_file.DefaultExt = ".txt";
			open_file.Filter = "Text files (.txt)|*.txt";

			bool? result = open_file.ShowDialog();

			if(result == true)
			{
				builds.Clear();
				foreach(string line in File.ReadAllLines(open_file.FileName))
				{
					MKBuild new_build = new MKBuild();
					Queue<string> q = new Queue<string>(line.Split('`'));

					//Setup selected items
					foreach (ItemGroup group in new_build.item_groups)
					{
						int index = int.Parse(q.Dequeue());
						if (index >= 0)
							group.selected_item = group.items[index];
					}

					//Setup stat weights
					foreach (NameValue nv in new_build.stat_weights)
						nv.value = double.Parse(q.Dequeue());

					new_build.name = q.Dequeue();

					builds.Add(new_build);
				}
			}
		}

		void OpenRecentFile()
		{

		}

		void AddBuild()
		{
			builds.Add(new MKBuild());
		}

		void RemoveBuild(MKBuild remove_me)
		{
			builds.Remove(remove_me);
		}

		//Commands
		RelayCommand _NewFileButton;
		public ICommand NewFileButton
		{
			get
			{
				if (_NewFileButton == null)
					_NewFileButton = new RelayCommand(param => this.NewFile());
				return _NewFileButton;
			}
		}

		RelayCommand _SaveFileButton;
		public ICommand SaveFileButton
		{
			get
			{
				if (_SaveFileButton == null)
					_SaveFileButton = new RelayCommand(param => this.SaveFile());
				return _SaveFileButton;
			}
		}

		RelayCommand _SaveFileAsButton;
		public ICommand SaveFileAsButton
		{
			get
			{
				if (_SaveFileAsButton == null)
					_SaveFileAsButton = new RelayCommand(param => this.SaveFileAs());
				return _SaveFileAsButton;
			}
		}

		RelayCommand _OpenFileButton;
		public ICommand OpenFileButton
		{
			get
			{
				if (_OpenFileButton == null)
					_OpenFileButton = new RelayCommand(param => this.OpenFile());
				return _OpenFileButton;
			}
		}

		RelayCommand _AddBuildButton;
		public ICommand AddBuildButton
		{
			get
			{
				if (_AddBuildButton == null)
					_AddBuildButton = new RelayCommand(param => this.AddBuild());
				return _AddBuildButton;
			}
		}

		RelayCommand _RemoveBuildButton;
		public ICommand RemoveBuildButton
		{
			get
			{
				if (_RemoveBuildButton == null)
					_RemoveBuildButton = new RelayCommand(param => this.RemoveBuild(param as MKBuild));
				return _RemoveBuildButton;
			}
		}
	}
}
