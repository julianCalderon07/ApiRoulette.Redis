using ApiRoulette.Redis.Entities;
using ApiRoulette.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Redis.Controllers
{
    [ApiController]
    [Route("roulette/[controller]")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteBL _iRouletteBL;
        public RouletteController(IRouletteBL _iRouletteBL)
        {
            this._iRouletteBL = _iRouletteBL;
        }
        [HttpPost]
        [Route("createRoulette")]
        public async Task<IActionResult> CreateRoulette()
        {
            return Ok(await _iRouletteBL.SetRouletteAsync());
        }
        [HttpPost]
        [Route("activateRoulette")]
        public async Task<IActionResult> ActivateRoulette(int idRoulette)
        {
            return Ok(await _iRouletteBL.SetActivateRouletteAsync(idRoulette));
        }
        [HttpPost]
        [Route("BetRoulette")]
        public async Task<IActionResult> BetRoulette(BetEntity betEntity)
        {
            return Ok(await _iRouletteBL.SetBetAsync(betEntity));
        }
        [HttpPost]
        [Route("closeRoulette")]
        public async Task<IActionResult> CloseRoulette(int idRoulette)
        {
            return Ok(await _iRouletteBL.SetCloseRouletteAsync(idRoulette));
        }
        [HttpGet]
        [Route("GetRoulettes")]
        public async Task<IActionResult> GetRoulette() 
        {
            return Ok(await _iRouletteBL.GetRouletteAsync());
        }
    }
}
