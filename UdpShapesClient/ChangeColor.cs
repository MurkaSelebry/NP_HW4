using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpShapesClient
{
    public partial class ChangeColor : Form
    {
        public MainForm MainForm { get; set; }

        public ChangeColor()
        {
            InitializeComponent();
        }


        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBoxColor.SelectedIndex == -1)
                    throw new ApplicationException("Выберите цвет");
                DialogResult = DialogResult.OK;
                Visible = false;
                MainForm.changeShapeColor((Color)comboBoxColor.SelectedItem);
               

                Close();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeColor_Load(object sender, EventArgs e)
        {
            comboBoxColor.Items.Add(Color.Red);
            comboBoxColor.Items.Add(Color.Yellow);
            comboBoxColor.Items.Add(Color.Blue);
            comboBoxColor.Items.Add(Color.Green);
        }
    }
}
