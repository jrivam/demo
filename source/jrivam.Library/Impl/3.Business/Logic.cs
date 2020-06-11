using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Business
{
    public class Logic<T, U> : ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public Logic()
        {
        }

        public virtual (Result result, U data) Load(U data, bool usedbcommand = false)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return data.Select(usedbcommand);
                }
                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Load), $"Id in {data.Description.DbName} cannot be null") } }, default(U));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Load), $"Primary Key column in {data.Description.DbName} not defined") } }, default(U));
        }
        public virtual (Result result, U data) LoadQuery(U data, int maxdepth = 1)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return data.SelectQuery(maxdepth);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(LoadQuery), $"Id in {data.Description.DbName} cannot be null") } }, default(U));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(LoadQuery), $"Primary Key column in {data.Description.DbName} not defined") } }, default(U));
        }

        public virtual (Result result, IEnumerable<U> datas) List(IQueryData<T, U> query,
            int maxdepth = 1, int top = 0, IListData<T, U> datas = null)
        {
            return query.Select(maxdepth, top, datas);
        }

        public virtual (Result result, U data) Save(U data, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = data.Update(useupdatedbcommand);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = data.Insert(useinsertdbcommand);

                    return (insert.result, insert.data);
                }
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Save), $"Primary Key column in {data.Description.DbName} not defined") } }, default(U));
        }
        public virtual (Result result, U data) Erase(U data, bool usedbcommand = false)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = data.Delete(usedbcommand);

                    return (delete.result, delete.data);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Erase), $"Id in {data.Description.DbName} cannot be null") } }, default(U));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Erase), $"Primary Key column in {data.Description.DbName} not defined") } }, default(U));
        }
    }
}
