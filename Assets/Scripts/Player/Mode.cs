using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeMode{
    DRIVING,
    WALKING
}

public class Mode : MonoBehaviour
{
    public static TypeMode mode = TypeMode.WALKING;
}
