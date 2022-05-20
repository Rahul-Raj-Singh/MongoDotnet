using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDotnet.Model;
using System.Linq;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDotnet.DataAccess
{
    public class UserDataAccess
    {
        private const string CONNECTION_STRING = "mongodb://localhost:27017";
        private const string DATABASE_NAME = "choredb";
        private const string USER_COLLECTION = "users";

        private IMongoCollection<T> ConnectToMongo<T>(string collection)
        {
            var client = new MongoClient(CONNECTION_STRING);
            var db = client.GetDatabase(DATABASE_NAME);

            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camel case", pack, _ => true);

            return db.GetCollection<T>(collection);
        }

        public List<User> GetUsers(Func<User, bool> filter = null)
        {
            var userCollection = ConnectToMongo<User>(USER_COLLECTION);

            if (filter is null)
                return userCollection.AsQueryable().ToList();
            else
                return userCollection.AsQueryable().Where(filter).ToList();
        }

        public void UpsertUser(User user)
        {
            var userCollection = ConnectToMongo<User>(USER_COLLECTION);

            if (user.Id is null)
                userCollection.InsertOne(user);
            else
                userCollection.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void DeleteUser(User user)
        {
            var userCollection = ConnectToMongo<User>(USER_COLLECTION);
            userCollection.DeleteOne(u => u.Id == user.Id);
        }
    }
}

