using System;
using System.Collections.Generic;
using System.Text;

namespace WizLib.Models.Models
{
    public class BookDetails
    {
        public int Id { get; set; }
        public string Details { get; set; }

        public Book Book { get; set; }
    }
}
