using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Transform knifePos;
    [SerializeField]
    protected float movementSpeed;
    protected bool facingRight;
    public Animator myAnimator { get; private set; }
    public bool Attack { get; set; }
    [SerializeField]
    private GameObject knifePrefab;
    [SerializeField]
    protected int health;
    public abstract bool IsDead { get; }
    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("CharStart");
        facingRight = true;

        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);

    }
    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }
    public abstract IEnumerator TakeDamage();
}
