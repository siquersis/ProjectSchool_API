using System.Threading.Tasks;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Data
{
    public interface IRepository
    {
        //GERAL
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //ALUNO
        Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor);

        Task<Aluno> GetAlunoAsyncById(int alunoId, bool includeProfessor);

        Task<Aluno[]> GetAlunosAsyncProfessorById(int professorId, bool includeProfessor);


        //PROFESSOR
        Task<Professor[]> GetAllProfessoresAsync(bool includeAluno);

        Task<Professor> GetProfessorAsyncById(int professorId, bool includeAluno);
    }
}