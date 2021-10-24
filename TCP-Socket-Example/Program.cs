// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

const int ServerPortNum = 49570;

Console.WriteLine("Enter 's' for server, 'c' for client");

string inp = Console.ReadLine();

if (inp.Equals("s"))
{
    Server();
}

else if (inp.Equals("c"))
{
    Client();
}

else
{
    Console.WriteLine("Unexpected input!");
}

void Client()
{
    // Create a Socket
    IPEndPoint clientEndPoint = new System.Net.IPEndPoint(IPAddress.Loopback, 0);
    Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
    clientSocket.Bind(clientEndPoint);

    // Create connection
    IPEndPoint serverEndPoint = new System.Net.IPEndPoint(IPAddress.Loopback, ServerPortNum);
    clientSocket.Connect(serverEndPoint);

    // Send message
    string messageToSend = "Hello World";
    byte[] bytesToSend = Encoding.Default.GetBytes(messageToSend);
    clientSocket.Send(bytesToSend);
    Console.WriteLine("Client sent message " + messageToSend);

    // Display received message 
    byte[] buffer = new byte[1024];
    int numberOfBytesReceived = clientSocket.Receive(buffer);
    byte[] receivedBytes = new byte[numberOfBytesReceived];
    Array.Copy(buffer, receivedBytes, numberOfBytesReceived);
    string receivedMessage = Encoding.Default.GetString(receivedBytes);

    Console.WriteLine("Client received message:" + receivedMessage);

    Console.ReadLine();
}
void Server()
{
    // Create a Socket
    IPEndPoint serverEndPoint = new System.Net.IPEndPoint(IPAddress.Loopback, ServerPortNum);
    Socket welcomingSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
    welcomingSocket.Bind(serverEndPoint);

    // Wait for connection
    welcomingSocket.Listen();
    Socket connectionSocket = welcomingSocket.Accept();

    // Display received message
    byte[] buffer = new byte[1024];
    int numberOfBytesReceived = connectionSocket.Receive(buffer);
    byte[] receivedBytes = new byte[numberOfBytesReceived];
    Array.Copy(buffer, receivedBytes, numberOfBytesReceived);
    string receivedMessage = Encoding.Default.GetString(receivedBytes);
    Console.WriteLine("Server received message: " + receivedMessage);

    // Send received message to the client
    connectionSocket.Send(receivedBytes);
    Console.ReadLine();
}