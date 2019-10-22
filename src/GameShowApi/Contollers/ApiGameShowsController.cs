using System;
using System.Collections.Generic;
using GameShowApi.Dto;
using GameShowApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GameShowApi
{

    [Route("api/gameshows")]
    public class GameShowsController : Controller
    {

        private IDataStore myData;
        public GameShowsController(IDataStore dataStore)
        {
            myData = dataStore;
        }

        [HttpGet()]
        public JsonResult GetGameShows()
        {
            return new JsonResult(myData.GameShows);
        }

        [HttpGet("{id}")]
        public IActionResult GetGameShowsById(string id)
        {
            var presenter = myData.GetGameShowsById(id);
            if (presenter != null)
            {
                return Ok(presenter);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("add")]
        public IActionResult PostNewGameShow([FromBody] GameShowCreationDto newGameShow)
        {
            GameShowDto createdGameShow = myData.AddNewGameShow(newGameShow);
            if (ModelState.IsValid)
            {
                return CreatedAtAction(nameof(GetGameShowsById), new { id = createdGameShow.Id }, createdGameShow);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}