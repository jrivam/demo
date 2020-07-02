using System;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl
{
    public class Result
    {
        public IList<ResultMessage> _messages = new List<ResultMessage>() { };

        public bool Success { get; protected set; } = true;
        public Exception Exception { get; set; }

        public Result(Result result = null)
        {
            Append(result);
        }
        public Result(ResultMessage message)
            : this()
        {
            SetMessage(message);
        }

        public Result Append(Result result)
        {
            if (result != null)
            {
                ((List<ResultMessage>)_messages).AddRange(result.GetMessages());

                Success = Success && result.Success;
            }

            return this;
        }

        public void SetMessage(ResultMessage message)
        {
            _messages.Add(message);
            Success = Success & message.Category == (message.Category & ResultCategory.Successful);
        }
        public IEnumerable<ResultMessage> GetMessages(Func<ResultMessage, bool> condition = null)
        {
            if (condition != null)
                return _messages.Where(condition);
            
            return _messages;
        }

        public string GetMessagesAsString(Func<ResultMessage, bool> condition = null, string newlinereplacement = null)
        {
            var messages = String.Join(newlinereplacement ?? Environment.NewLine, GetMessages(condition).Select(x => $"[{x.Category}]-({x.Name})-{x.Description}"));

            return messages;
        }
    }
}
