using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;

namespace StudentManagement.DAL.Service
{
    public class GenericService<T> : IDisposable, IGenericService<T> where T : class
    {
        protected StudentManagementDbContext _db { get; set; }

        protected DbSet<T> _table = null;

        public GenericService()
        {
            _db = new StudentManagementDbContext();
            _table = _db.Set<T>();
        }

        public GenericService(StudentManagementDbContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            //return _table.AsNoTracking().ToList();
            return _table.ToList();
        }

        public T SelectById(object id)
        {
            try
            {
                return _table.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public void Insert(T obj)
        {
            PropertyInfo info = obj.GetType().GetProperty("Created");
            if (info != null)
                info.SetValue(obj, DateTime.Now);
            _table.Add(obj);
        }

        public void Insert(ref T obj, string userName)
        {
            PropertyInfo info = obj.GetType().GetProperty("Created");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            info = obj.GetType().GetProperty("CreatedBy");
            if (info != null)
                info.SetValue(obj, userName);

            _table.Add(obj);
        }

        public void Update(T obj)
        {
            PropertyInfo info = obj.GetType().GetProperty("Modified");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Update(T obj, string userName)
        {
            PropertyInfo info = obj.GetType().GetProperty("Modified");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            info = obj.GetType().GetProperty("ModifiedBy");
            if (info != null)
                info.SetValue(obj, userName);

            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void DeleteMark(object id, string userName)
        {
            T obj = _table.Find(id);
            if (obj == null)
                return;

            PropertyInfo info = obj.GetType().GetProperty("Modified");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            info = obj.GetType().GetProperty("ModifiedBy");
            if (info != null)
                info.SetValue(obj, userName);

            info = obj.GetType().GetProperty("Deleted");
            if (info != null)
                info.SetValue(obj, true);

            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Delete(object id, string userName)
        {
            Delete(id);
            //Write log here
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            this._db.Dispose();
        }
    }
}