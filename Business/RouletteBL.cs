using ApiRoulette.Redis.Entities;
using ApiRoulette.Redis.Interfaces;
using ApiRoulette.Redis.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Business
{
    public class RouletteBL : IRouletteBL
    {
        private readonly IRouletteModel _IRoulette;
        public RouletteBL(IRouletteModel _IRoulette)
        {
            this._IRoulette = _IRoulette;
        }
        public async Task<string> SetActivateRouletteAsync(int idRoulette)
        {
            ResponseType response = new ResponseType();
            string responseActivate = await _IRoulette.SetActivateRouletteAsync(idRoulette);
            if (responseActivate == "true")
            {
                response.Code = 200;
                response.Message = "Se ha activado correctamente la ruleta #" + idRoulette;
                response.OutPut = idRoulette.ToString();
            }
            else
            {
                response.Code = 500;
                response.Message = responseActivate;
            }
            return JsonConvert.SerializeObject(response);
        }
        public async Task<string> SetRouletteAsync()
        {
            string keyRoulette = await _IRoulette.SetRouletteAsync();
            ResponseType response = new ResponseType();
            if (keyRoulette.Length != 0)
            {
                response.Code = 200;
                response.Message = "Se ha creado correctamente la ruleta #" + keyRoulette;
                response.OutPut = "Id: " + keyRoulette;
            }
            else
            {
                response.Code = 500;
                response.Message = "Ha ocurrido un error, verifiqué la información";
            }
            return JsonConvert.SerializeObject(response);
        }
        public async Task<string> SetBetAsync(BetEntity bet)
        {
            ResponseType response = new ResponseType();
            if (bet.idBet != 0)
            {
                response.Code = 500;
                response.Message = "por favor no cambie el campo idBet de valor 0 ";
                response.OutPut = JsonConvert.SerializeObject(bet);
                return JsonConvert.SerializeObject(response);
            }
            if (bet.number != 0 && bet.color is not null && bet.color != "string")
            {
                response.Code = 500;
                response.Message = "Verifique su apuesta, ya que no puede apostar a color y numero. Elija uno! ";
                response.OutPut = JsonConvert.SerializeObject(bet);
                return JsonConvert.SerializeObject(response);
            }
            if (bet.number is not null && bet.color == "" || bet.color == "string")
            {
                if (bet.number < 0 || bet.number > 36)
                {
                    response.Code = 500;
                    response.Message = "El numero al que aposto no existe. Recuerde que se puede apostar a los numeros del 1 al 36!";
                    response.OutPut = JsonConvert.SerializeObject(bet);
                    return JsonConvert.SerializeObject(response);
                }
            }
            if (bet.color is not null && bet.number == 0 || bet.number is null)
            {
                if (bet.color.ToUpper() != "NEGRO" && bet.color.ToUpper() != "ROJO")
                {
                    response.Code = 500;
                    response.Message = "El color al que aposto no existe. Recuerde que los colores son ROJO o NEGRO";
                    response.OutPut = JsonConvert.SerializeObject(bet);
                    return JsonConvert.SerializeObject(response);
                }
            }
            if (bet.valorBet > 10000)
            {
                response.Code = 500;
                response.Message = "El valor máximo a apostar es de $10.000 dólares";
                response.OutPut = JsonConvert.SerializeObject(bet);
                return JsonConvert.SerializeObject(response);
            }
            string responseActivate = await _IRoulette.SetBetAsync(bet);
            if (responseActivate == "True")
            {
                response.Code = 200;
                response.Message = "Se ha apostado a la ruleta #" + bet.idRoulette;
                response.OutPut = JsonConvert.SerializeObject(bet);
            }
            else
            {
                response.Code = 500;
                response.Message = responseActivate;
            }
            return JsonConvert.SerializeObject(response);
        }
        public async Task<string> SetCloseRouletteAsync(int idRoulette)
        {
            ResponseType response = new ResponseType();
            List<BetEntity> listBet = await _IRoulette.SetCloseRouletteAsync(idRoulette);
            if (listBet != null)
            {
                Random r = new Random();
                int numberWinner = r.Next(0, 37);
                string colorGanador;
                if (numberWinner % 2 == 0)
                    colorGanador = "ROJO";
                else
                    colorGanador = "NEGRO";
                List<WinnersType> winnersList = new List<WinnersType>();
                foreach (var item in listBet)
                {
                    WinnersType winnersType = new WinnersType();
                    winnersType.IdUser = item.idUser;
                    winnersType.ValorBet = item.valorBet;
                    winnersType.Number = item.number;
                    winnersType.Color = item.color;
                    winnersType.NumberWinner = numberWinner;
                    winnersType.ColorWinner = colorGanador;
                    if (item.number != 0)
                    {
                        if (item.number == numberWinner)
                        {
                            winnersType.ValorWinner = (item.valorBet * 5).ToString();
                            winnersType.State = "Ganador";
                        }
                        else
                        {
                            winnersType.State = "Perdedor";
                            winnersType.ValorWinner = "0";
                        }
                    }
                    else if (item.color is not null && item.color != "string")
                    {
                        if (item.color.ToUpper() == colorGanador)
                        {
                            winnersType.ValorWinner = (item.valorBet * 1.8).ToString();
                            winnersType.State = "Ganador";
                        }
                        else
                        {
                            winnersType.ValorWinner = "0";
                            winnersType.State = "Perdedor";
                        }
                    }
                    winnersList.Add(winnersType);
                }
                return JsonConvert.SerializeObject(winnersList);
            }
            else
            {
                return "No se encontro la ruleta o puede estar ya inactiva";
            }
        }
        public async Task<string> GetRouletteAsync()
        {
            return JsonConvert.SerializeObject(await _IRoulette.GetRouletteAsync());
        }
    }
}
