/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 11/9/2016
 * Time: 3:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MetroUICounter
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : MetroForm
	{
		int count;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			this.IncrementButton.Click += (object sender, EventArgs e) => {
				++this.count;
				UpdateCounterLabel();
			};
			
			this.ResetButton.Click += (object sender, EventArgs e) => {
				this.count = 0;
				UpdateCounterLabel();
			};
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		
		void UpdateCounterLabel() {
			this.CounterLabel.Text = this.count.ToString();
		}
	}
}
