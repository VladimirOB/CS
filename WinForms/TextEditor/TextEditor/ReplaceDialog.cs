using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public delegate void ReplaceDelegate(string Source, string ReplaceString);

    sealed partial class ReplaceDialog : Form
    {

        public event ReplaceDelegate PerformReplace;
        private static readonly ReplaceDialog rd = new ReplaceDialog();
        public ReplaceDialog() { }

        public static ReplaceDialog CreateReplaceDialog()
        {
            rd.InitializeComponent();
            return rd;
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            PerformReplace?.Invoke(textBoxInput.Text, textBoxOutput.Text);
        }

        private void ReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
