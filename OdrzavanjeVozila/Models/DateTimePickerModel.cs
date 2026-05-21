using System;

namespace OdrzavanjeVozila.Models
{
    public class DateTimePickerModel
    {
        public string FieldName { get; set; } = string.Empty;
        public string FieldId { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public DateTime? Value { get; set; }
        public bool IsRequired { get; set; }
        public bool IsNullable { get; set; }
        public string RequiredMessage { get; set; } = string.Empty;
        public string HelpText { get; set; } = string.Empty;
    }
}
