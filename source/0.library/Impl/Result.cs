using System.Collections.Generic;
using System.Linq;

namespace library.Impl
{
    public class Result
    {
        public bool Success { get; set; }
        public IList<(ResultCategory category, string message)> Messages { get; set; } = new List<(ResultCategory, string)>() { };

        public Result()
        {
        }
        public Result(IList<(ResultCategory category, string message)> messages)
            : this()
        {
            messages?.ToList()?.ForEach(x => Messages.Add(x));
        }

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
