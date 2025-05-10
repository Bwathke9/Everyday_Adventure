using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float transitionDelay = 0f;
    [SerializeField] private GameObject polePrefab;
    [SerializeField] private GameObject flagPolePrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerInformation.control.isPaused == false)
            {
                PlayerInformation.control.isPaused = true;
            }


            // Destroy the pole object
            //Destroy(polePrefab);
            // set the flag pole prefab to active
            if (flagPolePrefab != null)
            {
                StartCoroutine(WaitForflag());
                flagPolePrefab.SetActive(true);
            }

            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private IEnumerator WaitForflag()
    {
        yield return new WaitForSeconds(transitionDelay);
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(transitionDelay);

        SceneManager.LoadScene(sceneToLoad);

    }
}
