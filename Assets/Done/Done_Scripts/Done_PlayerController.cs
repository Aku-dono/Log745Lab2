using UnityEngine;
using Assets.Scripts;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton areaButton; 

	private float nextFire;
    private Quaternion calibrationQuaternion;

    void Start()
    {
        CalibrateAccelerometer();
    }

    void Update ()
	{
		if (areaButton.CanFire() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate ()
	{
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        if ((int)ConfigGlobal.InputMode == 0)
        {
            Vector3 accelerationRaw = Input.acceleration;
            Vector3 acceleration = FixAcceleration(accelerationRaw) * (float)ConfigGlobal.AccelDiplacementFactor;
            movement.Set(acceleration.x, 0.0f, acceleration.y);
        }
        else if ((int)ConfigGlobal.InputMode == 1)
        {
            Vector2 direction = touchPad.GetDirection();
            movement.Set(direction.x, 0.0f, direction.y);
        }

        GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

    //Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }
}
