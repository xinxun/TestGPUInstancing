using UnityEngine;

public class FunnyGPUInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject _instanceGo;//初实例化对你
    [SerializeField]
    private int _instanceCount;//实例化个数
    [SerializeField]
    private bool _bRandPos = false;
 
    private MaterialPropertyBlock _mpb = null;//与buffer交换数据
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _instanceCount; i++)
        {
            Vector3 pos = new Vector3(i * 1.5f, 0, 0);
            GameObject pGO = GameObject.Instantiate<GameObject>(_instanceGo);
            pGO.transform.SetParent(gameObject.transform);
            if (_bRandPos)
            {
                pGO.transform.localPosition = Random.insideUnitSphere * 10.0f;
            }
            else
            {
                pGO.transform.localPosition = pos;
            }
            //个性化显示
            SetPropertyBlockByGameObject(pGO);

        }
    }

    //修改每个实例的PropertyBlock
    private bool SetPropertyBlockByGameObject(GameObject pGameObject)
    {
        if(pGameObject == null)
        {
            return false;
        }
        if(_mpb == null)
        {
            _mpb = new MaterialPropertyBlock();
        }

        //随机每个对象的颜色
        _mpb.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f));
        _mpb.SetFloat("_Phi", Random.Range(-40f, 40f));

        MeshRenderer meshRenderer = pGameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            return false;         
        }

        meshRenderer.SetPropertyBlock(_mpb);

        return true;
    }
}
