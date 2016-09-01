/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 9/1/2016
 * Time: 12:01 AM
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

namespace WPFTestApp
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		int count;
		
		public Window1()
		{
			InitializeComponent();
			this.count = 0;
			this.Counter.Content = count;
		}
		
		void Increment_Click(object sender, RoutedEventArgs e)
		{
			++this.count;
			this.Counter.Content = count;
		}
		void ResetCount_Click(object sender, RoutedEventArgs e)
		{
			this.count = 0;
			this.Counter.Content = count;
		}
	}
}