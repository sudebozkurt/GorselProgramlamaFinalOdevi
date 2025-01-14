namespace GorselProgramlamaFinalOdevi;

public partial class Weather : ContentPage
{
	public Weather()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, System.EventArgs e)
    {
        string sehir = textBox.Text;
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('�', 'C');
        sehir = sehir.Replace('�', 'G');
        sehir = sehir.Replace('�', 'I');
        sehir = sehir.Replace('�', 'O');
        sehir = sehir.Replace('�', 'U');
        sehir = sehir.Replace('�', 'S');

        string imageUrl = $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={sehir}&basla=1&bitir=5&rC=111&rZ=fff";


        ImageButton newImage = new ImageButton
        {
            Source = imageUrl,
            HeightRequest = 200,
            WidthRequest = 400

        };

        Label text = new Label
        {
            Text = sehir,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 30,

        };



        newImage.Clicked += DeleteButton_Clicked;
        newImage.IsVisible = true;
        text.IsVisible = true;
        Microsoft.Maui.Controls.StackLayout imageLayout = new Microsoft.Maui.Controls.StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Spacing = 10
        };
        imageLayout.Children.Add(text);
        imageLayout.Children.Add(newImage);


        imageStackLayout.Children.Add(imageLayout);

    }

    private void DeleteButton_Clicked(object sender, System.EventArgs e)
    {
        ImageButton deleteButton = (ImageButton)sender;
        Microsoft.Maui.Controls.StackLayout imageLayout = (Microsoft.Maui.Controls.StackLayout)deleteButton.Parent;
        imageStackLayout.Children.Remove(imageLayout);
    }
}