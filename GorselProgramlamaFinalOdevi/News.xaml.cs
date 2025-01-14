using GorselProgramlamaFinalOdevi.Models;
using System.Diagnostics;
using System.Text.Json;
namespace GorselProgramlamaFinalOdevi;

public partial class News : ContentPage
{
    public News()
    {
        InitializeComponent();
        lstKategory.ItemsSource = haberKategoriList;

        selectedCategory = haberKategoriList[0];
    }

    List<HaberKategori> haberKategoriList = new List<HaberKategori>()
    {
         new HaberKategori(){Baslik = "Man�et", Link = "https://www.trthaber.com/manset_articles.rss"},
         new HaberKategori(){Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
         new HaberKategori(){Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
         new HaberKategori(){Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
         new HaberKategori(){Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
    };

    HaberKategori selectedCategory = null;

    private async void LoadNews(object sender, EventArgs e)
    {
        await Load();
        refreshView.IsRefreshing = false;
    }

    async Task Load()
    {
        try
        {
            string jsondata = await NewsService.GetNews(selectedCategory.Link);
            if (!string.IsNullOrEmpty(jsondata))
            {
                var haberler = JsonSerializer.Deserialize<HaberRoot>(jsondata);
                if (haberler != null && haberler.items != null)
                {
                    lstNews.ItemsSource = haberler.items;
                }
                else
                {
                    Debug.WriteLine("Haber verileri bo� d�nd�.");
                }
            }
            else
            {
                Debug.WriteLine("JSON verisi bo� d�nd�.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Hata: {ex.Message}");
        }
    }


    private async void lstKategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (lstKategory.SelectedItem is HaberKategori kategori)
        {
            selectedCategory = kategori;
            await Load();
        }
    }


    private void lstNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var url = (lstNews.SelectedItem as Item).link;
        NewsDetail page = new NewsDetail(url);
        Navigation.PushAsync(page);
    }

    public class HaberKategori
    {
        public string Baslik { get; set; }
        public string Link { get; set; }
    }
}