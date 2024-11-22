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

            // Criação das tabelas
            _database.CreateTableAsync<Project>().Wait();
            _database.CreateTableAsync<Model.Task>().Wait();
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Habit>().Wait();
            _database.CreateTableAsync<Statistic>().Wait();
        }

        // Métodos CRUD para Project
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

        // Métodos CRUD para Task
        public Task<int> SaveTaskAsync(Model.Task task)
        {
            if (task.Id == 0)
            {
                return _database.InsertAsync(task); // Insere se for nova
            }
            else
            {
                return _database.UpdateAsync(task); // Atualiza se já existir
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

        // Métodos CRUD para User
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
    
    }
}
