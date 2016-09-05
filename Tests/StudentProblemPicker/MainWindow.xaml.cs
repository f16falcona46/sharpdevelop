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
			
			this.Students = new System.Collections.ObjectModel.ObservableCollection<Student>();
			this.Problems = new System.Collections.ObjectModel.ObservableCollection<ProblemEntry>();
			
			this.DataContext = this;
		}
		
		public IList<Student> Students {get; set;}
		public IList<ProblemEntry> Problems {get; set;}
		
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
		
		void selectFromStudents_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		
		void getHelp_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("For a single problem, enter a number (e.g., 3), for a range, enter it using dashes (e.g., 3-9), and for skipped problems, enter the interval in parentheses (e.g., 3-9(3) for 3-9, multiples of 3).", "Help");
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