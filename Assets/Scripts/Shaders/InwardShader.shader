 Shader "Custom/InwardShader"
        {
            Properties
            {
                _Color ("Color", Color) = (1,1,1,1)
                _MainTex ("Albedo (RGB)", 2D) = "white" {}
               
            }
            SubShader
            {
                 Tags {"RenderType"="Transparent" "Queue"="Transparent"}

                Cull Front
           //      Pass {
             //        ColorMask 0
               //  }
                 // Render normally
         
               		ZWrite Off
                    Blend SrcAlpha One//OneMinusSrcAlpha
                    ColorMask RGB
     
     
     
     
       
                CGPROGRAM
     
                #pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade
                //#pragma target 3.0
     
     			void vert(inout appdata_full v){

     				v.normal.xyz = v.normal * -1;

     			}
     
                sampler2D _MainTex;
     
                struct Input {
                    float2 uv_MainTex;
                };
     
                fixed4 _Color;
     
                void surf (Input IN, inout SurfaceOutputStandard o)
                {
                    fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                    o.Albedo = c.rgb;
                    o.Alpha = c.a;
                }
                ENDCG
            }
            FallBack "Standard"
        }
