using System.Xml.Serialization;

namespace Canvas
{
    
    [XmlType("Point")]
    public class Point : Figure
    {

        public float _x;
        public float _y;

        public Point()
        {

        }

        public Point(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public Point(float x, float y, Style style)
        {
            _x = x;
            _y = y;

            this.ApplyStyle(style);
        }

        public override bool Contains(Point p)
        {
            bool result = false;
            if (_x == p._x && _y == p.Y)
                result = true;

            return result;
        }

        public override void Draw()
        {
            System.Console.WriteLine("Soy un punto ubicado en ({0},{1})", _x, _y);

        }
        
       
        [XmlElement]
        public float X { get { return _x; } set { _x = value; } }
        [XmlElement]
        public float Y { get { return _y; } set { _y = value; } }

    }
}
