using System;
using System.Collections.Generic;
using GameShowApi.Dto;
using GameShowApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GameShowApi
{

    [Route("api/presenters")]
    public class ApiPresentersController : Controller
    {

        private IDataStore myData;
        public ApiPresentersController(IDataStore dataStore)
        {
            myData = dataStore;
        }

        [HttpGet()]
        public JsonResult GetPresenters()
        {
            return new JsonResult(myData.Presenters);
        }

        [HttpGet("{id}")]
        public IActionResult GetPresentersById(string id)
        {
            var presenter = myData.GetPresentersById(id);
            if (presenter != null)
            {
                return Ok(presenter);
            }
            else
            {
                return NotFound();
            }
        }

    }
}