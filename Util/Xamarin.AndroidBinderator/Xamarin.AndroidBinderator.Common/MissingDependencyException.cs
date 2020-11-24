using System;
using System.Runtime.Serialization;
using MavenNet.Models;

namespace AndroidBinderator.Common
{
    public class MissingDependencyException : Exception
    {
        public MissingDependencyException(Dependency dependency)
        {
            Dependency = dependency;
        }
        public MissingDependencyException(Dependency dependency, string message) : base(message)
        {
            Dependency = dependency;
        }
        public MissingDependencyException(Dependency dependency, string message, Exception innerException) : base(message, innerException)
        {
            Dependency = dependency;
        }
        protected MissingDependencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public Dependency Dependency { get; set; }
    }
}
