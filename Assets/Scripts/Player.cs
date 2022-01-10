using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedForward = 10f;
    [SerializeField] private float _speedRotate = 400f;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private AudioClip _bonusUpSound;

    private bool _controlOnlyKey = true;
    private Vector3 _lastMousePosition;
    private Rect _canvasRect;
    private GameObject _shield;

    private void Start()
    {
        _canvasRect = transform.parent.GetComponent<RectTransform>().rect;
        ActivateShield();
    }


    private bool AnimationNotPlayNow()
    {
        return !GetComponent<ExplositionComponent>().NowPlayExplosition();
    }

    //handling player input 
    private void InputProcessing()
    {
        if (Input.GetButton("MoveForward")) MovePlayer("MoveForward");
        if (Input.GetButton("MoveRight")) MovePlayer("MoveRight");
        if (Input.GetMouseButtonDown(0)) GetComponent<BaseWeaponComponent>().FireStart();
        if (!_lastMousePosition.Equals(Input.mousePosition))
        {
            _controlOnlyKey = false;
            _lastMousePosition = Input.mousePosition;
        }

        if (!_controlOnlyKey) RotatePlayerOnMouse();
    }

    private void Update()
    {
        if (GetComponent<HealthComponents>().IsDead())
        {
            PlayerDead();
            return;
        }

        transform.localPosition = Utils.WrapScreen(transform.localPosition, _canvasRect);
        if (AnimationNotPlayNow()) InputProcessing();
    }

    private void RotatePlayerOnMouse()
    {
        if (PauseMenu.GameIsPause) return;
        var mousePosition = Input.mousePosition;
        mousePosition =
            Camera.main.ScreenToWorldPoint(mousePosition); //mouse position from screen coordinates to world coordinates

        var angle = Vector2.Angle(Vector2.up,
            mousePosition -
            transform.position); //angle between mouse and player

        transform.eulerAngles =
            new Vector3(0f, 0f, transform.position.x < mousePosition.x ? -angle : angle); //rotate player
    }

    private void MovePlayer(string action)
    {
        Vector3 dir;
        switch (action)
        {
            case "MoveForward":
                dir = transform.up * Input.GetAxis(action);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + dir,
                    Time.deltaTime * _speedForward);
                break;
            default:
                dir = transform.eulerAngles;
                _controlOnlyKey = true;
                dir.z += Input.GetAxis(action) > 0 ? -Time.deltaTime * _speedRotate : Time.deltaTime * _speedRotate;
                transform.eulerAngles = dir;
                break;
        }
    }

    private void PlayerDead()
    {
        if (AnimationNotPlayNow()) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (_shield == null && obj.gameObject.tag.Equals("Asteroid"))
        {
            GetComponent<HealthComponents>().DecHealth();
            GetComponent<ExplositionComponent>().StartPlayExplosition();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Contains("Bonus"))
        {
            Utils.PlayAudio(GetComponent<AudioSource>(), _bonusUpSound);
            switch (collider.tag)
            {
                case "HealthBonus":
                    GetComponent<HealthComponents>().IncHealth();
                    break;
                case "ShieldBonus":
                    ActivateShield();
                    break;
            }
        }
    }

    public void ActivateShield()
    {
        _shield = Instantiate(_shieldPrefab, transform);
    }
}