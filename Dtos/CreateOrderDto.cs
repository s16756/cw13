using System.Collections.Generic;

namespace cw13.Dtos
{
    public class CreateOrderDto
    {
        public string DataPrzyjecia { get; set; }
        
        public string Uwagi { get; set; }

        public IEnumerable<WyrobDto> Wyroby { get; set; }
        
        public class WyrobDto
        {
            public int Ilosc { get; set; }
            
            public string Wyrob { get; set; }
            
            public string Uwagi { get; set; }
        }
    }
}