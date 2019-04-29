using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinController : MonoBehaviour
{
    [SerializeField]
    Zeppelin zeppelin;
    [SerializeField]
    PropellerThrust thruster;
    // Start is called before the first frame update
    void Awake()
    {
        zeppelin = GetComponent<Zeppelin>();
        thruster = GameObject.Find("Not the Balloon Part").GetComponent<PropellerThrust>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            zeppelin.airVolume -= Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            zeppelin.airVolume += Time.deltaTime;
        }
        if (Input.GetKey("w"))
        {
            thruster.thrustMagnitude += Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            thruster.thrustAngle += Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            thruster.thrustMagnitude -= Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            thruster.thrustAngle -= Time.deltaTime;
        }
        if (Input.GetKeyUp("r"))
        {
            zeppelin.LevelOut();
        }
        if (Input.GetKeyUp("t"))
        {
            thruster.stopMoving = !thruster.stopMoving;
        }
    }
}
