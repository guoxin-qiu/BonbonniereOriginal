using Bonbonniere.Services;
using Bonbonniere.Website.Additions.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bonbonniere.Website.Features.BrainstormSession
{
    public class BrainstormSessionController : Controller
    {
        private readonly IBrainstormService _brainstormService;

        public BrainstormSessionController(IBrainstormService brainstormService)
        {
            _brainstormService = brainstormService;
        }

        public async Task<IActionResult> Index()
        {
            var sessionList = await _brainstormService.GetListAsync();
            var model = sessionList.Select(session => new BrainstormSessionViewModel
            {
                Id = session.Id,
                DateCreated = session.DateCreated,
                Name = session.Name,
                IdeaCount = session.Ideas.Count
            }).ToList();

            return View(model);
        }

        public class NewSessionModel
        {
            [Required]
            public string SessionName { get; set; }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Index(NewSessionModel model)
        {
            await _brainstormService.AddSessionAsync(new Core.Models.BrainstormSession()
            {
                DateCreated = DateTimeOffset.Now,
                Name = model.SessionName
            });

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var session = await _brainstormService.GetByIdAsync(id.Value);
            if (session == null)
            {
                return Content("Session not found.");
            }

            var viewModel = new BrainstormSessionViewModel
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                IdeaCount = session.Ideas.Count,
                Id = session.Id
            };

            return View(viewModel);
        }
    }
}
