using APIProject.Models.Domain;
using APIProject.Models.DTO;
using APIProject.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            return Ok(await walkDifficultyRepository.GetAllAsync());            
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]

        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null) 
            { 
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultiesAsync(AddWalkDifficulttyRequest addWalkDifficulttyRequest)
        {

            if (!ValidateAddWalkDifficultyRequest(addWalkDifficulttyRequest))
            {
                return BadRequest(ModelState);
            }

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficulttyRequest.Code
            };

            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyById),
                new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,
            UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (!ValidateUpdateWalkDifficultyRequest(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null) 
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null) 
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);

        }

        #region Private Methods

        private bool ValidateAddWalkDifficultyRequest(AddWalkDifficulttyRequest addWalkDifficulttyRequest)
        {
            if (addWalkDifficulttyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficulttyRequest),
                    $"{nameof(addWalkDifficulttyRequest)} data is required");

                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficulttyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficulttyRequest.Code),
                    $"{nameof(addWalkDifficulttyRequest.Code)} is required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private bool ValidateUpdateWalkDifficultyRequest(UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    $"{nameof(updateWalkDifficultyRequest)} data is required");

                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                    $"{nameof(updateWalkDifficultyRequest.Code)} is required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }   

            return true;
        }
        #endregion
    }
}
