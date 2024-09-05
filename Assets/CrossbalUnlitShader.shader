Shader "Custom/CrossbarUnlitShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}  // Base texture
        _Width ("Screen Width", Float) = 0     // Screen width
        _Height ("Screen Height", Float) = 0   // Screen height
        _Thickness ("Crossbar Thickness", Float) = 0  // Crossbar thickness
        _Size ("Crossbar Size", Float) = 0     // Crossbar size
    }
    SubShader {
        Tags { "RenderType"="Opaque" }  // Make sure this shader is considered opaque
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex shader
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Width;    
            float _Height;   
            float _Thickness;
            float _Size;

            // Fragment shader (pixel shader)
            fixed4 frag(v2f i) : SV_Target {
                // Sample the base texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Define crossbar screen areas
                float xStart = (1 - _Size / _Width) / 2;
                float xEnd = (1 + _Size / _Width) / 2;
                float yStart = (1 - _Thickness / _Height) / 2;
                float yEnd = (1 + _Thickness / _Height) / 2;

                // Horizontal crossbar color inversion
                if (i.uv.x > xStart && i.uv.x < xEnd && i.uv.y > yStart && i.uv.y < yEnd) {
                    col.rgb = 1.0 - col.rgb;  // Invert colors
                }

                // Redefine for the vertical crossbar and invert colors again
                xStart = (1 - _Thickness / _Width) / 2;
                xEnd = (1 + _Thickness / _Width) / 2;
                yStart = (1 - _Size / _Height) / 2;
                yEnd = (1 + _Size / _Height) / 2;

                // Vertical crossbar color inversion
                if (i.uv.x > xStart && i.uv.x < xEnd && i.uv.y > yStart && i.uv.y < yEnd) {
                    col.rgb = 1.0 - col.rgb;  // Invert colors
                }

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}