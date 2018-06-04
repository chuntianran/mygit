using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDowload : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            Down_Single();
        }else if(Input.GetKeyUp(KeyCode.A)){

            Down_List();

        }


   
    }

    void Down_Single()
    {
        string downUrl = @"http://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1493218983354&di=1721b5368b88209b4fe671afd3f9102f&imgtype=0&src=http%3A%2F%2Fpic41.nipic.com%2F20140523%2F18507730_165953869000_2.jpg";
        string savePath = Application.dataPath + "01.jpg";
        Debug.Log(savePath);
        DowloadUnit unit = new DowloadUnit(downUrl, savePath);
        BreakPoint.SingleDowload(unit, (currentSize, totalSize) =>
        {
            float percent = (currentSize / (float)totalSize) * 100;
            Debug.LogFormat("currentSize = {0}, totalSize = {1}, percent = {2}", currentSize, totalSize, percent.ToString("F2"));
            if (currentSize == totalSize)
            {
                Debug.Log("------------下载完成-----------");
            }
        },
        (downUnit) =>
        {
            Debug.Log("------------下载出错-----------");
        });
    }

    void Down_List()
    {
        string downUrl1 = @"http://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1493218983355&di=4db20f8769976f4df646d07c3545eeea&imgtype=0&src=http%3A%2F%2Fimg6.3lian.com%2Fc23%2Fdesk4%2F04%2F57%2Fd%2F10.jpg";
        string downUrl2 = @"http://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1493218983355&di=00af74a5435438d149dfbfa8f335fb63&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fpic%2Fitem%2F5d8ea144ad345982913052140cf431adcaef8455.jpg";
        string downUrl3 = @"http://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1493218983355&di=e925997036e582bac8875ccab462decc&imgtype=0&src=http%3A%2F%2Fattimg.dospy.com%2Fimg%2Fday_120706%2F20120706_a4ea28027b7545cd36a1Ku44Kt93sAPS.jpg";
        string downUrl4 = @"http://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1493218983355&di=5ab76db153e7b3af880e8354837ef09a&imgtype=0&src=http%3A%2F%2Fwww.bizhiwa.com%2Fuploads%2Fallimg%2F2012-01%2F20114H3-1-1M522.jpg";
        string downUrl5 = @"http://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1493218983355&di=205f4221df8463251686e7eb278bbda3&imgtype=0&src=http%3A%2F%2Fpic41.nipic.com%2F20140501%2F18539861_155959517170_2.jpg";
        string savePath1 = Application.dataPath + "T01.jpg";
        string savePath2 = Application.dataPath + "T02.jpg";
        string savePath3 = Application.dataPath + "T03.jpg";
        string savePath4 = Application.dataPath + "T04.jpg";
        string savePath5 = Application.dataPath + "T05.jpg";

        List<DowloadUnit> list = new List<DowloadUnit>();
        list.Add(new DowloadUnit(downUrl1, savePath1));
        list.Add(new DowloadUnit(downUrl2, savePath2));
        list.Add(new DowloadUnit(downUrl3, savePath3));
        list.Add(new DowloadUnit(downUrl4, savePath4));
        list.Add(new DowloadUnit(downUrl5, savePath5));
        BreakPoint.BatchDowload(list, (currentSize, totalSize) =>
        {
            float percent = (currentSize / (float)totalSize) * 100;
            Debug.Log("percent:" + percent.ToString("F2"));
            if (currentSize == totalSize)
            {
                Debug.Log("------------下载完成-----------");
            }
        },
        (downUnit) =>
        {
            Debug.Log("------------下载出错-----------");
        });
    }

}
