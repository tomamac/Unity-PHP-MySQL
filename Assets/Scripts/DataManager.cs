using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Serializable]
    public class JsonUserData
    {
        public int user_id;
        public int diamond;
        public int heart;
    }

    public static DataManager Instance;
    public static JsonUserData UserData = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitUserData(string response)
    {
        UserData = JsonUtility.FromJson<JsonUserData>(response);
    }

    public void AddDiamond(int amount, Action callback = null)
    {
        UserData.diamond += amount;
        callback?.Invoke();
    }

    public void AddHeart(int amount, Action callback = null)
    {
        UserData.heart += amount;
        callback?.Invoke();
    }

    public void RemoveDiamond(int amount, Action callback = null)
    {
        UserData.diamond -= amount;
        callback?.Invoke();
    }

    public void RemoveHeart(int amount, Action callback = null)
    {
        UserData.heart -= amount;
        callback?.Invoke();
    }
}
