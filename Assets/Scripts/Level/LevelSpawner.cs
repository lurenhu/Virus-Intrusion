using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSpawner : SingletonMonobehaviour<LevelSpawner>
{
    private List<LevelTemplateSO> levelTemplateList;
    public Dictionary<string, Level> levelDictionary = new Dictionary<string, Level>();
    public Dictionary<string, LevelTemplateSO> levelTemplateDictionary = new Dictionary<string, LevelTemplateSO>();

    protected override void Awake()
    {
        base.Awake();
        levelTemplateList = GameResources.Instance.LevelList;
    }

    public void GenerateLevel(string levelName)
    {
        LoadLevelTemplateDictionary();
        LoadLevelDictionary();

        foreach (KeyValuePair<string,Level> keyValue in levelDictionary)
        {
            if (keyValue.Key == levelName)
            {
                InstantiateLevelGameObject(keyValue.Value);
            }
        }
    }

    private void InstantiateLevelGameObject(Level level)
    {
        Grid grid = level.prefab.GetComponent<Grid>();

        Vector3Int CenterPointOffset = (Vector3Int)(level.lowerBound + level.upperBound)/2;
        Vector3 levelPosition = -(level.prefab.transform.position + grid.CellToWorld(CenterPointOffset));

        GameObject levelGameObject = Instantiate(level.prefab, levelPosition, Quaternion.identity, transform);

        InstantiateLevel instantiateLevel = levelGameObject.GetComponent<InstantiateLevel>();
        instantiateLevel.Initialize(levelGameObject);
        instantiateLevel.level = level;
        level.instantiateLevel = instantiateLevel;
    }

    /// <summary>
    /// 将LevelTemplateSO录入字典
    /// </summary>
    private void LoadLevelTemplateDictionary()
    {
        levelTemplateDictionary.Clear();
        foreach (LevelTemplateSO levelTemplate in levelTemplateList)
        {
            if (!levelTemplateDictionary.ContainsKey(levelTemplate.uid))
            {
                levelTemplateDictionary.Add(levelTemplate.uid,levelTemplate);
            }
            else
            {
                Debug.Log("Duplicate Level Template Key In" + levelTemplateDictionary);
            }
        }
    }

    /// <summary>
    /// 根据levelTemplate创建Level
    /// </summary>
    private Level CreateLevelFormLevelTemplate(LevelTemplateSO levelTemplate)
    {
        Level levelToCreate = new Level();

        levelToCreate.templateId = levelTemplate.uid;
        levelToCreate.prefab = levelTemplate.prefab;
        levelToCreate.lowerBound = levelTemplate.lowerBound;
        levelToCreate.upperBound = levelTemplate.upperBound;
        levelToCreate.setupPositionArray = levelTemplate.setupPositionArray;
        levelToCreate.spawnPositionArray = levelTemplate.spawnPositionArray;
        levelToCreate.targetPositionArray = levelTemplate.targetPositionArray;

        return levelToCreate;
    }

    /// <summary>
    /// 将levelDictionary填充
    /// </summary>
    private void LoadLevelDictionary()
    {
        foreach (KeyValuePair<string,LevelTemplateSO> valuePair in levelTemplateDictionary)
        {
            Level level = valuePair.Value.prefab.GetComponent<InstantiateLevel>().level;
            string LevelName = valuePair.Value.prefab.name;
            
            if (!levelDictionary.ContainsKey(LevelName))
            {
                Level levelToCreate = CreateLevelFormLevelTemplate(valuePair.Value);

                levelToCreate.name = LevelName;
                levelDictionary.Add(levelToCreate.name,levelToCreate);
            }
        }
    }
}
