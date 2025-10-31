using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CollegeApp.Data.Repository
{
    public class CollegeRepository<T>: ICollegeRepository<T> where T : class
    {
        private readonly CollegeContext _dbcontext;
        private readonly DbSet<T> _dbset;

        public CollegeRepository(CollegeContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbset = _dbcontext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if(typeof(T) == typeof(Course))
            {
                var courses = await _dbcontext.Courses
                               .Where(n => n.CourseId == id)
                               .FirstOrDefaultAsync();
                return (T)(object)(courses);
            }
            else if(typeof(T) == typeof(Student))
            {
                var students = await _dbcontext.Students
                               .Where(n => n.StudentId == id)
                               .FirstOrDefaultAsync();
                return (T)(object)(students);
            }
            return null;
        }
        public async Task<T> GetByName(string name)
        {
            if (typeof(T) == typeof(Course))
            {
                var courses = await _dbcontext.Courses
                       .Where(n => n.CourseName == name)
                       .FirstOrDefaultAsync();

                return (T)(object)courses;
            }
            else if (typeof(T) == typeof(Student))
            {
                var students = await _dbcontext.Students
                       .Where(n => n.Name == name)
                       .FirstOrDefaultAsync();

                return (T)(object)students;
            }

            return null;
        }
        public async Task AddAsync(T entity)
        {
            _dbset.Add(entity);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, T entity)
        {
            var existingEntity = await GetByIdAsync(id);

            // Get the primary key property name
            var keyPropertyName = _dbcontext.Model.FindEntityType(typeof(T))
                .FindPrimaryKey().Properties.First().Name;

            // Copy all properties EXCEPT the primary key
            foreach (var property in _dbcontext.Entry(existingEntity).Properties)
            {
                if (property.Metadata.Name != keyPropertyName)
                {
                    var newValue = typeof(T).GetProperty(property.Metadata.Name)?.GetValue(entity);
                    if (newValue != null)
                    {
                        property.CurrentValue = newValue;
                        property.IsModified = true;
                    }
                }
            }

            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbset.Remove(entity);
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
