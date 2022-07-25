using UnityEngine;
public class ZoneSpawn : MonoBehaviour
{
    public GameObject SpawnPointStart; //{ get; private set; }

    public GameObject SpawnPointEnd; //{ get; private set; }

    [Range(0, 1)]
    public float startX;

    [Range(0, 1)]
    public float startY;

    [Range(0, 1)]
    public float EndX;

    [Range(0, 1)]
    public float EndY;

    public float percentValue;

    void Awake()
    {
        startX = Screen.width * startX ;

        startY = Screen.height * startY;

        EndX = Screen.width * EndX;

        EndY = Screen.height * EndY;

        SpawnPointStart.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(startX, startY));

        SpawnPointEnd.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(EndX, EndY));

        percentValue /= 100;
    }

}
