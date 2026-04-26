using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableReturn : MonoBehaviour
{
    public bool returnOnRelease = true;
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
        _originalPosition = transform.position;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        _offset = transform.position - mouseWorld;

        if (_rb != null)
        {
            _rbPrevKinematic = _rb.isKinematic;
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
        }

        if (_col != null)
        {
            _colPrevIsTrigger = _col.isTrigger;
            _col.isTrigger = true; // avoid physics collisions while dragging
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

        if (_rb != null)
            _rb.isKinematic = _rbPrevKinematic;
        if (_col != null)
            _col.isTrigger = _colPrevIsTrigger;

            // No longer attempts to find or hand off to Ingredient/MixingBowl types.
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
