using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerThrust : MonoBehaviour
{
    Rigidbody rb;
    [Range(-30, 30)]
    public float thrustAngle;

    [Range(-2, 2)]
    public float thrustMagnitude;

    public Vector3 thrustForce;

    float thrusterPosition;

    public bool stopMoving;
    bool movementStopping;
    public float angleDiff;

    void Awake()
    {
        thrusterPosition = -transform.localScale.z / 2f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angleDiff = Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up);
        if (!stopMoving)
        {
            StopCoroutine("StopMoving");
            movementStopping = false;

            thrustForce = calculateThrust();
            Vector3 thrustPosition = transform.position - transform.forward * -thrusterPosition;
            rb.AddForceAtPosition(thrustForce, thrustPosition, ForceMode.Force);
            Debug.DrawLine(thrustPosition, thrustPosition + thrustForce * thrustMagnitude, Color.red);
        }
        else
        {
            if (!movementStopping)
                StartCoroutine("StopMoving");
        }


    }

    IEnumerator StopMoving()
    {
        movementStopping = true;
        while (true)
        {
            thrustAngle = 0;
            thrustMagnitude = 0;
            rb.velocity = new Vector3(0f,rb.velocity.y,0f);
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
            yield return new WaitForSeconds(0f);
        }
    }

    Vector3 calculateThrust()
    {
        return Quaternion.AngleAxis(thrustAngle, Vector3.up) * transform.forward * thrustMagnitude;
    }
}
