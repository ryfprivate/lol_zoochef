using UnityEditor;

public class CreateNewScriptClassFromCustomTemplate
{
    private const string pathToYourScriptTemplate = "Assets/com.dotdothorse.zoochef/Scripts/Templates/ZoochefTemplate.cs.txt";

    [MenuItem(itemName: "Assets/Create/Create New Script from Custom Template", isValidateFunction: false, priority: 51)]
    public static void CreateScriptFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToYourScriptTemplate, "YourDefaultNewScriptName.cs");
    }
}