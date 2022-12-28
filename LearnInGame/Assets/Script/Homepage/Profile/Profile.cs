using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Profile : MonoBehaviour
{
    private string urlProfile = URL.urlProfile;
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI emailText;
    [SerializeField] private TextMeshProUGUI passwordText;
    [SerializeField] private TextMeshProUGUI schoolText;
    [SerializeField] private TextMeshProUGUI classText;

    public void fetchProfile()
    {
        StartCoroutine(TryFetchProfile());
    }
    private IEnumerator TryFetchProfile()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", StudentData.getStudentId());

        UnityWebRequest request = UnityWebRequest.Post(urlProfile, form);
        var handler = request.SendWebRequest();
        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 30.0f)
            {
                break;
            }
            yield return null;
        }
        if (request.result == UnityWebRequest.Result.Success)
        {
            ProfileForm response = JsonUtility.FromJson<ProfileForm>(request.downloadHandler.text);
            userNameText.text = response.userName;
            emailText.text = response.email;
            passwordText.text = response.password;
            schoolText.text = response.school;
            classText.text = response.Class;
        }
        yield return null;
    }
}
