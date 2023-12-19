using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// 获取对应关卡的中心世界坐标
    /// </summary>
    public Vector3 GetLevelCenterPositionInWorld(string levelName)
    {
        Vector3 Position = new Vector3();
        foreach (KeyValuePair<string,Level> keyValue in levelDictionary)
        {
            if (keyValue.Key == levelName)
            {
                Grid grid =  keyValue.Value.prefab.transform.GetComponent<Grid>();
                Vector3Int CenterGridPosition = (Vector3Int)(keyValue.Value.lowerBound + keyValue.Value.upperBound)/2;
                Vector3 offset = new Vector3(grid.cellSize.x/2, grid.cellSize.y/2, 0);

                Position = grid.CellToWorld(CenterGridPosition) + offset;
            }
        }
        Position.z = -10;
        Debug.Log("Level Center Position In World: " + Position);
        return Position;
    }

    /// <summary>
    /// 生成对应的关卡对象
    /// </summary>
    public Level GenerateLevel(string levelName)
    {
        Level level = new Level();

        LoadLevelTemplateDictionary();
        LoadLevelDictionary();

        foreach (KeyValuePair<string,Level> keyValue in levelDictionary)
        {
            if (keyValue.Key == levelName)
            {
                level = InstantiateLevelGameObject(keyValue.Value);
            }
        }

        return level;
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
    /// 将levelDictionary填充
    /// </summary>
    private void LoadLevelDictionary()
    {
        foreach (KeyValuePair<string,LevelTemplateSO> valuePair in levelTemplateDictionary)
        {
            string LevelName = valuePair.Value.prefab.name;
            
            if (!levelDictionary.ContainsKey(LevelName))
            {
                Level levelToCreate = CreateLevelFormLevelTemplate(valuePair.Value);
                levelToCreate.name = LevelName;

                levelDictionary.Add(levelToCreate.name,levelToCreate);
            }
        }
    }

    /// <summary>
    /// 根据levelTemplate创建Level
    /// </summary>
    private Level CreateLevelFormLevelTemplate(LevelTemplateSO levelTemplate)
    {
        Level levelToCreate = new Level
        {
            templateId = levelTemplate.uid,
            prefab = levelTemplate.prefab,
            lowerBound = levelTemplate.lowerBound,
            upperBound = levelTemplate.upperBound,
            spawnPositionArray = levelTemplate.spawnPositionArray,
            targetPositionArray = levelTemplate.targetPositionArray,
            levelEnemyGenerateRule = levelTemplate.levelEnemyGenerateRule
        };
        return levelToCreate;
    }

    /// <summary>
    /// 将关卡level实例化
    /// </summary>
    private Level InstantiateLevelGameObject(Level level)
    {
        Grid grid = level.prefab.GetComponent<Grid>();

        GameObject levelGameObject = Instantiate(level.prefab, transform);

        InstantiateLevel instantiateLevel = levelGameObject.GetComponent<InstantiateLevel>();
        instantiateLevel.level = level;
        instantiateLevel.Initialize(levelGameObject);
        level.instantiateLevel = instantiateLevel;

        StaticEventHandler.CallLevelSpawnEvent(level);

        return level;
    }
    
}
