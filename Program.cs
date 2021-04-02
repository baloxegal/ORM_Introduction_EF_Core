using System;
using System.Collections.Generic;
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
            //    User user1 = new User { Name = "Tom", Age = 33 };
            //    User user2 = new User { Name = "Alice", Age = 26 };

            //    db.Users.Add(user1);
            //    db.Users.Add(user2);
            //    db.SaveChanges();
            //    Console.WriteLine("Objects are added");

            //    var users = db.Users.ToList();
            //    Console.WriteLine("Get objects from database");
            //    foreach (User u in users)
            //    {
            //        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            //    }
            //}

            // CREATE (INSERT)
            using (ACDBContext db = new ACDBContext())
            {                
                Customer customer1 = new Customer { FirstName = "Gheorghe", MainPhoneNum = "12634654635" };
                Customer customer2 = new Customer { FirstName = "Michael", MainPhoneNum = "54635465465" };

                // Добавление
                db.Customers.Add(customer1);
                db.Customers.Add(customer2);

                //db.Users.AddRange(user1, user2);

                db.SaveChanges();
            }

            // READ ALL (SELECT ALL)
            using (ACDBContext db = new ACDBContext())
            {
                var customers = db.Customers.ToList();
                Console.WriteLine("Selected data:");
                foreach (var c in customers)
                {
                    Console.WriteLine(c.ToString());
                }
            }

            // READ BY ID (SELECT BY ID)
            using (ACDBContext db = new ACDBContext())
            {
                var customer = db.Customers.Find(2);
                Console.WriteLine("Selected data is: " + customer.ToString());
            }

            // UPDATE
            Customer customer_1 = null;
            using (ACDBContext db = new ACDBContext())
            {
                customer_1 = db.Customers.Find(5);
                Console.WriteLine("Selected data for update is: " + customer_1.ToString());
            }

            using (ACDBContext db = new ACDBContext())
            {
                if (customer_1 != null)
                {
                    customer_1.FirstName = "Sam";
                    customer_1.LastName = "Deep";
                    db.Customers.Update(customer_1);
                }
                db.SaveChanges();

                Console.WriteLine("\nAfter update:");

                var customers = db.Customers.ToList();
                foreach (var c in customers)
                {
                    Console.WriteLine(c.ToString());
                }
            }

            // DELETE
            customer_1 = null;
            using (ACDBContext db = new ACDBContext())
            {
                customer_1 = db.Customers.Find(5);
                Console.WriteLine("Selected data for delete is: " + customer_1.ToString());
            }
            using (ACDBContext db = new ACDBContext())
            {                
                if (customer_1 != null)
                {
                    db.Customers.Remove(customer_1);

                    // db.Customers.RemoveRange(customer1, customer2);

                    db.SaveChanges();
                }
                Console.WriteLine("\nData after delete:");

                var customers = db.Customers.ToList();

                foreach (Customer u in customers)
                {
                    Console.WriteLine(u.ToString());
                }                
            }

            // CUSTOM DELETE - ALL REPEATED
            using (ACDBContext db = new ACDBContext())
            {
                var customers_1 = db.Customers.ToList();

                var customers_2 = new List<Customer>();
                
                foreach(var c in customers_1)
                {
                    if (customers_2.Any(i => i.FirstName == c.FirstName))
                    {
                        db.Customers.Remove(c);
                    }
                    else
                        customers_2.Add(c);
                }
                db.SaveChanges();
                
                Console.WriteLine("\nData after delete:");

                var customers = db.Customers.ToList();

                foreach (Customer c in customers)
                {
                    Console.WriteLine(c.ToString());
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
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=helloappdb_1;Trusted_Connection=True;");
        }
    }
}
