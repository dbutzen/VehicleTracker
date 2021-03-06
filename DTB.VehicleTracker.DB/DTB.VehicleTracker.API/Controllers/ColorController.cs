using DTB.VehicleTracker.BL;
using DTB.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTB.VehicleTracker.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        // GET: api/<ColorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BL.Models.Color>>> Get()
        {
            // return all the colors
            try
            {
                return Ok(await ColorManager.Load());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<ColorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> Get(Guid id)
        {
            try
            {
                return Ok(await ColorManager.LoadById(id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<ColorController>
        [HttpPost]
        public async Task<IActionResult> Post(Color color)
        {
            try
            {
                return Ok(await ColorManager.Insert(color));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<ColorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Color color)
        {
            try
            {
                return Ok(await ColorManager.Update(color));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await ColorManager.Delete(id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
