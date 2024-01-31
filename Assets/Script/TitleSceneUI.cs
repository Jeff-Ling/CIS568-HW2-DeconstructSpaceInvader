using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void gameStartButton()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void gameQuitButton()
    {
        Application.Quit();
    }
}
