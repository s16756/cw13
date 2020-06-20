using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw13.Models
{
    public class Pracownik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPacownika { get; set; }
        
        public string Imie { get; set; }
        
        public string Nazwisko { get; set; }
        
        public virtual IEnumerable<Zamowienie> Zamowienia { get; set; }
    }
}