using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace ChallengeBackend.Models.Helpers
{
    public class DBContextHelper
    {
        int _currUserId = 0;
        Room _room = null;

        private static DBContextHelper _instance = null;
        public DbContext Context { get; set; }
        private DBContextHelper() 
        {
        }

        int GetTimestamp()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public void JoinRoom( int IdUser, int IdRoom )
        {

            _room = Get<Room>(1);

            if (null == _room)
            {
                Room newRoom = new Room() { Title = "cola1" };
                _room = Create(newRoom);
            }

            _currUserId = IdUser;


            User_Room session = new User_Room() { IdRoom = _room.Id, IdUser = _currUserId, LoggedInTimestamp = GetTimestamp() };
            Create(session);
        }

        internal void StoreMessage(string sendMessage)
        {
            Message newMessage = new Message() { IdRoom = _room.Id, IdUser = _currUserId, Text = sendMessage, Timestamp = GetTimestamp() };
            Create(newMessage);
        }

        public static DBContextHelper Instance { get => _instance ?? (_instance = new DBContextHelper()); }
        public Room Room { get => _room; }

        public DbSet<T> Table<T>() where T : class
        {
            return Context.Set<T>();
        }

        List<T> List<T>(params string[] includes) where T : class
        {
            if (includes != null)
            {
                IQueryable<T> query = Context.Set<T>();
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }

                return query.ToList();
            }
            return Context.Set<T>().ToList();
        }
        async Task<List<T>> ListAsync<T>(params string[] includes) where T : class
        {
            if (includes != null)
            {
                IQueryable<T> query = Context.Set<T>();
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }

                return await query.ToListAsync();
            }
            return await Context.Set<T>().ToListAsync();
        }

        List<T> List<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {
            if (includes != null)
            {
                IQueryable<T> query = Context.Set<T>().Where( predicate ?? (predicate = _ => true) );
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }

                return query.ToList<T>();
            }

            return Context.Set<T>().Where(predicate ?? (predicate = _ => true)).ToList();
        }

        async Task<List<T>> ListAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {
            if (includes != null)
            {
                IQueryable<T> query = Context.Set<T>().Where(predicate ?? (predicate = _ => true));
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }

                return await query.ToListAsync();
            }
            return await Context.Set<T>().Where(predicate ?? (predicate = _ => true)).ToListAsync();
        }

        T Get<T>(int? id) where T : class
        {
            return Context.Set<T>().Find(id);
        }
        async Task<T> GetAsync<T>(int? id) where T : class
        {
            return await Context.Set<T>().FindAsync(id);
        }
        async Task<T> GetAsync<T>(params object[] keyValues) where T : class
        {
            return await Context.Set<T>().FindAsync(keyValues);
        }

        T Create<T>(T entity) where T : class
        {
            var context = new ApplicationDbContext();
            T reg = context.Set<T>().Add(entity);
            context.SaveChanges();
            context.Dispose();
            return reg;
        }
        async Task<T> CreateAsync<T>(T entity) where T : class
        {
            T reg = Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return reg;
        }

        int Update<T>(T entity) where T : class
        {
            Context.Set<T>().AddOrUpdate(entity);
            return Context.SaveChanges();
        }
        async Task<int> UpdateAsync<T>(T entity) where T : class
        {
            Context.Set<T>().AddOrUpdate(entity);
            return await Context.SaveChangesAsync();
        }
        int Delete<T>(T entity) where T : class
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChanges();
        }
        async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync();
        }

        int Delete<T>(int? id) where T : class
        {
            return Delete(Get<T>(id));
        }
    }
}