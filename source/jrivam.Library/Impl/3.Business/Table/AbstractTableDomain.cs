using jrivam.Library.Extension;
using jrivam.Library.Impl.Business.Attributes;
using jrivam.Library.Impl.Entities;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business.Table
{
    public abstract partial class AbstractTableDomain<T, U, V> : ITableDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
        protected U _data;
        public virtual U Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public IList<(string name, IColumnValidator validator)> Validations { get; set; } = new List<(string, IColumnValidator)>();

        protected readonly ILogicTableAsync<T, U, V> _logictableasync;

        public AbstractTableDomain(ILogicTableAsync<T, U, V> logictableasync,
            U data = default(U))
        {
            _logictableasync = logictableasync;

            if (data == null)
                _data = HelperTableRepository<T, U>.CreateData(HelperEntities<T>.CreateEntity());
            else
                _data = data;

            Init();
        }

        protected virtual void Init()
        {
        }

        public virtual Result Validate()
        {
            var result = new Result();

            foreach (var validation in Validations)
            {
                result.Append(validation.validator.Validate());
            }

            return result;
        }
        public virtual Result Validate(string name)
        {
            return Validations.FirstOrDefault(x => x.name == name).validator.Validate();
        }

        public virtual async Task<(Result result, V domain)> LoadQueryAsync(int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var loadquery = await _logictableasync.LoadQueryAsync(this as V,
                maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);

            return loadquery;
        }

        public virtual async Task<(Result result, V domain)> LoadAsync(
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var load = await _logictableasync.LoadAsync(this as V,
                connection,
                commandtimeout).ConfigureAwait(false);

            return load;
        }

        public virtual async Task<(Result result, V domain)> SaveAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var save = await _logictableasync.SaveAsync(this as V,
                connection, transaction,
                commandtimeout).ConfigureAwait(false);

            if (save.result.Success)
            {
                save.result.Append(await SaveChildrenAsync(connection, transaction,
                    commandtimeout).ConfigureAwait(false));
            }

            return save;
        }

        public virtual async Task<(Result result, V domain)> EraseAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            (Result result, V domain) erasechildren = (await EraseChildrenAsync(connection, transaction,
                commandtimeout).ConfigureAwait(false), default(V));

            if (erasechildren.result.Success)
            {
                var erase = await _logictableasync.EraseAsync(this as V,
                    connection, transaction,
                    commandtimeout).ConfigureAwait(false);

                erasechildren.result.Append(erase.result);

                return erase;
            }

            return erasechildren;
        }

        protected virtual async Task<Result> SaveChildrenAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var savechildren = new Result();

            foreach (var property in typeof(V).GetPropertiesFromType(iscollection: true, attributetypes: new System.Type[] { typeof(DomainAttribute) }))
            {
                var collection = property.info.GetValue(this);
                if (collection != null)
                {
                    var saveall = (Task<Result>)collection.GetType().GetMethod(nameof(IListDomainEditAsync<T, U, V>.SaveAllAsync)).Invoke(collection, new object[] { connection, transaction, commandtimeout });
                    savechildren.Append(await saveall.ConfigureAwait(false));
                }
            }

            return savechildren;
        }
        protected virtual async Task<Result> EraseChildrenAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var erasechildren = new Result();

            foreach (var property in typeof(V).GetPropertiesFromType(iscollection: true, attributetypes: new System.Type[] { typeof(DomainAttribute) }))
            {
                var collection = property.info.GetValue(this);
                if (collection != null)
                {
                    var refresh = (Task<(Result, IListDomain<T, U, V>)>)collection.GetType().GetMethod(nameof(IListDomainReloadAsync<T, U, V>.RefreshAsync)).Invoke(collection, new object[] { null, connection, commandtimeout });
                    var item2 = refresh.GetType().GetField("Item2").GetValue(await refresh.ConfigureAwait(false));
                    if (item2 != null)
                    {
                        var eraseall = (Task<Result>)item2.GetType().GetMethod(nameof(IListDomainEditAsync<T, U, V>.EraseAllAsync)).Invoke(item2, new object[] { connection, transaction, commandtimeout });
                        erasechildren.Append(await eraseall.ConfigureAwait(false));
                    }
                }
            }

            return erasechildren;
        }
    }
}
