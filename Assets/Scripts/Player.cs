using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedForward = 10f;
    [SerializeField] private float speedRotate = 400f;
    private bool _controlOnlyKey = true;
    private Vector3 _lastMousePosition;

    private void Update()
    {
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
                    Time.deltaTime * speedForward);
                break;
            default:
                Vector3 temp = transform.eulerAngles;
                _controlOnlyKey = true;
                temp.z += Input.GetAxis(action) > 0 ? -Time.deltaTime * speedRotate : Time.deltaTime * speedRotate;
                transform.eulerAngles = temp;
                break;
        }
    }
}