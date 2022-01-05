using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public Text scoreboard;
    public string bestPlayerName;
    public int highScore;
    public TMP_InputField nameInput;
    public string name1;

    private void Start()
    {
        LoadData();
        scoreboard.text = "Best Score : " + bestPlayerName + "Score : " + highScore;
    }
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void OnStart()
    {
        
        name1 = nameInput.text;
        Debug.Log(name1);
        SceneManager.LoadScene(1);

    }
    public void OnQuit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile(0).json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);
            bestPlayerName = data.bestPlayerName;
            highScore = data.highScore;
        }
    }
}
