using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public void Show(bool visibility)
    {
        gameObject.SetActive(visibility);
    }
}
