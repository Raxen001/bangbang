using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    [SerializeField] Transform aimLine;
    private VariableJoystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<ShootButton>().GetComponent<VariableJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aimLine.gameObject.activeSelf)
        {
            float angle = joystick.Vertical / joystick.Horizontal;
            float zangle = Mathf.Atan(angle);
            Quaternion quaternion;
            quaternion = Quaternion.Euler(0f, 0f, zangle);
            aimLine.rotation = quaternion;
        }

    }

    public void TurnOnAimLine()
    {
        aimLine.gameObject.SetActive(true);
    }
}
