
using System;

namespace QB2DB_Sync
{
    public class XMLProcessingException : Exception
    {
        public XMLProcessingException()
        {

        }

        public XMLProcessingException(string message)
            : base(message)
        {
        }

        public XMLProcessingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}