import json
from socket import *

serverName = 'localhost'
serverPort = 42069

clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((serverName, serverPort))

def send_and_recieve():
    command = input('Enter command: ')
    number1 = input('Enter first number: ')
    number2 = input('Enter second number: ')
    
    sentence = {"Command": command, "Number1": int(number1), "Number2": int(number2)}
    sentence = json.dumps(sentence)
    sentence = sentence + '\r\n'
    byteSentence = sentence.encode()
    clientSocket.send(byteSentence)

    returnSentence = clientSocket.recv(1024)
    print(returnSentence.decode())

send_and_recieve()

clientSocket.close()