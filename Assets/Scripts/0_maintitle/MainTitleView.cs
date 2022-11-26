using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitleView : MonoBehaviour
{
    public GameObject Option;
    
    public void StartGame()
    {
        SceneManager.LoadScene("1_MainTitle");
    }
    
    public void OptionOn()
    {
        Option.SetActive(true);
    }
    
    public void OptionOff()
    {
        Option.SetActive(false);
    }
}
