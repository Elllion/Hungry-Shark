using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Trail : MonoBehaviour
{
    [SerializeField] GameObject _targetObject;
    [SerializeField] float _maxDistance;

    private Vector2 _targetPosition;
    // Start is called before the first frame update
    void Start()
    {
     enabled = _targetObject != null;
    }

    // Update is called once per frame
    void Update()
    {
        //Trail behind the targeted object, moving closer if the object is too far
        _targetPosition = (transform.position - _targetObject.transform.position).normalized;
        _targetPosition *= Mathf.Min(Vector2.Distance(_targetObject.transform.position, transform.position), _maxDistance);
        _targetPosition += (Vector2)_targetObject.transform.position;

        transform.position =_targetPosition;

    }
}
