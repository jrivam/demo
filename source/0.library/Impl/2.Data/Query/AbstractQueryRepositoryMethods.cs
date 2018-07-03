using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryRepositoryMethods<T, U> : AbstractQueryRepositoryProperties, IQueryRepositoryMethods<T, U>
        where T : IEntity, new()
        where U : class, ITableRepositoryProperties<T>
    {
        protected readonly IRepositoryQuery<T, U> _repository;


        public AbstractQueryRepositoryMethods(IRepositoryQuery<T, U> repository,
            string name, string reference)
            : base(name, reference)
        {
            _repository = repository;
        }


        protected virtual IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)>
            GetQueryColumns
            (IQueryRepositoryProperties query,
            IList<string> tablenames,
            IList<string> aliasnames,
            int maxdepth = 1, int depth = 0)
        {
            var columns = new List<(IQueryColumn, IList<string>, IList<string>)>();

            if (tablenames == null)
                tablenames = new List<string>();
            tablenames.Add(query.Description.Name);

            if (aliasnames == null)
                aliasnames = new List<string>();
            aliasnames.Add(query.Description.Reference);

            foreach (var c in query.Columns)
            {
                columns.Add((c, new List<string>(tablenames), new List<string>(aliasnames)));
            }

            depth++;
            foreach (var j in query.GetJoins(maxdepth, depth))
            {
                columns.AddRange(GetQueryColumns(j.externalkey.Query, tablenames, aliasnames, maxdepth, depth));
            }

            return columns;
        }

        protected virtual IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)>
            GetQueryJoins
            (IQueryRepositoryProperties table,
            string prefix = "",
            int maxdepth = 1, int depth = 0,
            string tableseparator = "_")
        {
            var joins = new List<(IQueryRepositoryProperties, string, IQueryRepositoryProperties, string, IList<(IQueryColumn, IQueryColumn)>)>();

            depth++;
            foreach (var j in table.GetJoins(maxdepth, depth))
            {
                var tablename = $"{(prefix == "" ? "" : $"{prefix}{tableseparator}")}{j.externalkey.Query.Description.Name}";

                joins.Add((table, prefix, j.externalkey.Query, tablename, new List<(IQueryColumn, IQueryColumn)>() { (j.internalkey, j.externalkey) }));

                joins.AddRange(GetQueryJoins(j.externalkey.Query, tablename, maxdepth, depth, tableseparator));
            }

            return joins;
        }


        public virtual (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U))
        {
            var querycolumns = GetQueryColumns(this, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(this, Description.Name, maxdepth, 0);

            return _repository.SelectSingle(querycolumns, queryjoins, Description.Name, maxdepth, data);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth = 1, int top = 0, IList<U> datas = null)
        {
            var querycolumns = GetQueryColumns(this, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(this, Description.Name, maxdepth, 0);

            return _repository.SelectMultiple(querycolumns, queryjoins, Description.Name, maxdepth, top, datas);
        }

        public virtual (Result result, int rows) Update(IList<ITableColumn> columns, int maxdepth = 1)
        {
            var querycolumns = GetQueryColumns(this, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(this, Description.Name, maxdepth, 0);

            return _repository.Update(querycolumns, queryjoins, Description.Name, columns);
        }

        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            var querycolumns = GetQueryColumns(this, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(this, Description.Name, maxdepth, 0);

            return _repository.Delete(querycolumns, queryjoins, Description.Name);
        }
    }
}
