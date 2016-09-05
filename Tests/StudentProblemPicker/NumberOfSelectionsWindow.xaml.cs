/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 9/5/2016
 * Time: 4:23 PM
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

namespace StudentProblemPicker
{
	/// <summary>
	/// Interaction logic for NumberOfSelectionsWindow.xaml
	/// </summary>
	public partial class NumberOfSelectionsWindow : Window
	{
		public NumberOfSelectionsWindow() : this(5)
		{
		}
		
		public NumberOfSelectionsWindow(int max)
		{
			this.NumSelections = -1;
			InitializeComponent();
			this.numSelections.Maximum = max;
			this.DataContext = this;
		}
		
		//-1 means it was canceled
		internal int NumSelections;
		
		void Select_Click(object sender, RoutedEventArgs e)
		{
			this.NumSelections = (int)this.numSelections.Value;
			this.Close();
		}
		
		public static int GetNumSelections(Window owner, int max)
		{
			NumberOfSelectionsWindow window = new NumberOfSelectionsWindow(max);
			window.Owner = owner;
			window.ShowDialog();
			return window.NumSelections;
		}
	}
}