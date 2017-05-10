using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    public AudioSource click;

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1);
		SceneManager.LoadScene("GameScene");
    }


    public void OnClick()
    {
        click.Play();
        StartCoroutine(LoadGame());
    }

    public void Start()
    {
        Button button = playButton.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
}
