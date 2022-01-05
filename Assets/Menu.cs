using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menu : MonoBehaviour
{
    public TMP_InputField nameInput;
    public string name1;



   public void OnStart()
    {
        
        name1 = nameInput.text;
        Debug.Log(name1);
        SceneManager.LoadScene(1);

    }
}
