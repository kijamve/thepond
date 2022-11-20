Shader "Unlit/simple additive"
{
	Properties{
		_MainTex("Texture to blend", 2D) = "black" {}
		_Color("Tint", Color) = (1,1,1,1)
	}
	SubShader{
		Tags{ "Queue" = "Transparent" }
		Pass{
		Blend SrcAlpha One
			SetTexture[_MainTex]{ combine texture }
		}
	}
	
}
