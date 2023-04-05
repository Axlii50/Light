using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light
{
    class Line
    {
        public SFML.Graphics.VertexArray LineVertex = new SFML.Graphics.VertexArray();
         
        public Line(int x1, int y1, int x2, int y2)
        {
            
            this.LineVertex.PrimitiveType = SFML.Graphics.PrimitiveType.Lines;
            this.LineVertex.Clear();
            this.LineVertex.Append(new SFML.Graphics.Vertex(new SFML.System.Vector2f(x1, y1),SFML.Graphics.Color.Red));
            this.LineVertex.Append(new SFML.Graphics.Vertex(new SFML.System.Vector2f(x2, y2), SFML.Graphics.Color.Red));
        }


        public void Draw(SFML.Graphics.RenderWindow e)
        {
            this.LineVertex.Draw(e,SFML.Graphics.RenderStates.Default);
        }
    }
}
