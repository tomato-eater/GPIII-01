using UnityEngine;

public class BlueSlime : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (anim == null) anim = transform.GetChild(0).GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D cont in collision.contacts)
        {
            if (cont.normal.y > 0.5f)
            {
                if (collision.collider.CompareTag("Map"))
                {
                    Debug.Log("gagaga");
                }
                else
                {

                    break;
                }
            }
        }
    }
}
