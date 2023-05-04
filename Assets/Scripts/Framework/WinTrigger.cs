using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WinTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem winParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.GameplayMode?.OnWin();
        }
    }

}
