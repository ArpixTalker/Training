using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Components
{
    public class ApiServer
    {
        public int Port { get; set; }
        public IPAddress IP { get; set; }
        public int Timeout { get; set; }
        private TcpListener listener;
        private Logger logger;
        private Authenticator auth;

        public ApiServer(int port, string textIP) {

            if (IPAddress.TryParse(textIP, out IPAddress ip))
            {
                this.Port = port;
                this.IP = ip;
                this.listener = new TcpListener(ip,port);
                this.Timeout = 30;
                //this.logger = new Logger("C:\\","APIserverLOG.txt",false);
                this.auth = new Authenticator();
            }
            else {

                // finish for interface!
                throw new FormatException("Invalid IP address format");
            }
        }

        public ApiServer(int port, IPAddress ip)
        {
            this.Port = port;
            this.IP = ip;
            this.auth = new Authenticator();
            this.listener = new TcpListener(ip, port);
            //this.logger = new Logger("C:\\","APIserverLOG.txt",false);
        }

        public static IPAddress GetLocalhostIP() {

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public virtual void StartServer() {

            Console.WriteLine("Starting server...");
            listener.Start();
            Console.WriteLine("Success!");
            Console.WriteLine($"Listening on {this.IP.ToString()}:{this.Port}");

            byte[] bytes = new byte[1024];
            byte[] response;
            string data;

            while (true) {

                Console.WriteLine("\nAwaiting client connection....");
                var client = this.listener.AcceptTcpClient();
                Console.WriteLine("Client connection received...");

                var stream = client.GetStream();
                int i = stream.Read(bytes, 0, bytes.Length);
                var request = this.ParseRequest(data = Encoding.ASCII.GetString(bytes, 0, i));
                bool authorized = this.auth.ProcessAuthorization(request.Authorization, out string authMethod);
                //Console.WriteLine("Auth Method: " + authMethod);

                if (authorized)
                {
                    Console.WriteLine("AUTHORIZED");
                    Console.WriteLine(request.ToString());
                    request.PrintParams();
                    response = Encoding.ASCII.GetBytes(this.BuildResponse(request));
                }
                else {
                    Console.WriteLine("AUTHORIZATION FAILED");
                    response = Encoding.ASCII.GetBytes("Authorization failed!!");
                }
                stream.Write(response, 0, response.Length);
                Console.WriteLine("Response sent: " + Encoding.ASCII.GetString(response));
                stream.Close();
            }
        }

        private TcpRequest ParseRequest(string data) {

            var i = 0 ;
            var headerEnd = false;
            var request = new TcpRequest();
            var lines = data.Split('\n');
            string[] header;

            Console.WriteLine("Processing Headers ...");
            for (i = 0; i < lines.Length; i++) {
                try
                {
                    //Console.WriteLine("Processing line: " + lines[i]);
                    if (i == 0)
                    {
                        header = lines[i].Split(new string[] { " " }, StringSplitOptions.None);
                        request.Method = header[0];
                        request.Url = header[1];
                        var prot = header[2].Split('/');
                        request.Protocol = prot[0];
                        request.ProtocolVersion = prot[1];
                        continue;
                    }
                    else
                    {
                        header = lines[i].Split(new string[] { ": " }, StringSplitOptions.None);
                    }

                    switch (header[0].Trim()) {

                        
                        case "Host":
                            request.Host = header[1];
                            break;
                        case "User-Agent":
                            request.UserAgent = header[1];
                            break;
                        case "Accept":
                            request.Accept = header[1];
                            break;
                        case "Accept-Language":
                            request.Language = header[1];
                            break;
                        case "Accept-Encoding":
                            request.Encoding = header[1];
                            break;
                        case "Connection":
                            request.Connection = header[1];
                            break;
                        case "Referer":
                            request.Referer = header[1];
                            break;
                        case "Cache-Control":
                            request.CacheControl = header[1];
                            break;
                        case "Authorization":
                            request.Authorization = header[1];
                            break;
                        case "Content-Type":
                            request.ContentType = header[1];
                            break;
                        case "Content-Length":
                            if (int.TryParse(header[1], out int length))
                            {
                                request.ContentLength = length;
                            }
                            else {
                                throw new Exception($"Invalid Content Length: {header[1]}");
                            }
                            break;
                        case "":
                            i++;
                            headerEnd = true;
                            break;
                        default:
                            try
                            {
                                //request.OtherHeaders.Add(header[0], header[1]);
                            }
                            catch (NullReferenceException) {}
                            break;
                    }
                    if (headerEnd) break;
                }
                catch (IndexOutOfRangeException) {
                    //Console.WriteLine($"{i} / {lines.Length - 1} Out of range: " + lines[i]);
                }
            }
            Console.WriteLine("Processing request...");

            if (true)
            {
                request.Parameters = this.ParseURLTypeBody(lines[i]);
            }
            /*
            else {
                while (i < lines.Length)
                {

                    Console.WriteLine("Processing line: " + lines[i]);
                    i++;
                }
            }*/
            return request;
        }

        private Dictionary<string, string> ParseURLTypeBody(string body) {

            var temp = body.Split('&');
            var parameters = new Dictionary<string, string>();

            foreach (string s in temp) {

                try
                {
                    var pair = s.Split('=');
                    parameters.Add(pair[0], pair[1]);
                }
                catch (IndexOutOfRangeException) {

                    Console.WriteLine($"Index out of range: {s}");
                }
            }
            return parameters;
        }

        public string BuildResponse(TcpRequest request) {

            if (!request.Parameters.ContainsKey("Transaction"))
            {
                return "Incomplete request";
            }
            else {

                string transaction = request.Parameters["Transaction"];

                if (transaction == "GetTime")
                {

                    return DateTime.UtcNow.ToString();
                }
                else {
                    return "Unsupported transaction";
                }
            }
        }
    }
    
    public class TcpRequest {

        public string Method { get; set; }
        public string Url { get; set; }
        public string Protocol { get; set; }
        public string ProtocolVersion { get; set; }
        public string Host { get; set; }
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public string Language { get; set; }
        public string Encoding { get; set; }
        public string Connection { get; set; }
        public string Referer { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public int ContentDisposition { get; set; }
        public string CacheControl { get; set; }
        public string Authorization { get; set; }
        public string RequestBody { get; set; }
        public Dictionary<string, string> Parameters { get; set;}
        public Dictionary<string, string> OtherHeaders { get; set; }

        public TcpRequest()
        {
            this.Authorization = "";
            this.ContentLength = 0;
            this.ContentType = "none";
        }

        public void PrintParams() {

            foreach (var v in this.Parameters) {

                Console.WriteLine($"{v.Key} >> {v.Value}");
               
            }
        }

        public override string ToString() {

            return $"TcpRequest" +
                $"\n> Method: {this.Method}" +
                $"\n> URL: {this.Url}" +
                $"\n> Protocol {this.Protocol}" +
                $"\n> Protocol Version {this.ProtocolVersion}" +
                $"\n> Host {this.Host}" +
                $"\n> UserAgent {this.UserAgent}" +
                $"\n> Accept {this.Accept}" +
                $"\n> Language: {this.Language}" +
                $"\n> Encoding: {this.Encoding}" +
                $"\n> Connection: {this.Connection}" +
                $"\n> Referer: {this.Referer}" + 
                $"\n> CacheControl: {this.CacheControl}" +
                $"\n> ContentLength: {this.ContentLength}" +
                $"\n> ContentType: {this.ContentType}" +
                $"\n> Authorization: {this.Authorization}"+
                $"\n> RequestBody: {this.RequestBody}"+
                $"\n> ParameterCount: {this.Parameters.Count}";
        }             
    }
}
