using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScritpt : MonoBehaviour
{
    [SerializeField, Header("揺れる時間")] public float shakeTime = 0.5f;
    [SerializeField, Header("揺れの大きさ")] public Vector3 shakeRange = new Vector3(0.2f, 0.2f, 0f);

    private float _shakeTime;
    private float _timer;

    private Vector3 _pos;
    private bool _ShakeEnd;

    [SerializeField] GameObject Player;
    
    
    void Start()
    {
        _shakeTime = -1f;
        _timer = 0f;
        _pos = transform.position;
        _ShakeEnd = false;
    }

    void Update()
    {
        if (Player.transform.position.x > 0)
        {
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        }
        

        if(_timer <= _shakeTime)
        {
            _ShakeEnd = true;
            _timer += Time.deltaTime;
            transform.position = _pos + MulVector3(shakeRange, Random.insideUnitSphere);
        }
        else
        {
            if (_ShakeEnd)
            {
                transform.position = _pos;
                _ShakeEnd = false;
            }
            _pos = transform.position;
        }
    }

    public void Shake()
    {
        _timer = 0f;
        _shakeTime = shakeTime;
    }

    private Vector3 MulVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
