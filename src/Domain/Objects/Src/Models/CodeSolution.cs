using System;
using System.Reflection;
using Objects.Dto;

namespace Objects.Models
{
    public class CodeSolution
    {
        public ulong Id { get; set; }

        public ulong UserId { get; set; }

        public ulong TaskId { get; set; }

        public string Code { get; set; }

        public ulong TotalTests { get; set; }

        public ulong PassedTests { get; set; }

        public string MethodName { get; set; }

        public string Errors { get; set; }

        public string NamespaceName { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public SolutionStatus Status { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
