using library.Impl;
using System.Collections.Generic;

namespace presentation.Model
{
    public partial class Empresa
    {
        protected override Result SaveChildren()
        {
            return SaveChildren2();
        }
        protected override Result EraseChildren()
        {
            return EraseChildren2();
        }
        public override (Result result, presentation.Model.Empresa presentation) LoadQuery()
        {
            if (this.Id != null)
            {
                var query = new presentation.Query.Empresa();
                query.Domain.Data["Id"]?.Where(this.Id);

                return query.Retrieve(_maxdepth, this);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "LoadQuery: Id cannot be null") } }, null);

        }
    }
}

namespace presentation.Query
{
    public partial class Empresa
    {
    }
}

namespace presentation.Mapper
{
    public partial class Empresa
    {
        public virtual presentation.Model.Empresa Load(presentation.Model.Empresa entity)
        {
            return entity;
        }
    }
}

