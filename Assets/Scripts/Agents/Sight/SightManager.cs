using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     <author>Cyril Dubos</author>
///     Class <c>SightManager</c> allows agents to sight other game object in their field of view.
/// </summary>
public class SightManager : MonoBehaviour
{
    /// <summary>
    ///     <c>radius</c> models the maximal distance of the field of view.
    /// </summary>
    public float radius = 1.0f;

    /// <summary>
    ///     <c>angle</c> models horizontal and vertical angles of the field of view.
    /// </summary>
    public float angle = 90.0f;

    /// <summary>
    ///     <c>sphereCollider</c> represents the field of view.
    /// </summary>
    public SphereCollider sphereCollider;

    /// <summary>
    ///     <c>gameObjects</c> lists all game objects sightable.
    /// </summary>
    public List<GameObject> gameObjects;

    public void Start()
    {
        Initialize();
    }

    /// <summary>
    ///     This methods initializes all required components.
    /// </summary>
    public void Initialize()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = radius;

        gameObjects = new List<GameObject>();
    }

    /// <summary>
    ///     This methods adds a game object to the list of sightable objects.
    /// </summary>
    /// <param name="other">the game object</param>
    public void Add(GameObject other)
    {
        if (!gameObjects.Contains(other))
            gameObjects.Add(other);
    }

    /// <summary>
    ///     This methods removes a game object to the list of sightable objects.
    /// </summary>
    /// <param name="other">the game object</param>
    public void Remove(GameObject other)
    {
        if (gameObjects.Contains(other))
            gameObjects.Remove(other);
    }

    /// <summary>
    ///     This methods tells if a game object is sightable.
    /// </summary>
    /// <param name="other">the game object</param>
    /// <returns><c>true</c> if the game object is sightable, <c>false</c> otherwise.
    public bool IsSightable(GameObject other)
    {
        Vector3 forward = transform.forward;
        Vector3 direction = (other.transform.position - gameObject.transform.position).normalized;

        if (!(Mathf.Acos(Vector3.Dot(forward, direction)) * Mathf.Rad2Deg <= angle / 2))
            return false;

        bool isSightable = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
            isSightable |= hit.transform.gameObject == other;

        return isSightable;
    }
}
