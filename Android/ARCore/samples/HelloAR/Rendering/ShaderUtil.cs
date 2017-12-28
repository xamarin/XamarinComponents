using System;
using System.IO;
using Android.Content;
using Android.Opengl;
using Android.Util;

namespace HelloAR
{
	public static class ShaderUtil
	{
		public static int LoadGLShader (string tag, Context context, int type, int resId)
		{
			var code =  ReadRawTextFile(context, resId);
			var shader = GLES20.GlCreateShader(type);

			GLES20.GlShaderSource(shader, code);
			GLES20.GlCompileShader(shader);

			var compileStatus = new int[1];
			GLES20.GlGetShaderiv(shader, GLES20.GlCompileStatus, compileStatus, 0);

			if (compileStatus[0] == 0) {
				GLES20.GlDeleteShader(shader);
				shader = 0;
			}

			if (shader == 0)
				throw new Exception("Error creating shader");

			return shader;
		}

		public static void CheckGLError (string tag, string label)
		{
			int error;
			while ((error = GLES20.GlGetError()) != GLES20.GlNoError)
			{
				Log.Error(tag, label + ": glError " + error);
				throw new Exception(label + ": glError " + error);
			}
		}


		static string ReadRawTextFile (Context context, int resId)
		{
			string result = null;

			using (var rs = context.Resources.OpenRawResource(resId)) 
			using (var sr = new StreamReader (rs)) {
				result = sr.ReadToEnd();
			}

			return result;
		}
	}
}
