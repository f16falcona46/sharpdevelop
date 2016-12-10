/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 11/9/2016
 * Time: 3:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MetroUICounter
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private MetroFramework.Controls.MetroLabel CounterLabel;
		private MetroFramework.Controls.MetroButton IncrementButton;
		private MetroFramework.Controls.MetroButton ResetButton;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.CounterLabel = new MetroFramework.Controls.MetroLabel();
			this.IncrementButton = new MetroFramework.Controls.MetroButton();
			this.ResetButton = new MetroFramework.Controls.MetroButton();
			this.SuspendLayout();
			// 
			// CounterLabel
			// 
			this.CounterLabel.Location = new System.Drawing.Point(24, 64);
			this.CounterLabel.Name = "CounterLabel";
			this.CounterLabel.Size = new System.Drawing.Size(100, 23);
			this.CounterLabel.TabIndex = 0;
			this.CounterLabel.Text = "0";
			// 
			// IncrementButton
			// 
			this.IncrementButton.Location = new System.Drawing.Point(24, 91);
			this.IncrementButton.Name = "IncrementButton";
			this.IncrementButton.Size = new System.Drawing.Size(75, 23);
			this.IncrementButton.TabIndex = 1;
			this.IncrementButton.Text = "Increment";
			this.IncrementButton.UseSelectable = true;
			// 
			// ResetButton
			// 
			this.ResetButton.Location = new System.Drawing.Point(105, 90);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(75, 23);
			this.ResetButton.TabIndex = 2;
			this.ResetButton.Text = "Reset";
			this.ResetButton.UseSelectable = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 300);
			this.Controls.Add(this.ResetButton);
			this.Controls.Add(this.IncrementButton);
			this.Controls.Add(this.CounterLabel);
			this.Name = "MainForm";
			this.Text = "MetroUICounter";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);

		}
	}
}
