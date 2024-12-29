namespace TriviaGame
{
    public partial class AppShell : Shell
    {
        private bool userMute = false;

        public AppShell()
        {
            InitializeComponent();
        }

        private void VolumeTool_Clicked(object sender, EventArgs e)
        {
            if (!userMute)
            {
                TriviaGame.Audio.Muted = true;
                if(Application.Current.RequestedTheme == AppTheme.Light)
                {
                    VolumeTool.IconImageSource = "offlight.png";
                }
                else
                {
                    VolumeTool.IconImageSource = "offdark.png";
                }
                userMute = true;
            }
            else
            {
                TriviaGame.Audio.Muted = false;
                if (Application.Current.RequestedTheme == AppTheme.Light)
                {
                    VolumeTool.IconImageSource = "volumelight.png";
                }
                else
                {
                    VolumeTool.IconImageSource = "volumedark.png";
                }
                userMute = false;
            }
        }
    }
}
