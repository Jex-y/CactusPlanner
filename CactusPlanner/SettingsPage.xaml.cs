using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CactusPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        Cactus cactus;
        public SettingsPage(ref Cactus cactus)
        {
            InitializeComponent();
            this.cactus = cactus;

            nameEntry.ValueText = cactus.name;

            lastWateredEntry.Date = cactus.lastWatered;
            lastWateredEntry.MaximumDate = DateTime.Today;

            lastTurnedEntry.Date = cactus.lastTurned;
            lastWateredEntry.MaximumDate = DateTime.Today;

            wateringStart.Date = cactus.makeThisYear(cactus.wateringStart);

            wateringEnd.Date = cactus.makeThisYear(cactus.wateringEnd);

            remiderTime.Time = cactus.reminderTime.TimeOfDay;
        }

        async void onBackClicked(object sender, EventArgs args) 
        {
            await Navigation.PopAsync();
        }

        async void onNameEntryCompleted(object sender, EventArgs args) 
        {
            cactus.name = nameEntry.ValueText;
            cactus.saveToFile();
        }

        async void onLastWateredCompleted(object sender, EventArgs args)
        {
            if (cactus != null)
            {
                cactus.lastWatered = lastWateredEntry.Date;
                cactus.saveToFile();
            }
        }

        async void onLastTurnedCompleted(object sender, EventArgs args)
        {
            if (cactus != null)
            {
                cactus.lastTurned = lastTurnedEntry.Date;
                cactus.saveToFile();
            }
        }

        async void onWateringStartCompleted(object sender, EventArgs args)
        {
            if (cactus != null)
            {
                cactus.wateringStart = wateringStart.Date;
                cactus.saveToFile();
            }
        }

        async void onWateringEndCompleted(object sender, EventArgs args)
        {
            if (cactus != null)
            {
                cactus.wateringEnd = wateringEnd.Date;
                cactus.saveToFile();
            }
        }

        async void onReminderTimeCompleted(object sender, EventArgs args)
        {
            if (cactus != null)
            {
                cactus.reminderTime = new DateTime(
                    cactus.reminderTime.Year, cactus.reminderTime.Month, cactus.reminderTime.Day,
                    remiderTime.Time.Hours, remiderTime.Time.Minutes, remiderTime.Time.Seconds);
                cactus.saveToFile();
            }
        }
    }
}