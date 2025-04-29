using System;
using UnityEditor;
using UnityEngine;

namespace AyahaGraphicDevelopTools.GameViewScreenshot
{
    /// <summary>
    /// EditorでGameViewのスクショを取るEditor拡張
    /// Playをしなくても動く
    /// </summary>
    public class GameViewScreenshotWindow : EditorWindow
    {
        /// <summary>
        /// 保存先のPath
        /// </summary>
        private string _outputPath;

        /// <summary>
        /// 保存先を決めたか？
        /// </summary>
        private bool _isSetOutputPath = false;

        /// <summary>
        /// ウィンドウを出す
        /// </summary>
        [MenuItem("AyahaGraphicDevelopTools/GameViewScreenshotWindow")]
        public static void ShowWindow()
        {
            var window = GetWindow<GameViewScreenshotWindow>("GameViewScreenshotWindow");
            window.titleContent = new GUIContent("GameViewScreenshotWindow");
        }

        /// <summary>
        /// 描画する
        /// </summary>
        private void OnGUI()
        {
            using (new EditorGUILayout.VerticalScope())
            {
                ViewOutputPath();

                ViewFrameRecordButton();
            }
        }

        /// <summary>
        /// OutputPathを設定する
        /// </summary>
        private void ViewOutputPath()
        {
            GUILayout.Label("保存場所");
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("選択中のパス");
                GUILayout.Label(_isSetOutputPath ? _outputPath : "未設定");
            }

            if (GUILayout.Button("保存場所を設定"))
            {
                DateTime now = DateTime.Now;
                var dataName = string.Format("screenshot_{0}_{1:D2}_{2:D2}_{3:D2}_{4:D2}_{5:D2}.png", now.Year,
                    now.Month, now.Day, now.Hour, now.Minute, now.Second);
                _outputPath = EditorUtility.SaveFilePanel(
                    "スクリーンショットの保存先を選択",
                    "",
                    dataName,
                    "png"
                );

                _isSetOutputPath = !String.IsNullOrEmpty(_outputPath);
            }
        }

        /// <summary>
        /// フレームを画像保存するボタン
        /// </summary>
        private void ViewFrameRecordButton()
        {
            if (GUILayout.Button("GameViewを画像保存"))
            {
                CaptureScreenshot();
            }
        }

        /// <summary>
        /// スクリーンショットを撮る
        /// </summary>
        private void CaptureScreenshot()
        {
            ScreenCapture.CaptureScreenshot(_outputPath);
            Debug.LogFormat("Screenshot Save : {0}", _outputPath);
        }
    }
}
