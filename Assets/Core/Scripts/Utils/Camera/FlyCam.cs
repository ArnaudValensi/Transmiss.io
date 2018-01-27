using UnityEngine;

public class FlyCam : MonoBehaviour
{
    Vector3 movementVector;

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2

    }

    public int speed = 7;
    public int verticalSpeed = 7;

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 5F;
    public float sensitivityY = 5F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    void Update()
    {
        KeyInput();

    }

    void KeyInput()
    {
        movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movementVector += Camera.main.transform.forward * speed;

        if (Input.GetKey(KeyCode.S))
            movementVector -= Camera.main.transform.forward * speed;

        if (Input.GetKey(KeyCode.A))
            movementVector -= Camera.main.transform.right * speed;

        if (Input.GetKey(KeyCode.D))
            movementVector += Camera.main.transform.right * speed;

        if (Input.GetKey(KeyCode.Space))
            movementVector += Camera.main.transform.up * verticalSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            movementVector -= Camera.main.transform.up * verticalSpeed;

        Camera.main.transform.position = Camera.main.transform.position + movementVector * Time.deltaTime;
    }
}