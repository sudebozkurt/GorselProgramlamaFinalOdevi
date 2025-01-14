using Firebase.Database;
using GorselProgramlamaFinalOdevi.Models;

namespace GorselProgramlamaFinalOdevi;

public partial class TodoList : ContentPage
{
    public TodoList()
    {
        InitializeComponent();

        BL.LoadNotes();

        toDoList.ItemsSource = BL.Notes;
    }

    static FirebaseClient client = new FirebaseClient("https://mauiappdeneme-default-rtdb.firebaseio.com/");


    void AddNote(Notes note)
    {
        BL.AddNote(note);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        var m = sender as Button;
        var note = BL.Notes.First(o => o.Id == m.CommandParameter.ToString());

        string result = await DisplayPromptAsync("Notu Düzenle", "Notunuzu giriniz: ", "Tamam", "Ýptal");

        if (result != null)
        {
            note.NoteContent = result;
            BL.EditNote(note);
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var m = sender as Button;
        var note = BL.Notes.First(o => o.Id == m.CommandParameter.ToString());

        bool b = await DisplayAlert("Silmeyi Onayla", $"{note.NoteContent} notu silinsin mi?", "Evet", "Hayýr");

        if (b)
        {
            BL.DeleteNote(note);

        }

    }


    private async void NoteAdd_Clicked(object sender, EventArgs e)
    {
        Notes note;

        string noteContent = await DisplayPromptAsync("Not Ekle", "Notunuzu giriniz: ", "Tamam", "Ýptal");

        if (noteContent != null)
        {
            note = new Notes()
            {
                NoteContent = noteContent
            };

            AddNote(note);
        }
    }
}