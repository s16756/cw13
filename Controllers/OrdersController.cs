using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using cw13.Dtos;
using cw13.Models;
using cw13.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw13.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("api/orders")]
        public async Task<IActionResult> Get([FromQuery] string clientLastName)
        {
            var ordersSource = _databaseContext
                .Zamowienia
                .Include(o => o.ZamowieniaWyrobyCukiernicze)
                .ThenInclude(zwc => zwc.WyrobCukierniczy)
                .AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(clientLastName))
            {
                ordersSource = ordersSource.Where(o => o.Klient.Nazwisko == clientLastName);
            }

            if (!ordersSource.Any()) return NotFound();

            var result = await ordersSource.Select(o => new
            {
                o.Uwagi,
                o.DataPrzyjecia,
                o.DataRealizacji,
                o.IdZamowienia,
                Wyroby = o.ZamowieniaWyrobyCukiernicze.Select(zwc => new
                {
                    Cena = zwc.Ilosc * zwc.WyrobCukierniczy.CenaZaSzt,
                    zwc.Uwagi,
                    zwc.WyrobCukierniczy.Nazwa,
                    zwc.WyrobCukierniczy.Typ
                })
            }).ToListAsync();

            return Ok(result);
        }

        [HttpPost("api/clients/{id}/orders")]
        public async Task<IActionResult> Create([FromRoute] int id, [FromBody] CreateOrderDto dto)
        {
            if (!Regex.IsMatch(dto.DataPrzyjecia, "\\d{4}-\\d{2}-\\d{2}"))
                return BadRequest("Data przyjęcia musi być w formacie YYYY-MM-DD");

            var dateSplitted = dto.DataPrzyjecia.Split('-');
            int year = int.Parse(dateSplitted[0]);
            int month = int.Parse(dateSplitted[1]);
            int day = int.Parse(dateSplitted[2]);
            var date = new DateTime(year, month, day);
            
            var client = await _databaseContext.Klienci.SingleOrDefaultAsync(c => c.IdKlient == id);
            if (client == null) return BadRequest("Nie znaleziono klienta o podanym id");

            var productsExists =
                _databaseContext
                    .WyrobyCukiernicze
                    .Any(w => dto.Wyroby.Select(d => d.Wyrob).Contains(w.Nazwa));

            if (!productsExists)
                return BadRequest("Jeden lub więcej wskazanych produktów nie udało się znaleźć w bazie danych");



            var order = new Zamowienie()
            {
                Uwagi = dto.Uwagi,
                DataPrzyjecia = date,
                Klient = client,
                ZamowieniaWyrobyCukiernicze = dto.Wyroby.Select(w => new ZamowienieWyrobCukierniczy()
                {
                    Uwagi = w.Uwagi,
                    Ilosc = w.Ilosc,
                    WyrobCukierniczy = _databaseContext.WyrobyCukiernicze.Single(wc => wc.Nazwa == w.Wyrob)
                })
            };

            await _databaseContext.AddAsync(order);
            await _databaseContext.SaveChangesAsync();

            return Ok("Zamówienie zostało utworzone");
        }
    }
}