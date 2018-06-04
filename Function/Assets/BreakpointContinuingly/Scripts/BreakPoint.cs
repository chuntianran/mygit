using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.IO;
using System.Net;

//断线续传
public class BreakPoint {



    const int oneReadLen = 16384;           // 一次读取长度 16384 = 16*kb
    const int ReadWriteTimeOut = 2 * 1000;  // 超时等待时间
    const int TimeOutWait = 5 * 1000;       // 超时等待时间
    const int MaxTryTime = 3;

    //当个文件下载
    public static void SingleDowload(DowloadUnit dowloadUnit, Action<long, long> callBack, Action<DowloadUnit> errorCallBack)
    {

        Thread thread = new Thread(() => {


            Dowload(dowloadUnit,callBack,errorCallBack);
        
        });
        thread.Start();

    }

    //多个文件下载
    public static void BatchDowload(List<DowloadUnit> dowloadUnit, Action<long, long> callBack, Action<DowloadUnit> errorCallBack)
    {

        Thread thread = new Thread(() => {


            Dowload(dowloadUnit, callBack, errorCallBack);

        });
        thread.Start();

    }

    //下载单个文件
    public static void Dowload(DowloadUnit dowloadUnit,Action<long,long> callBack, Action<DowloadUnit> errorCallBack){


        //打开上次下载的文件
        long startPos = 0;
        string tempPath = dowloadUnit.saveUrl + ".temp";
        FileStream fileStream = null;
        if (File.Exists(tempPath)){

            //如果存在，移动指针到数据流的尾部
            fileStream = File.OpenRead(tempPath);
            startPos = fileStream.Length;
            fileStream.Seek(startPos, SeekOrigin.Current);

        }else{

            //不存在的情况就创建
            string direction = Path.GetDirectoryName(tempPath);
            if(Directory.Exists(direction) == false){

                Directory.CreateDirectory(direction);
            }
            fileStream = File.Create(tempPath);
        }

        //下载逻辑
        //请求以及回应
        HttpWebRequest httpWebRequest = null;
        WebResponse webResponse = null;

        //取得网络流
        Stream ns = null;
        try{

            //创建连接
			httpWebRequest = WebRequest.Create(dowloadUnit.dowloadUrl) as HttpWebRequest;
            //设置读取流的指针到指定位置
            if(startPos>0)
            httpWebRequest.AddRange((int)startPos);

            //设置超时时间
            httpWebRequest.ReadWriteTimeout = ReadWriteTimeOut;
            httpWebRequest.Timeout = TimeOutWait;

            //获取相应，并取得流
            webResponse = httpWebRequest.GetResponse();
            ns = webResponse.GetResponseStream();

            //获取文件的总大小
            long totalSize = webResponse.ContentLength;
            //指定流的当前大小
            long currentSize = startPos;

            //如果相等移动写入，更改格式
            if (totalSize == currentSize)
            {

                fileStream.Flush();
                fileStream.Close();
                fileStream = null;
                if (File.Exists(dowloadUnit.saveUrl))
                    File.Delete(dowloadUnit.saveUrl);
                File.Move(tempPath, dowloadUnit.saveUrl);

            }
            else
            {
                //否则就读取流的内容
                byte[] content = new byte[oneReadLen];
                int read = ns.Read(content, 0, oneReadLen);
                while (read > 0)
                {
                    //写入到指定位置
                    fileStream.Write(content, 0, read);
                    currentSize += read;

                    if (currentSize == totalSize)
                    {

                        fileStream.Flush();
                        fileStream.Close();
                        fileStream = null;
                        if (File.Exists(dowloadUnit.saveUrl))
                            File.Delete(dowloadUnit.saveUrl);
                        File.Move(tempPath, dowloadUnit.saveUrl);


                    }
                    //每读取一次，回调
                    if (callBack != null) callBack(currentSize, totalSize);

                    //再读取内容
                    read = ns.Read(content, 0, oneReadLen);
                }
            }
            }
            catch(WebException ex){

            if(errorCallBack!=null){

                errorCallBack(dowloadUnit);
                Debug.Log(ex.Message);
            }

            }

    }


    //下载多个文件
    public static void Dowload(List<DowloadUnit> dowloadUnit, Action<long, long> callBack, Action<DowloadUnit> errorCallBack)
    {

        //记录总体大小
        long totalSize = 0;
        long oneSize = 0;
        int i = 0;
        for ( i = 0; i < dowloadUnit.Count;i++){

            oneSize = GetNetFileLenght(dowloadUnit[i].dowloadUrl);
            totalSize += oneSize;

        }

        //每读取一次，记录读取大小，并回调
        i = 0;
        long currentSize = 0;
        DowloadUnit unit;
        for (i = 0; i < dowloadUnit.Count;i++){

            unit = dowloadUnit[i];
            long currentFileSize = 0;
            Dowload(unit,
                    (long _currenSize, long _fileSize) =>
                    {

                    currentFileSize = _currenSize;
                    //总下载大小，fileSize为当前下载的大小
                    long tempSize = currentSize + currentFileSize;
                Debug.Log("目前下载的大小为：" + tempSize + "  下载的文件为第" + i + "个" + " 大小为" + _fileSize+" 总大小为:"+totalSize);
                    if (callBack != null) callBack(tempSize,totalSize);


            },
             (DowloadUnit down)=>{

                if (errorCallBack != null) errorCallBack(unit); 
            });

            currentSize += currentFileSize;
        }


    }

    //获取网络文件的大小
    public static long GetNetFileLenght(string netUrl){

        HttpWebRequest httpWebRequest = null;
        WebResponse webResponse = null;
        long lenght = 0;
        try{

            httpWebRequest = WebRequest.Create(netUrl) as HttpWebRequest;
            httpWebRequest.Timeout = TimeOutWait;
            httpWebRequest.ReadWriteTimeout = ReadWriteTimeOut;
            webResponse = httpWebRequest.GetResponse();
            lenght = webResponse.ContentLength;

        }catch(Exception e){

            Debug.Log("读取文件大小出错"+e.Message);


        }finally{

            if (httpWebRequest != null) httpWebRequest.Abort();
            if (webResponse != null) webResponse.Close();

        }
        return lenght;

    }
}
