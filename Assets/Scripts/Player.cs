using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedForward = 10f;
    [SerializeField] private float _speedRotate = 400f;
    [SerializeField] private HealthComponents _healthComponents;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _deadAnimDuration = 0.3f;
    [SerializeField] private AudioClip _explosionSound;
    private bool _controlOnlyKey = true;
    private Vector3 _lastMousePosition;
    private Rect _CanvasRect;

    private GameObject explositionGO;

    private void Start()
    {
        _CanvasRect = transform.parent.GetComponent<RectTransform>().rect;
    }

    
    
    private bool PlayAnimation()
    {
        if (explositionGO != null) return false;
        return true;
    }

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
        if (_healthComponents.IsDead())
        {
            PlayerDead();
            return;
        }

        transform.localPosition = AutoTeleport.Teleport(transform.localPosition, _CanvasRect);
        if (PlayAnimation()) InputProcessing();
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

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag.Equals("Asteroid"))
        {
            _healthComponents.DecHealth();
            explositionGO = Instantiate(_explosionPrefab, transform);
            explositionGO.transform.localPosition = new Vector3(0, 0, -2);
            GetComponent<Collider2D>().enabled = false;
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = _explosionSound;
            audioSource.Play();
            Invoke("EndExplositionAnim", _deadAnimDuration);
        }
    }

    private void EndExplositionAnim()
    {
        Destroy(explositionGO);
        transform.localPosition = new Vector3(0, 0, -1);
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    private void PlayerDead()
    {
        if (PlayAnimation()) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}