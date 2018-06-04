using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using System.IO;

public class XluaDrive : MonoBehaviour {


    public static LuaEnv luaEnv;
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 


    private static string srcPath;
    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    private LuaTable luaTable;
	// Use this for initialization
	void Awake () {

        if(luaEnv == null){

            luaEnv = new LuaEnv();
        }

        //自定义加载数据，在Lua中可以通过Require进行查询
        luaEnv.AddLoader((ref string filename) => {
            
            if (filename.Contains("/"))
            {
                string script = GetLuaFileText(filename);
                return System.Text.Encoding.UTF8.GetBytes(script);
            }
            return null;
        });

        srcPath = Application.dataPath + "/XluaFrame/Lua/LuaBridge.lua";

        luaTable = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        luaTable.SetMetaTable(meta);
        meta.Dispose();

        luaTable.Set("self", this);

        string luaStr = File.ReadAllText(srcPath);

        luaEnv.DoString(luaStr, "XluaDrive", luaTable);

        Action luaAwake = luaTable.Get<Action>("awake");
        luaTable.Get("start", out luaStart);
        luaTable.Get("update", out luaUpdate);
        luaTable.Get("ondestroy", out luaOnDestroy);

        if (luaAwake != null)
        {
            luaAwake();
        }


	}


    public string GetLuaFileText(string fileName){

        string path =  Application.dataPath + "/XLuaFrame/"+fileName;
        string luaStr = File.ReadAllText(path);

        return luaStr;
    }

	private void Start()
	{
        if(luaStart != null){

            luaStart();

        }
		
	}



	// Update is called once per frame
	void Update () {
		
        if (luaUpdate != null)
        {
            luaUpdate();
        }
        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
	}

	private void OnDestroy()
	{
        if(luaOnDestroy != null){

            luaOnDestroy();
        }
        luaStart = null;
        luaUpdate = null;
        luaOnDestroy = null;
        luaEnv.Dispose();

	}
}
