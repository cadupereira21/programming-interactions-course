using UnityEngine;

public class Shot : MonoBehaviour {
    
    [SerializeField] private float timeToLive = 5f;
    
    private void Start() {
        Destroy(this.gameObject, timeToLive);
    }
}
