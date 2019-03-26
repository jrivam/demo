using System.Collections.Generic;

namespace Library.Interface.Data.Query
{
    public interface IQueryDataJoins
    {
        IList<(IColumnQuery internalkey, IColumnQuery externalkey)> GetJoins(int maxdepth = 1, int depth = 0);
    }
}
