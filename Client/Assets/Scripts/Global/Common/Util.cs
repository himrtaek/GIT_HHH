using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static string GetFileNameByPath(string path)
    {
        string fileName = path.Substring(
            path.LastIndexOf("/", System.StringComparison.CurrentCulture)+ 1, 
            path.LastIndexOf(".", System.StringComparison.CurrentCulture)
            - path.LastIndexOf("/", System.StringComparison.CurrentCulture)
            - 1);
        
        return fileName;
    }
    
}
