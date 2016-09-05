/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 9/4/2016
 * Time: 4:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using System.Linq;
using Microsoft.Win32;
using System.Collections.ObjectModel;
//using Xceed.Wpf.Toolkit;

namespace StudentProblemPicker
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
			this.Students = new ObservableCollection<Student>();
			this.Problems = new ObservableCollection<ProblemEntry>();
			
			this.DataContext = this;
		}
		
		public ObservableCollection<Student> Students {get; set;}
		public ObservableCollection<ProblemEntry> Problems {get; set;}
		private static Random rng = new Random();

		private static void Shuffle<T>(IList<T> list)  
		{
			int n = list.Count;
			while (n > 1) {
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
		
		//convention: if unsuccessful, returns a list with one element, which is the negative of the problem entry it failed on.
		private List<int> ParseProblemsList()
		{
			List<int> allProblems = new List<int>();
			for (int i = 0; i < this.Problems.Count; ++i) {
				string entryString = this.Problems[i].Problems;
				if (entryString == null) {
					allProblems.Clear();
					allProblems.Add(-(i+1));
					return allProblems;
				}
				entryString = entryString.Replace(" ", "");
				IList<string> subEntries = this.Problems[i].Problems.Split(',');
				foreach (string subentry in subEntries) {
					IList<string> numbers = subentry.Split('-', '(');
					try {
						switch (numbers.Count) {
							case 1:
								allProblems.Add(int.Parse(numbers[0]));
								break;
							case 2:
								{
									int begin = int.Parse(numbers[0]);
									int end = int.Parse(numbers[1]);
									for (int j = begin; j <= end; ++j) {
										allProblems.Add(j);
									}
								}
								break;
							case 3:
								{
									int begin = int.Parse(numbers[0]);
									int end = int.Parse(numbers[1]);
									int step = int.Parse(numbers[2].Substring(0, numbers[2].Length - 1));
									for (int j = begin; j <= end; j += step) {
										allProblems.Add(j);
									}
								}
								break;
							default:
								allProblems.Clear();
								allProblems.Add(-(i+1));
								return allProblems;
						}
					}
					catch (FormatException) {
						allProblems.Clear();
						allProblems.Add(-(i+1));
						return allProblems;
					}
				}
			}
			return allProblems;
		}
		
		void saveStudents_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.FileName = "Students";
			dialog.Filter = "XML Documents (*.xml)|*.xml|All files (*.*)|*.*";
			
			bool? result = dialog.ShowDialog();
			if (result == true) {
				XElement students = new XElement("Students", this.Students.Select(x => new XElement("student", new XAttribute("name", x.Name))));
				students.Save(dialog.FileName);
			}
		}
		
		void loadStudents_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "XML Documents (*.xml)|*.xml|All files (*.*)|*.*";
			
			bool? result = dialog.ShowDialog();
			if (result == true) {
				XDocument document = XDocument.Load(dialog.FileName);
				if (document.Element("Students") == null) {
					MessageBox.Show("This is not a student file.", "Filetype error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				var students = from record in document.Descendants("student")
					select new Student(record.Attribute("name").Value);
				
				this.Students.Clear();
				foreach (var student in students) {
					this.Students.Add(student);
				}
			}
		}
		
		void saveProblems_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.FileName = "Problems";
			dialog.Filter = "XML Documents (*.xml)|*.xml|All files (*.*)|*.*";
			
			bool? result = dialog.ShowDialog();
			if (result == true) {
				XElement problems = new XElement("Problems", this.Problems.Select(x => new XElement("problem", new XAttribute("range", x.Problems))));
				problems.Save(dialog.FileName);
			}
		}
		
		void loadProblems_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "XML Documents (*.xml)|*.xml|All files (*.*)|*.*";
			
			bool? result = dialog.ShowDialog();
			if (result == true) {
				XDocument document = XDocument.Load(dialog.FileName);
				if (document.Element("Problems") == null) {
					MessageBox.Show("This is not a problem file.", "Filetype error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				var problems = from record in document.Descendants("problem")
					select new ProblemEntry(record.Attribute("range").Value);
				
				this.Problems.Clear();
				foreach (var problem in problems) {
					this.Problems.Add(problem);
				}
			}
		}
		
		//kludgy as hell, almost no input validation
		void selectFromStudents_Click(object sender, RoutedEventArgs e)
		{
			List<int> allProblems = this.ParseProblemsList();
			if (allProblems[0] >= 0) {
				List<Student> shuffledStudents = new List<Student>(this.Students);
				Shuffle(shuffledStudents);
				Shuffle(allProblems);
				
				int numSelections = NumberOfSelectionsWindow.GetNumSelections(this, Math.Min(this.Students.Count, allProblems.Count));
				
				if (numSelections > 0) {
					string assignments = "";
					for (int i = 0; i < numSelections; ++i) {
						assignments += String.Format("{0}: Problem {1}\n", shuffledStudents[i].Name, allProblems[i]);
					}
					MessageBox.Show(assignments, "Problem Assignments");
				}
			}
			else {
				MessageBox.Show("Item " + -allProblems[0] + " wasn't in the correct format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		
		void getHelp_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("For a single problem, enter a number (e.g., 3), "+
			                	"for a range, enter it using dashes (e.g., 3-9), "+
			                	"and for skipped problems, enter the interval in parentheses (e.g., 3-9(3) for 3-9, multiples of 3); "+
			                	"each entry can also be separated by a comma instead of being a new entry (e.g., 4,5-10 is equivalent to two entries). "+
			                	"Spaces are ignored.",
			                "Help");
		}
	}
	
	public class Student
	{
		public Student() : this("NONE")
		{
		}
		
		public Student(string Name)
		{
			this.Name = Name;
		}
		
		public string Name {get; set;}
	}
	
	public class ProblemEntry
	{
		public ProblemEntry() : this("")
		{
		}
		
		public ProblemEntry(string Problems)
		{
			this.Problems = Problems;
		}
		
		public string Problems {get; set;}
		
		public IList<int> GetProblemList()
		{
			throw new NotImplementedException();
		}
	}
}