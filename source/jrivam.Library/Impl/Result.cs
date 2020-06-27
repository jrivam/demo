using System;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl
{
    public class Result
    {
        public bool Success { get; set; }
        
        public IList<(ResultCategory category, string name, string message)> Messages { get; set; } = new List<(ResultCategory, string, string)>() { };

        public Result(Result result = null)
        {
            Append(result);
        }

        public Result Append(Result result)
        {
            if (result != null)
            {
                ((List<(ResultCategory, string, string)>)Messages).AddRange(result.Messages);

                Success = Success && result.Success;
            }

            return this;
        }

        public string GetMessages(Func<(ResultCategory category, string name, string message), bool> condition = null, string newlinereplacement = null)
        {
            var messages = String.Join(Environment.NewLine, condition != null ? Messages.Where(condition) : Messages);

            if (newlinereplacement != null)
            {
                return messages.Replace(Environment.NewLine, newlinereplacement);
            }

            return messages;
        }
    }
}
