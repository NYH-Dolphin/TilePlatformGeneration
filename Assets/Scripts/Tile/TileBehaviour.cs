using System.Collections;
using Character;
using UnityEngine;


public class TileBehaviour : MonoBehaviour
{
    public float fDuration = 1f;
    private Vector3 _originalScale;


    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().PlayerInfo
                .CollectTile(int.Parse(gameObject.name.Replace("(Clone)", "")));
            StartCoroutine(CollectTile());
        }
    }


    /// <summary>
    /// Reduce the tile size
    /// </summary>
    /// <returns></returns>
    IEnumerator CollectTile()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fDuration)
        {
            float t = elapsedTime / fDuration;
            transform.localScale = Vector3.Lerp(_originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        Destroy(this);
    }
}