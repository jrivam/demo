using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl
{
    public class Result
    {
        public bool Success { get; set; }
        public IList<(ResultCategory category, string name, string message)> Messages { get; set; } = new List<(ResultCategory, string, string)>() { };

        public Result()
        {
        }
        public Result(Result result)
            : this()
        {
            Append(result);
        }
        public Result(IList<(ResultCategory category, string name, string message)> messages)
           : this()
        {
            messages?.ToList()?.ForEach(x => Messages.Add(x));
        }
        public Result(Result result, IList<(ResultCategory category, string name, string message)> messages)
            : this(result)
        {
            messages?.ToList()?.ForEach(x => Messages.Add(x));
        }

        public Result Append(Result result)
        {
            if (result != null)
            {
                Success = Success && result.Success;
                ((List<(ResultCategory, string, string)>)Messages).AddRange(result.Messages);
            }

            return this;
        }

        public IList<(ResultCategory category, string name, string message)> Filtered(Func<(ResultCategory category, string name, string message), bool> condition = null)
        {
            if (condition != null)
            {
                return Messages.Where(condition).ToList();
            }

            return Messages;
        }
        public string FilteredAsText(string newlinereplacement, Func<(ResultCategory category, string name, string message), bool> condition = null)
        {
            return String.Join(newlinereplacement, Filtered(condition).ToArray()).Replace(Environment.NewLine, string.Empty);
        }
        public string FilteredAsTextSelected<T>(string newlinereplacement, Func<(ResultCategory category, string name, string message), bool> condition = null, Func<(ResultCategory category, string name, string message), T> selector = null)
        {
            if (selector != null)
            {
                return String.Join(newlinereplacement, Filtered(condition).Select(selector).ToArray()).Replace(Environment.NewLine, string.Empty);
            }

            return FilteredAsText(newlinereplacement, condition);
        }
    }
}
