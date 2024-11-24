using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho02.Model;

namespace Trabalho02.Service
{
    public class ProjectService
    {
        private List<Project> Projects { get; set; } = new();

        public ProjectService()
        {
            Projects.Add(new Project { Id = 1, Name = "Example Project 1", Description = "An example project" });
            Projects.Add(new Project { Id = 2, Name = "Example Project 2", Description = "Another example project" });
        }

        public List<Project> GetAllProjects()
        {
            return Projects;
        }

        public void AddProject(Project project)
        {
            Projects.Add(project);
        }

        public void UpdateProject(Project project)
        {
            var existingProject = Projects.FirstOrDefault(p => p.Id == project.Id);
            if (existingProject != null)
            {
                existingProject.Name = project.Name;
                existingProject.Description = project.Description;
            }
        }
        }
    }
