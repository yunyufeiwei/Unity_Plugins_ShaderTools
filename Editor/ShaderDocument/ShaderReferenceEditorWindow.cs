using UnityEngine;
using UnityEditor;

namespace yuxuetian
{
    public class ShaderReferenceEditorWindow : EditorWindow
    {
        #region 折叠属性布尔值
        //渲染管线
        private bool _isFoldPipelineApplicationStage = true;
        private bool _isFoldPipelineGeometryStage = true;
        private bool _isFoldPipelineRasterizerStage = true;
        private bool _isFoldPipelineShaderLab = true;
        //渲染状态
        private bool _isFoldRenderStateCull = true;
        private bool _isFoldRenderStateStencilTest = true;
        private bool _isFoldRenderStateDepthTest = true;
        private bool _isFoldRenderStateColorMask = true;
        private bool _isFoldRenderStateBlend = true;
        //属性
        private bool _isFoldProperty = true;
        private bool _isFoldPropertyAttribute = true;
        //语义
        private bool _isFoldSemanticsAttribute = true;
        private bool _isFoldSemanticsVaryings = true;
        private bool _isFoldSemanticsPixelShading = true;
        //标签
        private bool _isFoldTagQueue = true;
        private bool _isFoldTagRenderType = true;
        private bool _isFoldTagLightMode = true;
        private bool _isFoldTagDisableBatching = true;
        private bool _isFoldTagIgnoreProjector = true;
        private bool _isFoldTagForceNoShadowCasting = true;
        private bool _isFoldTagPreviewType = true;
        private bool _isFoldTagCanUseSpriteAtlas = true;
        private bool _isFoldTagPerformanceChecks = true;
        //编译指令
        private bool _isFoldPragma = true;
        private bool _isFoldPragmaMultiCompile = true;
        private bool _isFoldPragmaTarget = true;
        private bool _isFoldPragmaRequire = true;
        private bool _isFoldPragmaShaderVariant = true;
        private bool _isFoldPragmaOther = true;
        //变换
        private bool _isFoldTransformationMatrix = true;
        private bool _isFoldTransformationFunction = true;
        private bool _isFoldTransformationBaseTransformationMatrix = true;
        //其它
        private bool _isFoldShaderLibrary = true;
        private bool _isFoldCustomHLSL = true;
        private bool _isFoldOther = true;
        //内置变量
        private bool _isFoldBuildInVariablesCameraAndScreen = true;
        private bool _isFoldBuildInVariablesTime = true;
        private bool _isFoldBuildInVariablesGPUInstancing = true;
        //预定义宏
        private bool _isFoldPredefinedMacrosTargetPlatform = true;
        private bool _isFoldPredefineMacrosBranch = true;
        private bool _isFoldPredefineMacrosLighting = true;
        private bool _isFoldPredefineMacrosOther = true;
        //数学
        private bool _isFoldMathFunction = true;
        //光照模型
        private bool _isFoldLightingLightModel = true;
        private bool _isFoldLightingNormal = true;
        private bool _isFoldLightingMainLight = true;
        private bool _isFoldLightingAdditionalLight = true;
        private bool _isFoldLightingReceiveShadow = true;
        private bool _isFoldLightingFog = true;
        private bool _isFoldLightingBake = true;
        private bool _isFoldLightingEnvironmentColor = true;
        //算法
        private bool _isFoldAlgorithmUVShape = true;
        private bool _isFoldAlgorithmuvTransform = true;
        private bool _isFoldAlgorithmVertex = true;
        private bool _isFoldAlgorithmColor = true;
        private bool _isFoldAlgorithmLight = true;
        private bool _isFoldAlgorithmFresnel = true;
        private bool _isFoldAlgorithmXRay = true;
        private bool _isFoldAlgorithmDither = true;
        //纹理采样
        private bool _isFoldTextureSampler = true;
        private bool _isFoldTextureArraySampler = true;
        //SubstancePainter
        private bool _isFoldSubstancePainterMaterialProperty = true;
        private bool _isFoldSubstancePainterFrag = true;
        #endregion
        
        private Vector2 _scrollPos;
        private int _selectedTabID;  
        private string[] _tabName = new string[]
        {
            "Pipeline(渲染管线)", 
            "RenderState(渲染状态)",
            "Property(属性说明)" , 
            "Semantics(语义说明)",
            "Tags(标签说明)",
            "Pragma(编译指令)",
            "Transformation(变换)",
            "ShaderLibrary(库文件引用)",
            "Build-In Variables(内置变量)",
            "Predefined Macros(预定义宏)",
            "Platform Difference(平台差异)",
            "LightingMode(光照模型)",
            "TextureSampler(纹理采样)",
            "Algorithm(常用算法)",
            "ColorBlendMode(颜色混合)",
            "MathFunction(数学函数)",
            "SubstancePainter",
            "StudyWebsite(学习网址)",
            "MathGraphical学图形函数)",
            "About"
        };
       
        private ShaderReferencePipeline _pipeline;
        private ShaderReferenceRenderState _renderState;
        private ShaderReferenceProperty _property;
        private ShaderReferenceSemantics _semantics;
        private ShaderReferenceTags _tags;
        private ShaderReferencePragma _pragma;
        private ShaderReferenceTransformation _transformation;
        private ShaderReferenceShaderLibrary _ShaderLibrary;
        private ShaderReferenceBuildInVariables _buildInVariables;
        private ShaderReferencePredefinedMacros _predefinedMacros;
        private ShaderReferencePlatformDifferences _platformDifferences;
        private ShaderReferenceLighting _lighting;
        private ShaderReferenceTextureSampler _textureSampler;
        private ShaderReferenceAlgorithm _algorithm;
        private ShaderReferenceColorBlendMode _colorBlendMode;
        private ShaderReferenceMath _mathFunction;
        private ShaderReferenceSubstancePainter _substancePainter;
        private ShaderReferenceStudyWebsite _studyWebsite;
        private ShaderReferenceMathGraphical _mathGraphical;
        private ShaderReferenceAbout _about;
        
        //快捷键组合方式 #-shift %-Ctrl &-Alt
        [MenuItem("ShaderTool/ShadserReference #R" ,false, 101)]
        public static void ShowWindow()
        {
            var windows = Resources.FindObjectsOfTypeAll<ShaderReferenceEditorWindow>();
    
            if (windows.Length > 0)
            {
                // 如果窗口已存在
                var window = windows[0];
        
                // 检查是否是当前焦点窗口
                if (window == EditorWindow.focusedWindow)
                {
                    window.Close();
                }
                else
                {
                    window.Focus();
                }
            }
            else
            {
                // 创建新窗口
                var window = CreateInstance<ShaderReferenceEditorWindow>();
                window.titleContent = new GUIContent("Shader参考手册");
                window.Show();
            }
        }

        private void OnGUI()
        {
            //绘制两块区域，左边用来描述分类，右边则显示不同分类下的具体内容
            EditorGUILayout.BeginHorizontal();
            
            float width = 170.0f;
            float height = position.height - 20;
            
            //左侧区域绘制
            EditorGUILayout.BeginVertical(EditorStyles.helpBox , GUILayout.MaxWidth(width) , GUILayout.MaxHeight(height));
            // 保存原始对齐方式
            var originalAlignment = GUI.skin.button.alignment;
            // 强制设置左对齐
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;
            _selectedTabID = GUILayout.SelectionGrid(_selectedTabID, _tabName, 1); 
            // 恢复原始对齐方式
            GUI.skin.button.alignment = originalAlignment;
            EditorGUILayout.EndVertical();

            //右侧区域绘制
            EditorGUILayout.BeginVertical(EditorStyles.helpBox , GUILayout.MaxWidth(position.width - width), GUILayout.MinHeight(height));
            DrawMainUI(_selectedTabID);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndHorizontal();
        }

        void OnEnable()
        {
            _selectedTabID = EditorPrefs.HasKey("SelectedTabID") ? EditorPrefs.GetInt("SelectedTabID") : 0;
            
            _pipeline = ScriptableObject.CreateInstance<ShaderReferencePipeline>();
            _renderState = ScriptableObject.CreateInstance<ShaderReferenceRenderState>();
            _property = ScriptableObject.CreateInstance<ShaderReferenceProperty>();
            _semantics = ScriptableObject.CreateInstance<ShaderReferenceSemantics>();
            _tags = ScriptableObject.CreateInstance<ShaderReferenceTags>();
            _pragma = ScriptableObject.CreateInstance<ShaderReferencePragma>();
            _transformation = ScriptableObject.CreateInstance<ShaderReferenceTransformation>();
            _ShaderLibrary = ScriptableObject.CreateInstance<ShaderReferenceShaderLibrary>();
            _buildInVariables = ScriptableObject.CreateInstance<ShaderReferenceBuildInVariables>();
            _predefinedMacros = ScriptableObject.CreateInstance<ShaderReferencePredefinedMacros>();
            _platformDifferences = ScriptableObject.CreateInstance<ShaderReferencePlatformDifferences>();
            _lighting = ScriptableObject.CreateInstance<ShaderReferenceLighting>();
            _textureSampler = ScriptableObject.CreateInstance<ShaderReferenceTextureSampler>();
            _algorithm = ScriptableObject.CreateInstance<ShaderReferenceAlgorithm>();
            _colorBlendMode = ScriptableObject.CreateInstance<ShaderReferenceColorBlendMode>();
            _mathFunction = ScriptableObject.CreateInstance<ShaderReferenceMath>();
            _substancePainter = ScriptableObject.CreateInstance<ShaderReferenceSubstancePainter>();
            _studyWebsite = ScriptableObject.CreateInstance<ShaderReferenceStudyWebsite>();
            _mathGraphical = ScriptableObject.CreateInstance<ShaderReferenceMathGraphical>();
            _about = ScriptableObject.CreateInstance<ShaderReferenceAbout>();
        }

        void OnDisable()
        {
            EditorPrefs.SetInt("SelectedTabID", _selectedTabID);
        }

        void DrawMainUI(int _selectedTabID)
        {
            switch (_selectedTabID)
            {
                case 0:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _pipeline.DrawTitleApplicationStage();
                    _isFoldPipelineApplicationStage = EditorGUILayout.Foldout(_isFoldPipelineApplicationStage, "应用程序阶段");
                    _pipeline.DrawContentApplicationStage(_isFoldPipelineApplicationStage);
                    
                    _pipeline.DrawTitleGeometryStage();
                    _isFoldPipelineGeometryStage = EditorGUILayout.Foldout(_isFoldPipelineGeometryStage, "几何阶段");
                    _pipeline.DrawContentGeometryStage(_isFoldPipelineGeometryStage);
                    
                    _pipeline.DrawTitleResterizerStage();
                    _isFoldPipelineRasterizerStage = EditorGUILayout.Foldout(_isFoldPipelineRasterizerStage, "光栅化阶段");
                    _pipeline.DrawContentResterizerStage(_isFoldPipelineRasterizerStage);
                    
                    _pipeline.DrawTitleShaderLab();
                    _isFoldPipelineShaderLab = EditorGUILayout.Foldout(_isFoldPipelineShaderLab, "Shader Lab");
                    _pipeline.DrawContentShaderLab(_isFoldPipelineShaderLab);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 1:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

                    _renderState.DrawTitleCull();
                    _isFoldRenderStateCull = EditorGUILayout.Foldout(_isFoldRenderStateCull, "Cull");
                    _renderState.DrawContentCull(_isFoldRenderStateCull);
                    
                    _renderState.DrawTitleStencilTest();
                    _isFoldRenderStateStencilTest = EditorGUILayout.Foldout(_isFoldRenderStateStencilTest, "Stencil Test");
                    _renderState.DrawContentStencilTest(_isFoldRenderStateStencilTest);
                    
                    _renderState.DrawTitleDepthTest();
                    _isFoldRenderStateDepthTest = EditorGUILayout.Foldout(_isFoldRenderStateDepthTest, "Depth Test");
                    _renderState.DrawContentDepthTest(_isFoldRenderStateDepthTest);
                    
                    _renderState.DrawTitleColorMask();
                    _isFoldRenderStateColorMask = EditorGUILayout.Foldout(_isFoldRenderStateColorMask, "ColorMask");
                    _renderState.DrawContentColorMask(_isFoldRenderStateColorMask);
                    
                    _renderState.DrawTitleBlend();
                    _isFoldRenderStateBlend = EditorGUILayout.Foldout(_isFoldRenderStateBlend, "Blend");
                    _renderState.DrawContentBlend(_isFoldRenderStateBlend);
                    
                    _renderState.DrawTitleOther();
                    _renderState.DrawContentOther();
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 2:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _property.DrawTitleProperty();
                    _isFoldProperty = EditorGUILayout.Foldout(_isFoldProperty, "属性");
                    _property.DrawContentProperty(_isFoldProperty);
                    
                    _property.DrawTitleAttribute();
                    _isFoldPropertyAttribute = EditorGUILayout.Foldout(_isFoldPropertyAttribute, "属性形式");
                    _property.DrawContentAttribute(_isFoldPropertyAttribute);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 3:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _semantics.DrawTitleAttribute();
                    _isFoldSemanticsAttribute = EditorGUILayout.Foldout(_isFoldSemanticsAttribute, "Attribute");
                    _semantics.DrawContentAttribute(_isFoldSemanticsAttribute);
                    
                    _semantics.DrawTitleVaryings();
                    _isFoldSemanticsVaryings = EditorGUILayout.Foldout(_isFoldSemanticsVaryings, "Varying");
                    _semantics.DrawContentVaryings(_isFoldSemanticsVaryings);
                    
                    _semantics.DrawTitlePixelShading();
                    _isFoldSemanticsPixelShading = EditorGUILayout.Foldout(_isFoldSemanticsPixelShading, "Pixel Shading");
                    _semantics.DrawContentPixelShading(_isFoldSemanticsPixelShading);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 4:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _tags.DrawTitleTag();
                    _tags.DrawContentTag();
                    
                    _tags.DrawTitleRenderPipeline();
                    _tags.DrawContentRenderPipeline();
                    
                    _tags.DrawTitleQueue();
                    _isFoldTagQueue = EditorGUILayout.Foldout(_isFoldTagQueue, "Queue");
                    _tags.DrawContentQueue(_isFoldTagQueue);
                    
                    _tags.DrawTitleRenderType();
                    _isFoldTagRenderType = EditorGUILayout.Foldout(_isFoldTagRenderType, "RenderType");
                    _tags.DrawContentRenderType(_isFoldTagRenderType);
                    
                    _tags.DrawTitleLightMode();
                    _isFoldTagLightMode = EditorGUILayout.Foldout(_isFoldTagLightMode, "LightMode");
                    _tags.DrawContentLightMode(_isFoldTagLightMode);
                    
                    _tags.DrawTitleDisableBatching();
                    _isFoldTagDisableBatching = EditorGUILayout.Foldout(_isFoldTagDisableBatching, "DisableBatching");
                    _tags.DrawContentDisableBatching(_isFoldTagDisableBatching);
                    
                    _tags.DrawTitleIgnoreProjector();
                    _isFoldTagIgnoreProjector = EditorGUILayout.Foldout(_isFoldTagIgnoreProjector, "IgnoreProjector");
                    _tags.DrawContentIgnoreProjector(_isFoldTagIgnoreProjector);
                    
                    _tags.DrawTitleForceNoShadowCasting();
                    _isFoldTagForceNoShadowCasting = EditorGUILayout.Foldout(_isFoldTagForceNoShadowCasting, "ForceNoShadowCasting");
                    _tags.DrawContentForceNoShadowCasting(_isFoldTagForceNoShadowCasting);

                    _tags.DrawTitlePreviewType();
                    _isFoldTagPreviewType = EditorGUILayout.Foldout(_isFoldTagPreviewType, "PreviewType");
                    _tags.DrawContentPreviewType(_isFoldTagPreviewType);

                    _tags.DrawTitleCanUseSpriteAtlas();
                    _isFoldTagCanUseSpriteAtlas = EditorGUILayout.Foldout(_isFoldTagCanUseSpriteAtlas, "CanUseSpriteAltas");
                    _tags.DrawContentCanUseSpriteAtlas(_isFoldTagCanUseSpriteAtlas);
                    
                    _tags.DrawTitlePerformanceChecks();
                    _isFoldTagPerformanceChecks = EditorGUILayout.Foldout(_isFoldTagPerformanceChecks, "PerformanceChecks");
                    _tags.DrawContentPerformanceChecks(_isFoldTagPerformanceChecks);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 5:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _pragma.DrawTitlePragma();
                    _isFoldPragma = EditorGUILayout.Foldout(_isFoldPragma, "Pragma");
                    _pragma.DrawContentPragma(_isFoldPragma);
                    _isFoldPragmaMultiCompile = EditorGUILayout.Foldout(_isFoldPragmaMultiCompile, "MultiCompile");
                    _pragma.DrawContentPragmaMultiCompile(_isFoldPragmaMultiCompile);
                    
                    _pragma.DrawTitletPragmaTarget();
                    _isFoldPragmaTarget = EditorGUILayout.Foldout(_isFoldPragmaTarget, "Target(目标)");
                    _pragma.DrawContentPragmaTarget(_isFoldPragmaTarget);
                    _isFoldPragmaRequire = EditorGUILayout.Foldout(_isFoldPragmaRequire, "Require(需要)");
                    _pragma.DrawContentPragmaRequire(_isFoldPragmaRequire);
                    
                    _pragma.DrawTitlePragmaShaderVariant();
                    _isFoldPragmaShaderVariant = EditorGUILayout.Foldout(_isFoldPragmaShaderVariant, "ShaderVariant(变体)");
                    _pragma.DrawContentPragmaShaderVariant(_isFoldPragmaShaderVariant);
                    
                    _pragma.DrawTitlePragmaOther();
                    _isFoldPragmaOther = EditorGUILayout.Foldout(_isFoldPragmaOther, "Other");
                    _pragma.DrawContentPragmaOther(_isFoldPragmaOther);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 6:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _transformation.DrawTitleTransformationBase();
                    _transformation.DrawContentTransformationBase();
                    
                    _transformation.DrawTitleSpaceTransformationMatrix();
                    _isFoldTransformationMatrix = EditorGUILayout.Foldout(_isFoldTransformationMatrix, "空间变换(矩阵)");
                    _transformation.DrawContentSpaceTransformationMatrix(_isFoldTransformationMatrix);
                    
                    _transformation.DrawTitleSpaceTransformationFunction();
                    _isFoldTransformationFunction = EditorGUILayout.Foldout(_isFoldTransformationFunction, "空间变换(方法)");
                    _transformation.DrawContentSpaceTransformationFunction(_isFoldTransformationFunction);
                    
                    _transformation.DrawTitleBaseTransformationMatrix();
                    _isFoldTransformationBaseTransformationMatrix = EditorGUILayout.Foldout(_isFoldTransformationBaseTransformationMatrix, "基础变换矩阵");
                    _transformation.DrawContentBaseTransformationMatrix(_isFoldTransformationBaseTransformationMatrix);
                    
                    _transformation.DrawTitleTransformationRules();
                    _transformation.DrawContentTransformationRules();
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 7:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _ShaderLibrary.DrawTitleShaderLibrary();
                    _isFoldShaderLibrary = EditorGUILayout.Foldout(_isFoldShaderLibrary, "ShaderLibrary");
                    _ShaderLibrary.DrawContentShaderLibrary(_isFoldShaderLibrary);
                    
                    _ShaderLibrary.DrawTitleCustomHLSL();
                    _isFoldCustomHLSL =  EditorGUILayout.Foldout(_isFoldCustomHLSL, "CustomHLSL");
                    _ShaderLibrary.DrawContentCustomHLSL(_isFoldCustomHLSL);
                    
                    _ShaderLibrary.DrawTitleOther();
                    _isFoldOther = EditorGUILayout.Foldout(_isFoldOther, "Other");
                    _ShaderLibrary.DrawContentOther(_isFoldOther);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 8: 
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _buildInVariables.DrawTitleVert();
                    _buildInVariables.DrawContentVert();
                    
                    _buildInVariables.DrawTitleBuildInVariabledCameraAndScreen();
                    _isFoldBuildInVariablesCameraAndScreen = EditorGUILayout.Foldout(_isFoldBuildInVariablesCameraAndScreen, "CameraAndScreen");
                    _buildInVariables.DrawContentBuildInVariabledCameraAndScreen(_isFoldBuildInVariablesCameraAndScreen);
                    
                    _buildInVariables.DrawTitleBuildInVariablesTime();
                    _isFoldBuildInVariablesTime = EditorGUILayout.Foldout(_isFoldBuildInVariablesTime, "Time");
                    _buildInVariables.DrawContentBuildInVariablesTime(_isFoldBuildInVariablesTime);
                    
                    _buildInVariables.DrawTitleBuidInVariablesGPUInstancing();
                    _isFoldBuildInVariablesGPUInstancing = EditorGUILayout.Foldout(_isFoldBuildInVariablesGPUInstancing, "GPUInstancing");
                    _buildInVariables.DrawContentBuildInVariablesGPUInstancing(_isFoldBuildInVariablesGPUInstancing);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 9:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _predefinedMacros.DrawTitleBranch();
                    _isFoldPredefineMacrosBranch = EditorGUILayout.Foldout(_isFoldPredefineMacrosBranch, "Branch");
                    _predefinedMacros.DrawContentBranch(_isFoldPredefineMacrosBranch);
                    
                    _predefinedMacros.DrawTitleTargetPlatform();
                    _isFoldPredefinedMacrosTargetPlatform = EditorGUILayout.Foldout(_isFoldPredefinedMacrosTargetPlatform, "TargetPlatform");
                    _predefinedMacros.DrawContentGargetPlatform(_isFoldPredefinedMacrosTargetPlatform);
                    
                    _predefinedMacros.DrawTitleShaderTargetModel();
                    _predefinedMacros.DrawContentShaderTargetModel();
                    
                    _predefinedMacros.DrawTitleUnityVersion();
                    _predefinedMacros.DrawContentUnityVersion();
                    
                    _predefinedMacros.DrawTitlePlatformDifferenceHelpers();
                    _predefinedMacros.DrawContentPlatformDifferenceHelpers();
                    
                    _predefinedMacros.DrawTitleUI();
                    _predefinedMacros.DrawContentUI();
                    
                    _predefinedMacros.DrawTitleLighting();
                    _isFoldPredefineMacrosLighting = EditorGUILayout.Foldout(_isFoldPredefineMacrosLighting, "Lighting");
                    _predefinedMacros.DrawContentLighting(_isFoldPredefineMacrosLighting);
                    
                    _predefinedMacros.DrawTitleOther();
                    _isFoldPredefineMacrosOther = EditorGUILayout.Foldout(_isFoldPredefineMacrosOther, "Other");
                    _predefinedMacros.DrawContentOther(_isFoldPredefineMacrosOther);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 10:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _platformDifferences.DrawTitlePlatformDifferenceHcsSpace();
                    _platformDifferences.DrawContentPlatformDifferenceHcsSpace();
                    
                    _platformDifferences.DrawTitlePlatformDifferenceReversedZ();
                    _platformDifferences.DrawContentPlatformDifferenceReversedZ();
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 11:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _lighting.DrawTitleLightModeInfo();
                    _lighting.DrawContentLightModelInfo();
                    
                    _lighting.DrawTitleLightModel();
                    _isFoldLightingLightModel = EditorGUILayout.Foldout(_isFoldLightingLightModel, "LightModel");
                    _lighting.DrawContentLightModel(_isFoldLightingLightModel);
                    
                    _lighting.DrawTitleNormal();
                    _isFoldLightingNormal = EditorGUILayout.Foldout(_isFoldLightingNormal, "NormalMap");
                    _lighting.DrawContentNormal(_isFoldLightingNormal);
                    
                    _lighting.DrawTitleMainLight();
                    _isFoldLightingMainLight = EditorGUILayout.Foldout(_isFoldLightingMainLight, "主方向光");
                    _lighting.DrawContentMainLight(_isFoldLightingMainLight);
                    
                    _lighting.DrawTitleAdditionalLight();
                    _isFoldLightingAdditionalLight = EditorGUILayout.Foldout(_isFoldLightingAdditionalLight, "额外光源");
                    _lighting.DrawContentAdditioinalLight(_isFoldLightingAdditionalLight);
                    
                    _lighting.DrawTitleCastShadow();
                    _lighting.DrawContentCastShadow();
                    
                    _lighting.DrawTitleReceiveShadow();
                    _isFoldLightingReceiveShadow = EditorGUILayout.Foldout(_isFoldLightingReceiveShadow, "ReceiveShadow");
                    _lighting.DrawContentReceiveShadow(_isFoldLightingReceiveShadow);
                    
                    _lighting.DrawTitleLightingFog();
                    _isFoldLightingFog = EditorGUILayout.Foldout(_isFoldLightingFog, "Fog");
                    _lighting.DrawContentLightingFog(_isFoldLightingFog);  
                    
                    _lighting.DrawTitleLightingBake();
                    _isFoldLightingBake = EditorGUILayout.Foldout(_isFoldLightingBake, "Light Bake");
                    _lighting.DrawContentLightingBake(_isFoldLightingBake);
                    
                    _lighting.DrawTitleLightingEnvironmentColor();
                    _isFoldLightingEnvironmentColor = EditorGUILayout.Foldout(_isFoldLightingEnvironmentColor, "EnvironmentColor");
                    _lighting.DrawContentLightEnvironmentColor(_isFoldLightingEnvironmentColor);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 12:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _textureSampler.DrawTitleTextureSampler();
                    _isFoldTextureSampler = EditorGUILayout.Foldout(_isFoldTextureSampler, "纹理采样");
                    _textureSampler.DrawContentTextureSampler(_isFoldTextureSampler);
                    
                    _textureSampler.DrawTitleTextureArraySampler();
                    _isFoldTextureArraySampler = EditorGUILayout.Foldout(_isFoldTextureArraySampler, "数组纹理采样");
                    _textureSampler.DrawContentTextureArraySampler(_isFoldTextureArraySampler);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 13:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _algorithm.DrawTitleAlgorithmVertex();
                    _isFoldAlgorithmVertex = EditorGUILayout.Foldout(_isFoldAlgorithmVertex, "Vertex");
                    _algorithm.DrawContentAlgorithmVertex(_isFoldAlgorithmVertex);
                    
                    _algorithm.DrawTitleuvTransform();
                    _isFoldAlgorithmuvTransform = EditorGUILayout.Foldout(_isFoldAlgorithmuvTransform, "UVTransform");
                    _algorithm.DrawContentuvTransform(_isFoldAlgorithmuvTransform);
                    
                    _algorithm.DrawTitleAlgorithmUVShape();
                    _isFoldAlgorithmUVShape = EditorGUILayout.Foldout(_isFoldAlgorithmUVShape, "UVShape");
                    _algorithm.DrawContentAlgorithmUVShape(_isFoldAlgorithmUVShape);
                    
                    _algorithm.DrawTitleAlgorithmColor();
                    _isFoldAlgorithmColor = EditorGUILayout.Foldout(_isFoldAlgorithmColor, "Color");
                    _algorithm.DrawContentAlgorithmColor(_isFoldAlgorithmColor);
                    
                    _algorithm.DrawTitleAlgorithmLight();
                    _isFoldAlgorithmLight = EditorGUILayout.Foldout(_isFoldAlgorithmLight, "Light");
                    _algorithm.DrawContentAlgorithmLight(_isFoldAlgorithmLight);
                    
                    _algorithm.DrawTitleAlgorithmOther();
                    _isFoldAlgorithmFresnel = EditorGUILayout.Foldout(_isFoldAlgorithmFresnel, "Fresnel");
                    _algorithm.DrawContentAlgorithmFresnel(_isFoldAlgorithmFresnel);
                    _isFoldAlgorithmXRay = EditorGUILayout.Foldout(_isFoldAlgorithmXRay, "x-Ray");
                    _algorithm.DrawContnetAlgorithmXRar(_isFoldAlgorithmFresnel);
                    _isFoldAlgorithmDither = EditorGUILayout.Foldout(_isFoldAlgorithmDither, "Dither");
                    _algorithm.DrawContentAlgorithmDither(_isFoldAlgorithmXRay);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 14:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _colorBlendMode.DrawTitleColorBlendMode();
                    _colorBlendMode.DrawContentColorBlendMode();
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 15:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _mathFunction.DrawTitleMathFunction();
                    _isFoldMathFunction = EditorGUILayout.Foldout(_isFoldMathFunction, "MathFunction(数学函数)");
                    _mathFunction.DrawContentMathFunction(_isFoldMathFunction);

                    EditorGUILayout.EndScrollView();
                    break;
                case 16:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _substancePainter.DrawTitleSubstancePainterURL();
                    _substancePainter.DrawContentSubstancePainterURL();
                    
                    _substancePainter.DrawTitleSubstancePainterMaterialProperty();
                    _isFoldSubstancePainterMaterialProperty = EditorGUILayout.Foldout(_isFoldSubstancePainterMaterialProperty, "Property");
                    _substancePainter.DrawContentSubstancePainterMaterialProperyt(_isFoldSubstancePainterMaterialProperty);
                    
                    _substancePainter.DrawTitleSubstancePainterFrag();
                    _isFoldSubstancePainterFrag = EditorGUILayout.Foldout(_isFoldSubstancePainterFrag, "Frag");
                    _substancePainter.DrawContentSubstancePainterFrag(_isFoldSubstancePainterFrag);
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 17:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _studyWebsite.DrawTitleStudyWebsiteForUnity();
                    _studyWebsite.DrawContentStudyWebsiteForUnity();
                    
                    EditorGUILayout.Space(20);
                    _studyWebsite.DrawTitleStudyWebsiteProgram();
                    _studyWebsite.DrawContentStudyWebsityProgram();
                    
                    EditorGUILayout.Space(20);
                    _studyWebsite.DrawTitleStudyWebsityGraphics();
                    _studyWebsite.DrawContentStudyWebsityGraphics();
                    
                    EditorGUILayout.Space(20);
                    _studyWebsite.DrawTitleStudyWebsityCalculatorTools();
                    _studyWebsite.DrawContentStudyWebsityCalculatorTools();
                    
                    EditorGUILayout.Space(20);
                    _studyWebsite.DrawTitleOnlinePPT();
                    _studyWebsite.DrawContentOnliePPT();
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 18:
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    
                    _mathGraphical.DrawTitle();
                    _mathGraphical.DrawContent();
                    
                    EditorGUILayout.EndScrollView();
                    break;
                case 19:
                    _about.DrawTitleURL();
                    //_about.DrawContentUnityTexture();
                    break;
                
            }
        }
    }
}


