using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        /*2. Разработать текстовый редактор, обладающий следующими функциями:
        - использовать диалоговые окна для открытия и сохранения файлов
        - использовать вкладки и/или MDI-окна
        - наличие верхнего меню
        - наличие status bar
        - меню файл: new, open, save, save all, close
        - диалоговое окно для поиска и замены текста (поиск при помощи выделения)
        - при закрытии программы или окна предлагать сохранить несохранённые документы*/

        Font defaultFont = new Font("Times new roman", 14);
        int cntOfPages = 2;
        List<bool> checkSave = new List<bool>();

        int indexOfSubstring = 0;
        bool firstFind = false;

        ReplaceDialog replaceDialog = ReplaceDialog.CreateReplaceDialog();
        FindDialog findDialog = FindDialog.CreateFindDialog();

        public Form1()
        {
            InitializeComponent();
            textBox1.Font = defaultFont;
            textBox1.TextChanged += TextBox_TextChanged;
            checkSave.Add(false);

            tabPage1.Tag = 1; // преобразование тега в инт

            replaceDialog.PerformReplace += ReplaceDialog_PerformReplace;
            findDialog.PerformFind += FindDialog_PerformFind;
        }

        // Создание новой вкладки с textBox внутри
        void CreateNewFile(string filename, string text)
        {
            TabPage page;
            if (filename.Length > 0)
            {
                page = new TabPage(filename);
            }
            else
            {
              page = new TabPage($"New {cntOfPages}");
            }
            
            page.AutoScroll = true;
            page.Tag = cntOfPages++;
            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Font = defaultFont;
            textBox.Parent = page;
            textBox.TextChanged += TextBox_TextChanged;
            checkSave.Add(false);
            tabControl1.TabPages.Add(page);


            //EventArgs e = null;
            //TextBox_TextChanged(textBox, e);
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFile("","");
        }

        // Обработчик запускается в ответ на внесение изменений в текстовое поле
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            toolStatusBarLength.Text = "Length: " + tb.Text.Length;
            MatchCollection m = Regex.Matches(tb.Text, "\n");
            int rowsMatches = m.Count+1;
            toolStatusBarRowText.Text = "Rows: " + rowsMatches;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load";
            openFileDialog1.Filter = "Text files|*.txt";
            // Проверка существования выбранного файла
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                string filename = "";
                if (fi.Name.Length > 7)
                {
                    filename = fi.Name.Remove(7);
                    filename += "...";
                }
                else
                    filename = fi.Name;

                CreateNewFile(filename, File.ReadAllText(openFileDialog1.FileName));
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save file";
            saveFileDialog1.Filter = "Text files|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, tabControl1.SelectedTab.Controls[0].Text);
                checkSave[tabControl1.SelectedIndex] = true;
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save file";
            saveFileDialog1.Filter = "Text files|*.txt";
            int countOfSaves = 1;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StringBuilder fileName = new StringBuilder(saveFileDialog1.FileName);
                fileName.Remove(fileName.Length - 4, 4);
                foreach (TabPage item in tabControl1.TabPages)
                {
                   checkSave[countOfSaves-1] = true;
                   File.WriteAllText(fileName.ToString() + countOfSaves++ + '.' + saveFileDialog1.DefaultExt, item.Controls[0].Text);
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < checkSave.Count; i++)
            {
                if (!checkSave[i])
                {
                    FormCheckSaves formCheck = new FormCheckSaves();
                    formCheck.label = $"Вы хотите сохранить изменения\nв файле \"New: {i + 1}\"?";

                    // показать модальное окно
                    DialogResult result = formCheck.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        saveFileToolStripMenuItem_Click(sender, e);
                    }
                    if (result == DialogResult.Cancel)
                    {
                        formCheck.Close();
                    }
                    if (result == DialogResult.Abort)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        
        private void FindDialog_PerformFind(string findStr)
        {
            TextBox mainTextBox = tabControl1.SelectedTab.Controls[0] as TextBox;
            if(mainTextBox.Text.Length > 0)
            {
                if (!firstFind)
                {
                    indexOfSubstring = mainTextBox.Text.IndexOf(findStr, 0);
                    firstFind = true;
                    mainTextBox.SelectionStart = indexOfSubstring;
                    mainTextBox.SelectionLength = findStr.Length;
                    mainTextBox.Focus();
                    return;
                }


                indexOfSubstring = mainTextBox.Text.IndexOf(findStr, indexOfSubstring + findStr.Length);// startIndex = prevIndex + Length 
                if (indexOfSubstring > -1)
                {
                    mainTextBox.SelectionStart = indexOfSubstring;
                    mainTextBox.SelectionLength = findStr.Length;
                    mainTextBox.Focus();
                }
                else
                    firstFind = false;
            }
            
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findDialog.Show();
            findDialog.Focus();
        }


        private void ReplaceDialog_PerformReplace(string source, string replace)
        {
            if(source.Length > 0 && replace.Length > 0)
            {
                TextBox mainTextBox = tabControl1.SelectedTab.Controls[0] as TextBox;
                string doc = mainTextBox.Text;
                string newDoc = doc.Replace(source, replace);
                mainTextBox.Text = newDoc;

            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceDialog.Show();
            replaceDialog.Focus();
        }

        private void closeActivePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tabControl1.TabPages.Count == 0)
            {
                return;
            }
            if(!checkSave[(int)tabControl1.SelectedTab.Tag-1])
            {
                FormCheckSaves formCheck = new FormCheckSaves();
                formCheck.label = $"Вы хотите сохранить изменения\nв файле \"New: {(int)tabControl1.SelectedTab.Tag}\"?";

                // показать модальное окно
                DialogResult result = formCheck.ShowDialog();

                if (result == DialogResult.OK)
                {
                    saveFileToolStripMenuItem_Click(sender, e);
                }
                if (result == DialogResult.Cancel)
                {
                    formCheck.Close();
                }
                if (result == DialogResult.Abort)
                {
                    return;
                }
                checkSave[(int)tabControl1.SelectedTab.Tag - 1] = true;
            }
            tabControl1.SelectedTab.Dispose();
        }

        private void жопкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}