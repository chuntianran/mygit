using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStart : MonoBehaviour {


    public GameObject cube;
    public int rowNumber;
    public int colNumber;

    private Grid[,] grids;
    private List<Grid> openList;
    private List<Grid> closeList;
    private List<GameObject> cubeList;
    Grid start;

	 void Start()
	{

        InitialData();
        start = GetGridByXY(2, 2);
      
	}

	 void Update()
	{
        
        if(Input.GetMouseButtonDown(0)){


            DestroyCubeList();
            int x = Random.Range(0,colNumber);
            int y = Random.Range(0,rowNumber);
            Debug.Log(x + " " + y);
            Grid end = GetGridByXY(x, y);
            List<Grid> path = FindPath(start, end);
            
            if (path != null)
            {
                foreach (Grid p in path)
                {
                    Debug.Log(p.x + " || " + p.y);
                    Vector3 pos = new Vector3(p.x,p.y,0);
                    GameObject cubeEntity = GameObject.Instantiate(cube,pos,Quaternion.identity);
                    cubeList.Add(cubeEntity);

                }
            }

        }
		
	}

    //清空物体
    public void DestroyCubeList(){

        for (int i = 0; i < cubeList.Count; i++)
        {
            Destroy(cubeList[i]);

        }
        cubeList.Clear();

    }

	public Grid GetGridByXY(int x,int y){

        if (x < 0 || x >= colNumber || y < 0 || y >= rowNumber)
        {
            return grids[0,0];
        }else{

            return grids[x, y];
        }

    }

    //初始化格子数据
    public void InitialData(){

        cubeList = new List<GameObject>();
        openList = new List<Grid>();
        closeList = new List<Grid>();

        grids = new Grid[rowNumber, colNumber];
        for (int i = 0; i < rowNumber;i++){

            for (int j = 0; j < colNumber;j++){

                Grid grid = new Grid(i,j);
                grids[i, j] = grid;
            }

        }
    }


    //寻找路径
    public List<Grid> FindPath(Grid startGrid,Grid endGrid){

        openList.Clear();
        closeList.Clear();

        openList.Add(startGrid);

        while(openList.Count>0){


            //获取F最小的格子
            Grid minGrid =  openList[0];
            for (int i = 0; i < openList.Count;i++){

                if(openList[i].f<minGrid.f){

                    minGrid = openList[i];
                }

            }

            //从开闭列表移除
            openList.Remove(minGrid);
            //从关闭列表移除
            closeList.Add(minGrid);

            //如果是所得值
            if(minGrid == endGrid){

                return GenerPath(minGrid);
            }

            //获取周边的格子
            List<Grid> tempGrids = GetRoundGride(minGrid);
            for (int i = 0; i < tempGrids.Count;i++){


                Grid temp = tempGrids[i];
                if (closeList.Contains(temp)) continue;
                if(openList.Contains(temp)){

                  float newG =  minGrid.g + ExcuteDistance(minGrid,temp); 
                    if(newG<minGrid.g){

                        temp.g = newG;
                        temp.h = ExcuteDistance(temp,endGrid);
                        temp.parent = minGrid;
                    }

                }else{

                    temp.g = ExcuteDistance(minGrid,temp);
                    temp.h = ExcuteDistance(temp,endGrid);
                    temp.parent = minGrid;
                    openList.Add(temp);
                        
                }

            }
        }

        return null;

    }

    //计算两点之间的距离
    public float ExcuteDistance(Grid grid1,Grid grid2){

        float x = Mathf.Abs(grid2.x - grid1.x);
        float y = Mathf.Abs(grid2.y - grid2.y);
        return x + y;

    }


    //根据节点获取路径
    public List<Grid> GenerPath(Grid grid)
    {

        List<Grid> tempGrids = new List<Grid>();
        Grid temp = grid;
        while (temp.parent != null)
        {

            tempGrids.Add(temp);
            temp = temp.parent;
        }
        tempGrids.Add(start);
        //反转
        tempGrids.Reverse();
        return tempGrids;
    }

    //获取周围的点
    public List<Grid> GetRoundGride(Grid grid){

        List<Grid> tempGrids = new List<Grid>();
        for (int i = -1; i <= 1;i++){

            for (int j = -1; j <= 1;j++){

                int x = j + grid.x;
                int y = i + grid.y;
                if (x < colNumber && x >= 0 && y < rowNumber && y >= 0)
                {
                    if (x == grid.x && y == grid.y)
                    {

                        continue;
                    }

                    tempGrids.Add(grids[x, y]);
                }

            }

        }
        return tempGrids;
    }




}
