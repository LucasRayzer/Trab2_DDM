using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trabalho02.Model;

namespace Trabalho02.Database
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Project>().Wait();
            _database.CreateTableAsync<Model.Task>().Wait();
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Habit>().Wait();
            _database.CreateTableAsync<Statistic>().Wait();
        }
        public Task<int> SaveProjectAsync(Project project)
        {
            return _database.InsertAsync(project);
        }

        public Task<List<Project>> GetProjectsAsync()
        {
            return _database.Table<Project>().ToListAsync();
        }

        public Task<Project> GetProjectByIdAsync(int id)
        {
            return _database.FindAsync<Project>(id);
        }

        public Task<int> DeleteProjectAsync(Project project)
        {
            return _database.DeleteAsync(project);
        }

        public Task<int> SaveTaskAsync(Model.Task task)
        {
            if (task.Id == 0)
            {
                return _database.InsertAsync(task);
            }
            else
            {
                return _database.UpdateAsync(task);
            }
        }
        public Task<List<Model.Task>> GetTasksAsync()
        {
            return _database.Table<Model.Task>().ToListAsync();
        }

        public Task<List<Model.Task>> GetTasksByProjectIdAsync(int projectId)
        {
            return _database.Table<Model.Task>().Where(t => t.ProjectId == projectId).ToListAsync();
        }

        public Task<Model.Task> GetTaskByIdAsync(int id)
        {
            return _database.FindAsync<Model.Task>(id);
        }

        public Task<int> DeleteTaskAsync(Model.Task task)
        {
            return _database.DeleteAsync(task);
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.FindAsync<User>(username);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public Task<int> SaveHabitAsync(Habit habit)
        {
            if (habit.Id == 0)
            {
                return _database.InsertAsync(habit); 
            }
            else
            {
                return _database.UpdateAsync(habit);
            }
        }

        public Task<List<Habit>> GetHabitsByTaskIdAsync(int taskId)
        {
            return _database.Table<Habit>().Where(h => h.TaskId == taskId).ToListAsync();
        }

        public Task<List<Habit>> GetHabitsByProjectIdAsync(int projectId)
        {
            return _database.QueryAsync<Habit>(
                "SELECT * FROM Habit WHERE TaskId IN (SELECT Id FROM Task WHERE ProjectId = ?)", projectId);
        }

        public Task<int> DeleteHabitAsync(Habit habit)
        {
            return _database.DeleteAsync(habit);
        }


    }
}
