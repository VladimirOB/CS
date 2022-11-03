namespace Web
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Location = new Point(1, 1);
            webBrowser.Size = new Size(1024, 768);
            webBrowser.Parent = this;
            webBrowser.Url = new Uri("https://youtube.com/");
        }
    }
}