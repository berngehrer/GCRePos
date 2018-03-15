using System;
using System.Globalization;

namespace GCRePos
{
    class DoubleFormatter : IFormatProvider, ICustomFormatter
    {
        private CultureInfo enUsCulture = CultureInfo.GetCultureInfo("en-US");

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return string.Format(enUsCulture, "{0:0.0000}", arg);
        }

        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }
    }
}
