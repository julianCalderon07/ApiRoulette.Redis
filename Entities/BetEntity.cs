using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Entities
{
    public class BetEntity
    {
        [Key]
        public int idBet { get; set; }
        public string? color { get; set; }
        public int? number { get; set; }
        public int valorBet { get; set; }
        public int idUser { get; set; }
        public int idRoulette { get; set; }
    }
}
