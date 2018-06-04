using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowloadUnit
{

    public string dowloadUrl { get; set; }
    public string saveUrl { get; set; }

    public DowloadUnit(string dowloadUrl, string saveUrl)
    {

        this.dowloadUrl = dowloadUrl;
        this.saveUrl = saveUrl;

    }
}
