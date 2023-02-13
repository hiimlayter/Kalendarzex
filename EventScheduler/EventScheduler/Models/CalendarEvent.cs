using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventScheduler.Models
{
    public class CalendarEvent
    {
        [Key]
        public virtual string Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual IdentityUser User { get; set; }
        [Required]
        public virtual bool isFullDay { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual string Color { get; set; }
        [Required]
        public virtual DateTime Date { get; set; }

    }
}
