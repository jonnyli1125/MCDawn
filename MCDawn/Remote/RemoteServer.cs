using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using NATUPNPLib;

namespace MCDawn
{
    public class RemoteServer // server side shit
    {
        public delegate void RemoteListChangeHandler(List<Remote> remotes);
        public static event RemoteListChangeHandler OnRemoteListUpdate = null;

        public struct User
        {
            public string name;
            public string password;
        }

        public static List<User> rcusers = new List<User>();
        public static ushort port = 4700;
        public static string rcpass = "";
        public static byte rcProtocolVersion = 1;
        public static Socket listen;

        public static string Hash(string password)
        {
            byte[] bytes = new SHA256Managed().ComputeHash(new UTF8Encoding().GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) { sb.Append(bytes[i].ToString("x2")); }
            return sb.ToString();
        }

        public static void LoadUsers()
        {
            try
            {
                if (!Directory.Exists("remote")) { Directory.CreateDirectory("remote"); }
                if (!Directory.Exists("remote/users/")) { Directory.CreateDirectory("remote/users/"); }
                rcusers.Clear();
                try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.liRCUsers.Items.Clear(); } }
                catch { }
                foreach (FileInfo fi in new DirectoryInfo("remote/users/").GetFiles())
                {
                    if (fi.Name.Remove(fi.Name.Length - 4) != "")
                    {
                        User u = new User();
                        u.name = fi.Name.Remove(fi.Name.Length - 4);
                        u.password = File.ReadAllText("remote/users/" + fi.Name);
                        rcusers.Add(u);
                        try { if (!Server.cli) MCDawn.Gui.Window.thisWindow.liRCUsers.Items.Add(u.name); }
                        catch { }
                    }
                }
            }
            catch { }
        }
        public static void AddUser(string name, string password) { File.WriteAllText("remote/users/" + name + ".xml", password); }
        public static void RemoveUser(string name) { if (File.Exists("remote/users/" + name + ".xml")) { File.Delete("remote/users/" + name + ".xml"); } }

        public static void RemoteListUpdate() { if (OnRemoteListUpdate != null) OnRemoteListUpdate(Remote.remotes); }

        public static void Start()
        {
            try
            {
                Server.s.Log("Creating listening socket on port " + port + " for Remote Console... ");
                if (Setup())
                {
                    if (Server.upnp)
                        if (UpnpSetup())
                        {
                            Server.s.Log("Remote Console Ports have been forwarded with UPnP.");
                            Server.upnpRunning = true;
                        }
                        else
                        {
                            Server.s.Log("Could not auto forward Remote Console ports with UPnP.");
                            Server.upnpRunning = false;
                        }

                    Server.s.Log("Done.");
                }
                else
                {
                    Server.s.Log("Could not create socket connection for Remote Console.  Shutting down.");
                    return;
                }
                LoadUsers();
                Server.s.Log("Remote Console users list loaded.");
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        public static bool Setup()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
                listen = new Socket(endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(endpoint);
                listen.Listen((int)SocketOptionName.MaxConnections);

                listen.BeginAccept(new AsyncCallback(Accept), null);
                return true;
            }
            catch (Exception e) { Server.ErrorLog(e); return false; }
        }

        public static bool UpnpSetup()
        {
            try
            {
                ushort ar = Convert.ToUInt16(port);
                UpnpHelper Upnp = new UpnpHelper();
                Upnp.AddMapping(ar, "TCP", "MCDawnTCP");
                return true;
            }
            catch (Exception e) { Server.ErrorLog(e); Server.s.Log("Failed. Your router may not be compatible with UPnP."); return false; }
        }

        static void Accept(IAsyncResult result)
        {
            try
            {
                if (Server.shuttingDown == false)
                {
                    // found information: http://www.codeguru.com/csharp/csharp/cs_network/sockets/article.php/c7695
                    // -Descention
                    Remote p = null;
                    try
                    {
                        p = new Remote(listen.EndAccept(result));
                        listen.BeginAccept(new AsyncCallback(Accept), null);
                    }
                    catch (SocketException)
                    {
                        if (p != null)
                            p.Disconnect();
                    }
                    catch (Exception e)
                    {
                        Server.ErrorLog(e);
                        if (p != null)
                            p.Disconnect();
                    }
                }
            }
            catch { }
        }

        public static void Exit()
        {
            try
            {
                Remote.remotes.ForEach(delegate(Remote r)
                {
                    r.Disconnect(Server.customShutdownMessage);
                });
                if (Server.upnpRunning || Server.upnp)
                {
                    UPnPNATClass u = new UPnPNATClass();
                    //u.StaticPortMappingCollection.Remove(Server.port, "UDP");
                    u.StaticPortMappingCollection.Remove(port, "TCP");
                    Server.s.Log("UPnP forwarded Remote Console ports have been closed.");
                }
                if (listen != null)
                    listen.Close();
            }
            catch { }
        }
    }
}