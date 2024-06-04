using KnowledgeHubPortal.Domain.DTOs;
using KnowledgeHubPortal.Domain.Entities;
using KnowledgeHubPortal.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHubPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository iRepo; 
        public CategoriesController(ICategoryRepository iRepo)
        {
            this.iRepo = iRepo;
            
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var allCategories = iRepo.GetAll();

            //automapper


            // extention methods
            //allCategories.AsDto();

            var allCategoriesDto = from c in allCategories
                                   where c.IsDeleated == false
                                   select new CategoryListDTO { CategoryID = c.CategoryId, Name = c.Name, Description = c.Description };
            //return Ok(iRepo.GetAll());
            return Ok(allCategoriesDto);
        }

        [HttpGet]
        [Route("{cid:int}")]
        public IActionResult GetCategoryById(int cid)
        {
            var categoryById = iRepo.GetById(cid);

             if (categoryById == null)
            {
                //return status code 404
                return NotFound("Data not found");
            }
            // if found return 200 - ok with response
            return Ok(categoryById);
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public IActionResult PostCategory(CategoryCreateDTO categoryDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            //automapper


            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                CreatedBy = User.Identity.Name ?? "me",
                DateCreated = DateTime.Now,
                IsDeleated = false
            };

            iRepo.Create(category);
            return Created($"api/categories/{category.CategoryId}",category);

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult EditCategory(Category categoryToEdit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = new Category
            {
                CategoryId = categoryToEdit.CategoryId,
                Name = categoryToEdit.Name,
                Description = categoryToEdit.Description,
                CreatedBy = User.Identity.Name ?? "me",
                DateCreated = DateTime.Now,
                IsDeleated = false
            };
            iRepo.Update(category);
            return Ok(category);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("{cid:int}")]
        public IActionResult DeleteCategory(int cid)
        {
            var categoryToDelete = iRepo.SoftDelete(cid);

            if (categoryToDelete == false)
            {
                //return status code 404
                return NotFound("Something went wrong");
            }
            // if found return 200 - ok with response
            return Ok();




        }

    }
}
