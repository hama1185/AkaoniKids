Shader "Custom/red"
{
    Properties {
    	_Darkness("Dark", Range(0, 0.1)) = 0.04
    	_Strength("Strength", Range(0.25, 1.0)) = 0.5
        _MainTex("MainTex", 2D) = ""{}
    }

    SubShader {
        Pass {
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert_img
            #pragma fragment frag

            sampler2D _MainTex;
            half _Darkness;
            half _Strength;

            fixed4 frag(v2f_img i) : COLOR {
                fixed4 c = tex2D(_MainTex, i.uv);
                half R = c.r;
                R = R + _Darkness * 2.5;
                R = ( R > 1.0 ) ? 1.0 : R;
                half G = (c.g - _Darkness < 0) ? 0 : c.g - _Darkness;
                half B = (c.b - _Darkness < 0) ? 0 : c.b - _Darkness;
                // G *= _Strength;
                // B *= _Strength;

                c.rgb = fixed3(R, G, B);
                return c;
            }

            ENDCG
        }
    }
}