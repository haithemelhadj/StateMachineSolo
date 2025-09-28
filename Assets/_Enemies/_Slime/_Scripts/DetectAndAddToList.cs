using UnityEngine;

public class DetectAndAddToList : MonoBehaviour
{
    public GroundNpcContext groundNpcContext;
    private void Start()
    {
        if (groundNpcContext == null)
        {
            groundNpcContext = GetComponentInParent<GroundNpcContext>();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    foreach (string tag in groundNpcContext.detectableTags)
    //    {
    //        if (collision.gameObject.tag == tag)
    //        {
    //            groundNpcContext.Detected.Add(collision.transform);
    //        }
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    foreach (string tag in groundNpcContext.detectableTags)
    //    {
    //        if (collision.gameObject.tag == tag)
    //        {
    //            groundNpcContext.Detected.Remove(collision.transform);

    //        }
    //    }
    //}


    #region Trigger Handling
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in groundNpcContext.detectableTags)
        {
            if (collision.CompareTag(tag))
            {
                if (!groundNpcContext.Detected.Contains(collision.transform))
                    groundNpcContext.Detected.Add(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (string tag in groundNpcContext.detectableTags)
        {
            if (collision.CompareTag(tag))
            {
                groundNpcContext.Detected.Remove(collision.transform);
            }
        }
    }
    #endregion

}
