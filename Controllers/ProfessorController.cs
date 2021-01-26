
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;
using System;

[Route("api/[controller]")]
[ApiController]
public class ProfessorController : Controller
{
    private readonly IRepository _repo;

    public ProfessorController(IRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _repo.GetAllProfessoresAsync(true);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
        }
    }

    [HttpGet("{professorId}")]
    public async Task<IActionResult> GetByProfessorId(int professorId)
    {
        try
        {
            var result = await _repo.GetProfessorAsyncById(professorId, true);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post(Professor professor)
    {
        try
        {
            _repo.Add(professor);
            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/professor/{professor.Id}", professor);
            }
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
        }

        return BadRequest();
    }


    [HttpPut("{ProfessorId}")]
    public async Task<IActionResult> Put(int professorId)
    {
        try
        {
            var existeProfessor = await _repo.GetProfessorAsyncById(professorId, false);

            if (existeProfessor == null)
                return NotFound($"Aluno " + professorId + " não encontrado");

            _repo.Update(existeProfessor);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
        }

        return BadRequest();
    }


    [HttpDelete("{ProfessorId}")]
    public async Task<IActionResult> Delete(int professorId)
    {
        try
        {
            var existeProfessor = await _repo.GetProfessorAsyncById(professorId, false);

            if (existeProfessor == null)
                return NotFound($"Professor " + professorId + " não encontrado");

            _repo.Delete(existeProfessor);

            if (await _repo.SaveChangesAsync())
            {
                existeProfessor = await _repo.GetProfessorAsyncById(professorId, true);
                return Ok();
            }
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
        }

        return BadRequest();
    }
}