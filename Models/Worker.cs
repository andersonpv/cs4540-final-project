using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs4540_final_project.Models
{
    /*
 * This class represents the model for a worker (i.e. a Barber, Hair Stylist, etc)
 */
    public class Worker
    {
        public int ID { get; set; }


        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        // Job title
        public string Job { get; set; }

        // Worker's name
        public string Name { get; set; }

        // What services the worker provides (i.e. beard, haircut, etc)
        public string Services { get; set; }

        public ICollection<DaySchedule> Schedule { get; set; }

        // ICollection<Comment> Reviews { get; set; }
    }
}

