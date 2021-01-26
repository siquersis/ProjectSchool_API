using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProjectSchool_API.Data;
using System.Threading.Tasks;
using ProjectSchool_API.Models;
using System;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllAlunosAsync(true);
                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
            }
        }

        [HttpGet("{alunoId}")]
        public async Task<IActionResult> Get(int alunoId)
        {
            try
            {
                var result = await _repo.GetAlunoAsyncById(alunoId, true);
                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
            }
        }

        [HttpGet("{ByProfessor/professorId}")]
        public async Task<IActionResult> GetByProfessorId(int professorId)
        {
            try
            {
                var result = await _repo.GetAlunosAsyncProfessorById(professorId, true);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Aluno aluno)
        {
            try
            {
                _repo.Add(aluno);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/aluno/{aluno.Id}", aluno);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
            }

            return BadRequest();
        }


        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> Put(int alunoId, Aluno aluno)
        {
            try
            {
                var existeAluno = await _repo.GetAlunoAsyncById(alunoId, false);

                if (existeAluno == null)
                    return NotFound($"Aluno " + alunoId + " não encontrado");

                _repo.Update(aluno);

                if (await _repo.SaveChangesAsync())
                {
                    existeAluno = await _repo.GetAlunoAsyncById(alunoId, true);
                    return Created($"/api/aluno/{aluno.Id}", existeAluno);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!");
            }

            return BadRequest();
        }


        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> Delete(int alunoId)
        {
            try
            {
                var existeAluno = await _repo.GetAlunoAsyncById(alunoId, false);

                if (existeAluno == null)
                    return NotFound($"Aluno " + alunoId + " não encontrado");

                _repo.Delete(existeAluno);

                if (await _repo.SaveChangesAsync())
                {
                    existeAluno = await _repo.GetAlunoAsyncById(alunoId, true);
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
}