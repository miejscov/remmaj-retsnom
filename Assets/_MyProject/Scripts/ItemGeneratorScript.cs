using UnityEngine;

public class ItemGeneratorScript : MonoBehaviour
{
    public Transform ground;
    public GameObject _preItemCollider;
    public LayerMask playerLayerMask;
    public float lobHeigh = 5f;
    public float lobUpTime = 1f;
    public float lobDownTime = 1f;
    public GameObject foodPref;
    public GameObject diamondPref;
    public int numberOfDiamonds = 3;
    public int numberOfFood = 2;

    private GameObject collid;
    private float[] corners = new float[4]; // 0 - (ujemne x), 1 - (dodatnie x), 2 - (ujemne z), 3 - (dodatnie z)

    public void generateDiamonds()
    {generatePref(numberOfFood, foodPref);
        generatePref(numberOfDiamonds, diamondPref);
        
    }

    private void generatePref(int count, GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 wherePutDiamond = Vector3.zero;
            while (true)
            {
                float x = (int)Random.Range(corners[0], corners[1]);
                float y = (int)Random.Range(corners[2], corners[3]);
                wherePutDiamond = new Vector3(x, 0f, y);
                Collider[] hitColliders = Physics.OverlapSphere(wherePutDiamond + Vector3.up * 0.4f, .2f);
                foreach (Collider col in hitColliders)
                    print(col.name);
                if (hitColliders.Length == 0)
                    break;
                else if (hitColliders.Length == 1 && hitColliders[0].gameObject.tag == "Player")
                    break;
            }
            GameObject pref = (GameObject)Instantiate(prefab);
            collid = Instantiate(_preItemCollider, new Vector3(wherePutDiamond.x, 0.5f, wherePutDiamond.z), Quaternion.identity);

            if (prefab.gameObject.CompareTag("Food"))
                pref.GetComponent<FoodControlScript>().SetFoodCollider(collid);
            else if (prefab.gameObject.CompareTag("Diamond"))
                pref.GetComponent<DiamondControlScript1>().SetDiamondsCollider(collid);
            else 
                pref.GetComponent<ExtraLifeScript>().SetExtraLifeCollider(collid);


            pref.transform.position = new Vector3(transform.position.x, .5f, transform.position.z);
            collid.GetComponent<MeshRenderer>().enabled = false;

            iTween.MoveAdd(pref, iTween.Hash("amount", wherePutDiamond - transform.position, "time", lobUpTime + lobDownTime, "easeType", iTween.EaseType.linear));
            iTween.MoveBy(pref, iTween.Hash("y", lobHeigh, "time", lobUpTime, "easeType", iTween.EaseType.easeOutQuad));
          //iTween.MoveBy(pref, iTween.Hash("y", -lobHeigh, "time", lobDownTime, "delay", lobUpTime, "easeType", iTween.EaseType.easeInCubic));
            iTween.MoveBy(pref, iTween.Hash("y", -lobHeigh, "time", lobDownTime, "delay", lobUpTime, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "destroyCollider", "oncompletetarget", this.gameObject, "oncompleteparams", collid));
            //           Destroy(collid.gameObject, lobDownTime + lobUpTime);
        }
    }

    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Transform>();

        int mapWidth = (int)ground.localScale.x;
        int mapHeight = (int)ground.localScale.z;
        Vector3 mapPosition = ground.position;

        corners[0] = mapPosition.x - (mapHeight / 2);
        corners[1] = mapPosition.x + (mapHeight / 2);
        corners[2] = mapPosition.z - (mapWidth / 2) + .5f;
        corners[3] = mapPosition.z + (mapWidth / 2) - .5f;
    }

    public void destroyCollider(GameObject col)
    {
        if (col != null)
            Destroy(col);
    }
}

