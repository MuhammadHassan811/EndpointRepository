using Microsoft.EntityFrameworkCore;
using Endpoint.API.Infra;
using Endpoint.API.Interfaces;
using Endpoint.API.Models;

namespace Endpoint.API.Repository
{
    public class TasksRepository : ITasksRepository
    {

        protected readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Tasks> GetByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            return task;
        }

        public async Task<Tasks> AddAsync(Tasks task)
        {
            await _context.AddAsync(task);
            // await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Tasks> UpdateAsync(Tasks task)
        {
            _context.Update(task);
            // await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Tasks> RemoveAsync(Tasks task)
        {
            _context.Remove(task);
            // await _context.SaveChangesAsync();

            return task;
        }

        async Task<IEnumerable<Tasks>> ITasksRepository.FilterByDateAsync(DateTime date)
        {
            var tasks = await _context.Tasks.Where(t => t.DataVencimento == date).ToListAsync();

            return tasks;
        }

        async Task<IEnumerable<Tasks>> ITasksRepository.FilterByPriorityAsync(string priority)
        {
            var tasks = await _context.Tasks.Where(t => t.Prioridade == priority).ToListAsync();

            return tasks;
        }

        async Task<IEnumerable<Tasks>> ITasksRepository.FilterByStatusAsync(bool status)
        {
            var tasks = await _context.Tasks.Where(t => t.Concluida == status).ToListAsync();

            return tasks;
        }
    }
}
