using AutoSzervizWebApp.Enums;
using AutoSzervizWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AutoSzervizWebApp.DTOs
{
    public class JavitasEditDTO
    {
        public int Id { get; set; }
        public int UgyfelId { get; set; }
        public string Rendszam { get; set; }
        public string? Leiras { get; set; }
        public decimal? Koltseg { get; set; }
    }
}
