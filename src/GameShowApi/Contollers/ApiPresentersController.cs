using System;
using System.Collections.Generic;
using GameShowApi.Dto;
using GameShowApi.Model;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost("add")]
        public IActionResult PostNewPresenter([FromBody] PresenterCreationDTO newPresenter)
        {
            PresentersDto createdPresenter = myData.AddNewPresenter(newPresenter);
            if (ModelState.IsValid)
            {
                return CreatedAtAction(nameof(GetPresentersById), new { id = createdPresenter.Id }, createdPresenter);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("patch/{id}")]
        public IActionResult PatchPresenters([FromBody] JsonPatchDocument<PresentersDto> patchDoc, string id)
        {
            if (patchDoc != null)
            {
                var presenter = myData.GetPresentersById(id);
                patchDoc.ApplyTo(presenter, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return new ObjectResult(presenter);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
