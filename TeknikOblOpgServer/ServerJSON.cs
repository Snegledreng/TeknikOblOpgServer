using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;

namespace TeknikOblOpgServer;

public class ServerJSON
{
	private const int PORT = 42069;

	public void Start()
	{
		//Definerer server
		TcpListener server = new TcpListener(PORT);
		server.Start();
		Console.WriteLine("ServerJSON startes på port: " + PORT);
		//Venter på klient
		while (true)
		{
			TcpClient socket = server.AcceptTcpClient();
			Task.Run(() => DoOneClient(socket));
		}
	}

	public void DoOneClient(TcpClient socket)
	{
		StreamReader sr = new StreamReader(socket.GetStream());
		StreamWriter sw = new StreamWriter(socket.GetStream());

        try
        {
            string json = sr.ReadLine();
            CommandFromClient commandFromClient = JsonSerializer.Deserialize<CommandFromClient>(json);

            int result = 0;
            void PrintResult()
            {
                sw.WriteLine(result);
                sw.Flush();
            }
            switch (commandFromClient.Command.ToLower())
            {
                case "add":
                    result = Add(commandFromClient.Number1, commandFromClient.Number2);
                    PrintResult();
                    break;
                case "subtract":
                    result = Subtract(commandFromClient.Number1, commandFromClient.Number2);
                    PrintResult();
                    break;
                case "random":
                    result = Random(commandFromClient.Number1, commandFromClient.Number2);
                    PrintResult();
                    break;
                default:
					sw.WriteLine("Unknown command. Use commands: add, subtract, random");
                    sw.Flush();
					break;
            }
        }
        catch (Exception e)
        {
			sw.WriteLine(e.Message);
        }
    }

    public class CommandFromClient
    {
        public string Command { get; set; }
		public int Number1 { get; set; }
		public int Number2 { get; set; }
    }

    public int Add(int a, int b)
	{
		return a + b;
	}

	public int Subtract(int a, int b)
	{
		return a - b;
	}

	public int Random(int a, int b)
	{
		System.Random random = new();
		return random.Next(a, b + 1);
	}
}