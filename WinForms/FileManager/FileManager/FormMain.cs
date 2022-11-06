using System.IO;
using System.Runtime;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace FileManager
{
    /*1. Разработать программу "Файловый менеджер", которая имеет следующие функции:
    - показывать папки и файлы в treeView и listView 
    - показывать предпросмотр для картинок и текстовых файлов
    - реализовать создание текстовых файлов
    - копирование файлов и папок
    - удаление файлов и папок
    - переименование / перемещение файлов и папок
    - строка состояние внизу
    - верхнее / контекстное меню
    - поиск файлов и папок*/
    public partial class FormMain : Form, IMessageFilter
    {
        private ImageList imgList;
        int newFileCount = 1;

        //Временная переменная для хранения изображения (нужна, чтоб можно было удалить изображение)
        Bitmap imagePreview;

        //файлы в текущей папке
        FileInfo[] files;

        //текущая папка
        DirectoryInfo currentDirInfo;

        //буфферы для хранения данных о копируемых папках и файлах.
        string sourceDirCopyFullName;
        string sourceDirCopyName;
        string sourceDirCutFullName;
        string sourceDirCutName;
        List<string> copyBuffer = new List<string>();
        List<FileInfo> cutBuffer = new List<FileInfo>();
        public FormMain()
        {
            InitializeComponent();

            // перехват ввода в любое текстовое поле (добавить интерфейс IMessageFilter)
            Application.AddMessageFilter(this);

            InitFileView();
            CreateImageList();
            InitTreeView();

            timerTime.Start();
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 256)
            {
                Keys key = (Keys)(int)m.WParam;
                switch (key)
                {
                    case Keys.F3:
                        buttonCopy_Click(null, null);
                        break;
                    case Keys.F4:
                        buttonCut_Click(null, null);
                        break;
                    case Keys.F5:
                        buttonPaste_Click(null, null);
                        break;
                    case Keys.F6:
                        buttonDelete_Click(null, null);
                        break;

                    case Keys.F7:
                        buttonCreate_Click(null, null);
                        break;
                }
            }
            return false;   // true, если требуется "съесть" нажатия
        }

        void InitFileView()
        {
            try
            {
                fileListView.SmallImageList = new ImageList();
                fileListView.LargeImageList = new ImageList();
                fileListView.LargeImageList.ImageSize = new Size(48, 48);
                fileListView.LargeImageList.Images.Add(Bitmap.FromFile("../../../resources/note11.ico"));
                fileListView.SmallImageList.Images.Add(Bitmap.FromFile("../../../resources/note11.ico"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при работе со списком изображений", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        void CreateImageList()
        {
            // Создание списка изображений 
            imgList = new ImageList();

            // Добавление иконок в список изображений
            imgList.Images.Add(Bitmap.FromFile("../../../resources/CLSDFOLD.ICO"));
            imgList.Images.Add(Bitmap.FromFile("../../../resources/OPENFOLD.ICO"));
            imgList.Images.Add(Bitmap.FromFile("../../../resources/NOTE11.ICO"));
            imgList.Images.Add(Bitmap.FromFile("../../../resources/NOTE12.ICO"));
            imgList.Images.Add(Bitmap.FromFile("../../../resources/Drive01.ico"));
        }

        void InitTreeView()
        {
            dirTreeView.ImageList = imgList;

            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Создание узла и иконок
                TreeNode node = new TreeNode(drive, 4, 4);

                // Добавили готовый узел к дереву
                dirTreeView.Nodes.Add(node);

                // Заполнение узлов с дисками
                FillByDirectories(node);
            }
        }

        private void FillByDirectories(TreeNode node)
        {
            try
            {
                // В node.FullPath - находится полный путь к ветке
                DirectoryInfo dirInfo = new DirectoryInfo(node.FullPath);

                // Получение информации о каталогах
                DirectoryInfo[] dirs = dirInfo.GetDirectories();

                foreach (DirectoryInfo dir in dirs)
                {
                    TreeNode tree = new TreeNode(dir.Name, 0, 1);
                    node.Nodes.Add(tree);
                }
            }
            // Исключение (дисковод без диска)
            catch { }
        }

        // Метод запускается ДО открытия узловой ветки дерева
        // sender - ссылка на дерево. e - параметры метода
        private void dirTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Запрет постоянной перерисовки дерева во время добавления элементов
            dirTreeView.BeginUpdate();
            
            try
            {
                //Перебор всех дочерних узлов для узла(разворачивается по +)
                foreach (TreeNode node in e.Node.Nodes)
                {
                    FillByDirectories(node);
                }
            }
            catch { }
            // возврат режима обычного обновления дерева (сразу вызывает перерисовку дерева)
            dirTreeView.EndUpdate();
        }

        private void dirTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                FillByFiles(e.Node.FullPath);
            }
            catch { }
        }

        private void FillByFiles(string path)
        {
            fileListView.BeginUpdate();
            fileListView.Items.Clear();

            statusBarLabel.Text = path;
            currentDirInfo = new DirectoryInfo(path);

            files = currentDirInfo.GetFiles();
            // Обработка информации
            fileListView.LargeImageList.Images.Clear();
            fileListView.SmallImageList.Images.Clear();
            int iconIndex = 0;
            fileListView.LargeImageList.Images.Add(Bitmap.FromFile("../../../resources/note11.ico"));
            fileListView.SmallImageList.Images.Add(Bitmap.FromFile("../../../resources/note11.ico"));

            foreach (FileInfo file in files)
            {
                ListViewItem item = new ListViewItem(file.Name);

                // Получить иконку для текущего файла
                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);
                fileListView.LargeImageList.Images.Add(icon);
                fileListView.SmallImageList.Images.Add(icon);
                iconIndex++;

                //Указать номер иконки для listView
                item.ImageIndex = iconIndex;

                //добавить пункт в listView
                item.SubItems.Add(file.LastWriteTime.ToString());
                item.SubItems.Add(file.Length.ToString());
                fileListView.Items.Add(item);
            }
            fileListView.EndUpdate();
        }

        private void fileListView_MouseClick(object sender, MouseEventArgs e)
        {
            previewPanel.Controls.Clear();
            foreach (ListViewItem file in fileListView.SelectedItems)
            {
                if (file.Text.Contains(".txt"))
                {
                    TextBox tbPreview = new TextBox();
                    tbPreview.Multiline = true;
                    tbPreview.Parent = previewPanel;
                    tbPreview.Size = previewPanel.Size;
                    tbPreview.Location = previewPanel.Location;
                    tbPreview.Dock = DockStyle.Fill;
                    tbPreview.ScrollBars = ScrollBars.Both;
                    tbPreview.Text = File.ReadAllText(files[file.Index].FullName);
                }
                if (file.Text.Contains(".jpg") || file.Text.Contains(".png") || file.Text.Contains(".bmp"))
                {
                    imagePreview = new Bitmap(files[file.Index].FullName);

                    PictureBox picturePreview = new PictureBox();
                    picturePreview.Parent = previewPanel;
                    picturePreview.BackgroundImage = imagePreview;
                    picturePreview.BackgroundImageLayout = ImageLayout.Zoom;
                    picturePreview.Size = previewPanel.Size;
                    picturePreview.Location = previewPanel.Location;
                    picturePreview.Dock = DockStyle.Fill;
                }
            }
        }

        //создание текстового файла
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (fileListView.Focus())
            {
                File.WriteAllText(currentDirInfo.FullName + $"\\New text file {newFileCount++}.txt", "");
            }
            FillByFiles(currentDirInfo.FullName);
        }

        //Переименование файла
        private void fileListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
                e.CancelEdit = true;
            else
            {
                try
                {
                    string source = currentDirInfo.FullName + "\\" + fileListView.SelectedItems[0].Text;
                    string destin = currentDirInfo.FullName + "\\" + e.Label.ToString();
                    if (File.Exists(source))
                    {
                        File.Move(source, destin, true);
                    }
                }
                catch
                {
                    e.CancelEdit = true;
                }
                FillByFiles(currentDirInfo.FullName);
            }
        }

        //Переименование папки
        private void dirTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                e.CancelEdit = true;
            else
            {
                string source = currentDirInfo.FullName;
                string destin = currentDirInfo.FullName.Replace(currentDirInfo.Name, "") + e.Label.ToString();
                try
                {
                    if (Directory.Exists(source))
                    {
                        Directory.Move(source, destin);
                        dirTreeView.SelectedNode.Text = e.Label.ToString();
                    }
                }
                catch
                {
                    e.CancelEdit = true;
                }
                FillByFiles(destin);
            }
        }

        //копирование файлов / папки
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            copyBuffer.Clear();
            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem file in fileListView.SelectedItems)
                {
                    copyBuffer.Add(files[file.Index].Name);
                }
                sourceDirCopyFullName = currentDirInfo.FullName;
                sourceDirCopyName = currentDirInfo.Name;
                return;
            }
            if (dirTreeView.Focus())
            {
                sourceDirCopyFullName = dirTreeView.SelectedNode.FullPath.Replace("\\\\", "\\");
                sourceDirCopyName = dirTreeView.SelectedNode.Text;
            }
        }

        private void buttonCut_Click(object sender, EventArgs e)
        {
            cutBuffer.Clear();
            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem file in fileListView.SelectedItems)
                {
                    cutBuffer.Add(files[file.Index]);
                }
                sourceDirCutFullName = currentDirInfo.FullName;
                sourceDirCutName = currentDirInfo.Name;
                return;
            }
            else if (dirTreeView.Focus())
            {
                sourceDirCutFullName = dirTreeView.SelectedNode.FullPath;
                sourceDirCutName = dirTreeView.SelectedNode.Text;
            }
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {

            if (copyBuffer.Count > 0)
            {
                foreach (var file in copyBuffer)
                {
                    if (!sourceDirCopyFullName.Equals(currentDirInfo.FullName))
                        File.Copy(Path.Combine(sourceDirCopyFullName, file), Path.Combine(currentDirInfo.FullName, file), true);
                }
            }
            else if(cutBuffer.Count > 0)
            {
                foreach (var file in cutBuffer)
                {
                    if (!sourceDirCutFullName.Equals(currentDirInfo.FullName))
                        File.Move(file.FullName, currentDirInfo.FullName + "\\" +  file.Name);
                }
            }
            else if(sourceDirCopyFullName != null)
            {
                //проверка чтоб сам в себя не копировал
                if(!sourceDirCopyFullName.Replace(sourceDirCopyName, "").Equals(currentDirInfo.FullName) &&
                    !sourceDirCopyFullName.Equals(currentDirInfo.FullName))
                CopyDir(sourceDirCopyFullName, currentDirInfo.FullName + "\\" + sourceDirCopyName);
                
            }
            else if (sourceDirCutFullName != null)
            {
                //проверка чтоб сам в себя не копировал
                if (!sourceDirCutFullName.Replace(sourceDirCutName, "").Equals(currentDirInfo.FullName) &&
                    !sourceDirCutFullName.Equals(currentDirInfo.FullName))
                {
                    DirectoryInfo destDir = new DirectoryInfo(currentDirInfo.FullName + "\\" + sourceDirCutName);
                    if (destDir.Exists)
                        destDir.Delete(true);
                    new DirectoryInfo(sourceDirCutFullName).MoveTo(destDir.FullName);
                }
            }
            FillByDirectories(dirTreeView.SelectedNode);
            FillByFiles(currentDirInfo.FullName);
        }

        //рекурсивный метод для копирования папок
        void CopyDir(string from, string to)
        {  
            DirectoryInfo newDir = new DirectoryInfo(to);
            if (newDir.Exists == false)
                newDir.Create();
            foreach (string s1 in Directory.GetFiles(from))
            {
                string s2 = to + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(from))
            {
                CopyDir(s, to + "\\" + Path.GetFileName(s));
            }
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            statusBarTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            statusBarTime.Margin = new Padding(Width - 120, 3, 0, 2);

            resizeButtons();
        }

        private void resizeButtons()
        {
            //ширина кнопки равна ширина окна / на кол-во кнопок - зазоры.
            int newWidthButton = Width / 6;

            buttonRename.Size = new Size(newWidthButton, 25);
            buttonRename.Location = new Point(0, 3);

            buttonCopy.Size = new Size(newWidthButton, 25);
            buttonCopy.Location = new Point(newWidthButton, 3);

            buttonCut.Size = new Size(newWidthButton, 25);
            buttonCut.Location = new Point(newWidthButton * 2, 3);

            buttonPaste.Size = new Size(newWidthButton, 25);
            buttonPaste.Location = new Point(newWidthButton * 3, 3);

            buttonDelete.Size = new Size(newWidthButton, 25);
            buttonDelete.Location = new Point(newWidthButton * 4, 3);

            buttonCreate.Size = new Size(newWidthButton, 25);
            buttonCreate.Location = new Point(newWidthButton * 5, 3);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            imagePreview.Dispose();
            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem file in fileListView.SelectedItems)
                {
                    File.Delete(files[file.Index].FullName);
                }
            }
            else if (dirTreeView.Focus() && dirTreeView.SelectedNode.Nodes.Count > 0)
            {
                Directory.Delete(dirTreeView.SelectedNode.FullPath, true);
            }
            FillByDirectories(dirTreeView.SelectedNode);
            FillByFiles(currentDirInfo.FullName);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonCut_Click(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonCopy_Click(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonPaste_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonDelete_Click(sender, e);
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDialog dialog = new FormDialog("Helper");
            Button buttonOk = new Button();
            Label label = new Label();
            label.Parent = dialog;
            label.Size = new Size(dialog.Width, dialog.Height - 75);
            label.Location = new Point(1, 1);
            label.Font = new Font("Times new roman", 14);
            label.Text = "Here you will see tips for the file manager!\n" +
                "\nPress F2 to rename file (Don't work xD)\n" +
                "\nPress F3 to copy files or folders\n" +
                "\nPress F4 to cut files or folders\n" +
                "\nPress F5 to insert files or folders\n" +
                "\nPress F6 to delete any files or folders\n" +
                "\nPress F7 to create text file\n" +
                "\nOr click on the button on the panel below\n" +
                "\nOr click RBM and select from the menu";

            buttonOk.BringToFront();
            buttonOk.Parent = dialog;
            buttonOk.Size = new Size(75, 25);
            buttonOk.Location = new Point(dialog.Width / 2 -37, dialog.Height - 75);
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            // показать модальное окно
            DialogResult result = dialog.ShowDialog();
            //MessageBox.Show("В стадии разработки...");
        }

        private void createTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonCreate_Click(sender, e);
        }

       
    }
}