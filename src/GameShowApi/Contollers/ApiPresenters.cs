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
    }
}