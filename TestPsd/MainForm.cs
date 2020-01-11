using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using SimplePsd;

namespace TestPsd
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private SimplePsd.CPSD psd = new SimplePsd.CPSD();
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(392, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.Click += new System.EventHandler(this.OpenFile);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Adobe Photoshop Files (*.psd)|*.psd";
            this.openFileDialog.Title = "Open Adobe Photoshop Files";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 296);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(8, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 72);
            this.label1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(496, 389);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void OpenFile(object sender, System.EventArgs e)
		{
			if(this.openFileDialog.ShowDialog().Equals(DialogResult.OK))
			{
				int nResult = psd.Load(openFileDialog.FileName);
				if(nResult == 0)
				{
					int nCompression = psd.GetCompression();
					string strCompression = "Unknown";
					switch(nCompression)
					{
						case 0:
							strCompression = "Raw data";
							break;
						case 1:
							strCompression = "RLE";
							break;
						case 2:
							strCompression = "ZIP without prediction";
							break;
						case 3:
							strCompression = "ZIP with prediction";
							break;
					}
					label1.Text = string.Format("Image Width: {0}px\r\nImage Height: {1}px\r\n"+
						"Image BitsPerPixel: {2}\r\n"+
						"Resolution (pixels/inch): X={3} Y={4}\r\n",
						psd.GetWidth(),
						psd.GetHeight(),
						psd.GetBitsPerPixel(),
						psd.GetXResolution(),
						psd.GetYResolution());
					label1.Text += "Compression: "+strCompression;
					pictureBox1.Image = System.Drawing.Image.FromHbitmap(psd.GetHBitmap());
				}
				else if(nResult == -1)
					MessageBox.Show("Cannot open the File");
				else if(nResult == -2)
					MessageBox.Show("Invalid (or unknown) File Header");
				else if(nResult == -3)
					MessageBox.Show("Invalid (or unknown) ColourMode Data block");
				else if(nResult == -4)
					MessageBox.Show("Invalid (or unknown) Image Resource block");
				else if(nResult == -5)
					MessageBox.Show("Invalid (or unknown) Layer and Mask Information section");
				else if(nResult == -6)
					MessageBox.Show("Invalid (or unknown) Image Data block");
			}
		}

	}
}
