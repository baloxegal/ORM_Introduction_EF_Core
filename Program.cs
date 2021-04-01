using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ORM_Introduction_EF_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    // создаем два объекта User
            //    User user1 = new User { Name = "Tom", Age = 33 };
            //    User user2 = new User { Name = "Alice", Age = 26 };

            //    // добавляем их в бд
            //    db.Users.Add(user1);
            //    db.Users.Add(user2);
            //    db.SaveChanges();
            //    Console.WriteLine("Objects are added");

            //    // получаем объекты из бд и выводим на консоль
            //    var users = db.Users.ToList();
            //    Console.WriteLine("Get objects from database");
            //    foreach (User u in users)
            //    {
            //        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            //    }
            //}

            // Добавление
            using (ACDBContext db = new ACDBContext())
            {
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Alice", Age = 26 };

                // Добавление
                db.Users.Add(user1);
                db.Users.Add(user2);

                //db.Users.AddRange(user1, user2);

                db.SaveChanges();
            }

            // получение
            using (ACDBContext db = new ACDBContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Данные после добавления:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            // Редактирование
            using (ACDBContext db = new ACDBContext())
            {
                // получаем первый объект
                User user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    user.Name = "Bob";
                    user.Age = 44;
                    //обновляем объект
                    //db.Users.Update(user);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после редактирования:");
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            // Редактирование
            using (ApplicationContext db = new ApplicationContext())
            {
                // Редактирование
                if (user != null)
                {
                    user.Name = "Sam";
                    user.Age = 33;
                    db.Users.Update(user);

                    //db.Users.UpdateRange(user1, user2);

                }
                db.SaveChanges();
                // выводим данные после обновления
                Console.WriteLine("\nДанные после редактирования:");
                var users = db.Users.ToList();
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            User user = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объект
                user = db.Users.FirstOrDefault();
                Console.WriteLine("Данные до редактирования:");
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            //...................

            // Редактирование
            using (ApplicationContext db = new ApplicationContext())
            {
                // Редактирование
                if (user != null)
                {
                    user.Name = "Sam";
                    user.Age = 33;
                }
                db.SaveChanges();
                // выводим данные после обновления
                Console.WriteLine("\nДанные после редактирования:");
                var users = db.Users.ToList();
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            // Удаление
            using (ACDBContext db = new ACDBContext())
            {
                // получаем первый объект
                User user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    //удаляем объект
                    db.Users.Remove(user);

                   // db.Users.RemoveRange(user1, user2);

                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после удаления:");
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}
