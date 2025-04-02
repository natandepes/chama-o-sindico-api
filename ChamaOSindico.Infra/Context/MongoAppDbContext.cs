using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Reflection;

namespace ChamaOSindico.Infra.Context
{
    public class MongoAppDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoAppDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            ApplyConfigurations();
        }

        // Método genérico capaz de recuperar/criar qualquer coleção de dados do banco (visando escalabilidade e manutenabilidade do código)
        public IMongoCollection<T> GetCollection<T>(string collectionName = null)
        {
            collectionName ??= typeof(T).Name; // Utiliza o nome da classe para recuperar/criar a coleção de dados
            return _database.GetCollection<T>(collectionName);
        }

        // Método para aplicar as configurações de mapeamento de entidades
        private void ApplyConfigurations()
        {
            var configTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMongoEntityConfiguration<>)));

            foreach (var type in configTypes)
            {
                var configInstance = Activator.CreateInstance(type);
                var entityType = type.GetInterfaces().First().GetGenericArguments()[0];
                var classMapType = typeof(BsonClassMap<>).MakeGenericType(entityType);
                var classMapInstance = Activator.CreateInstance(classMapType);

                var configureMethod = type.GetMethod("Configure");
                configureMethod.Invoke(configInstance, new object[] { classMapInstance });

                BsonClassMap.RegisterClassMap((BsonClassMap)classMapInstance);
            }
        }
    }
}
