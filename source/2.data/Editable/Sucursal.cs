using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Persistence.Query;
using System.Collections.Generic;
using System.Data;

namespace Persistence.Table
{
    public partial class Sucursal
    {
        public Sucursal(Entities.Table.Sucursal entity)
            : this(entity, "test.connectionstring.name")
        {
        }

        public override void Init()
        {
            SelectDbCommand = (false, ("gsp_sucursal_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_sucursal_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }

        public override IQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal> QueryUnique
        {
            get
            {
                var _query = new Persistence.Query.Sucursal();

                if (this.Id != null)
                {
                    _query.Id = (this.Id, WhereOperator.NotEquals);
                }
                _query.Codigo = (this.Codigo, WhereOperator.Equals);

                return _query;
            }
        }
        public override (Result result, Persistence.Table.Sucursal data, bool isunique) CheckIsUnique()
        {
            if (!string.IsNullOrWhiteSpace(this.Codigo))
            {
                var checkisunique = base.CheckIsUnique();

                if (!checkisunique.isunique)
                {
                    checkisunique.result.Append(new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Codigo {this.Codigo} already exists in Id: {checkisunique.data?.Id}") } });

                    return checkisunique;
                }

                return (new Result() { Success = true }, null, true);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Codigo cannot be empty") } }, null, false);
        }

        public virtual (Result result, Persistence.Table.Empresa data) Empresa_Refresh(int maxdepth = 1, Persistence.Query.Sucursal query = null)
        {
            if (this.IdEmpresa != null)
            {
                var _query = query ?? new Persistence.Query.Sucursal();

                _query.Empresa().Id = (this.IdEmpresa, WhereOperator.Equals);

                var selectsingle = _query?.Empresa()?.SelectSingle(maxdepth);

                Empresa = selectsingle?.data;

                return (selectsingle?.result, _empresa);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Empresa_Refresh", $"IdEmpresa in {this.Description.Name} cannot be null") } }, null);
        }
        public virtual (Result result, Persistence.Table.Empresas datas) Empresas_Refresh(int maxdepth = 1, int top = 0, Persistence.Query.Empresa query = null)
        {
            var _query = query ?? new Persistence.Query.Empresa();

            _query.Activo = (true, WhereOperator.Equals);

            var selectmultiple = _query.SelectMultiple(maxdepth, top);

            Empresas = (Persistence.Table.Empresas)new Persistence.Table.Empresas().Load(selectmultiple.datas);

            return (selectmultiple.result, _empresas);
        }
    }
}

namespace Persistence.Query
{
    public partial class Sucursal
    {
        public Sucursal()
            : this("test.connectionstring.name")
        {            
        }
    }
}
