using System.Collections.Generic;

namespace library.Impl
{
    public class Result
    {
        public bool Passed { get; set; }
        public bool Success { get; set; }
        public IList<(ResultCategory category, string message)> Messages { get; set; } = new List<(ResultCategory, string)>() { };
    }
}
