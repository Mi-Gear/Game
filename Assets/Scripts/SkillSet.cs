using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillSet : MonoBehaviour
{
    [SerializeField] private TextAsset skillSetFile;
    [SerializeField] private GameObject prefab;


    void Start()
    {
        LoadSkillSet();
    }
    void LoadSkillSet()
    {
        Vector2 startPos = new Vector2(-166, 120);
        Vector2 delta = new Vector2(66, 56);
        string[] table = skillSetFile.text.Split("\r\n");
        for (int i = 0; i < table.Length - 1; i++)
        {
            int j = Mathf.FloorToInt(i/6);
            string[] row = table[i].Split(";");
            GameObject data = Instantiate(prefab, transform, false);
            data.GetComponent<RectTransform>().anchoredPosition = startPos+new Vector2(delta.x*(i%6),-delta.y*j);
            string path = "PNG/" + row[1];
            data.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            Debug.Log("PNG/" + row[1]);
            data.GetComponentInChildren<TextMeshProUGUI>().text = row[0];
        }
    }
}
