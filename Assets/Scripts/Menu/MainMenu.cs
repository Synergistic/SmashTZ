using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Destroy(GameObject.Find("bgm"));
        Application.LoadLevel("MainGame");
    }

    public void ShowCredits()
    {
        Application.LoadLevel("Credits");
    }
}
