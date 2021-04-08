using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public float speed = 2f;
    public float mouseSpeed = 1.5f;

    public new GameObject camera;

    public void Update()
    {
        if (Menu.PauseMenu.Paused)
            return;
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftControl))
            transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        float yaw = transform.eulerAngles.y + mouseSpeed * Input.GetAxis("Mouse X");
        float pitch = camera.transform.eulerAngles.x - mouseSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0, yaw, 0);
        camera.transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
