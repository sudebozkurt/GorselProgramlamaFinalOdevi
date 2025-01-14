using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProgramlamaFinalOdevi.Models
{
    internal static class DL
    {
        static FirebaseClient client = new FirebaseClient("https://finalodevi-c8698-default-rtdb.europe-west1.firebasedatabase.app/");

        public static bool AddNote(Notes note)
        {
            client.Child($"mauiappdeneme/{note.Id}").PutAsync(note);
            return true;
        }

        public static bool EditNote(Notes note)
        {
            client.Child($"mauiappdeneme/{note.Id}").PutAsync(note);
            return true;
        }

        public static bool DeleteNote(Notes note)
        {
            client.Child($"mauiappdeneme/{note.Id}").DeleteAsync();
            return true;
        }

        public static ObservableCollection<Notes> GetAllNotes()
        {
            ObservableCollection<Notes> notes = new ObservableCollection<Notes>();
            var _notes = client.Child("mauiappdeneme").OnceAsync<Notes>();

            foreach (var n in _notes.Result)
            {
                notes.Add(n.Object);
            }

            return notes;
        }
    }
}
