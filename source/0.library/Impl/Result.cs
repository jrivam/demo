using System.Collections.Generic;

namespace library.Impl
{
    public class Result
    {
        public bool Success { get; set; }
        public IList<(ResultCategory category, string message)> Messages { get; set; } = new List<(ResultCategory, string)>() { };

        public Result Append(Result result)
        {
            if (result != null)
            {
                Success = Success && result.Success;
                ((List<(ResultCategory, string)>)Messages).AddRange(result.Messages);
            }

            return this;
        }
    }
}
