using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnTerrainGenerator : MonoBehaviour
{
    [MinAttribute(1)]
    public int count;
    
    public GameObject terrain;
    public GameObject[] prefabs;

    [Header("Size")]
    [MinAttribute(1)]
    public int minimumSize;
    [MinAttribute(1)]
    public int maximumSize;

    [Header("Space")]
    public int minimumSpace;
    public int maximumSpace;

    [Header("Scale")]
    public int minimumScale;
    public int maximumScale;

    public void Start()
    {
        System.Random random = new System.Random();

        GenerateGroups(random);
    }

    public void GenerateGroups(System.Random random)
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        Vector3 position = gameObject.transform.position;
        Vector3 size = boxCollider.size;

        Vector3 origin = new Vector3();
        Vector3 minimumOrigin = position - size / 2;
        Vector3 maximumOrigin = position + size / 2;

        for (int i = 0; i < count; i++)
        {
            RaycastHit hit;

            do {
                origin = Utilities.GetRandomVector3(random, minimumOrigin, maximumOrigin);
                origin.y = maximumOrigin.y;
            } while(!(Physics.Raycast(origin, Vector3.down, out hit) && boxCollider.ClosestPoint(hit.point) == hit.point && hit.collider.gameObject == terrain));

            GenerateGroup(random, origin);
        }
    }

    public void GenerateGroup(System.Random random, Vector3 origin) {
        GameObject g = new GameObject(gameObject.name);

        g.transform.position = origin;
        g.transform.parent = gameObject.transform;

        for (int i = 0; i < random.Next(minimumSize, maximumSize); i++)
        {
            origin = GenerateObject(random, g, origin);
        }
    }

    public Vector3 GenerateObject(System.Random random, GameObject parent, Vector3 origin) {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        float space = (float) Utilities.GetRandomDouble(random, minimumSpace, maximumSpace);

        Vector3 minimumOrigin = new Vector3(origin.x - space, origin.y, origin.z - space);
        Vector3 maximumOrigin = new Vector3(origin.x + space, origin.y, origin.z + space);

        RaycastHit hit;

        do {
            origin = Utilities.GetRandomVector3(random, minimumOrigin, maximumOrigin);
        } while(!(Physics.Raycast(origin, Vector3.down, out hit) && boxCollider.ClosestPoint(hit.point) == hit.point && hit.collider.gameObject == terrain));

        GameObject o = Instantiate(GetRandomPrefab(random));
        o.transform.parent = parent.transform;
        o.transform.position = hit.point;
        o.transform.rotation = Utilities.GetRandomQuaternion(random, false, true, false);
        o.transform.localScale = Utilities.GetRandomVector3(random, minimumScale, maximumScale);
    
        return origin;
    }

    public GameObject GetRandomPrefab(System.Random random)
    {
        return prefabs[random.Next(0, prefabs.Length)];
    }
}
