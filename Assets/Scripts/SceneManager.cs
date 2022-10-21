using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using System.Xml;
using System.IO;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI OutputText;

    [SerializeField]
    TextMeshProUGUI DebugText;

    [SerializeField]
    GameObject VizPanel;

    [SerializeField]
    DSNConnection[] DSNConnections = new DSNConnection[14];

    float dataTimer = 0f;
    float dataTimerMax = 2f;

    public void GetMethod()
    {
        DebugText.text = "GET Status";
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://eyes.nasa.gov/dsn/data/dsn.xml");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            DebugText.text = "GET Status\n" + www.error;
        }
        else {
            Debug.Log(www.downloadHandler.text);
            DebugText.text = "GET Status\n" + "XML download complete!";

            for (int i = 0; i < DSNConnections.Length; i++)
            {
                DSNConnections[i].SetSpeedWidths(0, 0);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.downloadHandler.text);
            XmlNodeList dishList = xmlDoc.SelectNodes("/dsn/dish");
            OutputText.text = "";
            foreach (XmlNode node in dishList)
            {
                string outputString = "";
                string dishName = node.Attributes.GetNamedItem("name").Value;
                outputString = dishName + "\n";
                XmlNodeList upList = node.SelectNodes("upSignal");
                XmlNodeList downList = node.SelectNodes("downSignal");
                float uploadWidth = 0;
                float downloadWidth = 0;
                foreach (XmlNode unode in upList)
                {
                    if (unode.Attributes.GetNamedItem("signalType").Value == "data")
                    {
                        string dataRateString = unode.Attributes.GetNamedItem("dataRate").Value;
                        float dataRate = float.Parse(dataRateString);
                        float dataRateConverted = dataRate;
                        string units = "b/sec";
                        if (dataRate < 1000f)
                        {
                            uploadWidth = Mathf.Max(uploadWidth, 2f);
                            units = "b/sec";
                        }
                        else if (dataRate < 1000000)
                        {
                            uploadWidth = Mathf.Max(uploadWidth, 4f);
                            dataRateConverted = dataRateConverted / 1000f;
                            units = "kb/sec";
                        }
                        else if (dataRate < 1000000000)
                        {
                            uploadWidth = Mathf.Max(uploadWidth, 8f);
                            dataRateConverted = dataRateConverted / 1000000f;
                            units = "mb/sec";
                        }
                        else if (dataRate < 1000000000000)
                        {
                            uploadWidth = Mathf.Max(uploadWidth, 16f);
                            dataRateConverted = dataRateConverted / 1000000000f;
                            units = "gb/sec";
                        }
                        outputString = outputString + "U:" + dataRateConverted.ToString("F2") + units + " ";
                    }
                }
                foreach (XmlNode dnode in downList)
                {
                    if (dnode.Attributes.GetNamedItem("signalType").Value == "data")
                    {
                        string dataRateString = dnode.Attributes.GetNamedItem("dataRate").Value;
                        float dataRate = float.Parse(dataRateString);
                        float dataRateConverted = dataRate;
                        string units = "b/sec";
                        if (dataRate < 1000f)
                        {
                            downloadWidth = Mathf.Max(downloadWidth, 2f);
                            units = "b/sec";
                        }
                        else if (dataRate < 1000000)
                        {
                            downloadWidth = Mathf.Max(downloadWidth, 4f);
                            dataRateConverted = dataRateConverted / 1000f;
                            units = "kb/sec";
                        }
                        else if (dataRate < 1000000000)
                        {
                            downloadWidth = Mathf.Max(downloadWidth, 8f);
                            dataRateConverted = dataRateConverted / 1000000f;
                            units = "mb/sec";
                        }
                        else if (dataRate < 1000000000000)
                        {
                            downloadWidth = Mathf.Max(downloadWidth, 16f);
                            dataRateConverted = dataRateConverted / 1000000000f;
                            units = "gb/sec";
                        }
                        outputString = outputString + "D:" + dataRateConverted.ToString("F2") + units + " ";
                    }
                }

                // find the matching dish
                for (int i = 0; i < DSNConnections.Length; i++)
                {
                    if (DSNConnections[i].dishID.ToString() == dishName)
                        DSNConnections[i].SetSpeedWidths(uploadWidth, downloadWidth);
                }

                OutputText.text = OutputText.text + outputString + "\n\n";

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        dataTimer -= Time.deltaTime;
        if (dataTimer <= 0)
        {
            GetMethod();
            dataTimer = dataTimerMax;
        }
    }

    public void ShowData()
    {
        VizPanel.SetActive(false);
    }
    public void ShowViz()
    {
        VizPanel.SetActive(true);
    }
}
