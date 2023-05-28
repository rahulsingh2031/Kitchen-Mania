using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;
        Loader.LoaderCallback();
    }
}
