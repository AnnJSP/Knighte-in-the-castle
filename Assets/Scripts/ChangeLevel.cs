using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeLevel : MonoBehaviour
{
    #region ChangeLevel

    [SerializeField] private AudioSource _audio;
    private Transform _player;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _audio.Play();
            StartCoroutine(LoardingLevel());
        }
    }

    private IEnumerator LoardingLevel()
    {
        yield return new WaitForSeconds(7f);

        SceneManager.LoadSceneAsync("Level_2");
    }

    #endregion
}