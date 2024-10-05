using System.Net.Sockets;
using System.Security.Cryptography;

namespace TeknikOblOpgServer;

public class Server
{
	private const int PORT = 6969;

	public void Start()
	{
		//Definerer server
		TcpListener server = new TcpListener(PORT);
		server.Start();
		Console.WriteLine("Server startes på port: " + PORT);
		//Venter på klient
		while (true)
		{
			TcpClient socket = server.AcceptTcpClient();
			Task.Run(() => DoOneClient(socket));
		}
	}

	public void DoOneClient(TcpClient socket)
	{
		//åbner for tekst-strenge
		StreamReader sr = new StreamReader(socket.GetStream());
		StreamWriter sw = new StreamWriter(socket.GetStream());
		
		//Læser linje fra netværket
		string kommando = sr.ReadLine();

		//Skriver linje tilbage
        if (kommando.ToLower() is "add" or "subtract" or "random")
        {
            sw.WriteLine(kommando + " recieved.");
            sw.WriteLine("Input two numbers:");
            sw.Flush();
        }
        else
        {
			sw.WriteLine(kommando + " unrecognised");
			sw.Flush();
        }

        //Læser linje fra netværket
		string[] tals = sr.ReadLine().Split(" ");
		List<int> talsInt = new();
		talsInt.Add(int.Parse(tals[0]));
		talsInt.Add(int.Parse(tals[1]));

        switch (kommando.ToLower())
        {
            //Laver beregninger, og skriver resultat tilbage.
            case "add":
                sw.WriteLine(Add(talsInt).ToString());
                break;
            case "subtract":
                sw.WriteLine(Subtract(talsInt).ToString());
                break;
            case "random":
                sw.WriteLine(Random(talsInt).ToString());
                break;
        }
        sw.Flush();
    }

    public int Add(List<int> l)
	{
		return l[0] + l[1];
	}

	public int Subtract(List<int> l)
	{
		return l[0] - l[1];
	}

	public int Random(List<int> l)
	{
		System.Random random = new();
		return random.Next(l[0], l[1] + 1);
	}
}