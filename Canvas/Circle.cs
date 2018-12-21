using System;
using System.Xml.Serialization;


namespace Canvas
{


    [XmlType("Circle")]
    public class Circle:Point
    {

        public float _r;
        public object x;

        public Circle() {
            
        }

        public Circle(Point p, float r):
            base(p.X,p.Y)
        {
            _r = r;
        }

        public Circle(Point p, float r, Style style) :
            base(p.X, p.Y, style)
        {
            _r = r;

        }

        public override void Draw()
        {
            System.Console.WriteLine("Soy un circulo ubicado en ({0},{1} con radio {2})", _x, _y,_r);

        }

        public override bool Contains(Point p)
        {
            bool resultado = false ;
            if ((Math.Pow(p.X, 2)) + (Math.Pow(p.Y, 2)) < _r)
            {
                resultado = true;
            }

            return resultado;
        }
    }
}
