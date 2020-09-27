
using UnityEngine;

public class rotation : MonoBehaviour
{
    bool t = true;
    float speed = 5.0f;
    public bool isRot = true;

    private void Update()
    {
        if (isRot == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<StartScript>().setP();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                FindObjectOfType<StartScript>().unSetP();
            }
        }
        else
        {
            FindObjectOfType<StartScript>().setP();
        }
        
    }
    private void OnMouseDrag()
    {
        if (isRot = true)
        {
            float rotX = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * speed / 2 * Mathf.Deg2Rad;

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    FindObjectOfType<StartScript>().setP();
            //}
            //else if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    FindObjectOfType<StartScript>().unSetP();
            //}
            //FindObjectOfType<StartScript>().setP();

            //if (Input.GetKey(KeyCode.Space)) {
            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
        
        //}
    }
}
