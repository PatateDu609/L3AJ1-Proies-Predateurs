using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Rotation autour de l'axe x
    private float yaw = 0f;
    // Rotation autour de l'axe y
    private float pitch = 0f;

    // Vitesse linéaire de la caméra
    public float speed = 2f;
    // Vitesse angulaire horizontale de la souris 
    public float horizontalMouseSpeed = 1.5f;
    // Vitesse angulaire verticale de la souris 
    public float verticalMouseSpeed = 1.5f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0) // Quand on appuie sur la flèche du haut, alors on se déplace vers l'avant (sur l'axe z positif)
            transform.Translate(transform.TransformDirection(Vector3.forward) * speed);
        if (Input.GetAxis("Vertical") < 0) // Quand on appuie sur la flèche du bas, alors on se déplace vers l'arrière (sur l'axe z négatif)
            transform.Translate(transform.TransformDirection(Vector3.back) * speed);
        if (Input.GetAxis("Horizontal") > 0) // Quand on appuie sur la flèche de droite, alors on se déplace vers la droite (sur l'axe x positif)
            transform.Translate(transform.TransformDirection(Vector3.right) * speed);
        if (Input.GetAxis("Horizontal") < 0) // Quand on appuie sur la flèche de gauche, alors on se déplace vers la gauche (sur l'axe x négatif)
            transform.Translate(transform.TransformDirection(Vector3.left) * speed);




        // On calcule l'angle de rotation en fonction de l'angle de déplacement de la souris et de la vitesse angulaire, puis on donne tout ça à la transformation de l'objet courant
        yaw += horizontalMouseSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalMouseSpeed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
