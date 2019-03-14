using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa
    {
        public Empresa(entities.Model.Empresa entity)
            : this(entity, "test.connectionstring.name")
        {
        }

        public override void Init()
        {
            SelectDbCommand = (false, ("gsp_empresa_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_empresa_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_empresa_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_empresa_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }

        public override IQueryRepositoryMethods<entities.Model.Empresa, data.Model.Empresa> QueryUnique
        {
            get
            {
                var _query = new data.Query.Empresa();

                if (this.Id != null)
                {
                    _query.Id = (this.Id, WhereOperator.NotEquals);
                }
                _query.Ruc = (this.Ruc, WhereOperator.Equals);

                return _query;
            }
        }
        public override (Result result, data.Model.Empresa data, bool isunique) CheckIsUnique()
        {
            if (this.Ruc != null)
            {
                var checkisunique = base.CheckIsUnique();

                if (!checkisunique.isunique)
                {
                    checkisunique.result.Append(new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Ruc {this.Ruc} already exists in Id: {checkisunique.data?.Id}") } });

                    return checkisunique;
                }

                return (new Result() { Success = true }, null, true);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Ruc cannot be null") } }, null, false);
        }

        public virtual (Result result, data.Model.Sucursales datas) Sucursales_Refresh(int maxdepth = 1, int top = 0, data.Query.Empresa query = null)
        {
            if (this.Id != null)
            {
                var _query = query ?? new data.Query.Empresa();

                _query.Sucursal().IdEmpresa = (this.Id, WhereOperator.Equals);

                var load = new data.Model.Sucursales().Load(_query?.Sucursal(), maxdepth, top);

                Sucursales = (data.Model.Sucursales)load.list;

                return (load.result, _sucursales);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Sucursales_Refresh", $"Id in {this?.Description?.Name} cannot be null") } }, null);
        }
    }
}

namespace data.Query
{
    public partial class Empresa
    {
        public Empresa()
            : this("test.connectionstring.name")
        {
        }
    }
}
