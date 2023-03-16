﻿using StereoKit;
using System.Text;

namespace GameSpec.App.Explorer.Tools
{
    class DebugToolWindow
    {
        static List<(float time, Pose pose)> recordingHead = new();
        static List<(float time, HandJoint[] pose)> recordingHand = new();
        static bool recordHead = false;
        static bool recordHand = false;
        static Pose pose = new(0, 0.3f, .5f, Quat.LookAt(new Vec3(0, 0.3f, .5f), new Vec3(0, 0.3f, 0)));
        static bool screenshots = false;
        static int screenshotId = 1;
        //static AvatarSkeleton avatar = null;

        static DemoAnim<Pose> headAnim = null;
        static DemoAnim<HandJoint[]> handAnim = null;

        public static void Step()
        {
            UI.WindowBegin("Helper", ref pose, new Vec2(20, 0) * U.cm);
            if (UI.Button("Print Screenshot Pose") || Input.Key(Key.F7).IsJustActive()) HeadshotPose();
            if (UI.Button("Print R Hand Pose") || Input.Key(Key.F8).IsJustActive()) HandshotPose(Handed.Right);
            if (UI.Button("Print L Hand Pose") || Input.Key(Key.F9).IsJustActive()) HandshotPose(Handed.Left);
            if (UI.Button("Print R Finger")) Log.Info(Input.Hand(Handed.Right)[FingerId.Index, JointId.Tip].position.ToString());
            if (UI.Toggle("Record Head", ref recordHead)) ToggleRecordHead();
            if (headAnim != null) { UI.SameLine(); if (UI.Button("Play1")) headAnim.Play(); }
            if (UI.Toggle("Record Hand", ref recordHand)) ToggleRecordHand();
            if (handAnim != null) { UI.SameLine(); if (UI.Button("Play2")) handAnim.Play(); }
            UI.Toggle("Enable Screenshots", ref screenshots);
            //var showAvatar = avatar != null;
            //if (UI.Toggle("Show Skeleton", ref showAvatar))
            //{
            //    if (showAvatar) avatar = SK.AddStepper<AvatarSkeleton>();
            //    else { SK.RemoveStepper(avatar); avatar = null; }
            //}
            UI.WindowEnd();

            if (screenshots) TakeScreenshots();
            if (recordHead) RecordHead();
            if (recordHand) RecordHand();
            if (headAnim != null && headAnim.Playing) Renderer.CameraRoot = headAnim.Current.ToMatrix();
            if (handAnim != null && handAnim.Playing) Input.HandOverride(Handed.Right, handAnim.Current);
            else Input.HandClearOverride(Handed.Right);
            ScreenshotPreview();
        }

        static void TakeScreenshots()
        {
            // Take a screenshot on the first frame both hands are gripped
            var valid = Input.Hand(Handed.Left).IsTracked && Input.Hand(Handed.Right).IsTracked;
            var right = Input.Hand(Handed.Right).grip;
            var left = Input.Hand(Handed.Left).grip;
            if (valid && left.IsActive() && right.IsActive() && (left.IsJustActive() || right.IsJustActive()))
            {
                Renderer.Screenshot($"Screenshot{screenshotId}.jpg", Input.Head.position, Input.Head.Forward, 1920 * 2, 1080 * 2);
                screenshotId += 1;
            }
        }

        static void RecordHead()
        {
            var prev = recordingHead[^1].pose;
            var diff = Quat.Difference(Input.Head.orientation, prev.orientation);
            if (Vec3.DistanceSq(Input.Head.position, prev.position) > (2 * U.cm * 2 * U.cm) || (diff.w) * (diff.w) < (0.8f * 0.8f)) recordingHead.Add((Time.Totalf, Input.Head));

        }

        static void ToggleRecordHead()
        {
            recordingHead.Add((Time.Totalf, Input.Head));
            if (!recordHead)
            {
                var rootTime = recordingHead[0].time;
                var result = "DemoAnim<Pose> anim = new DemoAnim<Pose>(Pose.Lerp,";
                for (var i = 0; i < recordingHead.Count; i++)
                {
                    recordingHead[i] = (recordingHead[i].time - rootTime, recordingHead[i].pose);
                    var f = recordingHead[i];
                    result += $"({f.time}f, new Pose(V.XYZ({f.pose.position.x:0.000}f,{f.pose.position.y:0.000}f,{f.pose.position.z:0.000}f), new Quat({f.pose.orientation.x:0.000}f,{f.pose.orientation.y:0.000}f,{f.pose.orientation.z:0.000}f,{f.pose.orientation.w:0.000}f)))";
                    if (i < recordingHead.Count - 1) result += ",";
                }
                result += ");";
                Log.Info(result);
                headAnim = new DemoAnim<Pose>(Pose.Lerp, recordingHead.ToArray());
                recordingHead.Clear();
            }
        }

        static void RecordHand()
        {
            if (Time.Totalf - recordingHand[recordingHand.Count - 1].time > 0.2f)
            {
                var h = Input.Hand(Handed.Right);
                var joints = new HandJoint[27];
                Array.Copy(h.fingers, 0, joints, 0, 25);
                joints[25] = new HandJoint(h.palm.position, h.palm.orientation, 0);
                joints[26] = new HandJoint(h.wrist.position, h.wrist.orientation, 0);
                recordingHand.Add((Time.Totalf, joints));
            }
        }

        static void ToggleRecordHand()
        {
            var h = Input.Hand(Handed.Right);
            var joints = new HandJoint[27];
            Array.Copy(h.fingers, 0, joints, 0, 25);
            joints[25] = new HandJoint(h.palm.position, h.palm.orientation, 0);
            joints[26] = new HandJoint(h.wrist.position, h.wrist.orientation, 0);
            recordingHand.Add((Time.Totalf, joints));
            if (!recordHand)
            {
                var rootTime = recordingHand[0].time;
                var result = new StringBuilder();
                result.Append("DemoAnim<HandJoint[]> anim = new DemoAnim<HandJoint[]>(JointsLerp,");
                for (var i = 0; i < recordingHand.Count; i++)
                {
                    recordingHand[i] = (recordingHand[i].time - rootTime, recordingHand[i].pose);
                    var f = recordingHand[i];
                    result.Append($"({f.time}f,new HandJoint[]{{");
                    for (var j = 0; j < f.pose.Length; j++)
                    {
                        result.Append($"new HandJoint(new Vec3({f.pose[j].position.x:0.000}f,{f.pose[j].position.y:0.000}f,{f.pose[j].position.z:0.000}f), new Quat({f.pose[j].orientation.x:0.000}f,{f.pose[j].orientation.y:0.000}f,{f.pose[j].orientation.z:0.000}f,{f.pose[j].orientation.w:0.000}f), {f.pose[j].radius:0.000}f)");
                        if (j < f.pose.Length - 1) result.Append(',');
                    }
                    result.Append("})");
                    if (i < recordingHand.Count - 1) result.Append(',');
                }
                result.Append(");");
                Log.Info(result.ToString());

                handAnim = new DemoAnim<HandJoint[]>(JointsLerp, recordingHand.ToArray());
                recordingHand.Clear();
            }
        }

        static void HeadshotPose()
        {
            var pos = Input.Head.position + Input.Head.Forward * 10 * U.cm;
            var fwd = pos + Input.Head.Forward;
            Log.Info($"Tests.Screenshot(\"image.jpg\", 600, 600, new Vec3({pos.x:0.000}f, {pos.y:0.000}f, {pos.z:0.000}f), new Vec3({fwd.x:0.000}f, {fwd.y:0.000}f, {fwd.z:0.000}f));");
            PreviewScreenshot(pos, fwd);
        }

        static void HandshotPose(Handed hand)
        {
            var h = Input.Hand(hand);
            var joints = new HandJoint[27];
            Array.Copy(h.fingers, 0, joints, 0, 25);
            joints[25] = new HandJoint(h.palm.position, h.palm.orientation, 0);
            joints[26] = new HandJoint(h.wrist.position, h.wrist.orientation, 0);
            recordingHand.Add((Time.Totalf, joints));

            var result = ($"Tests.Hand(new HandJoint[]{{");
            for (var j = 0; j < joints.Length; j++)
            {
                result += $"new HandJoint(V.XYZ({joints[j].position.x:0.000}f,{joints[j].position.y:0.000}f,{joints[j].position.z:0.000}f), new Quat({joints[j].orientation.x:0.000}f,{joints[j].orientation.y:0.000}f,{joints[j].orientation.z:0.000}f,{joints[j].orientation.w:0.000}f), {joints[j].radius:0.000}f)";
                if (j < joints.Length - 1) result += ",";
            }
            result += "});";
            Log.Info(result);
        }

        public static HandJoint[] JointsLerp(HandJoint[] a, HandJoint[] b, float t)
        {
            var result = new HandJoint[a.Length];
            for (var i = 0; i < a.Length; i++)
            {
                result[i].position = Vec3.Lerp(a[i].position, b[i].position, t);
                result[i].orientation = Quat.Slerp(a[i].orientation, b[i].orientation, t);
                result[i].radius = SKMath.Lerp(a[i].radius, b[i].radius, t);
            }
            return result;
        }

        #region Screenshot

        static Sprite screenshot;
        static Pose screenshotPose;
        static bool screenshotVisible;

        static void PreviewScreenshot(Vec3 from, Vec3 at)
        {
            var path = Path.GetTempFileName();
            path = Path.ChangeExtension(path, "jpg");
            Renderer.Screenshot(path, from, at, 600, 600);
            Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                screenshot = Sprite.FromTex(Tex.FromFile(path));
            });
            if (!screenshotVisible)
            {
                screenshotPose.position = Input.Head.position + Input.Head.Forward * 0.3f;
                screenshotPose.orientation = Quat.LookAt(screenshotPose.position, Input.Head.position);
            }
            screenshotVisible = true;
        }

        static void ScreenshotPreview()
        {
            if (!screenshotVisible) return;
            UI.WindowBegin("Screenshot", ref screenshotPose, new Vec2(20, 0) * U.cm);
            if (screenshot != null) UI.Image(screenshot, new Vec2(18, 0) * U.cm);
            if (UI.Button("Close")) { screenshotVisible = false; screenshot = null; }
            UI.WindowEnd();
        }

        #endregion
    }
}