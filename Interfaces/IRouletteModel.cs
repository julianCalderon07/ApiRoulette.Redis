using ApiRoulette.Redis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Interfaces
{
    public interface IRouletteModel
    {
        Task<string> SetRouletteAsync();
        Task<string> SetActivateRouletteAsync(int idRoulette);
        Task<string> SetBetAsync(BetEntity bet);
        Task<List<BetEntity>> SetCloseRouletteAsync(int idRoulette);
        Task<List<RouletteEntity>> GetRouletteAsync();
    }
}
