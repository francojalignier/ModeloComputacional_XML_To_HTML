using System.Xml.Serialization;

namespace Canvas
{
    [XmlType("Style")]
    public class Style {
        public string FillColor = "#FF0000";
        public float FillOpacity = 1f;

        public string StrokeColor = "#000000";
        public float StrokeWidth = 1;
        public float StrokeOpacity = 0.5f;
    }

    interface IStyleable
    {
        void ApplyStyle(Style style);
    }
}
