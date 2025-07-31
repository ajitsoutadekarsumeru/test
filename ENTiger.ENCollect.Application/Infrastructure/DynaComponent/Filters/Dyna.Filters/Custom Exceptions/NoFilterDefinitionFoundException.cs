using System;

namespace ENCollect.Dyna.Filters
{
    /// <summary>
    /// Thrown when SearchCriteria.Build<T>() is called 
    /// but no filter definitions have been added.
    /// </summary>
    internal class NoFilterDefinitionFoundException : Exception
    {
        public NoFilterDefinitionFoundException(string message) 
            : base(message)
        {
        }
    }
}