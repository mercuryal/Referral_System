using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Team
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [DefaultValue(0)]
        public int score { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLoses { get; set; }

    }
}