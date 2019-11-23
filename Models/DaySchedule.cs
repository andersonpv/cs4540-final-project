using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cs4540_final_project.Models
{
    /* 
     * Worker availability for a single day
     */
    public class DaySchedule
    {
        public int DayScheduleID { get; set; }

        public DateTime dateTime { get; set; }

        public bool Nine { get; set; }
        public bool NineThirty { get; set; }
        public bool Ten { get; set; }
        public bool TenThirty { get; set; }
        public bool Eleven { get; set; }
        public bool ElevenThirty { get; set; }
        public bool Twelve { get; set; }
        public bool TwelveThirty { get; set; }
        public bool One { get; set; }
        public bool OneThirty { get; set; }
        public bool Two { get; set; }
        public bool TwoThirty { get; set; }
        public bool Three { get; set; }
        public bool ThreeThirty { get; set; }
        public bool Four { get; set; }
        public bool FourThirty { get; set; }

    }
}