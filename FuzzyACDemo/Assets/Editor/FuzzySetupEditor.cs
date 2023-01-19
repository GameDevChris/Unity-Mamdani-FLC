using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FuzzySetupEditor : EditorWindow
{
    public enum UIScene
    {
        Menu,
        MembershipFunctions,
        Rules
    }

    private UIScene currentScene = UIScene.Menu;
    
    private string[] tabs = { "Membership Functions", "Rules" };
    private int tabSelect = -1;

    private List<MF> allMFs;
    private List<FuzzyRule> allRules;

    private bool updateEditor = false;
    
    [MenuItem("FuzzyLogic/FuzzySetup")]
    static void Init()
    {
        FuzzySetupEditor window =
            EditorWindow.GetWindow<FuzzySetupEditor>(true, "Fuzzy Setup:");
        window.ShowUtility();
    }

    void OnSelectionChange() {
        Repaint();
    }
    
    void OnFocus() {
        Repaint();
    }
    
    private void OnGUI()
    {
        Repaint();
        switch (currentScene)
        {
            case UIScene.Menu:
            {
                GUILayout.BeginHorizontal();
                int newTabSelect = GUILayout.Toolbar(tabSelect, tabs);
                GUILayout.EndHorizontal();

                if (newTabSelect != tabSelect)
                {
                    updateEditor = true;
                    tabSelect = newTabSelect;
                }
        
                if (tabSelect >= 0  && tabSelect < tabs.Length && updateEditor)
                {
                    switch (tabs[tabSelect])
                    {
                        case "Membership Functions":
                            currentScene = UIScene.MembershipFunctions;
                            break;
                        case "Rules":
                            currentScene = UIScene.Rules;
                            break;
                    }
                }
                
                var help = new Button()
                    { text = "help"};
                help.clickable.clicked += () => newWindow();
                rootVisualElement.Add(help);

                break;
            }
            case UIScene.MembershipFunctions:
                if (updateEditor)
                {
                    MFTab();
                }

                updateEditor = false;
                break;
            case UIScene.Rules:
                if (updateEditor)
                {
                    RuleTab();
                }

                updateEditor = false;
                break;
        }
    }
    
    public void CreateGUI()
    {
        var allMFInputs = AssetDatabase.FindAssets("t:MF");
        var allRuleInputs = AssetDatabase.FindAssets("t:FuzzyRule");
        
        allMFs = new List<MF>();
        foreach (var mf in allMFInputs)
        {
            allMFs.Add(AssetDatabase.LoadAssetAtPath<MF>(AssetDatabase.GUIDToAssetPath(mf)));
        }

        allRules = new List<FuzzyRule>();
        foreach (var rule in allRuleInputs)
        {
            allRules.Add(AssetDatabase.LoadAssetAtPath<FuzzyRule>(AssetDatabase.GUIDToAssetPath(rule)));
        }
    }

    public void MFTab()
    {
        var menu = new Button()
            { text = "Return to menu"};
        menu.clickable.clicked += ()=>currentScene = UIScene.Menu;
        menu.clickable.clicked += ()=>updateEditor = true;
        rootVisualElement.Add(menu);
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        rootVisualElement.Add(splitView);
        var leftPane = new ListView();
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allMFs[index].mfName; };
        leftPane.itemsSource = allMFs;
        
        splitView.Add(leftPane);
        var rightPane = new VisualElement(); 
        splitView.Add(rightPane);

       leftPane.makeItem = () => new Label();
       leftPane.bindItem = (item, index) => { (item as Label).text = allMFs[index].mfName; };
       leftPane.itemsSource = allMFs;
    }
    
    public void RuleTab()
    {
        var menu = new Button( () => currentScene = UIScene.Menu)
            { text = "Return to menu"};        
        rootVisualElement.Add(menu);

        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
    
        rootVisualElement.Add(splitView);
    
        var leftPane = new ListView();
        splitView.Add(leftPane);
        var rightPane = new VisualElement();
        splitView.Add(rightPane);
    
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allRules[index].name; };
        leftPane.itemsSource = allRules;
    }

    void newWindow()
    {
        FuzzySetupEditor newWindow = (FuzzySetupEditor) EditorWindow.GetWindow( typeof(FuzzySetupEditor), true, "Gib Halp Plis" );
        newWindow.ShowUtility();
    }

    
}
