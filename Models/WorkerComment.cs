/**
 * CS 4540 Web Software Architecture
 * WorkerComment
 * Authors: Kevin Nguyen
 * Date: 12-6-2019
 * 
 * Model for WorkerCommments
 **/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs4540_final_project.Models
{
    public class WorkerComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerCommentID { get; set; }

        public string Comment { get; set; }

        public string Name { get; set; }

        public int StarRating { get; set; }

        [DisplayName("Worker")]
        public int WorkerID { get; set; }

        public Worker Worker { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }
    }
}
