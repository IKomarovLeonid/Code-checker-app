using NUnit.Framework;
using Processing.Src;
using System.Collections.Generic;
using System.Linq;

namespace Unit
{
    public class Tests
    {
        private CodeParser _parser;

        [OneTimeSetUp]
        public void Setup()
        {
            _parser = new CodeParser();
        }

        [Test]
        public void System_CanParseCodeCorrectly_Success()
        {
            // arrange
            var code = @"
                           using System;

                           namespace First
                           {
                              public class Program
                                   {
                                      public static int Add(int x, int y)
                                         {
                                             " + 
                                                 "return x + y;"
                                              + @"
                                          }
                                    }
                                }
                                    ";

            var result = _parser.Parse("First.Program",  "Add", code);

            Assert.That(result.Info, Is.Not.Null);

            var opResult = result.Info.Invoke("First.Program", new object[]{ 3, 2});

            Assert.That(opResult, Is.EqualTo(5));
        }

        [Test]
        public void System_CanParseCodeCorrectly_Invalid()
        {
            // arrange
            var code = @"
                           using System;

                           namespace First
                           {
                              public class Program
                                   {
                                      public static int Add(int y)
                                         {
                                             " +
                                              "return x + y"
                                                             + @"
                                          }
                                    }
                                }
                                    ";

            var result = _parser.Parse("First.Program", "Add", code);
            var errors = result.Errors.ToList();

            Assert.That(errors.Count, Is.EqualTo(2));
        }
    }
}