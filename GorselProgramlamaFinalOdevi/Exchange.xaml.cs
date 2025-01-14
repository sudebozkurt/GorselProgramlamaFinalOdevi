using GorselProgramlamaFinalOdevi.Models;
using System.Text.Json;

namespace GorselProgramlamaFinalOdevi;

public partial class Exchange : ContentPage
{
	public Exchange()
	{
		InitializeComponent();
	}

	private static Exchange instance;
	public static Exchange Page
	{
		get
		{
			if (instance == null)
				instance = new Exchange();
			return instance;
		}
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await Load();
    }

    async Task Load()
    {
        try
        {
            string jsondata = await getCurrentExchangeRates();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using JsonDocument doc = JsonDocument.Parse(jsondata);
            JsonElement root = doc.RootElement;

            var currencies = new List<Currency>();

            foreach (JsonProperty property in root.EnumerateObject())
            {
                if (property.Name != "Update_Date")
                {
                    string purchase = property.Value.TryGetProperty("Alýþ", out var purchaseValue)
                                      ? purchaseValue.GetString() : "N/A";
                    string sale = property.Value.TryGetProperty("Satýþ", out var saleValue)
                                  ? saleValue.GetString() : "N/A";
                    string difference = property.Value.TryGetProperty("Deðiþim", out var differenceValue)
                                        ? differenceValue.GetString() : "N/A";

                    var currency = new Currency
                    {
                        Name = property.Name,
                        Purchase = purchase,
                        Sale = sale,
                        Difference = difference
                    };
                    currencies.Add(currency);
                }
            }


            currency_list.ItemsSource = currencies;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

    }

    async Task<string> getCurrentExchangeRates()
    {
        string url = "https://finans.truncgil.com/today.json";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string jsondata = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsondata);
        return jsondata;
    }

    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        await Load();
    }

}