using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public Animator scoreAnim;
    public Animator gameOverAnim;
    public Button QuitButton;


    public void GameOver()
    {
        scoreAnim.SetTrigger("GameOver");
        gameOverAnim.SetTrigger("GameOver");
        QuitButton.interactable = true;
        QuitButton.GetComponentInChildren<Text>().color = Color.black;
    }

    public void LoadMenu()
    {
        Application.LoadLevel("menu");
    }

}
