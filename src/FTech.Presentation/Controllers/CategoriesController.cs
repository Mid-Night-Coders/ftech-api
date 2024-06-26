﻿using FTech.Application.DataTransferObjects.Categories;
using FTech.Application.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FTech.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CategoryForCreationDTO dto)
            => Ok(await categoryService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        => Ok(await categoryService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
             => Ok(await categoryService.GetByIdAsync(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute(Name = "id")] long id)
            => Ok(await categoryService.DeleteAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyAsync([FromRoute(Name = "id")] long id, [FromForm] CategoryForCreationDTO dto)
            => Ok(await categoryService.UpdateAsync(id, dto));
    }
}
