using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public Transform pos1;
    public Transform pos2;
    public LayerMask whatIsGround;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector2 pos = Vector2.zero;
            //do
            //{
            //    //get random position
            //    Camera.main.ScreenToWorldPoint(pos);

            //} while (!Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(pos), whatIsGround));
            pos = new Vector2(Random.Range(pos1.position.x, pos2.position.x), Random.Range(pos1.position.y, pos2.position.y));
            Instantiate(slimePrefab, pos, Quaternion.identity);
        }
    }
}
