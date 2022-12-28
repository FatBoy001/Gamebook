using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class RecordData : MonoBehaviour
{
    [SerializeField] private readonly string fetchDataUrl = URL.SearchGameRecord;
    [SerializeField] private GameObject record;
    [SerializeField] private GameObject parentObject;
    [SerializeField] public static string type;
    [SerializeField] public static string game;

    public void fetchData()
    {
        StartCoroutine(TryFetchData());
    }


    private IEnumerator TryFetchData()
    {
        Debug.Log(RecordData.type+" "+ RecordData.game);
        WWWForm form = new WWWForm();
        form.AddField("rType", RecordData.type);
        form.AddField("rGame", RecordData.game);
        form.AddField("rStudent_oid", StudentData.getStudentId());
        //post檔案給node.js
        UnityWebRequest request = UnityWebRequest.Post(fetchDataUrl, form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 100.0f)
            {
                break;
            }
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)//僅代表成功連接
        {
            RecordArray response = JsonUtility.FromJson<RecordArray>(request.downloadHandler.text);
            Debug.Log(response.Items.Length);
            foreach (RecordForm item in response.Items)
            {
                record.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = item.score.ToString();
                record.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = item.end_time.ToString();
                Instantiate(record).transform.SetParent(parentObject.transform);
               
            }
            
        }
        yield return null;
    }
    public static void setDataType(string type,string game)
    {
        RecordData.type = type;
        RecordData.game = game;
    }
}
