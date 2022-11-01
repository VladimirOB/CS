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
            //Если запускать так, можно закрывать окно.
            //FormMenu menu = new FormMenu();
            //menu.Show();
            //Application.Run();

            //Запуск основного приложения, если его закрыть, закроется всё.
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