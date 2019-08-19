using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChallengeBackend.Models
{
    public class Room
    {
        public Room()
        {
            this.User_Rooms = new HashSet<User_Room>();
            this.Messages = new HashSet<Message>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Title { get; set; }

        public virtual ICollection<User_Room> User_Rooms { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}