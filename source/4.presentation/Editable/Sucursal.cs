using library.Impl;
using System.Collections.Generic;

namespace presentation.Model
{
    public partial class Sucursal
    {
        protected presentation.Model.Empresas _empresas;
        public virtual presentation.Model.Empresas Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    var query = new presentation.Query.Empresa();
                    query.Domain.Data["Activo"]?.Where(true);

                    Empresas = (presentation.Model.Empresas)new presentation.Model.Empresas().Load(query);
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Domain.Empresas = (domain.Model.Empresas)new domain.Model.Empresas().Load(_empresas?.Domains);
                }
            }
        }

        protected override Result SaveChildren()
        {
            return SaveChildren2();
        }
        protected override Result EraseChildren()
        {
            return EraseChildren2();
        }
        public override (Result result, presentation.Model.Sucursal presentation) LoadQuery()
        {
            if (this.Id != null)
            {
                var query = new presentation.Query.Sucursal();
                query.Domain.Data["Id"]?.Where(this.Id);

                return query.Retrieve(_maxdepth, this);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "LoadQuery: Id cannot be null") } }, null);
        }
    }
}

namespace presentation.Query
{
    public partial class Sucursal
    {
    }
}

namespace presentation.Mapper
{
    public partial class Sucursal
    {
        public virtual presentation.Model.Sucursal Load(presentation.Model.Sucursal entity)
        {
            return entity;
        }
    }
}