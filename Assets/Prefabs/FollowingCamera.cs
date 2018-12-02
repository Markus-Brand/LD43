using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public GameObject Followed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Followed.transform.position - transform.position;
        transform.Translate(direction * 2 * Time.deltaTime);
    }
}
