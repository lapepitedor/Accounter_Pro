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
using System.Numerics;


namespace Accounter.ViewModels
{
    public partial class DefektVM : BaseViewModel
    {
        [ObservableProperty]
        public int _artID;
        [ObservableProperty]
        public string _artName;
        [ObservableProperty]
        public int _anzahl;
        [ObservableProperty]
        public string _anmerkung;
        [ObservableProperty]
        public string _image;
        [ObservableProperty]
        public string _searchedName;
        //-----------------------------------------------
        [ObservableProperty]
        public int _artikelBestand;
        //-----------------------------------------------
        public ObservableCollection<Defekt> DefektListe { get; set; }
        public ObservableCollection<Defekt> SearchedDefektListe { get; set; }

        //-----------------------------------------------
        public IDefektService _defektService;
        public DefektVM(IDefektService defektService, Artikel artikel)
        {
            _defektService = defektService;
            ArtID = artikel.ArtID;
            ArtName = artikel.ArtName;
            Image = artikel.Image;
            ArtikelBestand = artikel.Anzahllager;
            DefektListe = new ObservableCollection<Defekt>();
            SearchedDefektListe = new ObservableCollection<Defekt>();
        }
        public DefektVM(IDefektService defektService)
        {
            Title = "Defekt";
            _defektService = defektService;
            DefektListe = new ObservableCollection<Defekt>();
            SearchedDefektListe = new ObservableCollection<Defekt>();
            _ = PerformSearch();
        }
        [RelayCommand]
        public async Task PerformSearch()
        {
            if (IsBusy) { return; }
            else if (string.IsNullOrEmpty(SearchedName))
            {
                await Aktualisieren();
                return;
            }
            else
            {
                IsBusy = true;
                SearchedDefektListe.Clear();

                foreach (var defekt in DefektListe)
                {
                    if (defekt.ArtName.ToString().ToLower().Contains(SearchedName.ToString().ToLower()))
                    {
                        SearchedDefektListe.Add(defekt);
                    }
                }
                IsBusy = false;
                return;
            }
        }
        public async Task Aktualisieren()
        {
            await GetDefektList();
            SearchedDefektListe.Clear();
            for (int i = 0; i < DefektListe.Count;)
            {
                SearchedDefektListe.Add(DefektListe[i]);
                i++;
            }
        }
        [RelayCommand]
        public async Task GetDefektList()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                var defektListe = await _defektService.GetDefektList();
                if (defektListe?.Count > 0)
                {
                    DefektListe.Clear();
                    foreach (var ausleihe in defektListe)
                    {
                        DefektListe.Add(ausleihe);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get Defekte: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //DeleteDefektCommand
        [RelayCommand]
        public async Task DeleteDefekt(Defekt defekt)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _defektService.DeleteDefekt(defekt);
                DefektListe.Remove(defekt);
                SearchedDefektListe.Remove(defekt);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to delete Defekt: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //UpdateDefektCommand
        [RelayCommand]
        public async Task UpdateDefekt(Defekt defekt)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _defektService.UpdateDefekt(defekt);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to update Defekt: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task AddDefekt()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                var defekt = new Defekt();
                defekt.ArtID = ArtID;
                defekt.ArtName = ArtName;
                defekt.Anzahl = Anzahl;
                defekt.Anmerkung = Anmerkung;
                defekt.Image = Image;
                await _defektService.AddDefekt(defekt);
                DefektListe.Add(defekt);
                SearchedDefektListe.Add(defekt);
                await Aktualisieren();
                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to add Defekt: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task Melden()
        {
            // Check if Artikel.Lagerbestand > Defekt.Anzahl
            if (Anzahl > ArtikelBestand)
            {
                await Shell.Current.DisplayAlert("Error!", $"Lagerbestand ist zu klein!", "OK");
                return;
            }
            else
            {
                await AddDefekt();
            }
        }
        [RelayCommand]
        public async Task Abbrechen()
        {
            await Shell.Current.GoToAsync("..");
        }
        
    }
}
