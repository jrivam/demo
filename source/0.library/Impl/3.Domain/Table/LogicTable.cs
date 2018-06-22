using library.Interface.Data.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain.Table
{
    public class LogicTable<T, U, V> : Logic<T, U, V>, ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        public LogicTable(IMapperLogic<T, U, V> mapper)
            : base(mapper)
        {
        }

        public virtual V Clear(V domain, ITableRepositoryMethods<T, U> entityrepository)
        {
            entityrepository.Clear();

            domain.Changed = false;
            domain.Deleted = false;

            _mapper.Map(domain);

            return domain;
        }

        public virtual (Result result, V domain) Load(V domain, ITableRepositoryMethods<T, U> entityrepository, bool usedbcommand = false)
        {
            if (domain.Data.Entity.Id != null)
            {
                var select = entityrepository.Select(usedbcommand);

                if (select.result.Success && select.data != null)
                {
                    _mapper.Clear(domain);
                    _mapper.Map(domain);

                    domain.Changed = false;
                    domain.Deleted = false;

                    return (select.result, domain);
                }

                return (select.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "Load: Id cannot be null") } }, domain);
        }
        public virtual (Result result, V domain) LoadQuery(V domain, ITableRepositoryMethods<T, U> entityrepository, int maxdepth = 1)
        {
            if (domain.Data.Entity.Id != null)
            {
                var select = entityrepository.SelectQuery(maxdepth);

                if (select.result.Success && select.data != null)
                {
                    _mapper.Clear(domain);
                    _mapper.Map(domain);

                    domain.Changed = false;
                    domain.Deleted = false;

                    return (select.result, domain);
                }

                return (select.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "LoadQuery: Id cannot be null") } }, domain);
        }
        public virtual (Result result, V domain) Save(V domain, ITableRepositoryMethods<T, U> entityrepository, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            if (domain.Changed)
            {
                var updateinsert = (domain.Data.Entity.Id != null ? entityrepository.Update(useupdatedbcommand) : entityrepository.Insert(useinsertdbcommand));

                if (updateinsert.result.Success)
                {
                    _mapper.Map(domain);

                    domain.Changed = false;
                }

                return (updateinsert.result, domain);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Save: no changes to persist") } }, domain);
        }
        public virtual (Result result, V domain) Erase(V domain, ITableRepositoryMethods<T, U> entityrepository, bool usedbcommand = false)
        {
            if (domain.Data.Entity.Id != null)
            {
                if (!domain.Deleted)
                {
                    var delete = entityrepository.Delete(usedbcommand);

                    if (delete.result.Success)
                    {
                        _mapper.Map(domain);

                        domain.Deleted = true;
                    }

                    return (delete.result, domain);
                }

                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Erase: already deleted") } }, domain);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "Erase: Id cannot be null") } }, domain);
        }
    }
}
