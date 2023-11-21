using Microsoft.EntityFrameworkCore;
using TaskMngr.Data;
using TaskMngr.Interface;
using TaskMngr.Models;

namespace TaskMngr.Service
{
    /// <summary>
    /// Service class for managing Task in the database.
    /// </summary>
    public class TskService : ITsk
    {

        private readonly TaskDb _context;

        public TskService(TaskDb context)
        {
            _context = context;
        }


        /// <summary>
        /// Get a list of all Tasks from the database.
        /// </summary>
        /// <returns>A list of Tasks.</returns>
        /// 
        public async Task<List<Tsk>> GetAll()
        {
            var task = await _context.Tsks.ToListAsync();
            return task;
        }


        /// <summary>
        /// Get a task by its ID.
        /// </summary>
        /// <param name="Id">The ID of the task to retrieve.</param>
        /// <returns>single task</returns>
        public async Task<Tsk> GetById(int Id)
        {
            var task = await _context.Tsks.Where(t => t.Id == Id).FirstOrDefaultAsync();
            return task;
        }


        /// <summary>
        /// Add a new task to the database.
        /// </summary>
        /// <param name="tsk">The task to add.</param>
        /// <returns>The added task.</returns>
        public async Task<Tsk> Add(Tsk tsk)
        {
            _context.Tsks.Add(tsk);
            await _context.SaveChangesAsync();
            return tsk;
        }

        /// <summary>
        /// Edit a task in the database.
        /// </summary>
        /// <param name="Id">The ID of the task to edit.</param>
        /// <param name="tsk">The updated task data.</param>
        /// <returns>The edited task.</returns>
        public async Task<Tsk> Edit(int Id, Tsk tsk)
        {
            var taskById = await _context.Tsks.FindAsync(Id);
            taskById.Name = tsk.Name;
            taskById.Description = tsk.Description;
            _context.Entry(taskById).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return taskById;
        }

        /// <summary>
        /// Delete a task from the database by its ID.
        /// </summary>
        /// <param name="Id">The ID of the task to delete.</param>
        /// <returns>The deleted task.</returns>
        public async Task<Tsk> Delete(int Id)
        {
            var taskById = await _context.Tsks.Where(t => t.Id == Id).FirstOrDefaultAsync();
            _context.Entry(taskById).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return taskById;
        }




    }
}
