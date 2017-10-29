using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class Ticket
    {
        public int id { get; }

        [Required(ErrorMessage = "title is required.")]
        public String title { get; set; }

        [Required(ErrorMessage = "body is required.")]
        public String body { get; set; }

        [Required(ErrorMessage = "status is required.")]
        public String status { get; set; }

        [Required(ErrorMessage = "author is required.")]
        public User author { get; }

        [Required(ErrorMessage = "assignee is required.")]
        public User assignee { get; set; }

        public DateTime created { get; }

        public Ticket()
        {

        }

        public Ticket(int pId, string pTitle, string pBody, string pStatus, User pAuthor, User pAssignee, DateTime pCreated)
        {
            this.id = pId;
            this.title = pTitle;
            this.body = pBody;
            this.status = pStatus;
            this.author = pAuthor;
            this.assignee = pAssignee;
            this.created = pCreated;
        }
    }
}