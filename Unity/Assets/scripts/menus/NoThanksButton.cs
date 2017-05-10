using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class NoThanksButton : MonoBehaviour
{
    public Button homeButton;
    public AudioSource click;

    IEnumerator LoadHome()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("HomeScene");
    }

    public void OnClick()
    {
        click.Play();
        StartCoroutine(LoadHome());
    }

    public void Start()
    {
        Button button = homeButton.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
}
