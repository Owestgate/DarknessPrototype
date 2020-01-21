using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject mirror;

    private void Start()
    {
        mirror.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mirror.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            mirror.SetActive(false);
        }
    }
}
