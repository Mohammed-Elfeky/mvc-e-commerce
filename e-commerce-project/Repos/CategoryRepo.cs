using System.Collections.Generic;
using e_commerce_project.Models;
using System.Linq;
namespace e_commerce_project.Repos
{
    public class CategoryRepo : ICategoryRepo
    {

        context db;
        public CategoryRepo(context db)
        {
            this.db = db;
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public Category FindById(int id)
        {
            return db.Categories.FirstOrDefault(x => x.Id == id);
        }
        public bool FindByName(string name)
        {
            return db.Categories.Any(cat=>cat.Name==name);
        }
        public int Insert(Category category)
        {
            db.Categories.Add(category);
            int raw = db.SaveChanges();
            return raw;
        }
        public int Edit(int id, Category category)
        {
            Category cat = FindById(id);
            if (cat != null)
            {
                cat.Name = category.Name;
                cat.Description = category.Description;
                int raw = db.SaveChanges();
                return raw;
            }
            return 0;
        }
        public int Delete(int id)
        {
            Category cat = FindById(id);
            db.Categories.Remove(cat);
            return db.SaveChanges();
        }

    }
}
