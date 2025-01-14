namespace GorselProgramlamaFinalOdevi;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
            Application.Current.UserAppTheme = AppTheme.Dark;
        else
            Application.Current.UserAppTheme = AppTheme.Light;
    }
}