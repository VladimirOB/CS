using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MusicPlayer
{
    public delegate void newSong(string name);
    public partial class App : Application
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(int hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);



        //[DllImport("kernel32.dll", SetLastError = true)]
        //unsafe static extern bool WriteProcessMemory(
        //    IntPtr hProcess,
        //    void* IpBaseadress,
        //    void* IpBuffer,
        //    uint nSize,
        //    uint IpNumberOfBytesWritten
        //   );
        //[DllImport("kernel32.dll")]
        //public static extern IntPtr OpenProcess(
        //int dwDesiredAccess,
        //bool bInheritHandle,
        //int dwProcessId);


        //Запуск одно копии
        System.Threading.Mutex mut;
        public event newSong song;
        uint id = RegisterWindowMessage("MyUniqueMessageIdentifier");
        string memoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MusicPlayer";

        private void test()
        {
            //var mproc = Process.GetProcessesByName("MusicPlayer");
            //var proc = mproc[1];
            //int id = proc.Id;

            //string s = proc.ToString();
            //Console.WriteLine(s);
            //IntPtr hproc = OpenProcess(0x001F0FFF, false, id);
            ////IntPtr hproc = proc.Handle;
            //Console.WriteLine(hproc.ToString());
            //Console.WriteLine(proc.Id.ToString());

            //byte data = 0x1;
            //if (!WriteProcessMemory(hproc, (void*)0x400000, &data, 1, 0))
            //{
            //    Console.WriteLine("Error");
            //}
            //WriteProcessMemory(hproc, (void*)0x400000, &data, 1, 0);
            //WriteProcessMemory(hproc, (void*)0x400000, &data, 1, 0);
            //WriteProcessMemory(hproc, (void*)0x400000, &data, 1, 0);
            ////Process.LeaveDebugMode();
            //Console.ReadLine();
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool createdNew;
            string mutName = "MusicPlayer.exe";
            mut = new System.Threading.Mutex(true, mutName, out createdNew);

            if(!createdNew)
            {
                if (e.Args.Length == 1 &&
                    (e.Args[0].EndsWith(".mp3") || e.Args[0].EndsWith("wav") || e.Args[0].EndsWith(".mpbv")))
                {
                    try
                    {
                        IntPtr hz = FindWindow("MainWindow", "Music Player");
                        IntPtr HWND = new IntPtr();
                        System.Diagnostics.Process[] procLst = System.Diagnostics.Process.GetProcesses();
                        foreach (System.Diagnostics.Process p in procLst)
                        {
                            if (p.ProcessName == "MusicPlayer")
                            {
                                HWND = p.MainWindowHandle;
                                break;
                            }
                        }

                        System.IO.File.WriteAllText(memoryPath + "\\buffer.db", e.Args[0]);

                        IntPtr wParam = Marshal.StringToHGlobalAnsi(e.Args[0]);
                        IntPtr lParam = new IntPtr(e.Args[0].Length);
                        Debug.Assert(HWND != IntPtr.Zero);
                        SendMessage(HWND, id, wParam, lParam);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                Shutdown();
            }

            MainWindow wnd = new MainWindow();
            if (e.Args.Length == 1) 
            {
                if (e.Args[0].EndsWith("mpbv"))
                wnd.playlistPath = e.Args[0];
                else if (e.Args[0].EndsWith("mp3"))
                {
                    wnd.playlist.Add(e.Args[0]);
                }
            }
            double screenHeight = SystemParameters.FullPrimaryScreenHeight; // общая высота
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;  // общая ширина
            wnd.Top = (screenHeight - wnd.Height + 23); // расположение окна снизу справа
            wnd.Left = (screenWidth - wnd.Width);
            wnd.Show();
        }
    }
}
