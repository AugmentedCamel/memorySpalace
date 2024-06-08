

Shader "SoapBubbleShader2"
{
	Properties
	{
		[Header(Base)]
		_BaseTex3("HightLight(R)", 2D) = "black" {}
		_pow("POW", Range(0.0, 20.0)) = 1
		[Header(NoiseDeform)]

		_deformX("DeformX", Range(0, 10)) = 0
		_deformY("DeformY", Range(0, 10)) = 0
		_deform_strength("Deform Strength", Range(0, 360)) = 1
		
		[Header(Mat)]
		_MColor("Mat Color", Color) = (0.5,0.5,0.5,1)
		[NoScaleOffset]_MatCap ("MatCap which has alpha (RGBA)", 2D) = "white" {}
		

		_powh("HightLight POW", Range(0.0, 20.0)) = 1
		[Header(Rim)]
		_RimColor("Rim Color", Color) = (1,1,1,1)
		rimWidth("rimWidth", Range(0.0, 2.0)) = 0.75
		_AlphaMode("Alpha", Range(0,1)) = 0.00

		[Header(Project)]
		[NoScaleOffset]_Project("Project(RGB)", 2D) = "white" {}
		_tile("Tile", Range(0.0, 20.0)) = 1
		_tileOffsetX("OffsetX", Range(0, 1)) = 0
		_tileOffsetY("OffseeY", Range(0, 1)) = 0
		[Header(Distortion)]
		[NoScaleOffset]_ProjectUV("UV Dis(R)", 2D) = "black" {}
		_tileUV("UV Dis Tile", Range(0.0, 20.0)) = 1
		_flow_offset("flow_offset", Range(-10, 10)) = 0
		_flow_strength("flow_strength", Range(-10, 10)) = 0.5
	

	}
	
	Subshader
	{
		Tags { "Queue" = "Transparent+1" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		



//shell
		Pass
		{
			Cull Back
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				float4 pos	: SV_POSITION;
				float4 cap	: TEXCOORD0;
				float2 uv	: TEXCOORD2;
				float4 uv_object : TEXCOORD1;
				fixed4 color : COLOR;
			};
			uniform float rimWidth;
			
			float _AlphaMode;
			uniform sampler2D _BaseTex3;
	
			float4 _BaseTex3_ST;
			float _pow;
			float _tile;
			float _tileUV;
			float _flow_offset;
			float _flow_strength;
		
			float _tileOffsetX;
			float _tileOffsetY;
			float _powh;
			float _deform_strength;
			float _deformX;
			float _deformY;
			v2f vert(appdata v)
			{
				v2f o;
				float3 worldNorm = normalize(unity_WorldToObject[0].xyz * v.normal.x + unity_WorldToObject[1].xyz * v.normal.y + unity_WorldToObject[2].xyz * v.normal.z);
				worldNorm = mul((float3x3)UNITY_MATRIX_V, worldNorm);
				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float dotProduct = 1 - dot(v.normal, viewDir);
				o.color.r = smoothstep(1 - rimWidth, 1.0, dotProduct);
				o.color.g = dotProduct;
				o.cap.xy = worldNorm.xy * 0.5 + 0.5;
				o.cap.z = 1 - abs(v.normal.y);
				o.cap.w = v.color.a;

				float4 move = float4(_deformX, _deformY,0,0);
				float n = tex2Dlod(_BaseTex3, o.cap + move* _Time.y).r;
 				o.pos = UnityObjectToClipPos(v.vertex + v.normal * _deform_strength * n);
				
				o.uv = TRANSFORM_TEX(v.texcoord, _BaseTex3);
				o.uv_object = v.vertex*0.5+0.5;
				return o;
			}

			float4 _RimColor;
			uniform float4 _MColor;
			uniform sampler2D _MatCap;
			uniform sampler2D _Project;
			uniform sampler2D _ProjectUV;

			float4 frag(v2f i) : SV_Target
			{

				float2 offset   = float2(_tileOffsetX, _tileOffsetY);
				fixed4 t1 = tex2D(_ProjectUV, (frac(_Time.y * _flow_offset) + i.uv_object.rb * _tileUV));
				fixed4 t2 = tex2D(_ProjectUV, (frac(_Time.y * _flow_offset) + i.uv * _tileUV * 0.5));
				fixed4 p1 = tex2D(_Project, offset+i.uv_object.rb * _tile + t1 * _flow_strength);
				fixed4 p2 = tex2D(_Project, offset+i.uv * _tile * 0.5 + t2 * _flow_strength);


				

				fixed4 mc1 = tex2D(_MatCap, i.cap.xy);

			
				fixed4 mc = saturate(mc1);
				
				fixed4 mt = tex2D(_BaseTex3, i.cap.xy + t1 * _flow_strength);
				fixed mtp = pow(mt.r, _pow);
				fixed highlight = pow(mc.a, _powh);
				fixed4 cc = _MColor * mc*2.7;
				
				cc.rgb = lerp(p1,p2, i.cap.z)* _MColor + _RimColor.rgb*_RimColor.a* i.color.r + mtp + highlight;
				cc.a =  lerp(cc.r, 1, _AlphaMode)* mc.r*i.cap.w + _RimColor.r * _RimColor.a * i.color.r + mtp + highlight;
				return cc;
			}
			ENDCG
		}

		
	}
	
	Fallback "VertexLit"
}
