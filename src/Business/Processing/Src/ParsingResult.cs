using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Src
{
    public class ParsingResult
    {
        public IEnumerable<string> Errors { get; private set; }

        public MethodInfo Info { get; private set; }

        private ParsingResult() { }

        public static ParsingResult Success(MethodInfo info)
        {
            return new ParsingResult()
            {
                Info = info,
                Errors = null
            };
        }

        public static ParsingResult Failure(IEnumerable<string> messages)
        {
            return new ParsingResult()
            {
                Errors = messages
            };
        }
    }
}
