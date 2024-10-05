// See https://aka.ms/new-console-template for more information


using TeknikOblOpgServer;

/*
 * Comment out one, and run other.
 * line 11 for the string-based server
 * line 13 for the json-based server
 */

//Server server = new(); server.Start();

ServerJSON serverJSON = new(); serverJSON.Start();