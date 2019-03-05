using library.Interface.Data.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain.Table
{
    public class LogicTable<T, U, V> : Logic<T, U>, ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>, ITableRepositoryMethods<T, U>
        where V : ITableLogic<T, U>
    {
        protected readonly IMapperLogic<T, U, V> _mapper;

        public LogicTable(IMapperLogic<T, U, V> mapper)
            : base()
        {
            _mapper = mapper;
        }

        public virtual (Result result, V domain) Load(V domain, bool usedbcommand = false)
        {
            if (domain.Data.Entity.Id != null)
            {
                var select = domain.Data.Select(usedbcommand);

                if (select.result.Success && select.data != null)
                {
                    _mapper.Clear(domain, 1, 0);
                    _mapper.Load(domain, 1, 0);

                    _mapper.Extra(domain, 1, 0);

                    domain.Changed = false;
                    domain.Deleted = false;

                    return (select.result, domain);
                }

                return (select.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Load", $"Id in {domain.Data.Description.Name} cannot be null") } }, domain);
        }
        public virtual (Result result, V domain) LoadQuery(V domain, int maxdepth = 1)
        {
            if (domain.Data.Entity.Id != null)
            {
                var selectquery = domain.Data.SelectQuery(maxdepth);

                if (selectquery.result.Success && selectquery.data != null)
                {
                    _mapper.Clear(domain, maxdepth, 0);
                    _mapper.Load(domain, maxdepth, 0);

                    _mapper.Extra(domain, maxdepth, 0);

                    domain.Changed = false;
                    domain.Deleted = false;

                    return (selectquery.result, domain);
                }

                return (selectquery.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "LoadQuery", $"Id in {domain.Data.Description.Name} cannot be null") } }, domain);
        }

        public virtual (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            if (domain.Changed)
            {
                var checkisunique = domain.Data.CheckIsUnique();

                if (checkisunique.isunique)
                {
                    var updateinsert = (domain.Data.Entity.Id != null ? domain.Data.Update(useupdatedbcommand) : domain.Data.Insert(useinsertdbcommand));

                    if (updateinsert.result.Success)
                    {
                        domain.Changed = false;
                    }

                    return (updateinsert.result, domain);
                }
                else
                {
                    return (checkisunique.result, domain); 
                }
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, "Save", $"No changes to persist in {domain.Data.Description.Name} with Id {domain.Data.Entity.Id}") } }, domain);
        }
        public virtual (Result result, V domain) Erase(V domain, bool usedbcommand = false)
        {
            if (domain.Data.Entity.Id != null)
            {
                if (!domain.Deleted)
                {
                    var delete = domain.Data.Delete(usedbcommand);

                    if (delete.result.Success)
                    {
                        domain.Deleted = true;
                    }

                    return (delete.result, domain);
                }

                return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, "Erase", $"{domain.Data.Description.Name} with Id {domain.Data.Entity.Id} already deleted") } }, domain);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Erase", $"Id in {domain.Data.Description.Name} cannot be null") } }, domain);
        }
    }
}
