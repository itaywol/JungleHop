using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

struct PlayerStatistics
{
    float topHeight;
}

public class Player : MonoBehaviour
{
    private const float FORCE_MULTIPLIER = 100f;

    [Range(2f, 25f)]
    public float jumpForce = 5f;

    private Rigidbody2D _rb;
    private bool _haveInitiallyJumped = false;
    private float _heightReached = 0f;

    public UnityEvent playerDied;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _heightReached = Mathf.Max(transform.position.y, _heightReached);

        if (transform.position.y < _heightReached - (Camera.main.ScreenToWorldPoint( new Vector3( 0, Screen.height / 2 ) ).y))
        {
            playerDied.Invoke();
        }
    }

    private void OnMouseDown()
    {
        if (!_haveInitiallyJumped)
        {
            _rb.AddForce(Vector3.up * FORCE_MULTIPLIER * jumpForce);
            _haveInitiallyJumped = true;
        }

    }
}
