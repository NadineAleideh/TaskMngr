
using TaskMngr.Models;

namespace TaskMngr.Interface
{/// <summary>
 /// Interface for the Task service, providing Task management functionality.
 /// </summary>
    public interface ITsk
    {

        /// <summary>
        /// Get a list of all tasks.
        /// </summary>
        Task<List<Tsk>> GetAll();

        /// <summary>
        /// Get the Task details by its ID.
        /// </summary>
        /// <param name="Id">The ID of the Task to retrieve.</param>
        Task<Tsk> GetById(int Id);

        /// <summary>
        /// Add a new Task with the provided data.
        /// </summary>
        /// <param name="tsk">The Task data to add.</param>
        Task<Tsk> Add(Tsk tsk);

        /// <summary>
        /// Edit the Task with the provided data.
        /// </summary>
        /// <param name="Id">The ID of the Task to edit.</param>
        /// <param name="tsk">The updated Task data.</param>
        Task<Tsk> Edit(int Id, Tsk tsk);

        /// <summary>
        /// Delete the Task by its ID.
        /// </summary>
        /// <param name="Id">The ID of the Task to delete.</param>
        Task<Tsk> Delete(int Id);

    }

}
