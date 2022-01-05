using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public static string playerName;
    public static int highscore;
    public Text ScoreText;
    public GameObject GameOverText;
    public Text scoreBoard;
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        scoreBoard.text = "Name1 :" + playerName + " Score: " + highscore;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        //Debug.Log(m_Points);
        Debug.Log(highscore);
        if (highscore<m_Points)
        {
            playerName = MenuManager.instance.name1;
            highscore = m_Points;
            scoreBoard.text = "Name1 :" + playerName + " Score: " + highscore;
            SaveData();
        }
        GameOverText.SetActive(true);
    }
    public void SaveData()
    {
        Data data = new Data();
        data.bestPlayerName = playerName;
        data.highScore = highscore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile(0).json", json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile(0).json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);
            playerName = data.bestPlayerName;
            highscore = data.highScore;
            Debug.Log(playerName);
            Debug.Log(highscore);
        }
    }
}
public class Data
{
    public string bestPlayerName;
    public int highScore;
}
