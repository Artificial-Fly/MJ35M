using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 RotationSpeedPerFrame;
    [SerializeField]
    private bool RotateAroundSelf=true;
    [SerializeField]
    private GameObject RotateAroundThisObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RotateAroundSelf)
        {
            transform.Rotate(RotationSpeedPerFrame * Time.deltaTime);
        }
        else
        {
            if (RotateAroundThisObject != null)
            {
                transform.RotateAround(RotateAroundThisObject.transform.position, Vector3.right, RotationSpeedPerFrame.x * Time.deltaTime);
                transform.RotateAround(RotateAroundThisObject.transform.position, Vector3.up, RotationSpeedPerFrame.y * Time.deltaTime);
                transform.RotateAround(RotateAroundThisObject.transform.position, Vector3.forward, RotationSpeedPerFrame.z * Time.deltaTime);
            }
        }
    }
}
