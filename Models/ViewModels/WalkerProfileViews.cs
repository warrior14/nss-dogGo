using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walks> Walks { get; set; }

        public string TotalDuration
        {
            get
            {
                var totalDuration = Walks.Select(w => w.Duration).Sum();
                var hours = totalDuration / 3600;
                var minutes = totalDuration % 3600 / 60;
                return $"{hours}hours :{minutes} minutes";
            }
        }

    }
}
