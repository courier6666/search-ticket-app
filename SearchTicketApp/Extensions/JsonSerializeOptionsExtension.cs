using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using JsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace SearchTicketApp.Extensions
{
    public static class JsonSerializeOptionsExtension
    {
        public static void ConfigureJsonSerializerOptions(this JsonSerializerOptions options)
        {
            options.AllowTrailingCommas = true;
            options.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
