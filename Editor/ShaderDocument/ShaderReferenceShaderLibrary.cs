using UnityEditor;

namespace yuxuetian
{
    public class ShaderReferenceShaderLibrary : EditorWindow
    {
        private ShaderReferenceUtil _reference = new ShaderReferenceUtil();

        public void DrawTitleShaderLibrary()
        {
            _reference.DrawTitle("ShaderLibrary");
        }

        public void DrawContentShaderLibrary(bool isFold)
        {
            if (isFold)
            {
                _reference.DrawContent("#include \"Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl\"\n" +
                                       "#include \"Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl\"\n" +
                                       "#include \"Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl\"\n" +
                                       "#include \"Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl\"\n" + 
                                       "#include \"Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl\"");
            }
        }

        public void DrawTitleCustomHLSL()
        {
            _reference.DrawTitle("CustomHLSL");
        }

        public void DrawContentCustomHLSL(bool isFold)
        {
            if (isFold)
            {
                _reference.DrawContent("自定义HLSL文件\n" , "预定义名在hlsl文件内需要大写\n"+ 
                                       "#ifndef MYHLSL_INCLUDED\n"+
                                       "#define MYHLSL_INCLUDED\n\n"+
                                       "具体函数算法封装内容....\n\n"+
                                       "#endif");
                
                _reference.DrawContent("引用自定义的hlsl文件\n", "#include \"XXX.hlsl\"\n" +
                                      "方案一：绝对路径，Asset/XX/xx.hlsl \n" +
                                      "方案二：相对路径，同层级的话使用./xx.hlsl.     上一层级使用../xx.hlsl.       上层级的同级目录下使用../xx/xx.hlsl");
            }
        }

        public void DrawTitleOther()
        {
            _reference.DrawTitle("Other");
        }

        public void DrawContentOther(bool isFold)
        {
            if (isFold)
            {
                _reference.DrawContent("CBUFFER_START(UnityPerMaterial)\n"+
                                       "CBUFFER_END\n" ,"将材质属性面板中的变量定义在这个常量缓冲区中，用于支持SRP Batcher.\n" +
                                       "GlobalProperty(全局属性)不能放在CBUFFER里面，否则会破坏SRPBatch机制，应该直接放置该代码片段之外。");
                _reference.DrawContent("HLSLPROGRAM\n"+
                                       "ENDHLSL\n"+
                                       "", "HLSL代码的开始与结束.");
                _reference.DrawContent("HLSLINCLUDE\n"+
                                       "ENDHLSL\n"+
                                       "", "通常用于定义多段vert/frag函数，然后这段CG代码会插入到所有Pass的CG中，根据当前Pass的设置来选择加载.");
                _reference.DrawContent("LOD\n", "Shader LOD，可利用脚本来控制LOD级别，通常用于不同配置显示不同的SubShader。注意SubShader要从高往低写，要不然会无法生效.");
                _reference.DrawContent("Category{}\n", "定义一组所有SubShader共享的命令，位于SubShader外面。");
                _reference.DrawContent("Name \"MyPassName\"\n", "给当前Pass指定名称，以便利用UsePass进行调用。");
                _reference.DrawContent("UsePass \"Shader/NAME\"\n", "调用其它Shader中的Pass，注意Pass的名称要全部大写！Shader的路径也要写全，以便能找到具体是哪个Shader的哪个Pass。另外加了UsePass后，也要注意相应的Properties要自行添加。");
                _reference.DrawContent("Fallback \"name\"\n", "备胎，当Shader中没有任何SubShader可执行时，则执行FallBack。默认值为Off,表示没有备胎。\n比如URP下默认的紫色报错Shader:Fallback \"Hidden/Universal Render Pipeline/FallbackError\"");
                _reference.DrawContent("CustomEditor \"name\"\n", "自定义材质面板，name为自定义的脚本名称。可利用此功能对材质面板进行个性化自定义。");
            }
        }
    }
}

