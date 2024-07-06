using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets; // The targets the camera will follow
    public Vector3 offset; // Offset between the camera and the target
    public float smoothing = 5f; // Smoothing factor for the camera movement

    public Transform background; // The background transform
    public float backgroundPadding = 1f; // Padding to ensure the background stays within view

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Remove null targets
        targets.RemoveAll(target => target == null);

        if (targets.Count == 0)
            return;

        Move();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothing * Time.deltaTime);
    }

    Vector3 ClampToBackground(Vector3 newPosition)
    {
        if (background == null)
            return newPosition;

        // Get the camera's view size in world units
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        // Get the background's bounds
        Bounds backgroundBounds = background.GetComponent<SpriteRenderer>().bounds;

        // Calculate the limits
        float minX = backgroundBounds.min.x + horzExtent - backgroundPadding;
        float maxX = backgroundBounds.max.x - horzExtent + backgroundPadding;
        float minY = backgroundBounds.min.y + vertExtent - backgroundPadding;
        float maxY = backgroundBounds.max.y - vertExtent + backgroundPadding;

        // Clamp the newPosition
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        return newPosition;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    public void AddTarget(Transform target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    public void RemoveTarget(Transform target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
        }
    }
}

