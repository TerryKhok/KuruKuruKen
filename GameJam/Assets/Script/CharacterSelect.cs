using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public Material lit;

    public List<Character> characters = new List<Character>();

    public GameObject charCellPrefab;

    //武器選択マスを生成する
    void Start() {
        foreach(Character character in characters){
            SpawnCharacterCell(character);
        }
    }

    private void SpawnCharacterCell(Character character){
        GameObject charCell = Instantiate(charCellPrefab, transform);

        charCell.name = character.characterName;

        Image artwork = charCell.transform.Find("Artwork").GetComponent<Image>();
        TextMeshProUGUI name=charCell.transform.Find("NameRect").GetComponentInChildren<TextMeshProUGUI>();

        artwork.sprite = character.characterSprite;
        artwork.material = lit;
        name.text = character.characterName;

        Vector2 pixelSize = new Vector2(artwork.sprite.texture.width, artwork.sprite.texture.height);
        Vector2 pixelPivot = artwork.sprite.pivot;
        Vector2 uiPivot = new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y/pixelSize.y);

        artwork.GetComponent<RectTransform>().pivot = uiPivot;
        artwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;
    }
}
