 using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WinTrigger : MonoBehaviour
{
    [SerializeField] private bool isLastLevel = false;
    [SerializeField] private ParticleSystem winParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.GameplayMode?.OnWin();
        }
    }

}
