﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class ChoseManager
{
    private static ChoseManager _Instace=null;

    public static ChoseManager Instance
    {
        get
        {
            if (_Instace == null)
            {
                _Instace = new ChoseManager();
            }

            return _Instace;
        }
    }


  

    private Dictionary<int, ChoseData> ChoseDataDic;

    void Init()
    {
        ChoseDataDic = new Dictionary<int, ChoseData>();
        LoadChoseXML("ChoseConfig", ChoseDataDic);
        InitChoseHander();
    }

    public void ShowChosePanel(int id)
    {
      

        if (ChoseDataDic == null)
        {
            Init();
        }
        ChoseData data = null;
        if(!ChoseDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("choseData is null");
            return;
        }
        ChosePanel.data = data;
        GUIManager.ShowView("ChosePanel");
    }





    #region 

    void InitChoseHander()
    {
        ChoseData data = ChoseDataDic[0];
        data.HanderList.Add(ContinueGo);

        ChoseData data1 = ChoseDataDic[1];
        data1.HanderList.Add(ObserveRabit_1);
        data1.HanderList.Add(GiveUpYinYangYu_1);
    }

    #endregion


    #region Stage0中选项方法设置
    void ContinueGo()
    {
        GUIManager.HideView("EventStoryPanel");
    }
    void ObserveRabit_1()
    {

    }

    void GiveUpYinYangYu_1()
    {

    }

    #endregion





    void LoadChoseXML(string pathName, Dictionary<int, ChoseData> DataDic)
    {
        string filePath = Application.dataPath + @"/Resources/Config/Chose/" + pathName + ".xml";
        if (!File.Exists(filePath))
        {
            Debug.LogError("not ChoseCofing");
            return;
        }

        if (File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode node = xmlDoc.SelectSingleNode("Chose");
            XmlNodeList nodeList = node.ChildNodes;

            foreach (XmlNode item in nodeList)
            {
                XmlNode id = item.SelectSingleNode("id");

                XmlNode name = item.SelectSingleNode("name");
     
                XmlNode choseList = item.SelectSingleNode("choseList");
              

                ChoseData data = new ChoseData();

                data.Id = CommonHelper.Str2Int(id.InnerText);
                data.Name = name.InnerText;
                foreach (XmlNode pair in choseList)
                {
                    data.ChoseDesList.Add(pair.InnerText);
                }
                DataDic.Add(data.Id, data);
            }
        }
    }
}
