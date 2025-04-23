using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text diamondAmountText;
    [SerializeField] private Image heartProgressBar;
    [SerializeField] private Button addDiamondButton;

    private void Awake()
    {
        UpdateDiamondAmount();
        UpdateHeartBar();
        addDiamondButton.onClick.AddListener(() => StartCoroutine(APIAddDiamond()));
    }

    private void OnDestroy()
    {
        addDiamondButton.onClick.RemoveAllListeners();
    }

    private void UpdateDiamondAmount()
    {
        diamondAmountText.text = DataManager.UserData.diamond.ToString();
        addDiamondButton.interactable = DataManager.UserData.diamond < 10000;
    }

    private void UpdateHeartBar() => heartProgressBar.fillAmount = DataManager.UserData.heart / 100f;

    private IEnumerator APIAddDiamond()
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", DataManager.UserData.user_id);
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/Basic_System/addDiamond.php", form);
        yield return www.SendWebRequest();
        Debug.Log($"Status: {www.responseCode} Result: {www.downloadHandler.text}");
        if (www.result == UnityWebRequest.Result.Success)
            DataManager.Instance.AddDiamond(100, UpdateDiamondAmount);
        else
            MsgBox.Instance.ShowMsg($"ไม่สามารถดำเนินการได้ กรุณาลองใหม่อีกครั้ง\n({www.downloadHandler.text})");
    }
}
