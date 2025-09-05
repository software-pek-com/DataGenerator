using CsvHelper;
using CsvHelper.Configuration;
using DataGenerator.Core;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace CompanyDataGenerator
{
  public static class CsvWriterHelper
  {
    public const string NewLine = "\r\n";

    public static CultureInfo Culture => CultureInfo.InvariantCulture;

    private static CsvConfiguration Configuration => new(Culture)
    {
      NewLine = NewLine
    };

    public static CsvWriter CreateWriter(TextWriter writer) => new(writer, Configuration);

    public static CsvWriter CreateWriter(TextWriter writer, CsvConfiguration configuration)
      => new(writer, configuration);

    public static CsvConfiguration GetDefaultConfiguration() => Configuration;

    /// <summary>
    /// Writes the headers based on the value of the name property of <see cref="DisplayAttribute"/>.
    /// </summary>
    public static async Task WriteHeaders<T>(CsvWriter writer)
    {
      var headers = GetHeaders(typeof(T));

      foreach (var header in headers)
      {
        writer.WriteField(header);
      }

      await writer.NextRecordAsync();
    }

    /// <summary>
    /// Writes the records asynchronous.
    /// </summary>
    public static async Task WriteRecordsAsync<T>(CsvWriter writer, IEnumerable<T> records, string nullDefaultValue = "")
    {
      Guard.ArgumentNotNull(writer, nameof(writer));
      Guard.ArgumentNotNullOrEmpty(records, nameof(records));

      foreach (var record in records!)
      {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
          var value = property.GetValue(record);
          writer!.WriteField(GetValueToWrite(value, nullDefaultValue));
        }

        await writer!.NextRecordAsync();
      }
    }

    /// <summary>
    /// Converts an object value into a string representation suitable for CSV output. Lists are
    /// stored in JSON format, complex objects are serialized as JSON, and primitive types are
    /// converted to their string representation. Ensures proper handling of missing values.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <param name="nullDefaultValue">The default string to return if the value is null.</param>
    /// <returns>A properly formatted string representation of the value.</returns>
    private static string GetValueToWrite(object? value, string nullDefaultValue)
    {
      if (value is IEnumerable<object> list && value is not string)
      {
        return list.Any() ? $"{string.Join(", ", list)}" : nullDefaultValue;
      }
      if (value is null || value is string text && string.IsNullOrWhiteSpace(text))
      {
        return nullDefaultValue;
      }
      return value.ToString()!; // Keep all other values as plain strings
    }

    /// <summary>
    /// Gets the headers.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// Returns the list of headers based on the value of the name property of Display attribute.
    /// </returns>
    private static string[] GetHeaders(Type type)
    {
      return type.GetProperties()
        .Select(prop => prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name)
        .ToArray();
    }
  }
}