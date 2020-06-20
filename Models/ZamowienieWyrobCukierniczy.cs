using System.ComponentModel.DataAnnotations.Schema;

namespace cw13.Models
{
    public class ZamowienieWyrobCukierniczy
    {
        public int Ilosc { get; set; }
        
        public string Uwagi { get; set; }
        
        public int IdWyrobCukierniczy { get; set; }
        
        public int IdZamowienie { get; set; }

        public WyrobCukierniczy WyrobCukierniczy { get; set; }
        
        public Zamowienie Zamowienie { get; set; }
    }
}