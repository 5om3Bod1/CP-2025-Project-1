using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class bubbleGen : MonoBehaviour
{
    //Num Gen
    public int value;

    //Total num of students to choose from
    private List<int> totalAmount = new List<int>();

    //Bubble Colors
    public GameObject[] bubble;

    int messageInt;

    Thread IOThread = new Thread(DataThread);
    private static SerialPort sp;
    private static string incomingMsg = "";

    private static void DataThread()
    {
        // Mac - /dev/cu.usbmodem1101
        // PC - COM
        sp = new SerialPort("/dev/cu.usbmodem11201", 9600);
        sp.Open();

        while (true)
        {
            incomingMsg = sp.ReadExisting();
            Thread.Sleep(200);
        }
    }

    private void OnDestroy()
    {
        if (IOThread != null && IOThread.IsAlive)
        {
            IOThread.Abort();
        }

        if (sp != null && sp.IsOpen)
        {
            sp.Close();
        }
    }

    private void Start()
    {
        // StartCoroutine(myCounter());
        IOThread.Start();
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(incomingMsg))
        {
            string trimmedMsg = incomingMsg.Trim();

            if (int.TryParse(trimmedMsg, out messageInt))
            {
                Debug.Log(messageInt);

                bool processed = false;

                if (messageInt == 1)
                {
                    bad();
                    processed = true;
                }
                else if (messageInt == 2)
                {
                    alright();
                    processed = true;
                }
                else if (messageInt == 3)
                {
                    good();
                    processed = true;
                }

                if (processed)
                {
                    incomingMsg = "";
                }
            }
            else
            {
                Debug.LogError("Failed to parse input: " + trimmedMsg);
            }
        }
    }

    void bad()
    {
        // totalAmount.Add(0);
        // textScript.Instance.badNum++; // Fixed typo in "Instance"

        int genThis = 0;
        Vector3 randompawnPosition = new Vector3(Random.Range(-2f, 2f), -3.75f, Random.Range(-2f, 2f));
        Instantiate(bubble[genThis], randompawnPosition, Quaternion.identity);
    }

    void alright()
    {
        // totalAmount.Add(1);
        // textScript.Instance.alrNum++; // Fixed typo in "Instance"

        int genThis = 1;
        Vector3 randompawnPosition = new Vector3(Random.Range(-2f, 2f), -3.75f, Random.Range(-2f, 2f));
        Instantiate(bubble[genThis], randompawnPosition, Quaternion.identity);
    }

    void good()
    {
        // totalAmount.Add(2);
        // textScript.Instance.goodNum++; // Fixed typo in "Instance"

        int genThis = 2;
        Vector3 randompawnPosition = new Vector3(Random.Range(-2f, 2f), -3.75f, Random.Range(-2f, 2f));
        Instantiate(bubble[genThis], randompawnPosition, Quaternion.identity);
    }
}