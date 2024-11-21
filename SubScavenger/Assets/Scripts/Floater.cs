using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Collections;
using System.Collections.Generic;


public class Floater : MonoBehaviour
{
    public Rigidbody floatTarget;
    public float depthBefSub;
    public float displacementAmount;

    public int numberOfFloaters;

    public float waterDrag;

    public float waterAngularDrag;

    public WaterSurface waterSurface;

    WaterSearchParameters Search;

    WaterSearchResult SearchResult;

    private void FixedUpdate()
    {
        floatTarget.AddForceAtPosition(Physics.gravity / numberOfFloaters, transform.position, ForceMode.Acceleration);

        Search.startPositionWS = transform.position;

        waterSurface.ProjectPointOnWaterSurface(Search, out SearchResult);

        if (transform.position.y < SearchResult.projectedPositionWS.y)
        {
            float displacementMultiplier = Mathf.Clamp01((SearchResult.projectedPositionWS.y - transform.position.y) / depthBefSub) * displacementAmount;
            floatTarget.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            floatTarget.AddForce(displacementMultiplier * -floatTarget.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            floatTarget.AddTorque(displacementMultiplier * -floatTarget.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}






