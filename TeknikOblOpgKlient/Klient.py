from socket import *

serverName = 'localhost'
serverPort = 6969

clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((serverName, serverPort))

print('Connected. Input command from list:')
print('add, subtract, random')

def send_and_recieve():
    sentence = input()
    sentence = sentence + '\r\n'
    byteSentence = sentence.encode()
    clientSocket.send(byteSentence)

    returnSentence = clientSocket.recv(1024)
    print(returnSentence.decode())

send_and_recieve()
send_and_recieve()

clientSocket.close()