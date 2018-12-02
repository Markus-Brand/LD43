using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowingCamera : MonoBehaviour
{

    public GameObject Followed;
    
    
    private Vector2 cameraPosition; //current position, without shake applied
    private float timeShaking = 99999;
    private float shakeIntensity = 0;
    private float shakeDuration = 1;
    private Vector2 shakeDirection = new Vector2(0, 0); //null if unidirectional
    
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (Vector2)Followed.transform.position - cameraPosition;
        cameraPosition += direction * direction.magnitude * Time.deltaTime;
        
        
        timeShaking += Time.deltaTime;
        float relativeTimeShaking = timeShaking / shakeDuration;
        Vector2 random = new Vector2(0, 0);
        if (relativeTimeShaking < 1) {
            float currentIntensity = (1 - relativeTimeShaking) * shakeIntensity;
            float timeFactor = (float) Math.Sqrt(Time.timeScale);
            if (shakeDirection.sqrMagnitude > 0) {
                //directional
                random = shakeDirection * (Random.value * 2 - 1) * currentIntensity * timeFactor;
            } else {
                //un-directional
                random = new Vector2((Random.value * 2 - 1) * currentIntensity * timeFactor,
                    (Random.value * 2 - 1) * currentIntensity * timeFactor);
            }
        } else {
            shakeDuration = 0.0000001f;
            shakeIntensity = 0;
        }


        Vector2 actualPosition = cameraPosition + random;
        transform.position = new Vector3(actualPosition.x, actualPosition.y, -10);
    }
    
    

    public void MakeUndirectedShake(float intensity, float duration)
    {
        MakeDirectedShake(intensity, duration, new Vector2(0, 0));
    }

    public void MakeDirectedShake(float intensity, float duration, Vector2 direction)
    {
        if (intensity > shakeIntensity) {
            shakeIntensity = intensity;
            shakeDuration = duration;
            shakeDirection = direction;
            timeShaking = 0;
        }
    }
}
