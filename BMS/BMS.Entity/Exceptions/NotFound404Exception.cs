using System;

namespace BMS.Entity.Exceptions
{
    [Serializable]
    public class NotFound404Exception : Exception
    {
        public NotFound404Exception() { }

        public NotFound404Exception(string message) : base(message)
        {
        }
    }
}
