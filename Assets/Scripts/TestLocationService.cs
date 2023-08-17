using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class TestLocationService : MonoBehaviour
{
    public string hintText;

    private void Start()
    {
        //GetLocalIPAddress();
        //Debug.Log(GetLocalIPAddress());
        //Debug.Log(Dns.GetHostAddresses(Dns.GetHostName()));
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                hintText = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

}