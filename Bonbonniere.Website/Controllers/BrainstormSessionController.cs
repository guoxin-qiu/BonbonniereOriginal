using Bonbonniere.Core.Interfaces;
using Bonbonniere.Website.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bonbonniere.Website.Controllers
{
    public class BrainstormSessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;

        public BrainstormSessionController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessionList = await _sessionRepository.ListAsync();
            var model = sessionList.Select(session => new BrainstormSessionViewModel
            {
                Id = session.Id,
                DateCreated = session.DateCreated,
                Name = session.Name,
                IdeaCount = session.Ideas.Count
            });

            return View(model);
        }

        public class NewSessionModel
        {
            [Required]
            public string SessionName { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewSessionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sessionRepository.AddAsync(new Core.Models.BrainstormSession()
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

            var session = await _sessionRepository.GetByIdAsync(id.Value);
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
