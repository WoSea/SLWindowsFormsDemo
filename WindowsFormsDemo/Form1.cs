using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace WindowsFormsDemo
{
	public partial class Form1 : Form
	{
		//private  Container components = null;
	//	private  TextBox textBox1;
	//	private  Button btnRead;
	//	private  Button btnWrite;
	//	private  Label label1;
		private TcAdsClient adsClient;
		private int varHandle;
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, System.EventArgs e)
		{
			try
			{
				adsClient = new TcAdsClient();

				// PLC1 Port -  TwinCAT 3=851
				adsClient.Connect(851);

				varHandle = adsClient.CreateVariableHandle("MAIN.text");
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
 
		private void button1_Click(object sender, EventArgs e)  //ReadButton
		{
			
			try
			{
				Form1_Load(sender,e);
				AdsStream adsStream = new AdsStream(30);
				AdsBinaryReader reader = new AdsBinaryReader(adsStream);
				adsClient.Read(varHandle, adsStream);
				textBox1.Text = reader.ReadPlcAnsiString(30).ToString();
				adsClient.Dispose();
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				Form1_Load(sender, e);
				AdsStream adsStream = new AdsStream(30);
				AdsBinaryWriter writer = new AdsBinaryWriter(adsStream);
				writer.WritePlcAnsiString(textBox1.Text, 30);
				adsClient.Write(varHandle, adsStream);
				adsClient.Dispose();
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
	}
}
