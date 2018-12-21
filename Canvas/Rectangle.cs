using System.Xml.Serialization;

namespace Canvas
{
   
    [XmlType("Rectangle")]
    public class Rectangle : Figure
    { 

        public float _x1;
        public float _y1;
        public float _x2;
        public float _y2;

        public Rectangle()
        {
        
        }
        //public float AX1 { get; set; }
        //public float AX2 { get; set; }
        //public float AY1 { get; set; }
        //public float AY2 { get; set; }
        public float AX1 { get; set; }
        public float AX2 { get; set; }
        public float AY1 { get; set; }
        public float AY2 { get; set; }


        public Rectangle(float x1, float y1, float x2, float y2)
        {
      
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        public Rectangle(float x1, float y1, float x2, float y2, Style style)
        {

            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;

            this.ApplyStyle(style);
        }


        public Rectangle(Point p1, Point p2)
        {
             
            _x1 = p1.X;
            _y1 = p1.Y;
            _x2 = p2.X;
            _y2 = p2.Y;
        }
       
        public override bool Contains(Point p)
        {
            bool result = false;
            if (p.X > _x1 && p.X < _x2)
            {
                if (p.Y > _y1 && p.Y < _y2)
                {
                    result = true;
                }                
            }
            return result;
        }

        
        public override void Draw()
        {
            System.Console.WriteLine("Soy un rectángulo con vertices opuestos  en ({0},{1}) y ({2},{3})", _x1, _y1, _x2, _y2);
        }

    }
}
