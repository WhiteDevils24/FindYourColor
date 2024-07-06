using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    private CameraFollow cameraFollow;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        string mode = PlayerPrefs.GetString("SelectedMode", "SinglePlayer");
        GameObject player1 = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        cameraFollow.AddTarget(player1.transform);

        if (mode == "MultiPlayer")
        {
            GameObject player2 = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);
            cameraFollow.AddTarget(player2.transform);
        }
    }
}
