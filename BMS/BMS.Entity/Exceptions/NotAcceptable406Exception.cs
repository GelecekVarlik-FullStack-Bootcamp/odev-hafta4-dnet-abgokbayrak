using System;

namespace BMS.Entity.Exceptions
{
    [Serializable]
    public class NotAcceptable406Exception : Exception
    {
        public NotAcceptable406Exception() { }

        public NotAcceptable406Exception(string message) : base(message)
        {
        }
    }
}
