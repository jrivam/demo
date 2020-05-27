using System.Collections.Generic;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryDataJoins
    {
        IList<(IColumnQuery internalkey, IColumnQuery externalkey)> GetJoins(int maxdepth = 1, int depth = 0);
    }
}
