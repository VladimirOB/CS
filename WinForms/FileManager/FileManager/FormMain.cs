using System.Collections.Specialized;
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
    - поиск файлов и папок
    - добавить для операций копирования и перемещения возможность использования Clipboard и Drag-N-Drop*/
    public partial class FormMain : Form, IMessageFilter
    {
        private ImageList imgList;
        //Временная переменная для хранения изображения (нужна, чтоб можно было удалить изображение)
        Bitmap imagePreview;
        PictureBox picturePreview;

        TreeNode tempNode;

        FileOperations fileOp;

        public FormMain()
        {
            InitializeComponent();

            // перехват ввода в любое текстовое поле (добавить интерфейс IMessageFilter)
            Application.AddMessageFilter(this);
            fileOp = new FileOperations();
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
                    case Keys.F2:
                        buttonRename_Click(null, null);
                        break;
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
                tempNode = e.Node;
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
            fileOp.currentDirInfo = new DirectoryInfo(path);

            fileOp.files = fileOp.currentDirInfo.GetFiles();
            // Обработка информации
            fileListView.LargeImageList.Images.Clear();
            fileListView.SmallImageList.Images.Clear();
            int iconIndex = 0;
            fileListView.LargeImageList.Images.Add(Bitmap.FromFile("../../../resources/note11.ico"));
            fileListView.SmallImageList.Images.Add(Bitmap.FromFile("../../../resources/note11.ico"));

            foreach (FileInfo file in fileOp.files)
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
                    tbPreview.Text = File.ReadAllText(fileOp.files[file.Index].FullName);
                }
                if (file.Text.Contains(".jpg") || file.Text.Contains(".png") || file.Text.Contains(".bmp"))
                {
                    imagePreview = new Bitmap(fileOp.files[file.Index].FullName);
                    /*FileStream stream = new FileStream(@"c:\temp\admin.gif", FileMode.Open, FileAccess.Read);
			        Bitmap bmp = (Bitmap)Bitmap.FromStream(stream);
                    stream.Close();*/
                    picturePreview = new PictureBox();
                    picturePreview.Name = fileOp.files[file.Index].Name;
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
                File.WriteAllText(fileOp.currentDirInfo.FullName + $"\\New text file {fileOp.newFileCount++}.txt", "");
            }
            FillByFiles(fileOp.currentDirInfo.FullName);
        }

        private void fileListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            fileOp.currentLabelFileName = fileListView.SelectedItems[0].Text;
        }

        //Переименование файла
        private void fileListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (fileOp.currentLabelFileName != null)
            {
                //если изображение выведено в превью, удалить его.
                if(picturePreview != null)
                if (picturePreview.Name.Equals(fileListView.SelectedItems[0].Text))
                    imagePreview.Dispose();

                if (fileOp.RenameFile(fileListView.SelectedItems[0].Text, fileOp.currentLabelFileName))
                FillByFiles(fileOp.currentDirInfo.FullName);
            }
        }

        //Переименование папки
        private void dirTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                e.CancelEdit = true;
            else
            {
                try
                {
                    string destin = fileOp.RenameFolder(e.Label);

                    dirTreeView.SelectedNode.Text = e.Label.ToString();
                    FillByFiles(destin);
                    
                }
                catch
                {
                    e.CancelEdit = true;
                }
            }
        }

        //копирование файлов / папки
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            StringCollection str = new StringCollection();

            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem file in fileListView.SelectedItems)
                {
                    //если изображение выведено в превью, удалить его.
                    if (picturePreview != null)
                        if (picturePreview.Name.Equals(file.Text))
                            imagePreview.Dispose();

                    str.Add(fileOp.currentDirInfo.FullName + "\\" +  file.Text);
                }
            }
            else if (dirTreeView.Focus())
            {
                str.Add(fileOp.currentDirInfo.FullName);
            }
            fileOp.isCopy = true;
            Clipboard.SetFileDropList(str);
        }

        private void buttonCut_Click(object sender, EventArgs e)
        {
            StringCollection str = new StringCollection();
            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem file in fileListView.SelectedItems)
                {
                    //если изображение выведено в превью, удалить его.
                    if (picturePreview != null)
                        if (picturePreview.Name.Equals(file.Text))
                            imagePreview.Dispose();

                    str.Add(fileOp.currentDirInfo.FullName + "\\" + file.Text);
                }
                Clipboard.SetFileDropList(str);
                return;
            }
            else if (dirTreeView.Focus())
            {
                str.Add(fileOp.currentDirInfo.FullName);
                Clipboard.SetFileDropList(str);
            }
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            IDataObject obj = Clipboard.GetDataObject();
            if(obj.GetDataPresent(DataFormats.FileDrop))
            {
                StringCollection files = Clipboard.GetFileDropList();

                if(fileOp.isCopy)
                {
                    foreach (var file in files)
                    {
                        fileOp.CopyPaste(file);
                    }
                    fileOp.isCopy = false;
                }
                else
                {
                    foreach (var file in files)
                    {
                        fileOp.CutPaste(file);
                    }
                }
            }
            FillByDirectories(dirTreeView.SelectedNode);
            FillByFiles(fileOp.currentDirInfo.FullName);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (imagePreview != null)
                imagePreview.Dispose();

            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem file in fileListView.SelectedItems)
                {
                    if(MessageBoxDialogYesNo("Delete " + file.Text + "?"))
                    {
                        fileOp.Delete(fileOp.currentDirInfo.FullName + "\\" + file.Text);
                    }
                    
                }
            }
            else if (dirTreeView.Focus())
            {
                if (MessageBoxDialogYesNo("Delete " + dirTreeView.SelectedNode.FullPath + "?"))
                {
                    fileOp.Delete(dirTreeView.SelectedNode.FullPath);
                }
            }
            // предыдущий узел?
            //FillByDirectories(tempNode);
            //FillByFiles(tempNode.FullPath);
        }

        private bool MessageBoxDialogYesNo(string message)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;

            DialogResult result = MessageBox.Show(message, "Warning!", btn);

            if(result == DialogResult.OK)
                return true;
            else
                return false;
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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            fileListView_BeforeLabelEdit(null, null);
            fileListView_AfterLabelEdit(null, null);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillByDirectories(dirTreeView.SelectedNode);
            FillByFiles(fileOp.currentDirInfo.FullName);
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

        private void createTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonCreate_Click(sender, e);
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
                "\nPress F3 to copy fileOperationsfiles or folders\n" +
                "\nPress F4 to cut fileOperationsfiles or folders\n" +
                "\nPress F5 to insert fileOperationsfiles or folders\n" +
                "\nPress F6 to delete any fileOperationsfiles or folders\n" +
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
        private void searchBetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDialog window = new FormDialog("Search");
            window.Width = 300;
            window.Height = 150;

            TextBox textBox = new TextBox();
            textBox.Parent = window;
            textBox.Size = new Size(window.Width-30, 50);
            textBox.Location = new Point(3, 31);
            Label label = new Label();
            label.Parent = window;
            label.Size = new Size(window.Width, window.Height-50);
            label.Location = new Point(1, 1);
            label.Font = new Font("Times new roman", 14);
            label.Text = "Enter шо искать:";
            DialogResult result = window.ShowDialog();
        }

        private void fileListView_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void fileListView_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] tempFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                //вытягиваем из массива строк по 1 имени и закидываем в папку
                for (int i = 0; i < tempFiles.Length; i++)
                {
                    fileOp.CopyPaste(tempFiles[i]);
                }
                FillByDirectories(dirTreeView.SelectedNode);
                FillByFiles(fileOp.currentDirInfo.FullName);
            }
        }

        private void fileListView_DoubleClick(object sender, EventArgs e)
        {
            if (fileListView.Focus() && fileListView.SelectedItems.Count > 0)
            {
                TextBox tb = new TextBox();
                tb.Parent = fileListView;
                tb.Location = fileListView.SelectedItems[0].Position;
                tb.Size = new Size(120, 15);
            }
        }
    }
}