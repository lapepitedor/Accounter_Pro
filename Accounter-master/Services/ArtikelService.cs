using Accounter.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.ViewModels;
using Xamarin.Essentials;
using System.Formats.Asn1;

namespace Accounter.Services
{
    public class ArtikelService : IArtikelService
    {
        static SQLiteAsyncConnection dbConnection;
        // Initial-Artikel-Daten
        Artikel a7 = new Artikel() { ArtName = "Kaffeemaschine", PreisProTag = 10.0M, Image = "kaffeemaschine.png", Ausleihbar=true, Barcode=223323, Anzahllager=3, LagerPlatz="A3", BestandLimit=1, NaechstePruefDatum = new DateTime(2025, 5, 20) };
        Artikel a2 = new Artikel() { ArtName = "Wasser", PreisProTag = 0.0M, PreisGesamt = 25.0M , Image = "wasserflaschen.png", Ausleihbar = true, Barcode = 648323, Anzahllager = 15, LagerPlatz = "D2", BestandLimit = 10, AblaufsDatum=DateTime.Now, NaechstePruefDatum=new DateTime(2023,5,20)};
        Artikel a3 = new Artikel() { ArtName = "Stühle", PreisProTag = 40.0M, Image = "stuehle.png", Ausleihbar = true, Barcode = 223323, Anzahllager = 3, LagerPlatz = "A3", BestandLimit = 1 , NaechstePruefDatum = new DateTime(2024, 5, 20) };
        Artikel a4 = new Artikel() { ArtName = "Jbl Music Box", PreisProTag = 5.0M, Image = "jblmusicbox.png", Ausleihbar = true, Barcode = 274923, Anzahllager = 2, LagerPlatz = "A1", BestandLimit = 1 , NaechstePruefDatum = new DateTime(2025, 5, 20) };
        Artikel a5 = new Artikel() { ArtName = "Fussball", PreisProTag = 0.0M, Image = "fussball.png", Ausleihbar = true, Barcode = 836323, Anzahllager = 13, LagerPlatz = "A5", BestandLimit = 8 , NaechstePruefDatum = new DateTime(2023, 10, 14) };
        Artikel a6 = new Artikel() { ArtName = "Cola Kleinflaschen", PreisProTag = 40.0M, Image = "colaflaschen.png", Ausleihbar = false, Barcode = 639323, Anzahllager = 50, LagerPlatz = "D2", BestandLimit = 20, AblaufsDatum = DateTime.Now };
        Artikel a1 = new Artikel() { ArtName = "Blumenvase", PreisProTag = 2.0M, Image = "blumenvase.png", Ausleihbar = true, Barcode = 449323, Anzahllager = 3, LagerPlatz = "E5", BestandLimit = 1 , NaechstePruefDatum = new DateTime(2026, 3, 30) };
        public ArtikelService()
        {
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await Init();
            try
            {
                if (await dbConnection.Table<Artikel>().CountAsync() == 0)
                {
                    await AddArtikel(a1);
                    await AddArtikel(a2);
                    await AddArtikel(a3);
                    await AddArtikel(a4);
                    await AddArtikel(a5);
                    await AddArtikel(a6);
                    await AddArtikel(a7);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private async Task Init()
        {

            if (dbConnection != null)
            {
                return;
            }


            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Accounter.db");

            dbConnection = new SQLiteAsyncConnection(dbPath);

            await dbConnection.CreateTableAsync<Artikel>();
        }

        public async Task<List<Artikel>> GetArtikelList()
        {
            await Init();
            return await dbConnection.Table<Artikel>().ToListAsync();
        }

        public async Task UpdateArtikel(Artikel artikel)
        {
            await Init();

            await dbConnection.UpdateAsync(artikel);
        }
        public async Task AddArtikel(Artikel artikel)
        {
            await Init();

            await dbConnection.InsertAsync(artikel);
        }

        public async Task DeleteArtikel(Artikel artikel)
        {
            await Init();

            await dbConnection.DeleteAsync(artikel);
        }
        // Delete all Artikel
        public async Task DeleteAllArtikels()
        {
            await Init();
            await dbConnection.DeleteAllAsync<Artikel>();
        }
    }
}
