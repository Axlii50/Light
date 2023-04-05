using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light
{
    class LightSource
    {
        private List<Ray> Rays = new List<Ray>();
        private VertexArray Borders = new VertexArray();
        private readonly SFML.System.Vector2f Position;
        private int Radius = 0;
        List<Line> Bounds;

        public LightSource(SFML.System.Vector2f Position, ref int radius,ref List<Line> Boundarys)
        {
            Borders.PrimitiveType = PrimitiveType.TrianglesStrip;
            Bounds = Boundarys;
            this.Radius = radius;
            this.Position = Position;
            Rays.Clear();
            int n = 360;
            for (int i = 0; i < n; i++)
            {
                Rays.Add(new Ray((int)this.Position.X, (int)this.Position.Y, Radius, i * (360 / n)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">Default 255,138,0 Alpha 0 </param>
        public void CalculateLight(Color? c = null, int Radius = 0)
        {
            if (Radius != 0)
            {
                this.Radius = Radius;

                Rays.Clear();
                int n = 360;
                for (int i = 0; i < n; i++)
                {
                    Rays.Add(new Ray((int)this.Position.X, (int)this.Position.Y, Radius, i * (360 / n)));
                }

            }
            if (!c.HasValue)
                c = new Color(60, 25, 0);
            Vertex? oldvertex = null;
            Borders.Clear();
            foreach (Ray x in Rays)
            {
                x.SetPosition(Position.X, Position.Y);
                SFML.System.Vector2f? ClostestPoint = null;
                foreach (Line y in Bounds)
                {
                    SFML.System.Vector2f? raypop = x.CastRay(y);

                    if (raypop != null)
                        ClostestPoint = Ray.Distance(x.GetCenter, ClostestPoint, raypop);
                }
                if (ClostestPoint != null)
                    x.EndLine(ClostestPoint);

                Color FinalColor = Ray.CalculateBrightnes(Radius, Position, ClostestPoint.Value, c.Value);//calculate collor of changing brightness
                SFML.Graphics.Color color = c.Value;//center color

                if (Borders.PrimitiveType == PrimitiveType.TrianglesStrip)//cosmetic visualization
                    if (oldvertex == null)
                        oldvertex = new Vertex(x.GetEnd, FinalColor);
                    else
                    {
                        //creating shape from triangles
                        Borders.Append(oldvertex.Value);
                        Borders.Append(new Vertex(Position, color));
                        Borders.Append(new Vertex(x.GetEnd, FinalColor));
                        oldvertex = new Vertex(x.GetEnd, FinalColor);
                    }
                else//cosmetic visualization
                    Borders.Append(new Vertex(x.GetEnd, Color.Blue));//cosmetic visualization

            }
            //Closing shape with first vertex
            Borders.Append(Borders[0]);
        }
        public void ChangePrimitiveType(PrimitiveType e) => Borders.PrimitiveType = e;


        public void Draw(RenderTarget e)
        {
            Borders.Draw(e,RenderStates.Default);
        }
    }
}
