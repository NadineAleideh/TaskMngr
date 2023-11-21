using Microsoft.AspNetCore.Mvc;
using TaskMngr.Interface;
using TaskMngr.Models;

namespace TaskMngr.Controllers
{

    /// <summary>
    /// Controller for managing tasks and related actions.
    /// </summary>
    public class TaskController : Controller
    {

        private readonly ITsk _task;

        public TaskController(ITsk task)
        {
            _task = task;
        }

        /// <summary>
        /// Display a list of all tasks.
        /// </summary>
        /// <returns>The view with a list of tasks.</returns>
        public async Task<IActionResult> Index()
        {
            var taskslist = await _task.GetAll();
            return View(taskslist);
        }

        /// <summary>
        /// To returns a partial view with the list of tasks.
        /// </summary>
        /// <returns> a partial view with the list of tasks.</returns>
        public async Task<IActionResult> LoadTasks()
        {
            var tasksList = await _task.GetAll();
            return PartialView("TaskListPartial", tasksList);
        }


        /// <summary>
        /// Display details of a task by its ID.
        /// </summary>
        /// <param name="Id">The ID of the task to display.</param>
        /// <returns>The view with task details.</returns>
        public async Task<IActionResult> Details(int Id)
        {
            var taskbyId = await _task.GetById(Id);

            return View(taskbyId);
        }

        /// <summary>
        /// get specific task by its ID.
        /// </summary>
        /// <param name="Id">The ID of the task to get.</param>
        /// <returns>Return task data as JSON</returns>
        [HttpGet]
        public async Task<IActionResult> GetTask(int Id)
        {
            var task = await _task.GetById(Id);
            return Json(task);
        }


        /// <summary>
        /// Display the form for adding a new task.
        /// </summary>
        /// <returns>The view with the task form.</returns>
        public async Task<IActionResult> Add()
        {
            Tsk task = new Tsk();
            return View(task);
        }

        /// <summary>
        /// Handle the submission of the task form to add a new task.
        /// </summary>
        /// <param name="tsk">The task to add.</param>
        /// <returns>Redirects to the tasks list if successful; otherwise, returns the adding task form with errors.</returns>

        [HttpPost]
        public async Task<IActionResult> AddTask(Tsk tsk)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400; // Bad Request
                return PartialView("TaskForm", tsk);
            }

            await _task.Add(tsk);

            return RedirectToAction("Index");

        }


        /// <summary>
        /// Display the form for editing a task.
        /// </summary>
        /// <param name="Id">The ID of the task to edit.</param>
        /// <returns>The view with the editing task form for editing.</returns>

        public async Task<IActionResult> Edit(int Id)
        {
            var taskbyId = await _task.GetById(Id);

            return View(taskbyId);
        }

        /// <summary>
        /// Handle the submission of the task form to edit a task.
        /// </summary>
        /// <param name="Id">The ID of the task to edit.</param>
        /// <param name="tsk">The updated category data.</param>
        /// <returns>returns a partial view with the updated list of tasks</returns>

        [HttpPost]
        public async Task<IActionResult> EditTask(Tsk tsk)
        {
            if (!ModelState.IsValid)
            {
                return View(tsk);
            }

            await _task.Edit(tsk.Id, tsk);

            return PartialView("TaskListPartial", await _task.GetAll());
        }

        /// <summary>
        /// Delete a task by its ID.
        /// </summary>
        /// <param name="Id">The ID of the task to delete.</param>
        /// <returns>Redirects to the tasks list after deletion.</returns>

        public async Task<IActionResult> Delete(int Id)
        {
            var task = await _task.GetById(Id);

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int Id)
        {
            var task = await _task.GetById(Id);

            if (task == null)
            {
                return NotFound();
            }

            await _task.Delete(Id);

            var tasksList = await _task.GetAll();

            return PartialView("TaskListPartial", tasksList);
        }


    }
}
