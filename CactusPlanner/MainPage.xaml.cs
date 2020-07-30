using ProgressRingControl.Forms.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CactusPlanner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Cactus cactus;
        public MainPage(ref Cactus cactus)
        {
            InitializeComponent();
            this.cactus = cactus;
            updateLable();
            updateProgress();
        }

        async void updateProgress() 
        {
            await waterRing.ProgressTo(cactus.getProgressToNextWater(), 1000, waterRing.AnimationEasing);
        }

        async void updateLable() 
        {
            int daysToWater = (cactus.getNextWater() - DateTime.Today).Days;
            if (daysToWater > 0)
            {
                statusLabel.Text = $"{daysToWater} days until {cactus.name} needs to be watered.";
            }
            else
            {
                statusLabel.Text = $"{cactus.name} needed to be watered {-daysToWater} days ago!";
                waterRing.RingProgressColor = Color.FromHex("#CC3300");
            }
            if (!cactus.getIsWateringSeaon(DateTime.Today))
            {
                statusLabel.Text += $"\n The next watering season starts {cactus.wateringStart.ToString("dd MMMM")}.";
            }
        }

        async void OnCactusClicked(object sender, EventArgs args) 
        {
            cactus.lastWatered = DateTime.Today;
            updateLable();
            updateProgress();
        }

        async void OnKebabClicked(object sender, EventArgs args) 
        {
            await Navigation.PushAsync( new SettingsPage(ref cactus) );
        }
    }
}
