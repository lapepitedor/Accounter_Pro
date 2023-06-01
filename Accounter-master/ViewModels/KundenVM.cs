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
    public partial class KundenVM : BaseViewModel
    {
        // Eigenschaften
        [ObservableProperty]
        public string _kundName;
        [ObservableProperty]
        public int _matrik;
        [ObservableProperty]
        public string _email;
        [ObservableProperty]
        public string _vermerk;
        [ObservableProperty]
        public string _searchedName;
        public ObservableCollection<Kunde> KundenListe { get; set; }
        public ObservableCollection<Kunde> SearchedKundenListe { get; set; }

        //-----------------------------------------------
        public IKundenService _kundeService;
        public KundenVM(IKundenService kundeService)
        {
            Title = "Kunden";
            _kundeService = kundeService;
            KundenListe = new ObservableCollection<Kunde>();
            SearchedKundenListe = new ObservableCollection<Kunde>();
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
                try
                {
                    IsBusy = true;
                    SearchedKundenListe.Clear();
                    var kunden = await _kundeService.GetKundenList();
                    foreach (var kunde in kunden)
                    {
                        if (kunde.KundName.ToLower().Contains(SearchedName.ToLower()))
                        {
                            SearchedKundenListe.Add(kunde);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    await Shell.Current.DisplayAlert("Fehler", "Fehler beim Suchen", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        public async Task Aktualisieren()
        {
            await GetKundenList();
            SearchedKundenListe.Clear();
            for (int i = 0; i < KundenListe.Count;)
            {
                SearchedKundenListe.Add(KundenListe[i]);
                i++;
            }
        }
        [RelayCommand]
        public async Task GetKundenList()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                KundenListe.Clear();
                var kunden = await _kundeService.GetKundenList();
                foreach (var kunde in kunden)
                {
                    KundenListe.Add(kunde);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Fehler", "Fehler beim Laden", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task AddKunde()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                var kunde = new Kunde
                {
                    KundName = KundName,
                    Matrik = Matrik,
                    Email = Email,
                    Vermerk = Vermerk
                };
                await _kundeService.AddKunde(kunde);
                KundenListe.Add(kunde);
                SearchedKundenListe.Add(kunde);
                await Shell.Current.Navigation.PopAsync();
                await Aktualisieren();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Fehler", "Fehler beim Hinzufügen", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task Abbrechen()
        {
            await Shell.Current.Navigation.PopAsync();
        }
        [RelayCommand]
        public async Task DeleteKunde(Kunde kunde)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _kundeService.DeleteKunde(kunde);
                KundenListe.Remove(kunde);
                SearchedKundenListe.Remove(kunde);
                await Aktualisieren();
                await Shell.Current.DisplayAlert("Erfolg", "Kunde wurde gelöscht", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Fehler", "Fehler beim Löschen", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task NeuerKunden_Seite()
        {
            await Shell.Current.Navigation.PushModalAsync(new NeuerKunde_Seite(new KundenVM(new KundenService())));
        }
    }
}
