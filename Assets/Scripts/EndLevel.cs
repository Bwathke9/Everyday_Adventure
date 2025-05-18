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
    private float scoreMultiplier = 1;
    private int score;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            score = PlayerInformation.control.score;

            if (PlayerInformation.control.isPaused == false)
            {
                PlayerInformation.control.isPaused = true;
            }
            if (PlayerInformation.control.timer > 40)
            {
                scoreMultiplier = .5f;
                score += (int)(score * scoreMultiplier);
            }
            else if (PlayerInformation.control.timer> 45) 
            {
                scoreMultiplier = .4f;
                score += (int)(score * scoreMultiplier);
            }
            else if (PlayerInformation.control.timer > 50)
            {
                scoreMultiplier = .3f;
                score += (int)(score * scoreMultiplier);
            }
            else if (PlayerInformation.control.timer > 55)
            {
                scoreMultiplier = .2f;
                score += (int)(score * scoreMultiplier);
            }
            else if (PlayerInformation.control.timer > 60)
            {
                scoreMultiplier = .1f;
                score += (int)(score * scoreMultiplier);
            }
            else
            {
                scoreMultiplier = 0;
                score += (int)(score * scoreMultiplier);
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
