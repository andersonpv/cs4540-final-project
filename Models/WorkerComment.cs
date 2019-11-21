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
        public int WorkerCommentId { get; set; }

        public string Comment { get; set; }

        [DisplayName("Worker")]
        public int WorkerID { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }
    }
}
