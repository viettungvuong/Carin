using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode{
    DRIVING,
    WALKING
}

public class Mode : MonoBehaviour
{
    public static PlayerMode mode = PlayerMode.WALKING;
}
