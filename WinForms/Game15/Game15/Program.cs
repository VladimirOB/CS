using Microsoft.VisualBasic.ApplicationServices;

namespace Game15
{
    class Program : WindowsFormsApplicationBase
    {
        [STAThread]
        static void Main(string[] arg)
        {
            ApplicationConfiguration.Initialize();
            Program p = new Program();
            p.Run(arg);
            //���� ��������� ���, ����� ��������� ����.
            //FormMenu menu = new FormMenu();
            //menu.Show();
            //Application.Run();

            //������ ��������� ����������, ���� ��� �������, ��������� ��.
            //Application.Run(new FormMenu(new FormGame(4)));
        }
        public Program()
        {
            this.EnableVisualStyles = true;
            this.ShutdownStyle = ShutdownMode.AfterAllFormsClose;
            this.MainForm = new FormMenu();
        }
    }
}