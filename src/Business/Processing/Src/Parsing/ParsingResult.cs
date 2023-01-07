using System.Collections.Generic;
using System.Reflection;

namespace Processing.Parsing
{
    public class ParsingResult
    {
        public IEnumerable<string> Errors { get; private set; }

        public MethodInfo Info { get; private set; }

        public bool IsSuccess { get; private set; }

        public string Namespace { get; private set; }

        public string MethodToCall { get; private set; }

        private ParsingResult() { }

        public static ParsingResult Success(MethodInfo info, string methodToCall, string nameSpace)
        {
            return new ParsingResult()
            {
                Info = info,
                Errors = null,
                IsSuccess = true,
                Namespace = nameSpace,
                MethodToCall = methodToCall
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
