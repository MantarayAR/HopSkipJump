using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidHome : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("HomeScene");
    }
}
