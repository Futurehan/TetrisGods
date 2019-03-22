using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour

{
    public AudioSource clickMe;
    public GameObject QuitPanel;
    private Animator _canvasAnimator;

    private void Awake()
    {
        clickMe = FindObjectOfType<MixMusic>().onClickSoundSourcec;
        _canvasAnimator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            QuitPanel?.SetActive(!QuitPanel.activeSelf);
    }


    public void PlayGame()
    {
        clickMe.Play();
        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        _canvasAnimator.SetTrigger("Game_starts");
        
        yield return null;
        
        float time = _canvasAnimator.GetCurrentAnimatorStateInfo(0).length;
        
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
