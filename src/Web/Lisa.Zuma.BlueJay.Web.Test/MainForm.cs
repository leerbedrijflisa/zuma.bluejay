using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lisa.Zuma.BlueJay.Web.Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAddMedia_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = "Please, select a file",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fileNames = ofd.FileNames;
                foreach (var fileName in fileNames)
                {
                    lbMedia.Items.Add(fileName);
                }
            }
        }
    }
}
