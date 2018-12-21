using System;
using System.Xml.Serialization;

namespace Canvas
{
    //[XmlInclude(typeof(Circle)), XmlInclude(typeof(Line)), XmlInclude(typeof(Point)), XmlInclude(typeof(Rectangle))]  
    [XmlType("Figure")]
    public abstract class Figure : IDrawable, IStyleable
    {
        public Style Style = new Style();

        public abstract void Draw();
        public abstract bool Contains(Point p);

        public void ApplyStyle(Style style)
        {
            this.Style = style;
        }
    }
}
