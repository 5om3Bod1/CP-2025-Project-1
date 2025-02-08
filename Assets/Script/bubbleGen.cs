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
    //private List<int> totalAmount = new List<int>();

    //Bubble Colors
    public GameObject[] bubble;

    int messageInt;

    public AudioSource audioSource;
    public AudioSource Thanks1;
    public AudioSource Thanks2;
    public AudioSource Thanks3;

    bool coolDown;

    //public Vector3 randomspawnPosition;

    public Vector3[] location;

    Thread IOThread = new Thread(DataThread);
    private static SerialPort sp;
    private static string incomingMsg = "";

    private static void DataThread()
    {
        // Mac - /dev/cu.usbmodem1101
        // PC - COM
        sp = new SerialPort("/dev/cu.usbmodem21301", 9600);
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

                if (messageInt == 1 && !coolDown)
                {
                    coolDown = true;
                    bad();
                    processed = true;
                }
                else if (messageInt == 2 && !coolDown)
                {
                    coolDown = true;
                    alright();
                    processed = true;
                }
                else if (messageInt == 3 && !coolDown)
                {
                    coolDown = true;
                    good();
                    processed = true;
                }
                else if (messageInt == 4)
                {
                    playSound();
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
        //Vector3 randomspawnPosition = new Vector3(Random.Range(-2f, 2f), -3.75f, Random.Range(-2f, 2f));
        Instantiate(bubble[genThis], location[0], Quaternion.identity);
        Thanks1.Play();
        Invoke("Off", .5f);
    }

    void alright()
    {
        // totalAmount.Add(1);
        // textScript.Instance.alrNum++; // Fixed typo in "Instance"

        int genThis = 1;
        //Vector3 randomspawnPosition = new Vector3(Random.Range(-2f, 2f), -3.75f, Random.Range(-2f, 2f));
        Instantiate(bubble[genThis], location[1], Quaternion.identity);
        Thanks2.Play();
        Invoke("Off", .5f);
    }

    void good()
    {
        // totalAmount.Add(2);
        // textScript.Instance.goodNum++; // Fixed typo in "Instance"

        int genThis = 2;
        //Vector3 randomspawnPosition = new Vector3(Random.Range(-2f,2f), 10.5f, Random.Range(26.6f, 30.6f));
        Instantiate(bubble[genThis], location[2], Quaternion.identity);
        Thanks3.Play();
        Invoke("Off", .5f);
    }
    void playSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    void Off()
    {
        coolDown = false;
    }
}
