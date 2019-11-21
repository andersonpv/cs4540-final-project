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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DayScheduleID { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        //public bool[] Availability { get; set; }
    }
}
