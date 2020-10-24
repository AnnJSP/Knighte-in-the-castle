using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boom : MonoBehaviour
{
    #region Boom

    private AudioSource _audioSource;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
            StartCoroutine(Die());
            _rigidbody.AddExplosionForce(5, Vector3.up, 100);
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync("Die");
    }

    #endregion
}
