fixed4 _LineColor;
float _LineSize;

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

v2f vert (appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    //If we are rendering in shaded mode (showing the original mesh renderer)
    //we want to ensure that the wireframe-processed mesh appears "on top" of
    //the original mesh. We achieve this by slightly decreasing the z component
    //(making the vertex closer to the camera) without actually changing its screen space position
    //since the w component remains the same, and thus, after w division, the x and y components
    //won't be affected by our "trick".
    //So, in essence, this just changes the value that gets written to the Z-Buffer
    o.vertex.z -= 0.001;
    o.uv = v.uv;
    return o;
}

float distanceSq(float2 pt1, float2 pt2)
{
    float2 v = pt2 - pt1;
    return dot(v,v);
}

float minimum_distance(float2 v, float2 w, float2 p) {
  // Return minimum distance between line segment vw and point p
  float l2 = distanceSq(v, w);  // i.e. |w-v|^2 -  avoid a sqrt
  // Consider the line extending the segment, parameterized as v + t (w - v).
  // We find projection of point p onto the line. 
  // It falls where t = [(p-v) . (w-v)] / |w-v|^2
  // We clamp t from [0,1] to handle points outside the segment vw.
  float t = max(0, min(1, dot(p - v, w - v) / l2));
  float2 projection = v + t * (w - v);  // Projection falls on the segment
  return distance(p, projection);
}

fixed4 frag (v2f i) : SV_Target
{
    float lineWidthInPixels = _LineSize;
    float lineAntiaAliasWidthInPixels = 1;

    float2 uVector = float2(ddx(i.uv.x),ddy(i.uv.x)); //also known as tangent vector
    float2 vVector = float2(ddx(i.uv.y),ddy(i.uv.y)); //also known as binormal vector

    float vLength = length(uVector);
    float uLength = length(vVector);
    float uvDiagonalLength = length(uVector+vVector);

    float maximumUDistance = lineWidthInPixels * vLength;
    float maximumVDistance = lineWidthInPixels * uLength;
    float maximumUVDiagonalDistance = lineWidthInPixels * uvDiagonalLength;

    float leftEdgeUDistance = i.uv.x;
    float rightEdgeUDistance = (1.0-leftEdgeUDistance);

    float bottomEdgeVDistance = i.uv.y;
    float topEdgeVDistance = 1.0 - bottomEdgeVDistance;

    float minimumUDistance = min(leftEdgeUDistance,rightEdgeUDistance);
    float minimumVDistance = min(bottomEdgeVDistance,topEdgeVDistance);
    float uvDiagonalDistance = minimum_distance(float2(0.0,1.0),float2(1.0,0.0),i.uv);

    float normalizedUDistance = minimumUDistance / maximumUDistance;
    float normalizedVDistance = minimumVDistance / maximumVDistance;
    float normalizedUVDiagonalDistance = uvDiagonalDistance / maximumUVDiagonalDistance;


    float closestNormalizedDistance = min(normalizedUDistance,normalizedVDistance);
    closestNormalizedDistance = min(closestNormalizedDistance,normalizedUVDiagonalDistance);


    float lineAlpha = 1.0 - smoothstep(1.0,1.0 + (lineAntiaAliasWidthInPixels/lineWidthInPixels),closestNormalizedDistance);

    lineAlpha *= _LineColor.a;
    
    return fixed4(_LineColor.rgb,lineAlpha);
}