using Android.App;
using Android.Widget;
using Android.OS;
using Google.VR.SDK.Base;
using Google.VR.SDK.Audio;
using Java.Nio;
using Android.Opengl;
using System;
using Javax.Microedition.Khronos.Egl;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.Nfc)]
[assembly: UsesPermission (Android.Manifest.Permission.Vibrate)]
[assembly: UsesPermission (Android.Manifest.Permission.ReadExternalStorage)]

// Make accelerometer and gyroscope hard requirements for good head tracking.
[assembly: UsesFeature ("android.hardware.sensor.accelerometer", Required=true)]
[assembly: UsesFeature ("android.hardware.sensor.gyroscope", Required = true)]

namespace TreasureHunt
{
    [Activity (Label = "Treasure Hunt", 
               Icon = "@mipmap/icon",
              ScreenOrientation= Android.Content.PM.ScreenOrientation.Landscape,
              ConfigurationChanges= Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    [IntentFilter (new [] { global::Android.Content.Intent.ActionMain }, 
                   Categories = new [] { global::Android.Content.Intent.CategoryLauncher, "com.google.intent.category.CARDBOARD" })]
    public class TreasureHuntActivity : GvrActivity, GvrView.IStereoRenderer
    {
        float [] modelCube;
        float [] modelPosition;

        const string TAG = "TreasureHuntActivity";

        const float Z_NEAR = 0.1f;
        const float Z_FAR = 100.0f;

        const float CAMERA_Z = 0.01f;
        const float TIME_DELTA = 0.3f;

        const float YAW_LIMIT = 0.12f;
        const float PITCH_LIMIT = 0.12f;

        const int COORDS_PER_VERTEX = 3;

        // We keep the light always position just above the user.
        static readonly float [] LIGHT_POS_IN_WORLD_SPACE = new float [] { 0.0f, 2.0f, 0.0f, 1.0f };

        // Convenience vector for extracting the position from a matrix via multiplication.
        static readonly float [] POS_MATRIX_MULTIPLY_VEC = { 0, 0, 0, 1.0f };

        static readonly float MIN_MODEL_DISTANCE = 3.0f;
        static readonly float MAX_MODEL_DISTANCE = 7.0f;

        const string SOUND_FILE = "cube_sound.wav";

        static float [] lightPosInEyeSpace = new float [4];

        FloatBuffer floorVertices, floorColors, floorNormals, cubeVertices, cubeColors, cubeFoundColors, cubeNormals;

        int cubeProgram, floorProgram;

        int cubePositionParam, cubeNormalParam, cubeColorParam, cubeModelParam, 
            cubeModelViewParam, cubeModelViewProjectionParam, cubeLightPosParam,
            floorPositionParam, floorNormalParam, floorColorParam, floorModelParam,
            floorModelViewParam, floorModelViewProjectionParam, floorLightPosParam;

        float [] camera, view, headView, modelViewProjection, 
                    modelView, modelFloor, tempPosition, headRotation;

        float objectDistance = MAX_MODEL_DISTANCE / 2.0f;
        float floorDepth = 20f;

        Vibrator vibrator;

        GvrAudioEngine gvrAudioEngine;
        volatile int soundId = GvrAudioEngine.InvalidId;



        /**
         * Converts a raw text file, saved as a resource, into an OpenGL ES shader.
         */
        int loadGLShader (int type, int resId)
        {
            var code = ReadRawTextFile (resId);
            var shader = GLES20.GlCreateShader (type);
            GLES20.GlShaderSource (shader, code);
            GLES20.GlCompileShader (shader);

            // Get the compilation status.
            var compileStatus = new int [1];
            GLES20.GlGetShaderiv (shader, GLES20.GlCompileStatus, compileStatus, 0);

            // If the compilation failed, delete the shader.
            if (compileStatus [0] == 0) {
                Android.Util.Log.Error (TAG, "Error compiling shader: " + GLES20.GlGetShaderInfoLog (shader));
                GLES20.GlDeleteShader (shader);
                shader = 0;
            }

            if (shader == 0)
                throw new Exception ("Error creating shader.");

            return shader;
        }

        /**
         * Checks if we've had an error inside of OpenGL ES, and if so what that error is.
         */
        static void CheckGLError (string label)
        {
            int error;
            while ((error = GLES20.GlGetError ()) != GLES20.GlNoError) {
                Android.Util.Log.Error (TAG, label + ": glError " + error);
                throw new Exception (label + ": glError " + error);
            }
        }

        /**
         * Sets the view to our GvrView and initializes the transformation matrices we will use
         * to render our scene.
         */
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            InitializeGvrView ();

            modelCube = new float [16];
            camera = new float [16];
            view = new float [16];
            modelViewProjection = new float [16];
            modelView = new float [16];
            modelFloor = new float [16];
            tempPosition = new float [4];

            // Model first appears directly in front of user.
            modelPosition = new float [] { 0.0f, 0.0f, -MAX_MODEL_DISTANCE / 2.0f };
            headRotation = new float [4];
            headView = new float [16];
            vibrator = (Vibrator) GetSystemService (Android.Content.Context.VibratorService);

            // Initialize 3D audio engine.
            gvrAudioEngine = new GvrAudioEngine (this, GvrAudioEngine.RenderingMode.BinauralHighQuality);
        }

        public void InitializeGvrView ()
        {
            SetContentView (Resource.Layout.common_ui);

            var gvrView = FindViewById<GvrView> (Resource.Id.gvr_view);
            gvrView.SetEGLConfigChooser (8, 8, 8, 8, 16, 8);

            gvrView.SetRenderer (this);
            gvrView.SetTransitionViewEnabled (true);
            gvrView.SetOnCardboardBackButtonListener (new CustomRunnable {
                RunHandler = () => {
                    OnBackPressed ();
                }
            });

            GvrView = gvrView;
        }

        class CustomRunnable : Java.Lang.Object, Java.Lang.IRunnable
        {
            public Action RunHandler { get; set; }
            public void Run ()
            {
                RunHandler?.Invoke ();
            }
        }

        protected override void OnPause ()
        {
            gvrAudioEngine.Pause ();
            base.OnPause ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            gvrAudioEngine.Resume ();
        }

        public void OnRendererShutdown ()
        {
            Android.Util.Log.Info (TAG, "onRendererShutdown");
        }

        public void OnSurfaceChanged (int width, int height)
        {
            Android.Util.Log.Info (TAG, "onSurfaceChanged");
        }

        /**
        * Creates the buffers we use to store information about the 3D world.
        *
        * OpenGL doesn't use Java arrays, but rather needs data in a format it can understand.
        * Hence we use ByteBuffers.
        */
        public void OnSurfaceCreated (Javax.Microedition.Khronos.Egl.EGLConfig config)
        {
            Android.Util.Log.Info (TAG, "onSurfaceCreated");

            GLES20.GlClearColor (0.1f, 0.1f, 0.1f, 0.5f); // Dark background so text shows up well.

            var bbVertices = ByteBuffer.AllocateDirect (WorldLayoutData.CUBE_COORDS.Length * 4);
            bbVertices.Order (ByteOrder.NativeOrder ());
            cubeVertices = bbVertices.AsFloatBuffer ();
            cubeVertices.Put (WorldLayoutData.CUBE_COORDS);
            cubeVertices.Position (0);

            var bbColors = ByteBuffer.AllocateDirect (WorldLayoutData.CUBE_COLORS.Length * 4);
            bbColors.Order (ByteOrder.NativeOrder ());
            cubeColors = bbColors.AsFloatBuffer ();
            cubeColors.Put (WorldLayoutData.CUBE_COLORS);
            cubeColors.Position (0);

            var bbFoundColors = ByteBuffer.AllocateDirect (WorldLayoutData.CUBE_FOUND_COLORS.Length * 4);
            bbFoundColors.Order (ByteOrder.NativeOrder ());
            cubeFoundColors = bbFoundColors.AsFloatBuffer ();
            cubeFoundColors.Put (WorldLayoutData.CUBE_FOUND_COLORS);
            cubeFoundColors.Position (0);

            var bbNormals = ByteBuffer.AllocateDirect (WorldLayoutData.CUBE_NORMALS.Length * 4);
            bbNormals.Order (ByteOrder.NativeOrder ());
            cubeNormals = bbNormals.AsFloatBuffer ();
            cubeNormals.Put (WorldLayoutData.CUBE_NORMALS);
            cubeNormals.Position (0);

            // make a floor
            var bbFloorVertices = ByteBuffer.AllocateDirect (WorldLayoutData.FLOOR_COORDS.Length * 4);
            bbFloorVertices.Order (ByteOrder.NativeOrder ());
            floorVertices = bbFloorVertices.AsFloatBuffer ();
            floorVertices.Put (WorldLayoutData.FLOOR_COORDS);
            floorVertices.Position (0);

            var bbFloorNormals = ByteBuffer.AllocateDirect (WorldLayoutData.FLOOR_NORMALS.Length * 4);
            bbFloorNormals.Order (ByteOrder.NativeOrder ());
            floorNormals = bbFloorNormals.AsFloatBuffer ();
            floorNormals.Put (WorldLayoutData.FLOOR_NORMALS);
            floorNormals.Position (0);

            var bbFloorColors = ByteBuffer.AllocateDirect (WorldLayoutData.FLOOR_COLORS.Length * 4);
            bbFloorColors.Order (ByteOrder.NativeOrder ());
            floorColors = bbFloorColors.AsFloatBuffer ();
            floorColors.Put (WorldLayoutData.FLOOR_COLORS);
            floorColors.Position (0);

            int vertexShader = loadGLShader (GLES20.GlVertexShader, Resource.Raw.light_vertex);
            int gridShader = loadGLShader (GLES20.GlFragmentShader, Resource.Raw.grid_fragment);
            int passthroughShader = loadGLShader (GLES20.GlFragmentShader, Resource.Raw.passthrough_fragment);

            cubeProgram = GLES20.GlCreateProgram ();
            GLES20.GlAttachShader (cubeProgram, vertexShader);
            GLES20.GlAttachShader (cubeProgram, passthroughShader);
            GLES20.GlLinkProgram (cubeProgram);
            GLES20.GlUseProgram (cubeProgram);

            CheckGLError ("Cube program");

            cubePositionParam = GLES20.GlGetAttribLocation (cubeProgram, "a_Position");
            cubeNormalParam = GLES20.GlGetAttribLocation (cubeProgram, "a_Normal");
            cubeColorParam = GLES20.GlGetAttribLocation (cubeProgram, "a_Color");

            cubeModelParam = GLES20.GlGetUniformLocation (cubeProgram, "u_Model");
            cubeModelViewParam = GLES20.GlGetUniformLocation (cubeProgram, "u_MVMatrix");
            cubeModelViewProjectionParam = GLES20.GlGetUniformLocation (cubeProgram, "u_MVP");
            cubeLightPosParam = GLES20.GlGetUniformLocation (cubeProgram, "u_LightPos");

            CheckGLError ("Cube program params");

            floorProgram = GLES20.GlCreateProgram ();
            GLES20.GlAttachShader (floorProgram, vertexShader);
            GLES20.GlAttachShader (floorProgram, gridShader);
            GLES20.GlLinkProgram (floorProgram);
            GLES20.GlUseProgram (floorProgram);

            CheckGLError ("Floor program");

            floorModelParam = GLES20.GlGetUniformLocation (floorProgram, "u_Model");
            floorModelViewParam = GLES20.GlGetUniformLocation (floorProgram, "u_MVMatrix");
            floorModelViewProjectionParam = GLES20.GlGetUniformLocation (floorProgram, "u_MVP");
            floorLightPosParam = GLES20.GlGetUniformLocation (floorProgram, "u_LightPos");

            floorPositionParam = GLES20.GlGetAttribLocation (floorProgram, "a_Position");
            floorNormalParam = GLES20.GlGetAttribLocation (floorProgram, "a_Normal");
            floorColorParam = GLES20.GlGetAttribLocation (floorProgram, "a_Color");

            CheckGLError ("Floor program params");

            Matrix.SetIdentityM (modelFloor, 0);
            Matrix.TranslateM (modelFloor, 0, 0, -floorDepth, 0); // Floor appears below user.

            // Avoid any delays during start-up due to decoding of sound files.
            System.Threading.Tasks.Task.Run (() => {
                // Start spatial audio playback of SOUND_FILE at the model postion. The returned
                //soundId handle is stored and allows for repositioning the sound object whenever
                // the cube position changes.
                gvrAudioEngine.PreloadSoundFile (SOUND_FILE);
                soundId = gvrAudioEngine.CreateSoundObject (SOUND_FILE);
                gvrAudioEngine.SetSoundObjectPosition (
                    soundId, modelPosition [0], modelPosition [1], modelPosition [2]);
                gvrAudioEngine.PlaySound (soundId, true /* looped playback */);
            });

            UpdateModelPosition ();

            CheckGLError ("onSurfaceCreated");
        }

        /**
        * Updates the cube model position.
        */
        protected void UpdateModelPosition ()
        {
            Matrix.SetIdentityM (modelCube, 0);
            Matrix.TranslateM (modelCube, 0, modelPosition [0], modelPosition [1], modelPosition [2]);

            // Update the sound location to match it with the new cube position.
            if (soundId != GvrAudioEngine.InvalidId) {
                gvrAudioEngine.SetSoundObjectPosition (
                    soundId, modelPosition [0], modelPosition [1], modelPosition [2]);
            }
            CheckGLError ("updateCubePosition");
        }

        /**
        * Converts a raw text file into a string.
        */
        string ReadRawTextFile (int resId)
        {
            var text = string.Empty;

            using (var inputStream = Resources.OpenRawResource (resId))
            using (var sr = new System.IO.StreamReader (inputStream)) {
                text = sr.ReadToEnd ();
            }

            return text;
        }

        /**
         * Prepares OpenGL ES before we draw a frame.
         */
        public void OnNewFrame (HeadTransform headTransform)
        {
            SetCubeRotation ();

            // Build the camera matrix and apply it to the ModelView.
            Matrix.SetLookAtM (camera, 0, 0.0f, 0.0f, CAMERA_Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);

            headTransform.GetHeadView (headView, 0);

            // Update the 3d audio engine with the most recent head rotation.
            headTransform.GetQuaternion (headRotation, 0);
            gvrAudioEngine.SetHeadRotation (
                headRotation [0], headRotation [1], headRotation [2], headRotation [3]);
            
            // Regular update call to GVR audio engine.
            gvrAudioEngine.Update ();

            CheckGLError ("onReadyToDraw");
        }

        protected void SetCubeRotation ()
        {
            Matrix.RotateM (modelCube, 0, TIME_DELTA, 0.5f, 0.5f, 1.0f);
        }

        /**
        * Draws a frame for an eye.
        *
        * @param eye The eye to render. Includes all required transformations.
        */
        public void OnDrawEye (Eye eye)
        {
            GLES20.GlEnable (GLES20.GlDepthTest);
            GLES20.GlClear (GLES20.GlColorBufferBit | GLES20.GlDepthBufferBit);

            CheckGLError ("colorParam");

            // Apply the eye transformation to the camera.
            Matrix.MultiplyMM (view, 0, eye.GetEyeView (), 0, camera, 0);

            // Set the position of the light
            Matrix.MultiplyMV (lightPosInEyeSpace, 0, view, 0, LIGHT_POS_IN_WORLD_SPACE, 0);

            // Build the ModelView and ModelViewProjection matrices
            // for calculating cube position and light.
            float [] perspective = eye.GetPerspective (Z_NEAR, Z_FAR);
            Matrix.MultiplyMM (modelView, 0, view, 0, modelCube, 0);
            Matrix.MultiplyMM (modelViewProjection, 0, perspective, 0, modelView, 0);

            DrawCube ();

            // Set modelView for the floor, so we draw floor in the correct location
            Matrix.MultiplyMM (modelView, 0, view, 0, modelFloor, 0);
            Matrix.MultiplyMM (modelViewProjection, 0, perspective, 0, modelView, 0);

            DrawFloor ();
        }

        public void OnFinishFrame (Viewport viewport) { }

        /**
         * Draw the cube.
         * We've set all of our transformation matrices. Now we simply pass them into the shader.
         */
        public void DrawCube ()
        {
            GLES20.GlUseProgram (cubeProgram);

            GLES20.GlUniform3fv (cubeLightPosParam, 1, lightPosInEyeSpace, 0);

            // Set the Model in the shader, used to calculate lighting
            GLES20.GlUniformMatrix4fv (cubeModelParam, 1, false, modelCube, 0);

            // Set the ModelView in the shader, used to calculate lighting
            GLES20.GlUniformMatrix4fv (cubeModelViewParam, 1, false, modelView, 0);

            // Set the position of the cube
            GLES20.GlVertexAttribPointer (
                cubePositionParam, COORDS_PER_VERTEX, GLES20.GlFloat, false, 0, cubeVertices);

            // Set the ModelViewProjection matrix in the shader.
            GLES20.GlUniformMatrix4fv (cubeModelViewProjectionParam, 1, false, modelViewProjection, 0);

            // Set the normal positions of the cube, again for shading
            GLES20.GlVertexAttribPointer (cubeNormalParam, 3, GLES20.GlFloat, false, 0, cubeNormals);
            GLES20.GlVertexAttribPointer (cubeColorParam, 4, GLES20.GlFloat, false, 0,
                IsLookingAtObject () ? cubeFoundColors : cubeColors);

            // Enable vertex arrays
            GLES20.GlEnableVertexAttribArray (cubePositionParam);
            GLES20.GlEnableVertexAttribArray (cubeNormalParam);
            GLES20.GlEnableVertexAttribArray (cubeColorParam);

            GLES20.GlDrawArrays (GLES20.GlTriangles, 0, 36);
            CheckGLError ("Drawing cube");
        }

        /**
         * Draw the floor.
         * This feeds in data for the floor into the shader. Note that this doesn't feed in data about
         * position of the light, so if we rewrite our code to draw the floor first, the lighting might
         * look strange.
         */
        public void DrawFloor ()
        {
            GLES20.GlUseProgram (floorProgram);

            // Set ModelView, MVP, position, normals, and color.
            GLES20.GlUniform3fv (floorLightPosParam, 1, lightPosInEyeSpace, 0);
            GLES20.GlUniformMatrix4fv (floorModelParam, 1, false, modelFloor, 0);
            GLES20.GlUniformMatrix4fv (floorModelViewParam, 1, false, modelView, 0);
            GLES20.GlUniformMatrix4fv (floorModelViewProjectionParam, 1, false, modelViewProjection, 0);
            GLES20.GlVertexAttribPointer (
                floorPositionParam, COORDS_PER_VERTEX, GLES20.GlFloat, false, 0, floorVertices);
            GLES20.GlVertexAttribPointer (floorNormalParam, 3, GLES20.GlFloat, false, 0, floorNormals);
            GLES20.GlVertexAttribPointer (floorColorParam, 4, GLES20.GlFloat, false, 0, floorColors);

            GLES20.GlEnableVertexAttribArray (floorPositionParam);
            GLES20.GlEnableVertexAttribArray (floorNormalParam);
            GLES20.GlEnableVertexAttribArray (floorColorParam);

            GLES20.GlDrawArrays (GLES20.GlTriangles, 0, 24);

            CheckGLError ("drawing floor");
        }


        /**
        * Called when the Cardboard trigger is pulled.
        */
        public override void OnCardboardTrigger ()
        {
            Android.Util.Log.Info (TAG, "onCardboardTrigger");

            if (IsLookingAtObject ()) {
                HideObject ();
            }

            // Always give user feedback.
            vibrator.Vibrate (50);
        }

        /**
         * Find a new random position for the object.
         * We'll rotate it around the Y-axis so it's out of sight, and then up or down by a little bit.
         */
        protected void HideObject ()
        {
            var random = new Random ();
            var rotationMatrix = new float [16];
            var posVec = new float [4];

            // First rotate in XZ plane, between 90 and 270 deg away, and scale so that we vary
            // the object's distance from the user.
            float angleXZ = (float)random.NextDouble () * 180 + 90;

            Matrix.SetRotateM (rotationMatrix, 0, angleXZ, 0f, 1f, 0f);
            float oldObjectDistance = objectDistance;
            objectDistance =
                (float)random.NextDouble () * (MAX_MODEL_DISTANCE - MIN_MODEL_DISTANCE) + MIN_MODEL_DISTANCE;

            float objectScalingFactor = objectDistance / oldObjectDistance;
            Matrix.ScaleM (rotationMatrix, 0, objectScalingFactor, objectScalingFactor, objectScalingFactor);
            Matrix.MultiplyMV (posVec, 0, rotationMatrix, 0, modelCube, 12);

            float angleY = (float)random.NextDouble () * 80 - 40; // Angle in Y plane, between -40 and 40.
            angleY = ((float)Math.PI / (float)180) * angleY; // Convert to radians, java has this: (float)Math.ToRadians (angleY);
            float newY = (float)Math.Tan (angleY) * objectDistance;

            modelPosition [0] = posVec [0];
            modelPosition [1] = newY;
            modelPosition [2] = posVec [2];

            UpdateModelPosition ();
        }

        /**
         * Check if user is looking at object by calculating where the object is in eye-space.
         */
        bool IsLookingAtObject ()
        {
            // Convert object space to camera space. Use the headView from onNewFrame.
            Matrix.MultiplyMM (modelView, 0, headView, 0, modelCube, 0);
            Matrix.MultiplyMV (tempPosition, 0, modelView, 0, POS_MATRIX_MULTIPLY_VEC, 0);

            float pitch = (float)Math.Atan2 (tempPosition [1], -tempPosition [2]);
            float yaw = (float)Math.Atan2 (tempPosition [0], -tempPosition [2]);

            return Math.Abs (pitch) < PITCH_LIMIT && Math.Abs (yaw) < YAW_LIMIT;
        }
    }
}
