using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MsgBox : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button confirmButton;

    public static MsgBox Instance;

    private Action onConfirm;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        confirmButton.onClick.AddListener(OnConfirmButton);
    }

    private void OnDestroy()
    {
        confirmButton.onClick.RemoveAllListeners();
    }

    public void ShowMsg(string message, Action onConfirm = null)
    {
        messageText.text = message;
        this.onConfirm = onConfirm;
        panel.SetActive(true);
    }

    private void OnConfirmButton()
    {
        panel.SetActive(false);
        onConfirm?.Invoke();
    }
}
