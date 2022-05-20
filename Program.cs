using System;
using System.Linq;
using MongoDB.Driver;
using MongoDotnet.DataAccess;
using MongoDotnet.Model;

namespace MongoDotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            var userDA = new UserDataAccess();
            var choreDA = new ChoreDataAccess();

            Console.WriteLine("MongoDB Operations Started");

            // userDA.UpsertUser(
            //     new User { Name = "Brad"}
            // ); 
            // userDA.UpsertUser(
            //     new User { Name = "Tim"}
            // ); 
            // userDA.UpsertUser(
            //     new User { Name = "Mike"}
            // );

            // Console.WriteLine("Users Added");

            var users = userDA.GetUsers();
            var chores = choreDA.GetChores();

            var firstChore = chores.First();
            firstChore.AssignedTo = users.First();

            choreDA.UpsertChore(firstChore);

            Console.WriteLine(string.Join("\n", users));
            Console.WriteLine(string.Join("\n", chores));


        }
    }
}
