using Accounter.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.Models;
using Accounter.Services;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Accounter.ViewModels
{
    public partial class AusleiheVM :BaseViewModel
    {
        public IAusleiheService _ausleiheService;
        [ObservableProperty]
        public string _artName;
        [ObservableProperty]
        public string _image;
        [ObservableProperty]
        public int _artID;
        [ObservableProperty]
        public DateTime _ausleiheDatum;
        [ObservableProperty]
        public DateTime _AbgabeFrist;
        [ObservableProperty]
        public int _kundID;
        [ObservableProperty]
        public int _artAnzahl;

        //-----------------------------------------------
        [ObservableProperty]
        public string _searchedWord;
        public ObservableCollection<Ausleihe> AusleiheListe { get; set; }
        public ObservableCollection<Ausleihe> SearchedAusleiheListe { get; set; }
        //-----------------------------------------------
        public AusleiheVM(IAusleiheService ausleiheService,Artikel artikel)
        {
            _ausleiheService = ausleiheService;
            ArtName = artikel.ArtName.ToString();
            Image = artikel.Image;
            ArtID = artikel.ArtID;
            AusleiheDatum = DateTime.Now;
            AbgabeFrist = DateTime.Now.AddDays(7);
            ArtName = artikel.ArtName;
        }
        public AusleiheVM(IAusleiheService ausleiheService)
        {
            _ausleiheService = ausleiheService;
            Title = "Ausleihe";
            AusleiheListe = new ObservableCollection<Ausleihe>();
            SearchedAusleiheListe = new ObservableCollection<Ausleihe>();
            _ = PerformSearch();
        }
        [RelayCommand]
        public async Task ArtikelAusleihen()
        {
            if(AusleiheDatum > AbgabeFrist)
            {
                await Shell.Current.DisplayAlert("Fehler", "Das Ausleihedatum darf nicht größer als die Abgabefrist sein", "OK");
                return;
            }
            if(ArtAnzahl <= 0)
            {
                await Shell.Current.DisplayAlert("Fehler", "Die Anzahl darf nicht kleiner als 1 sein", "OK");
                return;
            }
            if (KundID <= 0) 
            {
                await Shell.Current.DisplayAlert("Fehler", "Die Kunden-ID darf nicht leer sein", "OK");
                return;
            }
            var ausleihe = new Ausleihe()
            {
                ArtID = (int)ArtID,
                AusleiheDatum = AusleiheDatum,
                AbgabeFrist = AbgabeFrist,
                KundID = KundID,
                ArtAnzahl = ArtAnzahl,
                Image = Image,
                ArtName = ArtName
            };
            await _ausleiheService.AddAusleihe(ausleihe);
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task Abbrechen()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task PerformSearch()
        {
            if (IsBusy) { return; }
            else if (string.IsNullOrEmpty(SearchedWord))
            {
                await Aktualisieren();
                return;
            }
            else
            {
                IsBusy = true;
                SearchedAusleiheListe.Clear();

                foreach (var ausleihe in AusleiheListe)
                {
                    if (ausleihe.ArtName.ToLower().Contains(SearchedWord.ToLower()))
                    {
                        SearchedAusleiheListe.Add(ausleihe);
                    }
                }
                IsBusy = false;
                return;
            }

        }
        public async Task Aktualisieren()
        {
            await GetAusleiheList();
            SearchedAusleiheListe.Clear();
            for (int i = 0; i < AusleiheListe.Count;)
            {
                SearchedAusleiheListe.Add(AusleiheListe[i]);
                i++;
            }
        }
        [RelayCommand]
        public async Task GetAusleiheList()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                var ausleiheListe = await _ausleiheService.GetAusleiheList();
                if (ausleiheListe?.Count > 0)
                {
                    AusleiheListe.Clear();
                    foreach (var ausleihe in ausleiheListe)
                    {
                        AusleiheListe.Add(ausleihe);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get Ausleihe: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //DeleteArtikelCommand
        [RelayCommand]
        public async Task DeleteArtikel(Ausleihe ausleihe)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _ausleiheService.DeleteAusleihe(ausleihe);
                AusleiheListe.Remove(ausleihe);
                SearchedAusleiheListe.Remove(ausleihe);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to delete Ausleihe: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //UpdateAusleiheCommand
        [RelayCommand]
        public async Task UpdateAusleihe(Ausleihe ausleihe)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _ausleiheService.UpdateAusleihe(ausleihe);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to update Ausleihe: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
