using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float transitionDelay = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("Player")) 
      {
        StartCoroutine(LoadSceneAfterDelay());
      }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
