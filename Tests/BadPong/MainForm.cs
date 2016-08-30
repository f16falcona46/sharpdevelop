/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 6/17/2016
 * Time: 7:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Input;

namespace BadPong {
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form {
		const int PADDLE_V = 5;
		
		private Button leftPaddle, ball;
		private IntVec2 ballV;
		private System.Windows.Forms.Timer tim;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			this.MinimumSize = new Size(500, 500);
			leftPaddle = new Button();
			leftPaddle.Text = "YES";
			leftPaddle.Click += (Object sender, EventArgs e) => {leftPaddle.Text = "noperino";};
			leftPaddle.Location = new Point(0, 30);
			leftPaddle.Size = new Size(40, 100);
			
			ball = new Button();
			ball.Text = "Ball!";
			ball.Location = new Point(100, 100);
			this.Controls.Add(ball);
			IntVec2 vec;
			vec.X = 5;
			vec.Y = 5;
			ballV = vec;
			
			tim = new System.Windows.Forms.Timer();
			tim.Tick += OnUpdate;
			tim.Interval = 10;
			tim.Start();
			
			this.Controls.Add(leftPaddle);
		}
		
		public void OnUpdate(Object sender, EventArgs e) {
			Point p = leftPaddle.Location;
			int yCandidate = p.Y;
			if (Keyboard.IsKeyDown(Key.Up)) {
				yCandidate = p.Y - PADDLE_V;
				if ((yCandidate + leftPaddle.Size.Height > this.ClientSize.Height) || (yCandidate < 0)) {
					yCandidate = p.Y;
				}
			}
			if (Keyboard.IsKeyDown(Key.Down)) {
				yCandidate = p.Y + PADDLE_V;
				if ((yCandidate + leftPaddle.Size.Height > this.ClientSize.Height) || (yCandidate < 0)) {
					yCandidate = p.Y;
				}
			}
			p.Y = yCandidate;
			leftPaddle.Location = p;
			
			p = ball.Location;
			yCandidate = p.Y + ballV.Y;
			if ((yCandidate + ball.Size.Height > this.ClientSize.Height) || (yCandidate < 0)) {
				ballV.Y *= -1;
				yCandidate = p.Y + ballV.Y;
			}
			int xCandidate = p.X + ballV.X;
			if (xCandidate < 0) {
				Thread t = new Thread(() => MessageBox.Show("You lost."));
				t.Start();
				tim.Stop();
				Thread.Sleep(1000);
				Application.Exit();
			}
			if ((xCandidate < leftPaddle.Location.X + leftPaddle.Size.Width) && (Math.Abs(p.Y+ball.Size.Height/2-leftPaddle.Location.Y-leftPaddle.Size.Height/2) < ball.Size.Height/2+leftPaddle.Size.Height/2)) {
				ballV.X *= -1;
				xCandidate = p.X + ballV.X;
			}
			if (xCandidate + ball.Size.Width > this.ClientSize.Width) {
				ballV.X *= -1;
				xCandidate = p.X + ballV.X;
			}
			p = new Point(xCandidate, yCandidate);
			ball.Location = p;
		}
	}
}
