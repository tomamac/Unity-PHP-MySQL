using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] TMP_InputField confirmPasswordField;
    [SerializeField] Button registerButton;
    [SerializeField] LoginPanel loginPanel;

    private void Awake()
    {
        registerButton.onClick.AddListener(OnRegister);
    }

    private void OnDestroy()
    {
        registerButton.onClick.RemoveAllListeners();
    }

    private void OnRegister()
    {
        if (string.IsNullOrEmpty(usernameField.text) || string.IsNullOrEmpty(passwordField.text) || string.IsNullOrEmpty(confirmPasswordField.text))
        {
            MsgBox.Instance.ShowMsg("กรุณาใส่ข้อมูลให้ครบทุกช่อง");
            return;
        }

        if (!confirmPasswordField.text.Equals(passwordField.text))
        {
            MsgBox.Instance.ShowMsg("รหัสผ่านยืนยันไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", () => confirmPasswordField.text = "");
            return;
        }

        StartCoroutine(APIRegister());
    }

    private IEnumerator APIRegister()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/Basic_System/register.php", form);
        yield return www.SendWebRequest();
        Debug.Log($"Status: {www.responseCode} Result: {www.downloadHandler.text}");
        if (www.result == UnityWebRequest.Result.Success)
        {
            MsgBox.Instance.ShowMsg("สมัครสมาชิกสำเร็จ", () =>
            {
                gameObject.SetActive(false);
                loginPanel.gameObject.SetActive(true);
            });
        }
        else
        {
            MsgBox.Instance.ShowMsg($"ไม่สามารถดำเนินการได้ กรุณาลองใหม่อีกครั้ง\n({www.downloadHandler.text})");
        }
        usernameField.text = "";
        passwordField.text = "";
        confirmPasswordField.text = "";
    }

}
