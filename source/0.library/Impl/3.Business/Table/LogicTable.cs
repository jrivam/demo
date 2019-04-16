using Library.Interface.Business.Mapper;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Impl.Domain.Table
{
    public class LogicTable<T, U, V> : Logic<T, U, V>, ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public LogicTable(IMapperLogic<T, U, V> mapper)
            : base(mapper)
        {
        }

        public virtual (Result result, V domain) Load(V table, bool usedbcommand = false)
        {
            if (table.Data.Entity.Id != null)
            {
                var select = table.Data.Select(usedbcommand);

                if (select.result.Success && select.data != null)
                {
                    table.Data = select.data;

                    _mapper.Clear(table, 1, 0);
                    _mapper.Load(table, 1, 0);

                    _mapper.Extra(table, 1, 0);

                    table.Changed = false;
                    table.Deleted = false;

                    return (select.result, table);
                }

                return (select.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Load", $"Id in {table.Data.Description.Name} cannot be null") } }, default(V));
        }
        public virtual (Result result, V domain) LoadQuery(V table, int maxdepth = 1)
        {
            if (table.Data.Entity.Id != null)
            {
                var selectquery = table.Data.SelectQuery(maxdepth);

                if (selectquery.result.Success && selectquery.data != null)
                {
                    table.Data = selectquery.data;

                    _mapper.Clear(table, maxdepth, 0);
                    _mapper.Load(table, maxdepth, 0);

                    _mapper.Extra(table, maxdepth, 0);

                    table.Changed = false;
                    table.Deleted = false;

                    return (selectquery.result, table);
                }

                return (selectquery.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "LoadQuery", $"Id in {table.Data.Description.Name} cannot be null") } }, default(V));
        }

        public virtual (Result result, V domain) Save(V table, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            if (table.Changed)
            {
                var checkisunique = table.Data.CheckIsUnique();

                if (checkisunique.isunique)
                {
                    var updateinsert = (table.Data.Entity.Id != null ? table.Data.Update(useupdatedbcommand) : table.Data.Insert(useinsertdbcommand));

                    if (updateinsert.result.Success)
                    {
                        table.Changed = false;
                    }

                    return (updateinsert.result, table);
                }
                else
                {
                    return (checkisunique.result, default(V)); 
                }
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, "Save", $"No changes to persist in {table.Data.Description.Name} with Id {table.Data.Entity.Id}") } }, default(V));
        }
        public virtual (Result result, V domain) Erase(V table, bool usedbcommand = false)
        {
            if (table.Data.Entity.Id != null)
            {
                if (!table.Deleted)
                {
                    var delete = table.Data.Delete(usedbcommand);

                    if (delete.result.Success)
                    {
                        table.Deleted = true;
                    }

                    return (delete.result, table);
                }

                return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, "Erase", $"{table.Data.Description.Name} with Id {table.Data.Entity.Id} already deleted") } }, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Erase", $"Id in {table.Data.Description.Name} cannot be null") } }, default(V));
        }
    }
}
