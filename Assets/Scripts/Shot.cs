using UnityEngine;

public class Shot : MonoBehaviour {
    
    [SerializeField] private float timeToLive = 5f;
    
    private void Start() {
        this.Invoke(nameof(DestroyShot), timeToLive);
    }
    
    private void DestroyShot() {
        Destroy(this.gameObject);
    }
}
