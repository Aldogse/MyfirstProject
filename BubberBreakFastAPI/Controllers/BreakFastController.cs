
using Azure;
using BubberBreakFast.Contracts.Contracts;
using BubberBreakFastAPI.Data;
using BubberBreakFastAPI.Interfaces;
using BubberBreakFastAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BubberBreakFastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreakFastController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IBreakFastRepository _breakFastRepository;


        public BreakFastController(IBreakFastRepository breakFastRepository , ApplicationDBContext context)
        {
            _breakFastRepository = breakFastRepository;
            _context = context;

        }       
        

        [HttpPost]
        public IActionResult CreateBreakFast([FromBody]CreateBreakFastRequest request)
        {
            
            var breakFast = new BreakFast(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.tasteOfOrigin,
                DateTime.Now
                );
            _context.BreakFasts.Add(breakFast);
            _context.SaveChanges();

            var response = new BreakFastResponse(
                breakFast.id,
                breakFast.Name,
                breakFast.Description,
                breakFast.StartDate,
                breakFast.EndDate,
                breakFast.LastModifiedDate,
                breakFast.TasteOfOrigin
                );
            return CreatedAtAction(
                nameof(GetBreakFast),
                new { id = breakFast.id},
                response
                );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBreakFast(Guid id)
        {
            var request =  await _breakFastRepository.GetBreakFast(id);

            if(request == null )
            {
                return NotFound();
            }

            var response = new BreakFastResponse(
                request.id,
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.LastModifiedDate,
                request.TasteOfOrigin
                );

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertBreakFastAsync(Guid id,[FromBody] UpsertBreakFastRequest upsertBreakFast)
        {
            var http = new HttpClient();
            var breakFastToUpdate = await _context.BreakFasts.Where(b => b.id == id).FirstOrDefaultAsync();

            if(breakFastToUpdate == null)
            {
                return NotFound(nameof(breakFastToUpdate));
            }

            try
            {
                breakFastToUpdate.Update(
                    upsertBreakFast.Name,
                    upsertBreakFast.Description,
                    upsertBreakFast.StartDate,
                    upsertBreakFast.EndDate,
                    upsertBreakFast.tasteOfOrigin
                    );
                _context.Update(breakFastToUpdate);
                await _context.SaveChangesAsync();

                var response = new BreakFastResponse(
                    breakFastToUpdate.id,
                    breakFastToUpdate.Name,
                    breakFastToUpdate.Description,
                    breakFastToUpdate.StartDate,
                    breakFastToUpdate.EndDate,                  
                    DateTime.Now,
                    breakFastToUpdate.TasteOfOrigin);

                return Ok(response);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
            
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakFast(Guid id)
        {
            return BadRequest(id);
        }

        [HttpGet]
        public IActionResult GetAllBreakFast(string? sort)
        {
            var request = _breakFastRepository.GetBreakFasts().AsQueryable();

                 
            if(request == null)
            {
                return NotFound("No breakfast found!");
            }
            var responses = new List<BreakFastResponse>();

            foreach (var item in request)
            {
                var response = new BreakFastResponse(
                    item.id,
                    item.Name,
                    item.Description,
                    item.StartDate,
                    item.EndDate,
                    item.LastModifiedDate,
                    item.TasteOfOrigin
                    );
                responses.Add(response);
            }
            return Ok(responses);
        }
    }
}
