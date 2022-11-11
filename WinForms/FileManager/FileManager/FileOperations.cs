using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class FileOperations
    {
        //текущая папка
        public DirectoryInfo currentDirInfo;

        //файлы в текущей папке
        public FileInfo[] files;

        //текущее имя файла до переименования
        public string currentLabelFileName;

        //проверяет операцию копирование : вырезание
        public bool isCopy;

        public int newFileCount = 1;

        public bool RenameFile(string fileListSelectedItemText, string eLabel)
        {
            try
            {
                string source = currentDirInfo.FullName + "\\" + fileListSelectedItemText;
                string destin = currentDirInfo.FullName + "\\" + eLabel;
                if (File.Exists(source))
                {
                    File.Move(source, destin, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string RenameFolder(string eLabel)
        {
            string source = currentDirInfo.FullName;
            string destin = currentDirInfo.FullName.Replace(currentDirInfo.Name, "") + eLabel;
            if (Directory.Exists(source))
            {
                Directory.Move(source, destin);
            }
            return destin;
        }

        public void CopyPaste(string fileName)
        {
            FileAttributes attr = File.GetAttributes(fileName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo sourceDir = new DirectoryInfo(fileName);
                if (!fileName.Equals(currentDirInfo.FullName))
                    CopyDir(fileName, currentDirInfo.FullName + "\\" + sourceDir.Name);
            }
            else
            {
                FileInfo file = new FileInfo(fileName);
                File.Copy(fileName, currentDirInfo.FullName + "\\" + file.Name);
            }
        }

        //рекурсивный метод для копирования папок и подпапок
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

        public void CutPaste(string fileName)
        {
            FileAttributes attr = File.GetAttributes(fileName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                //проверка чтоб сам в себя не копировал
                if (!fileName.Replace(fileName, "").Equals(currentDirInfo.FullName))
                {
                    DirectoryInfo sourceDir = new DirectoryInfo(fileName);

                    DirectoryInfo destDir = new DirectoryInfo(currentDirInfo.FullName + "\\" + sourceDir.Name);
                    if (destDir.Exists)
                        destDir.Delete(true);
                    new DirectoryInfo(fileName).MoveTo(destDir.FullName);
                }
            }
            else
            {
                FileInfo file = new FileInfo(fileName);
                if (!fileName.Equals(currentDirInfo.FullName))
                    File.Move(fileName, currentDirInfo.FullName + "\\" + file.Name);
            }
        }

        public void Delete(string fileName)
        {
            FileAttributes attr = File.GetAttributes(fileName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.Delete(fileName, true);
            }
            else
            {
                File.Delete(fileName);
            }
        }

        public void DirTreeDragDrog(string fileName, string to)
        {
            FileAttributes attr = File.GetAttributes(fileName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo sourceDir = new DirectoryInfo(fileName);
                if (!fileName.Equals(currentDirInfo.FullName))
                    CopyDir(fileName, to);
            }
            else
            {
                FileInfo file = new FileInfo(fileName);
                File.Copy(fileName, to);
            }
        }
    }
}
