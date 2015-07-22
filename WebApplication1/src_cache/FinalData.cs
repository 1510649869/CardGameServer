using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.tools;
using System.IO;
public class FinalData
{
    public static FinalData data;
    /// <summary>
    /// 存放英雄卡牌数据的字典
    /// </summary>
    private Dictionary<int, HeroData> hero = new Dictionary<int, HeroData>();
    /// <summary>
    /// 存放装备卡牌数据的字典
    /// </summary>
    private Dictionary<int, ClothData> cloth = new Dictionary<int, ClothData>();//从json文件读取保存到该缓存层
    private static string pathURL;
    public static FinalData Instance(string path)
    {
        pathURL = path;
        return Instance();
    }
    public static FinalData Instance()
    {
        if (data == null)
        {
            data = new FinalData(pathURL);
        }
        return data;
    }

    public FinalData(string path)
    {
        pathURL = path;
        initHeroData();
        initClothData();
    }
    void initHeroData()
    {
        FileStream fs = new FileStream(pathURL + "/data/hero.json", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string result = sr.ReadToEnd();
        HeroData[] datas = LitJson.JsonMapper.ToObject<HeroData[]>(result);
        foreach(HeroData data in datas)
        {
            hero.Add(data.code, data);
        }
        sr.Close();
        fs.Close();
    }
    /// <summary>
    /// 装备数据模型的初始化
    /// </summary>
    void initClothData()
    {
        using (FileStream fs = new FileStream(pathURL + "/data/cloth.json", FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                string result = sr.ReadToEnd();
                ClothData[] datas = LitJson.JsonMapper.ToObject<ClothData[]>(result);
                foreach (ClothData data in datas)
                {
                    cloth.Add(data.type, data);
                }
            }
        }
    }
    public bool hasHero(int code)//传入卡片唯一标识符
    {
        return hero.ContainsKey(code);//在配置文件中有这个
    }
    /// <summary>
    /// 判断缓存成中是否有该装备
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool hasCloth(int type)
    {
        return cloth.ContainsKey(type);
    }
    public ClothData GetCloth(int typeCode)
    {
        if(hasCloth(typeCode))
        {
            return cloth[typeCode];
        }
        return null;
    }
    public HeroData GetCard(int cardCode)
    {
        if (hasHero(cardCode))
        {
            return hero[cardCode];//获取cardCode的英雄卡牌
        }
        return null;
    }
}