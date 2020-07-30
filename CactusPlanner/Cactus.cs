using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace CactusPlanner
{
    public class Cactus
    {
        private static String SAVEPATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CactusPlannerData.json");

        [JsonProperty(PropertyName = "wateringStart")]
        public DateTime wateringStart;
        [JsonProperty(PropertyName = "wateringEnd")]
        public DateTime wateringEnd;
        [JsonProperty(PropertyName = "lastWatered")]
        public DateTime lastWatered;
        [JsonProperty(PropertyName = "lastTurned")]
        public DateTime lastTurned;
        [JsonProperty(PropertyName = "reminderTime")]
        public DateTime reminderTime;
        [JsonProperty(PropertyName = "name")]
        public String name;

        public static Cactus loadCactus()
        {
            Cactus newCactus;
            if (!File.Exists(SAVEPATH))
            {
                newCactus = new Cactus();
            }
            else
            {
                newCactus = JsonConvert.DeserializeObject<Cactus>(File.ReadAllText(SAVEPATH));
            }
            return newCactus;
        }

        public Cactus()
        {
            DateTime now = DateTime.Now;
            lastTurned = now;
            lastWatered = now;
            wateringStart = new DateTime(DateTime.Today.Year, 3, 20);
            wateringEnd = new DateTime(DateTime.Today.Year, 12, 21);
            reminderTime = new DateTime(2000, 1, 1, 17, 00, 00);
            name = "Colin The Cactus";
        }

        public bool getIsWateringSeaon(DateTime date)
        {
            return (wateringStart <= date && date <= wateringEnd);
        }

        public DateTime getNextWater()
        {
            DateTime nextWater = lastWatered.AddDays(7);
            while (!getIsWateringSeaon(nextWater)) 
            {
                nextWater = nextWater.AddDays(7);
            }
            return nextWater;
        }

        public DateTime getNextTurn() 
        {
            return lastTurned.AddMonths(1);
        }

        public Boolean getShouldBeWatered() 
        {
            return DateTime.Today >= getNextWater();
        }

        public double getProgressToNextWater() 
        {
            DateTime nextWater = getNextWater();
            double result = 1.0 - (double)(nextWater-DateTime.Today).Days / (double)(nextWater-lastWatered).Days;
            if (result > 1) {
                result = 1;
            }
            return result;
        }

        public async void saveToFile()
        {
            String json = JsonConvert.SerializeObject(this);
            File.WriteAllText(SAVEPATH, json);

        }

        public DateTime makeThisYear(DateTime date) 
        {
            return new DateTime(DateTime.Now.Year, date.Month, date.Day);
        }


    }
}
