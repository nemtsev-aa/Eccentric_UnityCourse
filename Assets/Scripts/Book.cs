using UnityEngine;

public class Book : MonoBehaviour
{
    
    [SerializeField] float _rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //FindObjectOfType<CoinManager>().AddOne();
        Destroy(gameObject);
    }
}
