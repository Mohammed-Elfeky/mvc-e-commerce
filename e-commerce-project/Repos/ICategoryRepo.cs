using System.Collections.Generic;
using e_commerce_project.Models;

namespace e_commerce_project.Repos
{
    public interface ICategoryRepo
    {
        int Delete(int id);
        int Edit(int id, Category category);
        Category FindById(int id);
        List<Category> GetAll();
        int Insert(Category category);
        public bool FindByName(string name);
    }
}