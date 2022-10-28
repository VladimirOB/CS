using System.IO;
using System.Windows.Forms;

namespace Folders
{
    public partial class Form1 : Form
    {
        HashSet<FileInfo> files;
        DirectoryInfo dinfo;

        public Form1()
        {
            /*������������ ����� � ���������� ���� ������� �����, ����� ���� � ���������� ������������ � CheckListBox, 
             * �� ������� ������ ���������� ����� � ����� � � ������ ���������*/
            InitializeComponent();
        }

        private void openFileDialog1_FileOk_1(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // �������� ���� ���������� �������
            while (checkedListBox1.CheckedIndices.Count > 0)
            {
                // ������� ������� �� ����� ����������� ������� ��������
                checkedListBox1.Items.RemoveAt(checkedListBox1.CheckedIndices[0]);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"c:\temp\";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                dinfo = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                files = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToHashSet();
            }
            foreach (var item in files)
            {
                checkedListBox1.Items.Add(item);
            }
        }
        
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {
          
               
        }
    }
}