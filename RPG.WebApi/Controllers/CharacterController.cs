using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPG.Domain.Entities;
using RPG.Repository;
using RPG.WebApi.Dto;
using RPG.Domain.Util;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System;

namespace RPG.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private IRPGRepository _repository { get; }
        private IMapper _mapper { get; }
        private IWebHostEnvironment _hostEnviroment { get; }

        public CharacterController(IRPGRepository _repository, IMapper _mapper, IWebHostEnvironment _hostEnviroment)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            this._hostEnviroment = _hostEnviroment;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var results = await _repository.GetAllCharactersAsync(pagination);
                var CharacterDtos = _mapper.Map<IEnumerable<CharacterDto>>(results);
                if (pagination.totalCount == 0)
                    pagination.totalCount = _repository.Count<Character>();

                var response = new
                {
                    CharacterDtos,
                    pagination
                };

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Characters não encontrados: {ex.Message}");
            }
        }

        [HttpGet("{CharacterId}")]
        public async Task<IActionResult> Get(int CharacterId)
        {
            try
            {
                var results = await _repository.GetCharacterById(CharacterId);
                var resultsDto = _mapper.Map<CharacterDto>(results);
                return Ok(resultsDto);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Character não encontrado: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CharacterDto model)
        {
            try
            {
                var Character = _mapper.Map<Character>(model);
                _repository.Add(Character);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/Character/{Character.Id}", _mapper.Map<CharacterDto>(Character));
                else
                    throw new Exception("Não foi possível salvar o Character");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível inserir Character : {ex.Message}");
            }
        }

        [HttpPost("upload-image/{CharacterId}")]
        public async Task<IActionResult> UploadImage(int CharacterId)
        {
            try
            {
                var Character = await _repository.GetCharacterById(CharacterId);
                if (Character == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(Character.Image);
                    Character.Image = await SaveImage(file);
                }

                _repository.Update(Character);
                if (await _repository.SaveChangesAsync())
                    return Ok(Character);
                else
                    throw new Exception("Não foi possível salvar o Character");

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível inserir Character : {ex.Message}");
            }
        }

        [NonAction]
        public void DeleteImage(string name)
        {
            var path = Path.Combine(_hostEnviroment.ContentRootPath,"Images", name);
            if(System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            string name = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ','-');
            name = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}{name}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(_hostEnviroment.ContentRootPath, "Images",name);
            using(var fileStream = new FileStream(path,FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return name;
        }

        [HttpPut("{CharacterId}")]
        public async Task<IActionResult> Put(int CharacterId, CharacterDto model)
        {
            try
            {
                var Character = await _repository.GetCharacterById(CharacterId);
                if (Character == null)
                    return NotFound("Character não encontrado para atualizar");

                _mapper.Map(model, Character);
                _repository.Update(Character);

                if (await _repository.SaveChangesAsync())
                    return Created($"/api/Character/{model.Id}", _mapper.Map<CharacterDto>(model));
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível atualizar Character : {ex.Message}");
            }

            return BadRequest("Error Put");
        }

        [HttpDelete("{CharacterId}")]
        public async Task<IActionResult> Delete(int CharacterId)
        {
            try
            {
                var Character = await _repository.GetCharacterById(CharacterId);
                if (Character == null)
                    return NotFound("Character não encontrado para atualizar");

                _repository.Delete(Character);
                if (await _repository.SaveChangesAsync()) 
                {
                    DeleteImage(Character.Image);
                    return Ok();
                }
                else 
                    throw new Exception("Não foi possível deletar o Character");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Character não encontrado: {ex.Message}");
            }
        }
    }
}