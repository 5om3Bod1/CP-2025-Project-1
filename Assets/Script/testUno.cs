using System.IO.Ports;
using UnityEngine;
using System.Threading;

public class testUno : MonoBehaviour
{
    Thread IOThread = new Thread(DataThread);
    private static SerialPort sp;
    private static string incomingMsg = "";

    private static void DataThread()
    {
        //Mac - /dev/cu.usbmodem1101
        //PC - COM
        sp = new SerialPort("/dev/cu.usbmodem11201", 9600);
        sp.Open();

        while (true)
        {
            incomingMsg = sp.ReadExisting();
            Thread.Sleep(200);
        }
    }

    private void OnDestory()
    {
        IOThread.Abort();
        sp.Close();
    }

    void Start()
    {
        IOThread.Start();
    }

    void Update()
    {
        Debug.Log(incomingMsg);
        /*
        if(incomingMsg == "1")
        {
            bad();
            incomingMsg = "0";
        }
        else if(incomingMsg == "2")
        {
            alright();
            incomingMsg = "0";
        }
        else if(incomingMsg == "3")
        {
            good();
            incomingMsg = "0";
        }
    }

    void bad()
    {

    }
    void alright()
    {

    }
    void good()
    {

    }
    */
    }
}
