using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light
{
    class Program
    {
        //Create LightHandler
        static List<Line> Boundarys = new List<Line>();

        //Radius of LightSource
        static int radius = 300;

        //List of all Light sources
        static List<LightSource> Lights = new List<LightSource>();

        //cosmetic
        static bool drawobj = true;

        //is running for break loop 
        static bool running = true;
        
        
        static RenderWindow render = new RenderWindow(new VideoMode((uint)Screen.PrimaryScreen.Bounds.Width
                                , (uint)Screen.PrimaryScreen.Bounds.Height),
                                "Smlf Window", Styles.Fullscreen);

        static void Main(string[] args)
        {
            Lights.Add(new LightSource(new SFML.System.Vector2f(300, 300),ref  radius, ref Boundarys));
            Lights.Add(new LightSource(new SFML.System.Vector2f(600, 600), ref radius, ref Boundarys));
            Lights.Add(new LightSource(new SFML.System.Vector2f(1000, 800), ref radius, ref Boundarys));

            render.SetVerticalSyncEnabled(true);

            Random r = new Random();

            //obstacles
            Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
            Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
            Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
            Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
            Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
            Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));

            render.KeyPressed += Render_KeyPressed;

            while (running)
            {
               //run all events
                render.DispatchEvents();

                //draw black background
                render.Clear(Color.Black);

                //draw LightsSources
                foreach (LightSource x in Lights)
                {
                    x.CalculateLight(null,radius);

                    x.Draw(render);
                }
                
                //Draw Obstacles
                if (drawobj)//cosmetic visualization
                        foreach (Line x in Boundarys)
                            x.Draw(render);
                //show Created frame 
                render.Display();
            }
        }

        private static void Render_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            //exit app
            if (e.Code == Keyboard.Key.Escape)
                running = false;
            //Draw Lines 
            if (e.Code == Keyboard.Key.A)
                drawobj = !drawobj;
            //Change positions of lines
            if (e.Code == Keyboard.Key.S)
            {
                Boundarys.Clear();
                Random r = new Random();
                Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
                Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
                Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
                Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
                Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
                Boundarys.Add(new Line(r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200), r.Next(0, 1200)));
            }
            //Determin how light will be displayed
            if (e.Code == Keyboard.Key.Z)
                foreach (LightSource x in Lights)
                    x.ChangePrimitiveType(PrimitiveType.TrianglesStrip);
            if (e.Code == Keyboard.Key.X)
                foreach (LightSource x in Lights)
                    x.ChangePrimitiveType(PrimitiveType.LinesStrip);
        }
    }
}
