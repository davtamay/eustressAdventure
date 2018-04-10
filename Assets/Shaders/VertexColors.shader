Shader "Custom/VertexColors" {
	Properties {
		
		_MainTint ("Global Color Tint", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert


		half4 _MainTint;

		struct Input {
			
			float4 vertColor;

		};


		void vert (inout appdata_full v, out Input o)
		{
			o.vertColor = v.color;
		}
		void surf (Input IN, inout SurfaceOutput o) 
		{
			
			o.Albedo = IN.vertColor.rgb * _MainTint.rgb;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
