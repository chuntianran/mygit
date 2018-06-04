using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;



//压缩文件的回调类
public abstract class ZipCallBack
{
    //压缩前的回调
    public abstract bool OnPreZip(ZipEntry zipEntry);

    //压缩后的回调
    public abstract bool OnAfterZip(ZipEntry zipEntry);

    //是否压缩成功
    public abstract void OnFinish(bool isSure);

}


public  class ZipUtility  {

    //单利模式
    private static ZipUtility _instance { get; set; }
    public static ZipUtility Instance
    {
        get
        {
            if (_instance == null)
            {

                _instance = new ZipUtility();
            }
            return _instance;
        }
    }

    //压缩文件
    public bool ZipFile(string filePath,string parent,string password = null,ZipCallBack zipCallBack = null,ZipOutputStream zipOutputStream = null){

        //声明压缩节点
        ZipEntry zipEntry = null;
        //读取源文件的流
        FileStream fileStream = null;
        ////输出到压缩的流
        //ZipOutputStream zipOutputStream = null;
        try{

            //取得压缩路径
            string path = parent + "/"+Path.GetFileName(filePath);


            //设置压缩节点的路径
            zipEntry = new ZipEntry(path);


            //打开源文件的流
            fileStream = File.OpenRead(filePath);


            //设置压缩节点的大小
            zipEntry.Size = fileStream.Length;

            //设置压缩节点的时间
            zipEntry.DateTime = System.DateTime.Now;

            //设置压缩节点的压缩方式
            zipEntry.CompressionMethod = CompressionMethod.Stored;

            //回调压缩前的方法
            if (zipCallBack != null)
                zipCallBack.OnPreZip(zipEntry);

            //声明取得数据的容器
            byte[] content = new byte[fileStream.Length];


            //读取数据
            fileStream.Read(content,0,content.Length);


            //设置压缩密码
            if (password != null)
                zipOutputStream.Password = password;

            //压缩优先级
            zipOutputStream.SetLevel(6);


            //设置压缩的节点，向这个节点传数据
            zipOutputStream.PutNextEntry(zipEntry);

            //写入压缩数据
            zipOutputStream.Write(content, 0, content.Length);

            //回调成功方法
            if (zipCallBack != null)
                zipCallBack.OnAfterZip(zipEntry);


        }catch(System.Exception e){

            Debug.Log("Error:"+e.Message);

        }finally{

            if(fileStream != null){

                 fileStream.Flush();
                fileStream.Close();

            }
        }
        if (zipCallBack != null)
            zipCallBack.OnFinish(true);
     
        return true;

    }
    //压缩方法
    public bool Zip(string fileName,string outPath,string password = null,ZipCallBack zipCallBack = null){

        if(string.IsNullOrEmpty(fileName)||string.IsNullOrEmpty(outPath)){

            return false;
        }

        bool result = false;
        ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(outPath));
        if(Directory.Exists(fileName)){

            result = ZipDirectory(fileName,string.Empty,password,zipCallBack,zipOutputStream);
        }else if(File.Exists(fileName)){

            result = ZipFile(fileName,string.Empty,password,zipCallBack,zipOutputStream);
        }


        zipOutputStream.Finish();
        zipOutputStream.Close();

        return result;

    }

    //压缩文件夹
    public bool ZipDirectory(string dirPath,string parentName,string password = null,ZipCallBack zipCallBack = null, ZipOutputStream zipOutputStream = null){

        ZipEntry zipEntry = null;

        try{
       
            string path = Path.Combine(parentName,Path.GetFileName(dirPath))+"/";
            zipEntry = new ZipEntry(path);
            zipEntry.Size = 0;
            zipEntry.DateTime = System.DateTime.Now;
            zipEntry.CompressionMethod = CompressionMethod.Stored;

            //回调压缩前的方法
            if (zipCallBack != null)
                zipCallBack.OnPreZip(zipEntry);

            zipOutputStream.PutNextEntry(zipEntry);


            string[] subFiles = Directory.GetFiles(dirPath);

            for (int i = 0; i < subFiles.Length;i++){
                
                ZipFile(subFiles[i],Path.Combine(parentName, Path.GetFileName(dirPath)),password,null,zipOutputStream);
            }

            
        }catch(System.Exception e){

            Debug.Log("Error:"+"压缩文件出错:"+e.Message);
            return false;

        }

        string[] dirFiles = Directory.GetDirectories(dirPath);
        for (int i = 0; i < dirFiles.Length;i++){

            //组合子目录
            string temp = Path.Combine(parentName,Path.GetFileName(dirPath));
            bool isZip = ZipDirectory(dirFiles[i],temp,password,zipCallBack,zipOutputStream);
            if (isZip == false) return false;
        }
        return true;
    }

    //解压文件
    public bool UnZip(string zipFilePath,string outPath,string password = null,ZipCallBack zipCallBack = null){

        //
        if(string.IsNullOrEmpty(outPath)){

            return false;
        }

        //声明节点
        ZipEntry zipEntry = null;
        //声明流
        Stream stream = null;

        //压缩输出流
        ZipInputStream zipInputStream;
    
            //打开压缩文件，并取得流
            stream = File.OpenRead(zipFilePath);

          //转换为压缩输入流
            using(zipInputStream = new ZipInputStream(stream)){

            //设置密码
                if (password != null)
                    zipInputStream.Password = password;

            //判断循环，取得压缩节点
                while((zipEntry = zipInputStream.GetNextEntry()) != null){

                //判断是否为空，名称为空就跳到下一个节点
                    if (string.IsNullOrEmpty(zipEntry.Name))
                          continue;
                        
                //如果是目录的话，就创建目录
                    if(zipEntry.IsDirectory){      
                         Directory.CreateDirectory(outPath+"/"+zipEntry.Name);
                        continue;

                    }

                //如果是文件，取得当前的文件流，并把数据写入到指定的位置
                using (FileStream fileStream = File.Create(outPath + "/" + zipEntry.Name))
                {
                        byte[] bytes = new byte[99999];
                        while (true)
                        {
                            //从压缩文件中取得数据
                            int count = zipInputStream.Read(bytes, 0, bytes.Length);
                            if (count > 0)

                                //把数据写入到文件中
                                fileStream.Write(bytes, 0, count);
                            else
                            {
                                //压缩完成，调用压缩后的文件
                                if (null != zipCallBack)
                                    zipCallBack.OnAfterZip(zipEntry);

                                break;
                            }
                        }
                 }

             

                }


            }



        return true;


    }



}
