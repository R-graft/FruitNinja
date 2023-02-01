using UnityEngine;

namespace winterStage
{
    public class SpawnZone : MonoBehaviour
    {
        public string zoneTag;

        public Vector2 pointOne;

        public Vector2 pointTwo;

        public float spawnPecent;

        public void GetPoints()
        {
            if (PlayerPrefs.HasKey(zoneTag + "one") && PlayerPrefs.HasKey(zoneTag + "two"))
            {
                pointOne = JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(zoneTag + "one"));

                pointTwo = JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(zoneTag + "two"));
            }
            else
            {
                Debug.Log("zonePoint not exist" + zoneTag);
            }
        }

    }
}
