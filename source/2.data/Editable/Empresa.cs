using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Persistence.Query;
using System.Collections.Generic;
using System.Data;

namespace Persistence.Table
{
    public partial class Empresa
    {
        public Empresa(Entities.Table.Empresa entity)
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

        public override IQueryData<Entities.Table.Empresa, Persistence.Table.Empresa> QueryUnique
        {
            get
            {
                var _query = new Persistence.Query.Empresa();

                if (this.Id != null)
                {
                    _query.Id = (this.Id, WhereOperator.NotEquals);
                }
                _query.Ruc = (this.Ruc, WhereOperator.Equals);

                return _query;
            }
        }
        public override (Result result, Persistence.Table.Empresa data, bool isunique) CheckIsUnique()
        {
            if (!string.IsNullOrWhiteSpace(this.Ruc))
            {
                var checkisunique = base.CheckIsUnique();

                if (!checkisunique.isunique)
                {
                    checkisunique.result.Append(new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Ruc {this.Ruc} already exists in Id: {checkisunique.data?.Id}") } });

                    return checkisunique;
                }

                return (new Result() { Success = true }, null, true);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Ruc cannot be empty") } }, null, false);
        }

        public virtual (Result result, Persistence.Table.Sucursales datas) Sucursales_Refresh(int maxdepth = 1, int top = 0, Persistence.Query.Sucursal query = null)
        {
            if (this.Id != null)
            {
                var _query = query ?? new Persistence.Query.Sucursal();

                _query.IdEmpresa = (this.Id, WhereOperator.Equals);

                var selectmultiple = _query.SelectMultiple(maxdepth, top);

                Sucursales = (Persistence.Table.Sucursales)new Persistence.Table.Sucursales().Load(selectmultiple.datas);

                return (selectmultiple.result, _sucursales);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Sucursales_Refresh", $"Id in {this?.Description?.Name} cannot be null") } }, null);
        }
    }
}

namespace Persistence.Query
{
    public partial class Empresa
    {
        public Empresa()
            : this("test.connectionstring.name")
        {
        }
    }
}
