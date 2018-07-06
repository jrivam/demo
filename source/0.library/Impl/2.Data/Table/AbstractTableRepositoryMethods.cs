﻿using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Impl.Data.Table
{
    public abstract class AbstractTableRepositoryMethods<T, U> : AbstractTableRepositoryProperties<T>, ITableRepositoryMethods<T, U>
        where T : IEntity, new()
        where U : class, ITableRepositoryProperties<T>
    {
        protected readonly IRepositoryTable<T, U> _repository;

        public AbstractTableRepositoryMethods(IRepositoryTable<T, U> repository,
            string name, string reference)
            : base(name, reference)
        {
            _repository = repository;

            InitDbCommands();
        }
        public AbstractTableRepositoryMethods(IRepositoryTable<T, U> repository,
            string name, string reference, T entity)
            : this(repository, name, reference)
        {
            SetProperties(entity);
        }

        public virtual U Clear()
        {
            return _repository.Clear(this as U);
        }

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            return _repository.Select(this as U, usedbcommand);
        }
        public abstract (Result result, U data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<T, U> query = default(IQueryRepositoryMethods<T, U>));

        public virtual (Result result, U data) Insert(bool usedbcommand = false)
        {
            return _repository.Insert(this as U, usedbcommand);
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false)
        {
            return _repository.Update(this as U, usedbcommand);
        }
        public virtual (Result result, U data) Delete(bool usedbcommand = false)
        {
            return _repository.Delete(this as U, usedbcommand);
        }

        public U SetProperties(T entity, bool nulls = false)
        {
            return Entities.Helper.SetProperties<T, U>(entity, this as U, nulls);
        }
    }
}
