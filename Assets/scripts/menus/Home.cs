using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void LoadGame()
    {
		SceneManager.LoadScene("Game");
    }
}
