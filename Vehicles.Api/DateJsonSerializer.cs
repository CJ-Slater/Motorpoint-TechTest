using Newtonsoft.Json.Converters;
using System;

public class DateJsonSerializer : IsoDateTimeConverter
{
    //Override default json date format to fit the given vehicles.json.
    public DateJsonSerializer() {
        base.DateTimeFormat = "dd/MM/yyyy";
    }

}
