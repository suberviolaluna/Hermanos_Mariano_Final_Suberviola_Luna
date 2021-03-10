using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Foward, Backwards }
public enum Cycle { Loop, GoBack }
public enum StartHow { Auto, OnContact }
public enum Move { Auto, OnContact}
public class PlataformaMovil : MonoBehaviour
{
    public Direction direction = Direction.Foward;
    public Cycle cycle = Cycle.Loop;
    public StartHow startHow = StartHow.Auto;
    public Move move = Move.Auto;

    //public Transform target;
    public float speed;

    public bool slow = true;

    public Transform[] Points;

    private Vector3 target;

    [HideInInspector] public int index = 0;

    bool onContact = false;

    private void Awake()
    {
        GameObject MovingPlatParent = new GameObject("MovingPlatParent");
        //Instantiate(MovingPlatParent, transform.position, transform.rotation);

        transform.parent = MovingPlatParent.transform;

        int max = transform.childCount;

        for (int i = 0; i < max; i++)
        {
            transform.GetChild(0).transform.parent = MovingPlatParent.transform;
        }
    }

    void Start()
    {
        if (startHow == StartHow.Auto)
        {
            move = Move.Auto;
        }

        if (direction == Direction.Foward)
        {
            index = 1;            
        }
        else if (direction == Direction.Backwards)
        {
            index = Points.Length;
        }

        target = Points[index].position;        
    }

    private void FixedUpdate()
    {
        float movingSpeed = speed;

        if( (index == 0 || (index == Points.Length-1 && cycle != Cycle.Loop) ) && slow)
        {
            movingSpeed *= Mathf.Clamp( Vector3.Distance(transform.position, target), 0.2f, 1f);
        }
        else if ( (index - 1 == 0 || (index + 1 == Points.Length - 1 && cycle != Cycle.Loop) ) && slow)
        {
            int ind = index - 1 == 0 ? 0 : Points.Length-1;

            movingSpeed *= Mathf.Clamp(Vector3.Distance(transform.position, Points[ind].position), 0.1f, 1f);
        }

        if ( (move==Move.Auto || (move == Move.OnContact && onContact) ) && startHow != StartHow.OnContact)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movingSpeed * Time.deltaTime);
        }

        if (transform.position == target)
        {
            if (direction == Direction.Foward)
            {
                index++;
            }
            else if (direction == Direction.Backwards)
            {
                index--;
            }

            if (index < 0)
            {
                if(cycle == Cycle.Loop)
                {
                    index = Points.Length;
                }
                else if(cycle == Cycle.GoBack)
                {
                    index = 1;
                    direction = Direction.Foward;
                }
                
            }

            if (index > Points.Length-1)
            {
                if (cycle == Cycle.Loop)
                {
                    index = 0;
                }
                else if (cycle == Cycle.GoBack)
                {
                    index = Points.Length-1;
                    direction = Direction.Backwards;
                }
            }

            target = Points[index].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.parent = transform;
            onContact = true;

            if(startHow == StartHow.OnContact)
            {
                startHow = StartHow.Auto;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = null;
            onContact = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(cycle == Cycle.GoBack)
        {
            if(direction == Direction.Foward)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i + 1 < Points.Length)
                    {
                        Gizmos.DrawLine(Points[i].position, Points[i + 1].position);
                    }
                }
            }
            else
            {
                for (int i = Points.Length-1; i > 1; i--)
                {
                    if (i - 1 >= 0)
                    {
                        Gizmos.DrawLine(Points[i].position, Points[i - 1].position);
                    }                    
                }

                Gizmos.DrawLine(Points[0].position, Points[Points.Length - 1].position);
            }

        }
        else
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (i + 1 < Points.Length)
                {
                    Gizmos.DrawLine(Points[i].position, Points[i + 1].position);
                }
            }
            
            Gizmos.DrawLine(Points[0].position, Points[Points.Length - 1].position);
        }        
    }
}
