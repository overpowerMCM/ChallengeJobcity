using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChallengeBackend.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(1024)]
        public string Text { get; set; }

        public int Timestamp { get; set; }
        [ForeignKey("User")]
        [Column(Order = 1)]
        public int IdUser { get; set; }

        [ForeignKey("Room")]
        [Column(Order = 2)]
        public int IdRoom { get; set; }

        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
    }
}