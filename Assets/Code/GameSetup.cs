using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        string mode = PlayerPrefs.GetString("SelectedMode", "SinglePlayer");
        Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);

        if (mode == "MultiPlayer")
        {
            Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);
        }
    }
}
