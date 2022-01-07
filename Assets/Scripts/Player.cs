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
    private bool _controlOnlyKey = true;
    private Vector3 _lastMousePosition;
    private Rect _CanvasRect;

    private void Awake()
    {
        _CanvasRect = transform.parent.GetComponent<RectTransform>().rect;
    }

    private void Update()
    {
        if (_healthComponents.IsDead())
        {
            PlayerDead();
            return;
        }

        transform.localPosition = AutoTeleport.Teleport(transform.localPosition, _CanvasRect);
        
        if (Input.GetButton("MoveForward")) MovePlayer("MoveForward");
        if (Input.GetButton("MoveRight")) MovePlayer("MoveRight");
        if (!_lastMousePosition.Equals(Input.mousePosition))
        {
            _controlOnlyKey = false;
            _lastMousePosition = Input.mousePosition;
        }

        if (!_controlOnlyKey) RotatePlayerOnMouse();
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
        if (obj.gameObject.tag.Equals("Asteroid")) _healthComponents.DecHealth();
    }

    private void PlayerDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}