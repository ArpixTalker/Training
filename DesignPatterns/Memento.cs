
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Editor //ORIGINATOT: Holds the current content only
    {
        public ContentState CurrentContent { get; private set; }

        public Editor() {

            this.CurrentContent = new ContentState();
        }

        public void EditContent(string newContent) {

            this.CurrentContent = new ContentState(newContent);
        }

        public void Undo(ContentState previous) {

            this.CurrentContent = previous;
        }
    }

    public class ContentState //MEMENTO: State of the Editor
    {

        public string Content { get; private set; }

        public ContentState() {

            this.Content = "";
        }

        public ContentState(string content)
        {

            this.Content = content;
        }
    }

    public class ContentHistory //CARETAKER: Holds the content history in stack while loosely coupled in Main with Editor;
    {

        Stack<ContentState> history;

        public ContentHistory() {

            history = new Stack<ContentState>();

        }

        public void Push(ContentState state) {

            history.Push(state);
        }

        public ContentState Pop() {

            return history.Pop();
        }
    }

    /*MAIN
    
            var e = new Editor();
            var h = new ContentHistory();

            h.Push(e.CurrentContent);
            e.EditContent("Ahoj. ");
            h.Push(e.CurrentContent);
            e.EditContent("Ahoj. Jak ");
            h.Push(e.CurrentContent);
            e.EditContent("Ahoj. Jak se");
            h.Push(e.CurrentContent);
            e.EditContent("Ahoj. Jak se mas?");

            Console.WriteLine($"Current State: {e.CurrentContent.Content}");
            while (true) {

                try {

                    e.Undo(h.Pop());
                    Console.WriteLine($"Undo: {e.CurrentContent.Content}");
                } catch {

                    Console.WriteLine("Cannot Undo!");
                    break;
                }
            }

            Console.ReadLine();
     
     
     
     
     
     
     
     
     
     */
}
