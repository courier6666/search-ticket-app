using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using JsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace SearchTicketApp.Extensions
{
    public static class JsonSerializeOptionsExtension
    {
        public static void ConfigureJsonSerializerOptions(this JsonOptions options)
        {
            options.JsonSerializerOptions.AllowTrailingCommas = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
