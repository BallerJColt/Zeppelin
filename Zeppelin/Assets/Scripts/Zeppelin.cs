using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : MonoBehaviour
{

    private Rigidbody rb;

    public float mass;
    private float chassisMass;
    [SerializeField]
    private float heliumDensity;

    [SerializeField]
    private float airDensity;

    public float airVolume;

    public float heliumVolume;
    public Vector3 buoyancyForce;
    public float dragCoefficient;
    public Vector3 airDrag;
    Vector3 stopForce;
    bool isRunning;


    // Start is called before the first frame update
    void Awake()
    {
        chassisMass = GameObject.Find("Not the Balloon Part").GetComponent<Rigidbody>().mass;
        heliumDensity = 0.1786f;
        //airDensity = 1.225f;
        rb = GetComponent<Rigidbody>();
        CalculateAirDensity();
        LevelOut();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateAirDensity();
        rb.mass = mass + airMass();

        buoyancyForce = CalculateBuoyancyForce();
        airDrag = CalculateDrag();

        rb.AddForce(buoyancyForce, ForceMode.Force);
        rb.AddForce(airDrag, ForceMode.Force);
    }

    void CalculateAirDensity()
    {
        airDensity = 1.1225f * Mathf.Exp(-9.81f*0.0289644f*transform.position.y / (8.3144598f*288.15f)); 
    }

    public void LevelOut()
    {
        airVolume = (heliumVolume * (airDensity - heliumDensity) - (mass + chassisMass)) / airDensity;
        StopAllCoroutines();
        StartCoroutine("KeepLeveling");
    }

    IEnumerator KeepLeveling()
    {
        isRunning = true;
        while (Mathf.Abs(rb.velocity.y) > 0f)
        {
            stopForce = new Vector3(0f, -rb.velocity.y, 0f).normalized * 3f;
            rb.AddForce(stopForce, ForceMode.Force);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            yield return new WaitForSeconds(0f);
        }
        isRunning = false;
    }

    float airMass()
    {
        return airVolume * airDensity;
    }

    Vector3 CalculateDrag()
    {
        return -dragCoefficient * rb.velocity;
    }

    Vector3 CalculateBuoyancyForce()
    {
        return -Physics.gravity * heliumVolume * (airDensity - heliumDensity);
    }
}
