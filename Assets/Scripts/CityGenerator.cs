using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public Material buildingMaterial;
    public int countX = 6;         // broj zgrada levo-desno
    public int countZ = 6;         // broj zgrada napred-nazad
    public float spacing = 30f;    // širina zgrade (20) + ulica (10)

    void Start()
    {
        float startX = -(countX - 1) * spacing / 2f;
        float startZ = -(countZ - 1) * spacing / 2f;

        for (int z = 0; z < countZ; z++)
        {
            for (int x = 0; x < countX; x++)
            {
                float posX = startX + x * spacing;
                float posZ = startZ + z * spacing;

                GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
                building.name = "Building " + x + "_" + z;
                building.transform.SetParent(transform);
                building.transform.position = new Vector3(posX, 10f, posZ);
                building.transform.localScale = new Vector3(20f, 20f, 20f);
                building.GetComponent<Renderer>().material = buildingMaterial;
            }
        }
    }
}