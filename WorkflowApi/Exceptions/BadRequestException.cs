using System.Runtime.Serialization;

namespace WorkflowApi.Exceptions
{

    internal class BadRequestException : Exception
    {
        //400
        public BadRequestException(string message) : base(message)
        {
        }
    }
}