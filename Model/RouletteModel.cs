using ApiRoulette.Redis.Entities;
using ApiRoulette.Redis.Interfaces;
using EntityRoulette.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Model
{
    public class RouletteModel:IRouletteModel
    {
        private readonly DB_Context _Context;
        public RouletteModel(DB_Context _Context){
            this._Context = _Context;
        }
        public async Task<string> SetRouletteAsync()
        {
            bool Roulette = _Context.Roulette.OrderByDescending(x => x.idRoulette).Any(x => x.idRoulette != null);
            int numRoulette = 0;
            if (Roulette)
            {
                string RouletteNumber = _Context.Roulette.OrderByDescending(x => x.idRoulette).FirstOrDefault().idRoulette.ToString();
                numRoulette = int.Parse(RouletteNumber) + 1;
            }
            else 
            {
                numRoulette = 1;
            }
            RouletteEntity entityRoulette = new RouletteEntity
            {
                nameRoulette = "Roulette" + numRoulette.ToString(),
                estado = false
            };
            await _Context.Roulette.AddAsync(entityRoulette);
            await _Context.SaveChangesAsync();
            return numRoulette.ToString();
        }
        public async Task<string> SetActivateRouletteAsync(int idRoulette)
        {
            RouletteEntity rouletteEntity = _Context.Roulette.Find(idRoulette);
            if (rouletteEntity is null || rouletteEntity.estado == true)
            {
                return "No existe o ya esta activa la ruleta!";
            }
            rouletteEntity.estado = true;
            _Context.Roulette.Attach(rouletteEntity);
            _Context.Entry(rouletteEntity).Property(x => x.estado).IsModified = true;
            await _Context.SaveChangesAsync();

            RouletteEntity entity = _Context.Roulette.Find(idRoulette);
            if (entity.estado==true)
            {
                return "true";
            }
            else {
                return "No existe o ya esta activa la ruleta!";
            }
        }
        public async Task<string> SetBetAsync(BetEntity bet)
        {
            RouletteEntity rouletteEntity = _Context.Roulette.Find(bet.idRoulette);
            if (rouletteEntity is null  || rouletteEntity.estado == false)
            {
                return "Verifique el estado de la ruleta";
            }
            else
            {
                await _Context.Bet.AddAsync(bet);
                _Context.SaveChanges();
            }

            return "True";
        }
        public async Task<List<BetEntity>> SetCloseRouletteAsync(int idRoulette)
        {
            RouletteEntity rouletteEntity =await _Context.Roulette.FindAsync(idRoulette);
            if (rouletteEntity is null || rouletteEntity.estado == false)
            {
                return null;
            }
            rouletteEntity.estado = false;
            _Context.Roulette.Attach(rouletteEntity);
            _Context.Entry(rouletteEntity).Property(x => x.estado).IsModified = true;
            await _Context.SaveChangesAsync();
            List<BetEntity> listBet = (from lb in _Context.Bet
                                       where lb.idRoulette==idRoulette
                                       select lb).ToList();
            return listBet;
        }
        public async Task<List<RouletteEntity>> GetRouletteAsync()
        {
            List<RouletteEntity> Roulette =await _Context.Roulette.ToListAsync();
            return Roulette;
        }
    }
}
