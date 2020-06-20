using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw13.Models
{
    public class Klient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdKlient { get; set; }
        
        public string Imie { get; set; }
        
        public string Nazwisko { get; set; }
        
        public virtual IEnumerable<Zamowienie> Zamowienia { get; set; }
    }
}