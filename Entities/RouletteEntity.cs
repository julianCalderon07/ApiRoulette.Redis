using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Entities
{
    public class RouletteEntity
    {
        [Key]
        public int? idRoulette { get; set; }
        public string? nameRoulette { get; set; }
        public bool? estado { get; set; }
    }
}
