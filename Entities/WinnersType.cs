using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Entities
{
    public class WinnersType
    {
        public int IdUser { get; set; }
        public int ValorBet { get; set; }
        public string ValorWinner { get; set; }
        public string? Color { get; set; }
        public int? Number { get; set; }
        public string ColorWinner { get; set; }
        public int NumberWinner { get; set; }
        public string State { get; set; }
    }
}
