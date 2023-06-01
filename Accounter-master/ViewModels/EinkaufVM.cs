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
    public partial class EinkaufVM : BaseViewModel
    {
        [ObservableProperty]
        public int _bestellID;
        [ObservableProperty]
        public DateTime _bestellDatum;
        [ObservableProperty]
        public int _bestellAnzahl;
        [ObservableProperty]
        public decimal _einkaufsPreis;
        [ObservableProperty]
        public string _anmerkung;
        [ObservableProperty]
        public string _image;
        [ObservableProperty]
        public string _searchedID;
        public ObservableCollection<Einkauf> EinkaufsListe { get; set; }
        public ObservableCollection<Einkauf> SearchedEinkaufsListe { get; set; }

        //-----------------------------------------------
        public IEinkaufService _einkaufService;
        public EinkaufVM(IEinkaufService einkaufService)
        {
            Title = "Einkauf";
            _einkaufService = einkaufService;
            EinkaufsListe = new ObservableCollection<Einkauf>();
            SearchedEinkaufsListe = new ObservableCollection<Einkauf>();
            _ = PerformSearch();
        }
        [RelayCommand]
        public async Task PerformSearch()
        {
            if (IsBusy) { return; }
            else if (string.IsNullOrEmpty(SearchedID))
            {
                await Aktualisieren();
                return;
            }
            else
            {
                IsBusy = true;
                SearchedEinkaufsListe.Clear();

                foreach (var einkauf in EinkaufsListe)
                {
                    if (einkauf.BestellID.ToString().ToLower().Contains(SearchedID.ToString().ToLower()))
                    {
                        SearchedEinkaufsListe.Add(einkauf);
                    }
                }
                IsBusy = false;
                return;
            }

        }
        public async Task Aktualisieren()
        {
            await GetEinkaufsList();
            SearchedEinkaufsListe.Clear();
            for (int i = 0; i < EinkaufsListe.Count;)
            {
                SearchedEinkaufsListe.Add(EinkaufsListe[i]);
                i++;
            }
        }
        [RelayCommand]
        public async Task GetEinkaufsList()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                var einkaufsListe = await _einkaufService.GetEinkaufsList();
                if (einkaufsListe?.Count > 0)
                {
                    EinkaufsListe.Clear();
                    foreach (var ausleihe in einkaufsListe)
                    {
                        EinkaufsListe.Add(ausleihe);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get Einkauf: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //DeleteEinkaufCommand
        [RelayCommand]
        public async Task DeleteEinkauf(Einkauf einkauf)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _einkaufService.DeleteEinkauf(einkauf);
                EinkaufsListe.Remove(einkauf);
                SearchedEinkaufsListe.Remove(einkauf);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to delete Einkauf: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //UpdateEinkaufCommand
        [RelayCommand]
        public async Task UpdateEinkauf(Einkauf einkauf)
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                await _einkaufService.UpdateEinkauf(einkauf);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to update Einkauf: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task AddEinkauf()
        {
            if (IsBusy) { return; }
            try
            {
                IsBusy = true;
                var einkauf = new Einkauf();
                einkauf.BestellID = BestellID;
                einkauf.BestellDatum = BestellDatum;
                einkauf.BestellAnzahl = BestellAnzahl;
                einkauf.EinkaufsPreis = EinkaufsPreis;
                einkauf.Anmerkung = Anmerkung;
                einkauf.Image = Image;
                if (Image is null) 
                {
                    einkauf.Image = "what.png";
                }
                await _einkaufService.AddEinkauf(einkauf);
                EinkaufsListe.Add(einkauf);
                SearchedEinkaufsListe.Add(einkauf);
                await Aktualisieren();
                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to add Einkauf: {ex.Message}", "OK");
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
        public async Task CreateNewEinkauf()
        {
            await Shell.Current.Navigation.PushModalAsync(new NeuerEinkauf_Seite(new EinkaufVM(new EinkaufService())));
        }
        [RelayCommand]
        public async Task FotoAuswahl()
        {
            // First ask if the user wants to take a photo or choose from the gallery
            var result = await Shell.Current.DisplayActionSheet("Foto auswählen", "Cancel", null, "Foto aufnehmen", "Aus Galerie auswählen");
            if (result == "Foto aufnehmen")
            {
                await TakePhoto();
            }
            else if (result == "Aus Galerie auswählen")
            {
                await ChoosePhoto();
            }
        }
        // TakePhoto
        async Task TakePhoto()
        {
            if (!MediaPicker.IsCaptureSupported)
            {
                await Shell.Current.DisplayAlert("Error!", "Capture not supported on this device", "OK");
                return;
            }
            var result = await MediaPicker.CapturePhotoAsync();
            if (result != null)
            {
                Image = result.FullPath;
            }
        }
        // ChoosePhoto
        async Task ChoosePhoto()
        {
            //if (!MediaPicker.IsPickPhotoSupported)
            //{
            //    await Shell.Current.DisplayAlert("Error!", "Pick photo is not supported on this device", "OK");
            //    return;
            //}
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                Image = result.FullPath;
            }
        }

    }
}
