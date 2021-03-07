using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
        public interface ICommunicator {

            void SendMessage(string message);
            void SendFile(string file);
            void ShowContacts();
        }

        public class MailServiceAdapter : ICommunicator
        {
            private MailService ms;

            public MailServiceAdapter(MailService ms) {
                this.ms = ms;
            }

            public void SendFile(string file)
            {
                this.ms.AttachFile(file);    
            }

            public void SendMessage(string message)
            {
                this.ms.SendMail(message);
            }


            public void ShowContacts()
            {
                this.ms.ExportContacts();
            }
        }

        public class MessagingService : ICommunicator{

            public void SendMessage( string message) {
                Console.WriteLine($"Sending Message: {message}");
            }

            public void SendFile(string file) {
                Console.WriteLine($"Sending File: {file}");
            }

            public void ShowContacts() {
                Console.WriteLine("Showing comtacts");
            }

        }

        public class MailService
        {

            public void SendMail(string message)
            {
                Console.WriteLine($"Sending Email: {message}");
            }

            public void AttachFile(string file)
            {
                Console.WriteLine($"Attaching File: {file}");
            }

            public void ExportContacts()
            {
                Console.WriteLine("Exporting comtacts");
            }

    }/*
     -- MAIN --
            ICommunicator com = new MessagingService();

            com.SendFile("fotka");
            com.SendMessage("Ahoj");
            com.ShowContacts();

            com = new MailServiceAdapter(new MailService());

            com.SendFile("fotka");
            com.SendMessage("Ahoj");
            com.ShowContacts();


            Console.ReadLine();
    */
}
