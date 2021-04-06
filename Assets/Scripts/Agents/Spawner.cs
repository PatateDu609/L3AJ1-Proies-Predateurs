using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agents
{
    class Spawner : MonoBehaviour
    {
        public List<AgentType> agentTypes;
        private float[,] heightMap;
        private System.Random rnd = new System.Random();

        public void Start()
        {
            heightMap = MapGenerator.instance.HeightMap;
            float[] spawnable = MapGenerator.instance.GetSpawnable;
            GameObject terrain = GameObject.Find("Terrain");

            //Debug.LogWarning("Terrain scale : " + terrain.transform.localScale);
            //Debug.LogWarning("Spawnable : (" + spawnable[0] + ", " + spawnable[1] + ")");

            foreach (AgentType type in agentTypes)
            {
                for (int i = 0; i < type.amount; i++)
                {
                    Vector3 position;
                    Collider[] colliders;
                    int x, z;
                    float fx, y, fz;

                    do
                    {
                        do
                        {
                            x = rnd.Next(heightMap.GetLength(0));
                            z = rnd.Next(heightMap.GetLength(1));
                            Debug.Log("printing for " + Enum.GetName(typeof(Species), type.type) + "... x = " + x + ", z = " + z + ", heightMap = " + heightMap[x, z] + ", spawnable : start = " + spawnable[0] + ", end = " + spawnable[1]);
                        } while (spawnable[0] > heightMap[x, z] || heightMap[x, z] >= spawnable[1]);

                        y = MapGenerator.instance.meshHeightCurve.Evaluate(heightMap[x, z]) * MapGenerator.instance.meshHeightMultiplier;
                        y = Mathf.Max(y, 0.5f);

                        position = new Vector3(x, y, z);
                        position.Normalize();


                        colliders = Physics.OverlapSphere(position, Mathf.Max(new float[] { x, y, z }), LayerMask.NameToLayer("Tree"));
                    } while (colliders.Length != 0);

                    //Debug.Log("Spawning a " + Enum.GetName(typeof(Species), type.type) + " at " + position + " with unscaled (" + fx + ", " + y + ", " + fz + ")");
                    Spawn(type.type, position);
                }
            }
        }

        private void Spawn(Species type, Vector3 position)
        {
            GameObject prefab = agentTypes.Find(o => o.type == type).prefab;
            GameObject instance = Instantiate(prefab, gameObject.transform);
            instance.name = Enum.GetName(typeof(Species), type);

            instance.transform.position = position;
            /*Rigidbody rigidBody = instance.GetComponent<Rigidbody>();
            if (rigidBody == null)
                return;

            RigidbodyConstraints save = rigidBody.constraints;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;

            RaycastHit hit;
            while (!Physics.Raycast(position, Vector3.down, out hit))
            {
                instance.transform.position += 20 * Vector3.up;
            }
            rigidBody.constraints = save; */
        }

        [Serializable]
        public struct AgentType
        {
            public Species type;
            public GameObject prefab;
            public int amount;
        }
    }
}
