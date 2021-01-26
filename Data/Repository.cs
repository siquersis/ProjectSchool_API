using Microsoft.EntityFrameworkCore;
using ProjectSchool_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSchool_API.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.Professor);
            }

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunoAsyncById(int alunoId, bool includeProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.Professor);
            }

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(a => a.Id == alunoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Aluno[]> GetAlunosAsyncProfessorById(int professorId, bool includeProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.Professor);
            }

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(a => a.ProfessorId == professorId);

            return await query.ToArrayAsync();
        }



        public async Task<Professor[]> GetAllProfessoresAsync(bool includeAluno)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(a => a.Alunos);
            }

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetProfessorAsyncById(int professorId, bool includeProfessor)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeProfessor)
            {
                query = query.Include(a => a.Alunos);
            }

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(p => p.Id == professorId);

            return await query.FirstOrDefaultAsync();

        }
    }
}
