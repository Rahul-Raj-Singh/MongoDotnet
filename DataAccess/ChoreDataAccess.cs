using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDotnet.Model;
using System.Linq;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDotnet.DataAccess
{
    public class ChoreDataAccess
    {
        private const string CONNECTION_STRING = "mongodb://localhost:27017";
        private const string DATABASE_NAME = "choredb";
        private const string CHORE_COLLECTION = "chores";

        private IMongoCollection<T> ConnectToMongo<T>(string collection)
        {
            var client = new MongoClient(CONNECTION_STRING);
            var db = client.GetDatabase(DATABASE_NAME);

            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camel case", pack, _ => true);

            return db.GetCollection<T>(collection);
        }

        public List<Chore> GetChores(Func<Chore, bool> filter = null)
        {
            var choreCollection = ConnectToMongo<Chore>(CHORE_COLLECTION);

            if (filter is null)
                return choreCollection.AsQueryable().ToList();
            else
                return choreCollection.AsQueryable().Where(filter).ToList();
        }

        public void UpsertChore(Chore chore)
        {
            var choreCollection = ConnectToMongo<Chore>(CHORE_COLLECTION);

            if (chore.Id is null)
                choreCollection.InsertOne(chore);
            else
                choreCollection.ReplaceOne(c => c.Id == chore.Id, chore);
        }

        public void DeleteChore(Chore chore)
        {
            var choreCollection = ConnectToMongo<Chore>(CHORE_COLLECTION);
            choreCollection.DeleteOne(c => c.Id == chore.Id);
        }
    }
}

