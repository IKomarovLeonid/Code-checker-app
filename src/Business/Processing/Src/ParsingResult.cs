using System.Collections.Generic;
using System.Reflection;

namespace Processing.Src
{
    public class ParsingResult
    {
        public IEnumerable<string> Errors { get; private set; }

        public MethodInfo Info { get; private set; }

        public bool IsSuccess { get; private set; }

        private ParsingResult() { }

        public static ParsingResult Success(MethodInfo info)
        {
            return new ParsingResult()
            {
                Info = info,
                Errors = null,
                IsSuccess = true
            };
        }

        public static ParsingResult Failure(IEnumerable<string> messages)
        {
            return new ParsingResult()
            {
                Errors = messages,
                IsSuccess = false
            };
        }
    }
}
