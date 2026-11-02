
using System;

namespace QB2DB_Sync
{
    public class DataProcessingException : Exception
    {
        public DataProcessingException()
        {

        }

        public DataProcessingException(string message)
            : base(message)
        {
        }

        public DataProcessingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}