using System;
namespace Canvas
{
    public interface IDrawable
    {
         bool Contains(Point p);

         void Draw();
    }
}
