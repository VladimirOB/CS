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
            /*Пользователь может в диалоговом окне выбрать папку, после чего её содержимое отображается в CheckListBox, 
             * По нажатии кнопки выделенные файла в папке и в списке удаляются*/
            InitializeComponent();
        }

        private void openFileDialog1_FileOk_1(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Удаление всех выделенных пунктов
            while (checkedListBox1.CheckedIndices.Count > 0)
            {
                // удление первого по счёту выделенного птичкой элемента
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