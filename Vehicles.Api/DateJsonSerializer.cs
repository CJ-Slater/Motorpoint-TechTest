using Newtonsoft.Json.Converters;
using System;

public class DateJsonSerializer : IsoDateTimeConverter
{
    public DateJsonSerializer() {
        base.DateTimeFormat = "dd/MM/yyyy";
    }

}
