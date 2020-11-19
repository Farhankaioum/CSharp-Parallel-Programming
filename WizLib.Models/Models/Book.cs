using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WizLib.Models.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public BookDetails BookDetails { get; set; }
        [ForeignKey("BookDetails")]
        public int BookDetailsId { get; set; }

    }
}
