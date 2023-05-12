Shader "Mask Paint Effect"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_FillAmount("Fill Limit", float) = 0
		_Opacity("Opacity Amount", Range(0,1)) = 1
		_TopColor("Top Color", Color) = (1,0,0,1)
		_BottomColor("Bottom Color", Color) = (0,1,0,1)
		_Freq("Frequency", float) = 0.5
		_Amp("Amplitude", float) = 1
		_Speed("Speed", float) = 10
		_Freq2("Frequency2", float) = 0.5
		_Amp2("Amplitude2", float) = 1
		_Speed2("Speed2", float) = 10
	}

		SubShader
		{
			Tags { "Queue" = "Transparent" }

			CGPROGRAM

			#pragma surface surf StandardSpecular fullforwardshadows //alpha:fade
			#pragma target 3.0

			sampler2D _MainTex;
			float _FillAmount;
			fixed4 _TopColor;
			fixed4 _BottomColor;
			float _Freq;
			float _Amp;
			float _Speed;
			float _Freq2;
			float _Amp2;
			float _Speed2;
			float _Opacity;

			struct Input
			{
				float2 uv_MainTex;
				float3 worldPos;
			};

			void surf(Input IN, inout SurfaceOutputStandardSpecular o)
			{
				float z = IN.worldPos.y;
				float y = sin(_Speed * IN.worldPos.x * _Freq) * _Amp;
				z += y;

				o.Alpha = _Opacity;
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex);

				o.Albedo += ( z > _FillAmount) ? _TopColor : _BottomColor;

				z = IN.worldPos.y;
				y = sin(_Speed2 * IN.worldPos.x * _Freq2) * _Amp2;
				z += y;

				o.Albedo += (z > _FillAmount) ? _TopColor : _BottomColor;
			}

			ENDCG
	}
		Fallback "Diffuse"
}