using System;
using System.Collections.Generic;
using GameShowApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GameShowApi {
    [Route ("api")]
    public class ApiPresenters : Controller {
        [HttpGet ("presenters")]
        public IActionResult GetPresenters () {
            var presenters = new List<PresentersDto> () {
                new PresentersDto (1, "Bibble", 1985, 2007)
            };

            if (presenters == null) {
                return NotFound ();
            } else {
                return Ok (presenters);
            }
        }
    }
}