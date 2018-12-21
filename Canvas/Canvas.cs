using System.Collections.Generic;
using System.Xml.Serialization;

namespace Canvas
{
    [XmlType("Canvas")]
    public class Canvas : List<Figure>, IDrawable
    {
        public void Draw()
        {
            foreach (Figure f in this)
                f.Draw();
        }

        public void SendToFront(int index = 0)
        {
            Figure t = this[index];
            Figure f1 = this[Count - 1];
            this[Count - 1] = t;
            this[index] = f1;
        }

        public bool Contains(Point a)
        {
            return false;
        }

        public void Save(string path)
        {
            //xmlTextWriter
        }

        public Figure Cont(Point p)
        {
            Figure figure = null;
            bool primera = true;

           
            // return this.Where(f => f.Contains(p)).LastOrDefault();

            foreach (Figure item in this)
            {
                if (item.Contains(p) == true)
                {
                    if (primera == true)
                    {
                        figure = item;
                        primera = false;
                    }
                    else
                    {
                        int position = this.IndexOf(figure);
                        if(position > this.IndexOf(item))
                        {
                            figure = item;
                        }

                    }       
                }
            }

            return figure;
        }
    }
}
