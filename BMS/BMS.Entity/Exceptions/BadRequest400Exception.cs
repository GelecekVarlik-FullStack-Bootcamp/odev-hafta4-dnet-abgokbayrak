using System;

namespace BMS.Entity.Exceptions
{
    [Serializable]
    public class BadRequest400Exception : Exception
    {
        public BadRequest400Exception() { }

        public BadRequest400Exception(string message) : base(message)
        {
        }
    }
}
