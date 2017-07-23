using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffBall : MonoBehaviour {

    public int scale;
    //public int food;
    //public bool move;

    private Animator animator;
    private int state;

    // Use this for initialization
    void Start () {
        GameManager.instance.puffManager.AddPuffToList(this);
        setColour();
        scale = 1;
        //move = false;
        animator = GetComponent<Animator>();
    }

    void setColour ()
    {
        GameObject body = this.transform.GetChild(0).gameObject;
        body.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    public void Mood(int food)
    {
        if(food >= 10)
        {
            setMoodHappy();
        }
        else if(food <=5)
        {
            setMoodAngry();
        }
        else
        {
            setMoodIdle();
        }

    }

    void setMoodHappy()
    {
        animator.SetBool("IdleHappy", true);
        animator.SetBool("IdleAngry", false);
    }

    void setMoodAngry()
    {
        animator.SetBool("IdleHappy", false);
        animator.SetBool("IdleAngry", true);
    }

    void setMoodIdle()
    {
        animator.SetBool("IdleHappy", false);
        animator.SetBool("IdleAngry", false);
    }

    public void Moving(bool move)
    {
        animator.SetBool("Moving", move);
    }

    //private void Grow()
    //{
    //    scale += food;
    //}

    //void Update ()
    //{
    //    food = GameManager.instance.puffManager.food;
    //    Moving();
    //    Mood();
    //}

    

}
