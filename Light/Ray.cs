using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light
{
    class Ray
    {
        SFML.Graphics.VertexArray LineVertex = new SFML.Graphics.VertexArray();
        public SFML.System.Vector2f GetCenter => LineVertex[0].Position;
        public SFML.System.Vector2f GetEnd => LineVertex[1].Position;//możliwosc sprawdzaniac czy null
        SFML.System.Vector2f dir;
        readonly double angle;
        readonly int radius;
        public Ray(int x, int y,int radius, double angle)
        {
            this.LineVertex.PrimitiveType = SFML.Graphics.PrimitiveType.Lines;
            this.LineVertex.Clear();
            this.LineVertex.Append(new SFML.Graphics.Vertex(new SFML.System.Vector2f(x, y), SFML.Graphics.Color.Blue));

            this.angle = angle;
            this.radius = radius;

            dir = new SFML.System.Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public SFML.System.Vector2f? CastRay(Line Boundary)
        {
            //Line–line intersection
            //point on maximum radius with specyfic angle
            double x = radius * Math.Cos(angle * (Math.PI / 180)) + this.LineVertex[0].Position.X;
            double y = radius * Math.Sin(angle * (Math.PI / 180)) + this.LineVertex[0].Position.Y;

            this.LineVertex.Append(new SFML.Graphics.Vertex(new SFML.System.Vector2f((int)x, (int)y)));

            float x1 = Boundary.LineVertex[0].Position.X;
            float y1 = Boundary.LineVertex[0].Position.Y;
            float x2 = Boundary.LineVertex[1].Position.X;
            float y2 = Boundary.LineVertex[1].Position.Y;

            float x3 = this.LineVertex[0].Position.X;
            float y3 = this.LineVertex[0].Position.Y;
            float x4 = this.LineVertex[1].Position.X + this.dir.X;
            float y4 = this.LineVertex[1].Position.Y + this.dir.Y;

            //denominator
            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0)
                return null;

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

            if (t > 0 && t < 1 && u > 0)
            {
                float xr = this.LineVertex[0].Position.X, yr = this.LineVertex[0].Position.Y;
                double f = Distance(this.LineVertex[0].Position, new SFML.System.Vector2f(x1 + t * (x2 - x1), y1 + t * (y2 - y1)));

                if (f <= radius)
                    return new SFML.System.Vector2f(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
                else
                    return new SFML.System.Vector2f((int)x, (int)y);
            }
            else
                return new SFML.System.Vector2f((int)x, (int)y);
        }

        public void EndLine(SFML.System.Vector2f? point)
        {
            SFML.System.Vector2f poi = this.LineVertex[0].Position;

            this.LineVertex.Clear();
            this.LineVertex.Append(new SFML.Graphics.Vertex(poi, SFML.Graphics.Color.White));
            this.LineVertex.Append(new SFML.Graphics.Vertex((SFML.System.Vector2f)point, SFML.Graphics.Color.Black));
        }

        public static SFML.Graphics.Color CalculateBrightnes(int radius,SFML.System.Vector2f center,
                                                             SFML.System.Vector2f endpoint,
                                                             SFML.Graphics.Color GivenColor)
        {
            double distance = Ray.Distance(center, endpoint);
            if (distance > radius)
            {
                GivenColor.A = 0;
                return GivenColor;
            }
            double PercentValue = distance / radius;
            double ReverseValue = 1 - PercentValue;

            GivenColor.A = (byte)(255 * ReverseValue);

            return GivenColor;
        }

        public void SetPosition(float x, float y)
        {
            this.LineVertex.Clear();
            this.LineVertex.Append(new SFML.Graphics.Vertex(new SFML.System.Vector2f(x, y), SFML.Graphics.Color.Blue));
        }

        public static double Distance(SFML.System.Vector2f Center, SFML.System.Vector2f? second)
        {
            double radius1 = Math.Round(Math.Sqrt(Math.Pow((Center.X - second.Value.X) * 1, 2) + Math.Pow((Center.Y - second.Value.Y) * 1, 2)), 0);
            return radius1;
        }
        public static SFML.System.Vector2f? Distance(SFML.System.Vector2f Center, SFML.System.Vector2f? oldpos, SFML.System.Vector2f? newpos)
        {
            if (oldpos == null)
                return newpos;
            double radius1 = Math.Round(Math.Sqrt(Math.Pow((Center.X - oldpos.Value.X) * 1, 2) + Math.Pow((Center.Y - oldpos.Value.Y) * 1, 2)), 0);
            double radius2 = Math.Round(Math.Sqrt(Math.Pow((Center.X - newpos.Value.X) * 1, 2) + Math.Pow((Center.Y - newpos.Value.Y) * 1, 2)), 0);

            return radius1 < radius2 ? oldpos : newpos;
        }

        public void Draw(SFML.Graphics.RenderWindow e)
        {
            if (LineVertex.VertexCount == 2)
                LineVertex.Draw(e, SFML.Graphics.RenderStates.Default);
        }
    }
}
