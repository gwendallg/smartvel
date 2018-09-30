using System;
using SmartVel.Utils;
using Xamarin.Forms;

namespace SmartVel.Exceptions
{
    public class ByResourceApplicationException : Exception
    {
        public ByResourceApplicationException()
        {
        }

        public ByResourceApplicationException(string idMessage, params object[] args) : base(
            Application.Current.Translate(idMessage, args))
        {
        }

        public ByResourceApplicationException(string idMessage, Exception innerException, params object[] args) : base(
            Application.Current.Translate(idMessage, args),
            innerException)
        {
        }
    }
}
