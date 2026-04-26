using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class DraggableReturn : MonoBehaviour
{
    [Tooltip("Return to original position when mouse is released")]
    public bool returnOnRelease = true;

    [Tooltip("Seconds it takes to lerp back")]
    public float returnDuration = 0.2f;

    Vector3 _originalPosition;
    Vector3 _offset;
    bool _dragging;
    Rigidbody2D _rb;
    bool _rbPrevKinematic;
    Collider2D _col;
    bool _colPrevIsTrigger;

    void Start()
    {
        _originalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        if (_col != null)
            _colPrevIsTrigger = _col.isTrigger;
    }

    void OnMouseDown()
    {
        // capture start position (useful if object was moved programmatically)
        _originalPosition = transform.position;

        // compute offset so the object doesn't snap the center to the cursor
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        _offset = transform.position - mouseWorld;

        // disable physics while dragging (if any)
        if (_rb != null)
        {
            _rbPrevKinematic = _rb.isKinematic;
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
        }

        // prevent physical collisions with other items while dragging
        if (_col != null)
        {
            _colPrevIsTrigger = _col.isTrigger;
            _col.isTrigger = true;
        }

        _dragging = true;
    }

    void OnMouseDrag()
    {
        if (!_dragging) return;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        transform.position = mouseWorld + _offset;
    }

    void OnMouseUp()
    {
        if (!_dragging) return;
        _dragging = false;

        // restore physics setting
        if (_rb != null)
            _rb.isKinematic = _rbPrevKinematic;

        // restore collider mode so it collides again after release
        if (_col != null)
            _col.isTrigger = _colPrevIsTrigger;

        // If you have logic that accepts the item (e.g. drop on customer),
        // check here and skip return when accepted.
        if (returnOnRelease)
            StartCoroutine(ReturnToOriginal());
    }

    IEnumerator ReturnToOriginal()
    {
        Vector3 start = transform.position;
        float t = 0f;
        if (returnDuration <= 0f)
        {
            transform.position = _originalPosition;
            yield break;
        }

        while (t < 1f)
        {
            t += Time.deltaTime / returnDuration;
            transform.position = Vector3.Lerp(start, _originalPosition, t);
            yield return null;
        }

        transform.position = _originalPosition;
    }
}
