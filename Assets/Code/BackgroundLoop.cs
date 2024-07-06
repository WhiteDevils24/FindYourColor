using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] backgrounds; // Array of background tiles
    public float backgroundWidth; // Width of each background tile
    public Transform cameraTransform; // Reference to the camera transform

    private float viewZone = 10f; // Distance at which a background tile should be repositioned
    private int leftIndex; // Index of the leftmost background tile
    private int rightIndex; // Index of the rightmost background tile

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        leftIndex = 0;
        rightIndex = backgrounds.Length - 1;
    }

    private void Update()
    {
        if (cameraTransform.position.x - viewZone > backgrounds[leftIndex].transform.position.x + backgroundWidth)
        {
            ScrollRight();
        }

        if (cameraTransform.position.x + viewZone < backgrounds[rightIndex].transform.position.x - backgroundWidth)
        {
            ScrollLeft();
        }
    }

    private void ScrollRight()
    {
        backgrounds[leftIndex].transform.position = new Vector3(
            backgrounds[rightIndex].transform.position.x + backgroundWidth,
            backgrounds[leftIndex].transform.position.y,
            backgrounds[leftIndex].transform.position.z
        );

        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == backgrounds.Length)
        {
            leftIndex = 0;
        }
    }

    private void ScrollLeft()
    {
        backgrounds[rightIndex].transform.position = new Vector3(
            backgrounds[leftIndex].transform.position.x - backgroundWidth,
            backgrounds[rightIndex].transform.position.y,
            backgrounds[rightIndex].transform.position.z
        );

        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = backgrounds.Length - 1;
        }
    }
}
