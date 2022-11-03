using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

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

        ReplaceDialog replaceDialog = ReplaceDialog.CreateReplaceDialog();
        FindDialog findDialog = FindDialog.CreateFindDialog();
        public Form1()
        {
            InitializeComponent();
            textBox1.Font = defaultFont;
            textBox1.TextChanged += TextBox_TextChanged;
            checkSave.Add(false);

            replaceDialog.PerformReplace += ReplaceDialog_PerformReplace;
            findDialog.PerformFind += FindDialog_PerformFind;
        }

        // Создание новой вкладки с textBox внутри
        void CreateNewFile(string text)
        {
            TabPage page = new TabPage($"New {cntOfPages++}");
            page.AutoScroll = true;
            page.Tag = cntOfPages;
            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Font = defaultFont;
            textBox.Parent = page;
            textBox.TextChanged += TextBox_TextChanged;
            checkSave.Add(false);
            //EventArgs e = null;
            //TextBox_TextChanged(textBox, e);

            //VScrollBar vScrollBar = new VScrollBar();
            //vScrollBar.Location = new System.Drawing.Point(705, 3);
            //vScrollBar.Name = $"vScrollBar{cntOfPages}";
            //vScrollBar.Size = new System.Drawing.Size(17, 509);
            //vScrollBar.TabIndex = cntOfPages;
            //vScrollBar.Parent = page;
            //vScrollBar.Dock = DockStyle.Right;
            tabControl1.TabPages.Add(page);
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFile("");
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
                CreateNewFile(File.ReadAllText(openFileDialog1.FileName));
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
                    int formCheckWidth = 550;
                    int formCheckHeight = 480;
                    FormCheckSaves formCheck = new FormCheckSaves();
                    formCheck.label = $"Вы хотите сохранить изменения\nв файле \"New: {i+1}\"?";

                    // показать модальное окно
                    DialogResult result = formCheck.ShowDialog();
                   
                    if (result == DialogResult.OK)
                    {
                        saveAllToolStripMenuItem_Click(sender, e);
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
            int indexOfSubstring = mainTextBox.Text.IndexOf(findStr);
            mainTextBox.SelectionStart = indexOfSubstring;
            mainTextBox.SelectionLength = findStr.Length;
            mainTextBox.Focus();

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findDialog.Show();
            findDialog.Focus();
        }


        private void ReplaceDialog_PerformReplace(string source, string replace)
        {
            TextBox mainTextBox = tabControl1.SelectedTab.Controls[0] as TextBox;
            string doc = mainTextBox.Text;
            string newDoc = doc.Replace(source, replace);
            mainTextBox.Text = newDoc;
            
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceDialog.Show();
            replaceDialog.Focus();
        }
    }
}