using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal
    {
        public Sucursal(entities.Model.Sucursal entity)
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

        public override IQueryRepositoryMethods<entities.Model.Sucursal, data.Model.Sucursal> QueryUnique
        {
            get
            {
                var _query = new data.Query.Sucursal();

                if (this.Id != null)
                {
                    _query.Id = (this.Id, WhereOperator.NotEquals);
                }
                _query.Codigo = (this.Codigo, WhereOperator.Equals);

                return _query;
            }
        }
        public override (Result result, data.Model.Sucursal data, bool isunique) CheckIsUnique()
        {
            if (this.Codigo != null)
            {
                var checkisunique = base.CheckIsUnique();

                if (!checkisunique.isunique)
                {
                    checkisunique.result.Append(new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Codigo {this.Codigo} already exists in Id: {checkisunique.data?.Id}") } });

                    return checkisunique;
                }

                return (new Result() { Success = true }, null, true);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"Codigo cannot be null") } }, null, false);
        }

        public virtual (Result result, data.Model.Empresa data) Empresa_Refresh(int maxdepth = 1, data.Query.Sucursal query = null)
        {
            if (this.IdEmpresa != null)
            {
                var _query = query ?? new data.Query.Sucursal();

                _query.Empresa().Id = (this.IdEmpresa, WhereOperator.Equals);

                var selectsingle = _query?.Empresa()?.SelectSingle(maxdepth);

                Empresa = selectsingle?.data;

                return (selectsingle?.result, _empresa);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Empresa_Refresh", $"IdEmpresa in {this.Description.Name} cannot be null") } }, null);
        }
        public virtual (Result result, data.Model.Empresas datas) Empresas_Refresh(int maxdepth = 1, int top = 0, data.Query.Sucursal query = null)
        {
            var _query = query ?? new data.Query.Sucursal();

            _query.Empresa().Activo = (true, WhereOperator.Equals);

            var load = new data.Model.Empresas().Load(_query?.Empresa(), maxdepth, top);

            Empresas = (data.Model.Empresas)load.list;

            return (load.result, _empresas);
        }
    }
}

namespace data.Query
{
    public partial class Sucursal
    {
        public Sucursal()
            : this("test.connectionstring.name")
        {            
        }
    }
}
