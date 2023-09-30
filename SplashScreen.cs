using CefSharp.WinForms;
using CefSharp;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ComparedLyric
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private async void SplashScreen_Load(object sender, EventArgs e)
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            Cef.Initialize(settings);

            await OpenMainViewAfterDelay();
        }

        private async Task OpenMainViewAfterDelay()
        {
            await Task.Delay(3000);

            MainView mainpage = new MainView();
            mainpage.FormClosed += (s, args) => this.Close();
            mainpage.Show();
            Hide();
        }

    }
}
