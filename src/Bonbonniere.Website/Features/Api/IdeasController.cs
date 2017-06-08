using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bonbonniere.Core.Sample.Interfaces;
using Bonbonniere.Core.Sample.Model;

namespace Bonbonniere.Website.Features.Api
{
    [Route("api/ideas")]
    [AllowAnonymous]
    public class IdeasController : Controller
    {
        private readonly IBrainstormService _brainstormService;

        public IdeasController(IBrainstormService brainstormService)
        {
            _brainstormService = brainstormService;
        }

        [HttpGet("forsession/{sessionId}")]
        public async Task<IActionResult> ForSession(int sessionId)
        {
            var session = await _brainstormService.GetByIdAsync(sessionId);
            if(session == null)
            {
                return NotFound(sessionId);
            }

            var result = session.Ideas.Select(idea => new IdeaDTO
            {
                Id = idea.Id,
                Name = idea.Name,
                DateCreated = idea.DateCreated,
                Description = idea.Description
            }).ToList();

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var session = await _brainstormService.GetByIdAsync(model.SessionId);
            if(session == null)
            {
                return NotFound(model.SessionId);
            }

            var idea = new Idea
            {
                DateCreated = DateTimeOffset.Now,
                Description = model.Description,
                Name = model.Name
            };

            session.AddIdea(idea);

            await _brainstormService.UpdateAsync(session);

            return Ok(session);
        }
    }
}