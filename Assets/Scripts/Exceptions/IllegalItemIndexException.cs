using System;
namespace Exceptions
{
    public class IllegalItemIndexException : Exception
    {
        public IllegalItemIndexException(string message) : base(message)
        {
        }
    }
}