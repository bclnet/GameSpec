using Microsoft.Maui.Controls;
using StereoKit;

namespace GameSpec.App.Explorer.Views
{
    partial class MainPage
    {
        private HorizontalStackLayout MainTab;
        private ContentView MainTabContent;
        private FileContent FileContent;
        private Entry Status;

        void InitializeComponent()
        {
            MainTab = NameScopeExtensions.FindByName<HorizontalStackLayout>(this, "MainTab");
            MainTabContent = NameScopeExtensions.FindByName<ContentView>(this, "MainTabContent");
            FileContent = NameScopeExtensions.FindByName<FileContent>(this, "FileContent");
            Status = NameScopeExtensions.FindByName<Entry>(this, "Status");
        }

        Pose pose = new(new Vec3(0, 0, -0.6f), Quat.LookDir(-Vec3.Forward));
        Sprite powerButton = Sprite.FromTex(Tex.FromFile("power.png"));

        public void Step()
        {
            UI.WindowBegin("MainPage", ref pose, new Vec2(50 * U.cm, 0));
            //for (var i = 0; i < demoNames.Count; i++)
            //{
            //    if (UI.Button(demoNames[i])) Tests.SetDemoActive(i);
            //    UI.SameLine();
            //}
            UI.NextLine();
            UI.HSeparator();
            if (UI.ButtonImg("Exit", powerButton)) SK.Quit();
            UI.WindowEnd();
        }
    }
}
