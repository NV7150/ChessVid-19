using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvLoader : MonoBehaviour {
    [SerializeField] private TextAsset structureText;
    [SerializeField] private bool awakeOnLoad;
    private readonly List<List<string>> _loadedCsv = new List<List<string>>();

    public List<List<string>> LoadedCsv => _loadedCsv;

    private void Awake() {
        if(awakeOnLoad)
            loadCsv();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void loadCsv() {
        var textRows = structureText.text.Split('\n');
        int i = 0;
        foreach (var textRow in textRows) {
            _loadedCsv.Add(new List<string>());
            var rowTexts = textRow.Split(',');
            foreach (var text in rowTexts) {
                _loadedCsv[i].Add(text.Trim());
            }
            i++;
        }
    }
}
