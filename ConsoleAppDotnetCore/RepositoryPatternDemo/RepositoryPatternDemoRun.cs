using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace ConsoleAppDotnetCore.RepositoryPatternDemo
{
    internal class RepositoryPatternDemoRun
    {
        public static void Run(ServiceCollection services)
        {
            Console.WriteLine("Repository Pattern Demo - NoSQL (simulated)");

            // Register simulated NoSQL client and repositories
            services.AddSingleton<INoSqlClient, InMemoryNoSqlClient>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IUserRepository, UserRepository>();

            using (var provider = services.BuildServiceProvider())
            {
                var userRepo = provider.GetRequiredService<IUserRepository>();

                // Create users
                var user1 = new User { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@example.com" };
                var user2 = new User { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@example.com" };

                userRepo.Add(user1);
                userRepo.Add(user2);

                Console.WriteLine("Added two users.");

                // Read users
                var all = userRepo.GetAll();
                Console.WriteLine($"All users ({all.Count()}):");
                foreach (var u in all)
                {
                    Console.WriteLine($" - {u.Id} {u.Name}<{u.Email}>");
                }

                // Update a user
                user1.Name = "Alice Cooper";
                userRepo.Update(user1);
                Console.WriteLine($"Updated user {user1.Id} name to {user1.Name}.");

                // Query by email using custom repository method
                var queried = userRepo.GetByEmail("bob@example.com");
                Console.WriteLine(queried is null ? "Bob not found" : $"Found by email: {queried.Name}");

                // Delete a user
                userRepo.Delete(user2.Id);
                Console.WriteLine($"Deleted user {user2.Id}.");

                Console.WriteLine("Users after delete:");
                foreach (var u in userRepo.GetAll())
                {
                    Console.WriteLine($" - {u.Id} {u.Name}<{u.Email}>");
                }
            }

            Console.WriteLine();
        }

        // Generic repository interface
        public interface IRepository<T> where T : class
        {
            T Get(Guid id);
            IEnumerable<T> GetAll();
            void Add(T entity);
            void Update(T entity);
            void Delete(Guid id);
        }

        // User-specific repository interface
        public interface IUserRepository : IRepository<User>
        {
            User GetByEmail(string email);
        }

        // Generic repository implementation using a simple NoSQL client
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly INoSqlClient _client;

            public Repository(INoSqlClient client)
            {
                _client = client;
            }

            public T Get(Guid id) => _client.Get<T>(id);

            public IEnumerable<T> GetAll() => _client.GetAll<T>();

            public void Add(T entity) => _client.Insert(entity);

            public void Update(T entity) => _client.Update(entity);

            public void Delete(Guid id) => _client.Delete<T>(id);
        }

        // Concrete user repository with domain-specific methods
        public class UserRepository : Repository<User>, IUserRepository
        {
            private readonly INoSqlClient _client;

            public UserRepository(INoSqlClient client) : base(client)
            {
                _client = client;
            }

            public User GetByEmail(string email)
            {
                return _client.GetAll<User>().FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
            }
        }

        // Simple NoSQL client abstraction
        public interface INoSqlClient
        {
            T Get<T>(Guid id) where T : class;
            IEnumerable<T> GetAll<T>() where T : class;
            void Insert<T>(T entity) where T : class;
            void Update<T>(T entity) where T : class;
            void Delete<T>(Guid id) where T : class;
        }

        // In-memory simulation of a NoSQL store (thread-safe)
        public class InMemoryNoSqlClient : INoSqlClient
        {
            private readonly ConcurrentDictionary<Type, ConcurrentDictionary<Guid, object>> _store = new();

            private ConcurrentDictionary<Guid, object> GetCollection<T>()
            {
                var type = typeof(T);
                return _store.GetOrAdd(type, _ => new ConcurrentDictionary<Guid, object>());
            }

            public T Get<T>(Guid id) where T : class
            {
                var col = GetCollection<T>();
                if (col.TryGetValue(id, out var obj))
                    return obj as T;
                return null;
            }

            public IEnumerable<T> GetAll<T>() where T : class
            {
                var col = GetCollection<T>();
                return col.Values.Cast<T>().ToList();
            }

            public void Insert<T>(T entity) where T : class
            {
                var idProp = GetIdProperty(entity);
                if (idProp == null) throw new InvalidOperationException("Entity must have an Id property of type Guid.");
                var id = (Guid)idProp.GetValue(entity);
                var col = GetCollection<T>();
                col[id] = entity;
            }

            public void Update<T>(T entity) where T : class
            {
                var idProp = GetIdProperty(entity);
                if (idProp == null) throw new InvalidOperationException("Entity must have an Id property of type Guid.");
                var id = (Guid)idProp.GetValue(entity);
                var col = GetCollection<T>();
                col[id] = entity;
            }

            public void Delete<T>(Guid id) where T : class
            {
                var col = GetCollection<T>();
                col.TryRemove(id, out _);
            }

            private System.Reflection.PropertyInfo GetIdProperty<T>(T entity)
            {
                return entity.GetType().GetProperty("Id");
            }
        }
    }
}
