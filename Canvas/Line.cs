using System.Xml.Serialization;

namespace Canvas
{

    [XmlType("Line")]
    public class Line : Figure
    {


        public float _x1;
        public float _y1;
        public float _x2;
        public float _y2;

        public Line()
        {

        }
        //Log try gitGHub
        public Line(float x1, float y1, float x2, float y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        public Line(float x1, float y1, float x2, float y2, Style style)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;

            this.ApplyStyle(style);
        }


        [XmlElement]
        public Point P1M { get; set; }
        [XmlElement]
        public Point P2M { get; set; }

        public Line(Point p1,Point p2)
        {
            _x1 = p1.X;
            _y1 = p1.Y;
            _x2 = p2.X;
            _y2 = p2.Y;
        }

        public override void Draw()
        {
            System.Console.WriteLine("Soy una línea que va desde  ({0},{1}) hasta ({2},{3})", _x1, _y1,_x2,_y2);

        }

        public override bool Contains(Point p)
        {
            bool s = false;
            return s;
        }

    }
}
