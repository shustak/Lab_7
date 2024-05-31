using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace Lab_7
{
    class EntityContext : DbContext
    {
        public EntityContext(string name) : base(name)
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

        public DbSet<Student> Students { get; set; }
    }

    class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            context.Students.AddRange(new Student[]
            {
                new Student { FullName = "John Smith", Age = 23, Payment = 234, GroupId = 2 },
                new Student { FullName = "Uncle Benz", Age = 80, Payment = 231, GroupId = 2 },
                new Student { FullName = "Papa Johns", Age = 36, Payment = 532, GroupId = 1 },
            });
        }
    }
}
