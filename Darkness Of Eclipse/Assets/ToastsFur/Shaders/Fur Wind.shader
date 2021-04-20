Shader "ToastsFur/Fur Wind" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
	_Parallax("Height", Range(0.0, 1.0)) = 0.5
	_EdgeLength ("EdgeLength", Range(1,10)) = 3
	_WindScale("WindScale", Range(0.0, 0.5)) = 0.2
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_ParallaxMap("Heightmap (A)", 2D) = "black" {}
	_WindMap("Windmap", 2D) = "black" {}

}
SubShader { 
	Tags { "RenderType"="Opaque" }
	LOD 800
	
CGPROGRAM
#pragma surface surf BlinnPhong addshadow vertex:disp tessellate:tessEdge
#include "Tessellation.cginc"

struct appdata {
	float4 vertex : POSITION;
	float4 tangent : TANGENT;
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD0;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
};

float _EdgeLength;
float _Parallax;
float _WindScale;

float4 tessEdge (appdata v0, appdata v1, appdata v2)
{
	return UnityEdgeLengthBasedTessCull (v0.vertex, v1.vertex, v2.vertex, _EdgeLength, _Parallax * 1.5f);
}

sampler2D _ParallaxMap;
uniform float4 _ParallaxMap_ST;
sampler2D _WindMap;
uniform float4 _WindMap_ST;

void disp (inout appdata v)
{
	float3 norm = normalize(v.normal);
	float3 tang = normalize(v.tangent);
	float3 perp = normalize(cross(norm, tang));
	float2 uv1 = TRANSFORM_TEX(v.texcoord.xy, _ParallaxMap);
	float d = tex2Dlod(_ParallaxMap, float4(uv1,0,0)).a * _Parallax;
	v.vertex.xyz += norm * d;
	float2 uv = TRANSFORM_TEX(v.texcoord.xy, _WindMap);
	float3 w = tex2Dlod(_WindMap, float4(uv, 0, 0)).rgb - float3(0.5,0.5,0.5);
	v.vertex.xyz += (w.x * perp + w.y * tang + w.z * norm) * _WindScale;
	//float w = tex2Dlod(_WindMap, float4(uv, 0, 1)).a;
	//v.vertex.xyz += w * tang * _WindScale;
}

sampler2D _MainTex;
fixed4 _Color;
half _Shininess;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a * _Color.a;
	o.Specular = _Shininess;
}
ENDCG
}

FallBack "Bumped Specular"
}
