using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";
    [SerializeField] private readonly string loginUrl = URL.Login;
    [SerializeField] private readonly string createUrl = URL.Create;

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private TextMeshProUGUI createAlertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button createButton;

    //Create
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField checkPasswordInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField schoolInputField;
    [SerializeField] private TMP_InputField classInputField;

    //Login
    [SerializeField] private TMP_InputField enailLoginInputField;
    [SerializeField] private TMP_InputField passwordLoginInputField;
    public void OnLoginClick()
    {
        alertText.text = "Signing in...";
        ActivateButtons(false);

        StartCoroutine(TryLogin());
    }
    public void OnCreateClick()
    {
        createAlertText.text = "Creating in...";
        ActivateButtons(false);
        StartCoroutine(TryCreate());
    }

    private IEnumerator TryLogin()
    {
        string email = enailLoginInputField.text;
        string password = passwordLoginInputField.text;

        if (email.Length < 3 || email.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }

        /*if (!Regex.IsMatch(password, PASSWORD_REGEX))
        {
            alertText.text = "Invalid credentials";
            ActivateButtons(true);
            yield break;
        }*/

        WWWForm form = new WWWForm();
        form.AddField("rEmail", email);
        form.AddField("rPassword", password);

        //post檔案給node.js
        UnityWebRequest request = UnityWebRequest.Post(loginUrl, form);
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
        //Debug.Log(request.downloadHandler.text);
        Debug.Log(request.result);
        if (request.result == UnityWebRequest.Result.Success)//僅代表成功連接
        {
            Debug.Log("Success");
            //node.js回傳的res,不會回傳資料而是有沒有成功code=0就是有找到
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

            if (response.code == 0) // login success?
            {
                ActivateButtons(false);
                alertText.text = "Welcome";
                Debug.Log(response.student_id);
                StudentData.setStudentId(response.student_id);
                SceneManager.LoadScene("HomePage");
            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        alertText.text = "Invalid credentials";
                        ActivateButtons(true);
                        break;
                    default:

                        alertText.text = "Corruption detected";
                        ActivateButtons(false);
                        break;
                }
            }

        }
        else
        {
            alertText.text = "Error connecting to the server";
            ActivateButtons(true);
        }
        Debug.Log($"{email}:{password}");

        yield return null;
    }

    private IEnumerator TryCreate()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string checkPassword = checkPasswordInputField.text;
        string email = emailInputField.text;
        string school = schoolInputField.text;
        string cluss = classInputField.text;

        if (username.Length < 3 || username.Length > 24)
        {
            createAlertText.text = "Invalid username";
            ActivateButtons(true);
            Debug.Log("Create Erro 1");
            yield break;
        }
        if (!checkPassword.Equals(password))
        {
            createAlertText.text = "密碼輸入不同請重新檢查";
            ActivateButtons(true);
            Debug.Log("Create Erro 2");
            yield break;
        }
        /*if (!Regex.IsMatch(password, PASSWORD_REGEX))
        {
            createAlertText.text = "Invalid credentials";
            ActivateButtons(true);
            Debug.Log("Create Erro 2");
            yield break;
        }*/
        Debug.Log("Create...");
        WWWForm form = new WWWForm();
        form.AddField("rEmail", email);
        form.AddField("rPassword", password);
        form.AddField("rUsername", username);
        form.AddField("rSchool", school);
        form.AddField("rClass", cluss);
        UnityWebRequest request = UnityWebRequest.Post(createUrl, form);
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
        Debug.Log(request.result);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            CreateResponse response = JsonUtility.FromJson<CreateResponse>(request.downloadHandler.text);

            if (response.code == 0)
            {
                createAlertText.text = "帳號建立完成!";
            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        createAlertText.text = "Invalid credentials";
                        break;
                    case 2:
                        createAlertText.text = "已存在使用者";
                        break;
                    case 3:
                        createAlertText.text = "密碼強度不足最好有一個大寫英文8個字元";
                        break;
                    default:
                        createAlertText.text = "Corruption detected";
                        break;
                }
            }
        }
        else
        {
            createAlertText.text = "Error connecting to the server...";
        }
        ActivateButtons(true);
        yield return null;
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        createButton.interactable = toggle;
    }
}
