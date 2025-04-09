using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button registerButton;
    [SerializeField] Button loginButton;
    [SerializeField] RegisterPanel registerPanel;

    private void Awake()
    {
        registerButton.onClick.AddListener(OnRegister);
        loginButton.onClick.AddListener(OnLogin);
    }

    private void OnDestroy()
    {
        registerButton.onClick.RemoveAllListeners();
        loginButton.onClick.RemoveAllListeners();
    }

    private void OnRegister()
    {
        gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(true);
    }

    private void OnLogin()
    {
        if (string.IsNullOrEmpty(usernameField.text) || string.IsNullOrEmpty(passwordField.text))
        {
            MsgBox.Instance.ShowMsg("กรุณาใส่ข้อมูลให้ครบทุกช่อง");
            return;
        }

        StartCoroutine(APILogin());
    }

    private IEnumerator APILogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest www = UnityWebRequest.Post("https://test-piggy.codedefeat.com/worktest/dev02/login.php", form);
        yield return www.SendWebRequest();
        Debug.Log($"Status: {www.responseCode} Result: {www.downloadHandler.text}");
        if (www.result == UnityWebRequest.Result.Success)
        {
            DataManager.Instance.InitUserData(www.downloadHandler.text);
            SceneManager.LoadScene(1);
        }
        else
            MsgBox.Instance.ShowMsg($"ไม่สามารถดำเนินการได้ กรุณาลองใหม่อีกครั้ง\n({www.downloadHandler.text})");
    }
}
