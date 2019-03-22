using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    // Start is called before the first frame update
   public void GoToMain ()
   {
        StartCoroutine(ChillLoad(SceneManager.GetActiveScene().buildIndex - 1));
   }

    public void Restart ()
    {
        StartCoroutine(ChillLoad(SceneManager.GetActiveScene().buildIndex + 0));
    }

    private IEnumerator ChillLoad(int index)
    {
        GameManager.IsPaused = false;
        yield return null;
        SceneManager.LoadSceneAsync(index);

    }
}
