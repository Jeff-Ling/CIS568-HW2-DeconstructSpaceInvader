using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    public GameObject ship;
    public float alienNum;
    public float score;

    public TextMeshProUGUI ScoreTextUI;
    public TextMeshProUGUI LiveTextUI;

    // Start is called before the first frame update
    void Start()
    {
        alienNum = CountAliens();

        ScoreTextUI.text = "0";
        LiveTextUI.text = ship.GetComponent<Ship>().lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ship.GetComponent<Ship>().lives <= 0) Lose();
        if (alienNum <= 0 && ship.GetComponent<Ship>().lives > 0) Win();
    }

    public void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void Lose()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void AlienDie(float s)
    {
        alienNum--;
        score += s;
        ScoreTextUI.text = score.ToString();
    }

    public void UpdateLivesTextUI()
    {
        LiveTextUI.text = ship.GetComponent<Ship>().lives.ToString();
    }

    int CountAliens()
    {
        int count = 0;

        // Get all game objects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Iterate through them and count those starting with "Alien"
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Alien")
            {
                count++;
            }
        }

        return count;
    }
}
