using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    #region Unity Methods

    private Rigidbody _player;
    private Animator _animator;
    private AudioSource _audioStep;
    private Quaternion _rotation = Quaternion.identity;
    private bool _isReloading = false;

    private void Start()
    {
        _player = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioStep = GetComponent<AudioSource>();
    }

    void Update()
    {
        //_direction.x = Input.GetAxis("Horizontal");
        //_direction.z = Input.GetAxis("Vertical");
        //_direction.Normalize();

        //_player.AddForce(_direction * _speed * Time.deltaTime, ForceMode.Impulse);

        //Vector3 desireForward = Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.deltaTime, 0f);
        //transform.rotation = Quaternion.LookRotation(desireForward);

        SetHealthBarValue(HeaithPoint);

        PlayAnimation("Horizontal", "Walk");
        PlayAnimation("Vertical", "Walk");
        PlayAnimation("Jump", "Jump");
        PlayAnimation("Fire1", "Attack");
        PlayAnimation("Fire2", "Throw");

        if (Input.GetButtonDown("Jump")) Jump();

        if (Input.GetButtonDown("Fire2")) StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _direction.Set(horizontal, 0f, vertical);
        _direction.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        if (isWalking)
        {
            if (!_audioStep.isPlaying)
            {
                _audioStep.Play();
            }
        }
        else
        {
            _audioStep.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);
    }

    private void Reload()
    {
        _isReloading = false;
    }

    #endregion


    #region Animation

    private void PlayAnimation(string actMotion, string trigerName)
    {
        if (Input.GetButtonDown(actMotion)) _animator.SetTrigger(trigerName);
        if (Input.GetButtonUp(actMotion)) _animator.SetTrigger("Idle");
    }

    #endregion


    #region Attack

    [SerializeField] private float _powerOfAttack;

    private void Attack(float _powerOfAttack)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GolemController attack = gameObject.GetComponent<GolemController>();
            attack.Hurt(_powerOfAttack);
        }
    }

    #endregion


    #region CharacteristicsPlayer

    public float HeaithPoint;

    public void Hurt(float Damage)
    {
        HeaithPoint -= Damage;

        _animator.SetTrigger("Hit");
        _animator.SetTrigger("Idle");

        if (HeaithPoint <= 0)
        {
            SceneManager.LoadSceneAsync("Die");
        }
    }

    public void Treatment(float Helth)
    {
        HeaithPoint += Helth;

        if (HeaithPoint > 100)
        {
            HeaithPoint = 100;
        }
    }

    #endregion


    #region HP Bar

    [SerializeField] private Image _healthBarImage;

    private void SetHealthBarValue(float HealthPoint)
    {
        _healthBarImage.fillAmount = HealthPoint;
        if (_healthBarImage.fillAmount < 0.15f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (_healthBarImage.fillAmount < 0.5f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    private void SetHealthBarColor(Color healthColor)
    {
        _healthBarImage.color = healthColor;
    }

    #endregion


    #region MovingPlayer

    //[SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    private Vector3 _direction;

    private void Jump()
    {
        if (gameObject.layer == 0)
        {
            _player.AddForce(Vector3.up * _player.mass * _jumpForce, ForceMode.Impulse);

            _isReloading = true;
            Invoke("Reload", 2);
        }

    }

    void OnAnimatorMove()
    {
        _player.MovePosition(_player.position + _direction * _animator.deltaPosition.magnitude);
        _player.MoveRotation(_rotation);
    }

    #endregion


    #region Throw

    [SerializeField] private GameObject _bottle;
    [SerializeField] private Transform _startBottle;


    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.5f);

        if (!_isReloading)
        {
            GameObject bottleGameObject = Instantiate(_bottle, _startBottle.position, _startBottle.rotation);
            Rigidbody bottle = bottleGameObject.GetComponent<Rigidbody>();
            var impulse = transform.forward * bottle.mass * 15f;

            bottle.AddForce(impulse, ForceMode.Impulse);

            _isReloading = true;
            Invoke("Reload", 2);
        }
    }

    #endregion
}
