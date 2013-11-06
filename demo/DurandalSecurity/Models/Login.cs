using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DurandalSecurity.Models {
    public class Login {
        public int Id { get; set; }
        
        [Required(), MaxLength(128), Display(Name="Email Address")]
        public string Email { get; set; }

        [Required(), MaxLength(128)]
        public string Hash { get; set; }

        [Required(), MaxLength(128)]
        public string Salt { get; set; }

        [Required(), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        public DateTime LastLogin { get; set; }
    }
}