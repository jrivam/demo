﻿using library.Impl.Business;
using Library.Interface.Business.Loader;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Domain.Table
{
    public class LogicTable<T, U, V> : LogicLoader<T, U, V>, ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public LogicTable(ILoader<T, U, V> loader)
            : base(loader)
        {
        }

        public virtual (Result result, V domain, bool isunique) CheckIsUnique(V table)
        {
            var checkisunique = table.Data.CheckIsUnique();

            return (checkisunique.result, table, checkisunique.isunique);
        }

        public virtual (Result result, V domain) Load(V table, bool usedbcommand = false)
        {
            var primarykeycolumn = table.Data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    var select = table.Data.Select(usedbcommand);

                    if (select.result.Success && select.data != null)
                    {
                        table.Data = select.data;

                        _loader.Clear(table);
                        Load(table, 1);

                        table.Changed = false;
                        table.Deleted = false;

                        return (select.result, table);
                    }

                    return (select.result, default(V));
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Load", $"Id in {table.Data.Description.Name} cannot be null") } }, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Load", $"Primary Key column in {table.Data.Description.Name} not defined") } }, default(V));
        }
        public virtual (Result result, V domain) LoadQuery(V table, int maxdepth = 1)
        {
            var primarykeycolumn = table.Data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    var selectquery = table.Data.SelectQuery(maxdepth);

                    if (selectquery.result.Success && selectquery.data != null)
                    {
                        table.Data = selectquery.data;

                        _loader.Clear(table);
                        Load(table, maxdepth);

                        table.Changed = false;
                        table.Deleted = false;

                        return (selectquery.result, table);
                    }

                    return (selectquery.result, default(V));
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "LoadQuery", $"Id in {table.Data.Description.Name} cannot be null") } }, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "LoadQuery", $"Primary Key column in {table.Data.Description.Name} not defined") } }, default(V));
        }

        public virtual (Result result, V domain) Save(V table, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            if (table.Changed)
            {
                var checkisunique = CheckIsUnique(table);

                if (checkisunique.isunique)
                {
                    var primarykeycolumn = table.Data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
                    if (primarykeycolumn != null)
                    {
                        if (primarykeycolumn.DbValue == null)
                        {
                            var insert = table.Data.Insert(useinsertdbcommand);

                            if (insert.result.Success)
                            {
                                table.Changed = false;
                            }

                            return (insert.result, table);
                        }
                        else
                        {
                            var update = table.Data.Update(useupdatedbcommand);

                            if (update.result.Success)
                            {
                                table.Changed = false;
                            }

                            return (update.result, table);
                        }
                    }

                    return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Save", $"Primary Key column in {table.Data.Description.Name} not defined") } }, default(V));
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
            if (!table.Deleted)
            {
                var primarykeycolumn = table.Data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
                if (primarykeycolumn != null)
                {
                    if (primarykeycolumn?.Value != null)
                    {
                        var delete = table.Data.Delete(usedbcommand);

                        if (delete.result.Success)
                        {
                            table.Deleted = true;
                        }

                        return (delete.result, table);
                    }

                    return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Erase", $"Id in {table.Data.Description.Name} cannot be null") } }, default(V));
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Erase", $"Primary Key column in {table.Data.Description.Name} not defined") } }, default(V));
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, "Erase", $"{table.Data.Description.Name} with Id {table.Data.Entity.Id} already deleted") } }, default(V));
        }
    }
}
