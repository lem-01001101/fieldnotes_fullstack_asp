using FieldNotesWeb.Data;
using FieldNotesWeb.Models.Domain;
using FieldNotesWeb.Models.ViewModels;
using FieldNotesWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FieldNotesWeb.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {

        private readonly ITagRepository tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
              // mapping request to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var tag = await tagRepository.GetAsync(id);
            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            
            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null) 
            {
                // show success notif
            } 
            else
            {
                //show error notif
            }

             // show error notif
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(EditTagRequest editTagRequest)
        {
           var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(deletedTag != null)
            {
                //show success notif
                return RedirectToAction("List");
            }

            // show error notif
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
