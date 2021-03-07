using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface iEditorTool
    {
        void MouseDown();
        void MouseUp();
        string ToString();
    }

    public class PencilTool : iEditorTool{

        public void MouseDown() { Console.WriteLine("Begin Drawing"); }
        public void MouseUp() { Console.WriteLine("Stop Drawing"); }
        public override string ToString()
        {
            return "Pencil"; 
        }
    }

    public class BrushTool : iEditorTool{

        public void MouseDown() { Console.WriteLine("Begim Painting"); }
        public void MouseUp() { Console.WriteLine("Stop Painting"); }
        public override string ToString()
        {
            return "Brush";
        }
    }

    public class CutterTool : iEditorTool{

        public void MouseDown() { Console.WriteLine("Begin Cutting"); }
        public void MouseUp() { Console.WriteLine("Stop Cutting"); }
        public override string ToString()
        {
            return "Cutter";
        }
    }

    public class SprayTool : iEditorTool{

        public void MouseDown() { Console.WriteLine("Begin Spraying"); }
        public void MouseUp() { Console.WriteLine("Stop Spraying"); }
        public override string ToString()
        {
            return "Spray";
        }
    }

    /* -- MAIN --
     
            var tools = new List<iEditorTool>();

            tools.Add(new BrushTool());
            tools.Add(new PencilTool());
            tools.Add(new SprayTool());
            tools.Add(new CutterTool());

            foreach (iEditorTool tool in tools) {

                Console.WriteLine($"Now using: {tool.ToString()}");
                tool.MouseDown();
                tool.MouseUp();
            }
            Console.ReadLine();
     
     */
}
