using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class UpdateProfile : MonoBehaviour
{
    private string updateProfile = URL.UpdateProfile;
    public GameObject[] labels,inputs;
    public GameObject modifyButtons,modifyModeButton;
    private string newUserName,newEmail, newPassword, newSchool,newClass;
    public void OnUpdateClick()
    {
        Debug.Log(inputs[0]);
        newUserName = inputs[0].GetComponent<TMP_InputField>().text;
        newEmail = inputs[1].GetComponent<TMP_InputField>().text;
        newPassword = inputs[2].GetComponent<TMP_InputField>().text;
        newSchool = inputs[3].GetComponent<TMP_InputField>().text;
        newClass = inputs[4].GetComponent<TMP_InputField>().text;
        StartCoroutine(TryUpdateProfile());
    }
    private IEnumerator TryUpdateProfile()
    {
        WWWForm form = new WWWForm();
        form.AddField("id",StudentData.getStudentId());
        form.AddField("userName", newUserName);
        form.AddField("email", newEmail);
        form.AddField("password", newPassword);
        form.AddField("school", newSchool);
        form.AddField("Class", newClass);

        UnityWebRequest request = UnityWebRequest.Post(updateProfile, form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 10.0f)
            {
                break;
            }
            yield return null;
        }
        if (request.result == UnityWebRequest.Result.Success)//僅代表成功連接
        {
            
            openUpdate();
            //node.js回傳的res,不會回傳資料而是有沒有成功code=0就是有找到
            ProfileForm response = JsonUtility.FromJson<ProfileForm>(request.downloadHandler.text);
            Debug.Log(response.userName);
            labels[0].GetComponent<TextMeshProUGUI>().text = response.userName;
            labels[1].GetComponent<TextMeshProUGUI>().text = response.email;
            labels[2].GetComponent<TextMeshProUGUI>().text = response.password;
            labels[3].GetComponent<TextMeshProUGUI>().text = response.school;
            labels[4].GetComponent<TextMeshProUGUI>().text = response.Class;
        }
        /*
        else
        {

        }
        */

        yield return null;

    }
    public void openUpdate()
    {
        for(int i=0; i < labels.Length; i++)
        {
            labels[i].SetActive(!labels[i].activeSelf);
        }
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].SetActive(!inputs[i].activeSelf);
        }
        for (int i = 0; i < labels.Length; i++)
        {
            inputs[i].GetComponent<TMP_InputField>().text = labels[i].GetComponent<TextMeshProUGUI>().text;
        }
        modifyModeButton.SetActive(!modifyModeButton.activeSelf);
        modifyButtons.SetActive(!modifyButtons.activeSelf);
    }
}
