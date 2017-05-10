using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class RateButton : MonoBehaviour
{
    public Button playButton;
    public AudioSource click;

    IEnumerator LoadHome()
    {
        yield return new WaitForSeconds(1);
        #if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.mantarayar.jumpit");
        #elif UNITY_IPHONE
        // XXX
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1236236942");
        #else
        Application.OpenURL("http://mantarayar.com/");
        #endif
    }

    public void OnClick()
    {
        click.Play();
        StartCoroutine(LoadHome());
    }

    public void Start()
    {
        Button button = playButton.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
}
