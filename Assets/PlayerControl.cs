using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Unit selectedUnit = null;
    public ParticleSystem SelectParticle;
    public ParticleSystem ClickParticle;
    public Vector3 groundOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedUnit != null)
        {
            SelectParticle.gameObject.transform.position = selectedUnit.gameObject.transform.position;
            
            if (!SelectParticle.isPlaying)
                SelectParticle.Play();
        }
        else
        {
            if (SelectParticle.isPlaying)
                SelectParticle.Stop();
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Unit unit = hit.collider.gameObject.GetComponent<Unit>();

                if (unit != null)
                {
                    selectedUnit = unit;
                }

            }
            else
            {
                selectedUnit = null;
            }

        }

        if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue ))
            {
                ClickParticle.transform.position = hit.point + groundOffset;
                ClickParticle.Play();

                if (hit.collider.CompareTag("Crystal"))
                {
                    selectedUnit.Collect(hit.point, hit.collider.gameObject);
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        selectedUnit.AddMove(hit.point);
                    }
                    else
                    {
                        selectedUnit.Move(hit.point);
                    }

                    
                }

            }
            

        }


    }
}
