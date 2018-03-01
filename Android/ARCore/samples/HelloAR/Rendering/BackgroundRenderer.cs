using System;
using Android.Content;
using Android.Opengl;
using Google.AR.Core;
using Java.Nio;

namespace HelloAR
{
	public class BackgroundRenderer
	{
		const string TAG = "BACKGROUNDRENDERER";

		const int COORDS_PER_VERTEX = 3;
		const int TEXCOORDS_PER_VERTEX = 2;
		const int FLOAT_SIZE = 4;

		FloatBuffer mQuadVertices;
		FloatBuffer mQuadTexCoord;
		FloatBuffer mQuadTexCoordTransformed;

		private int mQuadProgram;

		private int mQuadPositionParam;
		private int mQuadTexCoordParam;
		private int mTextureTarget = GLES11Ext.GlTextureExternalOes;

		public BackgroundRenderer()
		{
		}

		public int TextureId
		{
			get; private set;
		} = -1;

		/**
		 * Allocates and initializes OpenGL resources needed by the background renderer.  Must be
		 * called on the OpenGL thread, typically in
		 * {@link GLSurfaceView.Renderer#onSurfaceCreated(GL10, EGLConfig)}.
		 *
		 * @param context Needed to access shader source.
		 */
		public void CreateOnGlThread(Context context)
		{
			// Generate the background texture.
			var textures = new int[1];
			GLES20.GlGenTextures(1, textures, 0);
			TextureId = textures[0];
			GLES20.GlBindTexture(mTextureTarget, TextureId);
			GLES20.GlTexParameteri(mTextureTarget, GLES20.GlTextureWrapS, GLES20.GlClampToEdge);
			GLES20.GlTexParameteri(mTextureTarget, GLES20.GlTextureWrapT, GLES20.GlClampToEdge);
			GLES20.GlTexParameteri(mTextureTarget, GLES20.GlTextureMinFilter, GLES20.GlNearest);
			GLES20.GlTexParameteri(mTextureTarget, GLES20.GlTextureMagFilter, GLES20.GlNearest);

			int numVertices = 4;
			if (numVertices != QUAD_COORDS.Length / COORDS_PER_VERTEX)
				throw new Exception("Unexpected number of vertices in BackgroundRenderer.");

			var bbVertices = ByteBuffer.AllocateDirect(QUAD_COORDS.Length * FLOAT_SIZE);
			bbVertices.Order(ByteOrder.NativeOrder());
			mQuadVertices = bbVertices.AsFloatBuffer();
			mQuadVertices.Put(QUAD_COORDS);
			mQuadVertices.Position(0);

			var bbTexCoords = ByteBuffer.AllocateDirect(numVertices * TEXCOORDS_PER_VERTEX * FLOAT_SIZE);
			bbTexCoords.Order(ByteOrder.NativeOrder());
			mQuadTexCoord = bbTexCoords.AsFloatBuffer();
			mQuadTexCoord.Put(QUAD_TEXCOORDS);
			mQuadTexCoord.Position(0);

			var bbTexCoordsTransformed = ByteBuffer.AllocateDirect(numVertices * TEXCOORDS_PER_VERTEX * FLOAT_SIZE);
			bbTexCoordsTransformed.Order(ByteOrder.NativeOrder());
			mQuadTexCoordTransformed = bbTexCoordsTransformed.AsFloatBuffer();

			int vertexShader = ShaderUtil.LoadGLShader(TAG, context,
					GLES20.GlVertexShader, Resource.Raw.screenquad_vertex);
			int fragmentShader = ShaderUtil.LoadGLShader(TAG, context,
					GLES20.GlFragmentShader, Resource.Raw.screenquad_fragment_oes);

			mQuadProgram = GLES20.GlCreateProgram();
			GLES20.GlAttachShader(mQuadProgram, vertexShader);
			GLES20.GlAttachShader(mQuadProgram, fragmentShader);
			GLES20.GlLinkProgram(mQuadProgram);
			GLES20.GlUseProgram(mQuadProgram);

			ShaderUtil.CheckGLError(TAG, "Program creation");

			mQuadPositionParam = GLES20.GlGetAttribLocation(mQuadProgram, "a_Position");
			mQuadTexCoordParam = GLES20.GlGetAttribLocation(mQuadProgram, "a_TexCoord");

			ShaderUtil.CheckGLError(TAG, "Program parameters");
		}

		/**
		 * Draws the AR background image.  The image will be drawn such that virtual content rendered
		 * with the matrices provided by {@link Frame#getViewMatrix(float[], int)} and
		 * {@link Session#getProjectionMatrix(float[], int, float, float)} will accurately follow
		 * static physical objects.  This must be called <b>before</b> drawing virtual content.
		 *
		 * @param frame The last {@code Frame} returned by {@link Session#update()}.
		 */
		public void Draw(Frame frame)
		{
			// If display rotation changed (also includes view size change), we need to re-query the uv
			// coordinates for the screen rect, as they may have changed as well.
			if (frame.HasDisplayGeometryChanged)
			{
				frame.TransformDisplayUvCoords(mQuadTexCoord, mQuadTexCoordTransformed);
			}

			// No need to test or write depth, the screen quad has arbitrary depth, and is expected
			// to be drawn first.
			GLES20.GlDisable(GLES20.GlDepthTest);
			GLES20.GlDepthMask(false);

			GLES20.GlBindTexture(GLES11Ext.GlTextureExternalOes, TextureId);

			GLES20.GlUseProgram(mQuadProgram);

			// Set the vertex positions.
			GLES20.GlVertexAttribPointer(
				mQuadPositionParam, COORDS_PER_VERTEX, GLES20.GlFloat, false, 0, mQuadVertices);

			// Set the texture coordinates.
			GLES20.GlVertexAttribPointer(mQuadTexCoordParam, TEXCOORDS_PER_VERTEX,
					GLES20.GlFloat, false, 0, mQuadTexCoordTransformed);

			// Enable vertex arrays
			GLES20.GlEnableVertexAttribArray(mQuadPositionParam);
			GLES20.GlEnableVertexAttribArray(mQuadTexCoordParam);

			GLES20.GlDrawArrays(GLES20.GlTriangleStrip, 0, 4);

			// Disable vertex arrays
			GLES20.GlDisableVertexAttribArray(mQuadPositionParam);
			GLES20.GlDisableVertexAttribArray(mQuadTexCoordParam);

			// Restore the depth state for further drawing.
			GLES20.GlDepthMask(true);
			GLES20.GlEnable(GLES20.GlDepthTest);

			ShaderUtil.CheckGLError(TAG, "Draw");
		}

		static readonly float[] QUAD_COORDS = new float[]{
			-1.0f, -1.0f, 0.0f,
			-1.0f, +1.0f, 0.0f,
			+1.0f, -1.0f, 0.0f,
			+1.0f, +1.0f, 0.0f,
		};

		static readonly float[] QUAD_TEXCOORDS = new float[]{
			0.0f, 1.0f,
			0.0f, 0.0f,
			1.0f, 1.0f,
			1.0f, 0.0f,
		};

	}
}
