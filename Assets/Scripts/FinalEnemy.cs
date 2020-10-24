using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalEnemy : MonoBehaviour
{
    #region FinalEnemy

    private GameObject _finalEnemy;

    private void Start()
    {
        _finalEnemy = GameObject.FindWithTag("Finish");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = GameObject.Find("PlaneDelaySpawner");
        GameObject rock = GameObject.Find("PBR_Rock_Boom");

        var other = gameObject.GetComponent<DelaySpawner>();
        other.enabled = false;
        Destroy(rock);
    }

    private void FinishGame()
    {
        if (_finalEnemy == null)
        {
            SceneManager.LoadSceneAsync("Win");
        }
    }

    #endregion
}
