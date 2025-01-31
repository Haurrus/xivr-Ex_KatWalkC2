﻿using System.Diagnostics;
using System.Numerics;
using Dalamud.Interface;
using ImGuiNET;
using xivr.Structures;

namespace xivr
{
    public static class PluginUI
    {
        public static bool isVisible = false;

        public static void Draw(uiOptionStrings lngOptions)
        {
            if (!isVisible)
                return;

            ImGui.SetNextWindowSize(new Vector2(750, 700) * ImGuiHelpers.GlobalScale, ImGuiCond.FirstUseEver);
            ImGui.SetNextWindowSizeConstraints(new Vector2(750, 700) * ImGuiHelpers.GlobalScale, new Vector2(9999));
            //if (ImGui.Begin("Configuration", ref isVisible, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            

            if (ImGui.Begin("Configuration", ref isVisible, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                ImGui.BeginChild("Outer", new Vector2(730, 680) * ImGuiHelpers.GlobalScale, true);

                ShowKofi(lngOptions);

                ImGui.BeginChild("VR", new Vector2(350, 200) * ImGuiHelpers.GlobalScale, true);

                if (ImGui.Checkbox(lngOptions.isEnabled_Line1, ref xivr_Ex.cfg.data.isEnabled))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.isAutoEnabled_Line1, ref xivr_Ex.cfg.data.isAutoEnabled))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.mode2d_Line1, ref xivr_Ex.cfg.data.mode2d))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.autoResize_Line1, ref xivr_Ex.cfg.data.autoResize))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.autoMove_Line1, ref xivr_Ex.cfg.data.autoMove))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Button(lngOptions.runRecenter_Line1))
                    xivr_Ex.cfg.data.runRecenter = true;

                if (ImGui.Checkbox(lngOptions.vLog_Line1, ref xivr_Ex.cfg.data.vLog))
                    xivr_Ex.Plugin.doUpdate = true;

                ImGui.EndChild();

                ImGui.SameLine();

                ImGui.BeginChild("Misc", new Vector2(350, 200) * ImGuiHelpers.GlobalScale, true);

                if (ImGui.Checkbox(lngOptions.motioncontrol_Line1, ref xivr_Ex.cfg.data.motioncontrol))
                {
                    xivr_Ex.cfg.data.hmdPointing = !xivr_Ex.cfg.data.motioncontrol;
                    xivr_Ex.Plugin.doUpdate = true;
                }

                if (ImGui.Checkbox(lngOptions.conloc_Line1, ref xivr_Ex.cfg.data.conloc))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.thrmillloc_Line1, ref xivr_Ex.cfg.data.thrmillloc))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.hmdloc_Line1, ref xivr_Ex.cfg.data.hmdloc))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.vertloc_Line1, ref xivr_Ex.cfg.data.vertloc))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.showWeaponInHand_Line1, ref xivr_Ex.cfg.data.showWeaponInHand))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.forceFloatingScreen_Line1, ref xivr_Ex.cfg.data.forceFloatingScreen))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.forceFloatingInCutscene_Line1, ref xivr_Ex.cfg.data.forceFloatingInCutscene))
                    xivr_Ex.Plugin.doUpdate = true;

                if (ImGui.Checkbox(lngOptions.recalibrate_threadmill_Line1, ref xivr_Ex.cfg.data.recalibrate_threadmill))
                    xivr_Ex.Plugin.doUpdate = true;

                ImGui.EndChild();

                DrawLocks(lngOptions);
                ImGui.SameLine();
                DrawUISetings(lngOptions);

                ImGui.EndChild();

                if (xivr_Ex.Plugin.doUpdate == true)
                    xivr_Ex.cfg.Save();

                ImGui.End();
            }
        }

        public static void DrawLocks(uiOptionStrings lngOptions)
        {
            ImGui.BeginChild("Snap Turning", new Vector2(350, 200) * ImGuiHelpers.GlobalScale, true);

            if(ImGui.Checkbox(lngOptions.horizonLock_Line1, ref xivr_Ex.cfg.data.horizonLock))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.snapRotateAmountX_Line1, ref xivr_Ex.cfg.data.horizontalLock))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.snapRotateAmountX_Line2); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawLocks:hsa", ref xivr_Ex.cfg.data.snapRotateAmountX, 0, 90, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.snapRotateAmountY_Line1, ref xivr_Ex.cfg.data.verticalLock))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.snapRotateAmountY_Line2); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawLocks:vsa", ref xivr_Ex.cfg.data.snapRotateAmountY, 0, 90, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.EndChild();
        }



        public static void DrawUISetings(uiOptionStrings lngOptions)
        {
            ImGui.BeginChild("UI", new Vector2(350, 400) * ImGuiHelpers.GlobalScale, true);

            ImGui.Text(lngOptions.uiOffsetZ_Line1); ImGui.SameLine(); 
            if(ImGui.SliderFloat("##DrawUISetings:uizoff", ref xivr_Ex.cfg.data.uiOffsetZ, 0, 100, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.uiOffsetScale_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:uizscale", ref xivr_Ex.cfg.data.uiOffsetScale, 1, 5, "%.00f"))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.uiDepth_Line1, ref xivr_Ex.cfg.data.uiDepth))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.ipdOffset_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:ipdoff", ref xivr_Ex.cfg.data.ipdOffset, -10, 10, "%f"))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.swapEyes_Line1, ref xivr_Ex.cfg.data.swapEyes))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.swapEyesUI_Line1, ref xivr_Ex.cfg.data.swapEyesUI))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.offsetAmountX_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:xoff", ref xivr_Ex.cfg.data.offsetAmountX, -150, 150, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.offsetAmountY_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:yoff", ref xivr_Ex.cfg.data.offsetAmountY, -150, 150, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.offsetAmountZ_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:zoff", ref xivr_Ex.cfg.data.offsetAmountZ, -150, 150, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.offsetAmountYFPS_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:fpsyoff", ref xivr_Ex.cfg.data.offsetAmountYFPS, -150, 150, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.offsetAmountZFPS_Line1); ImGui.SameLine();
            if (ImGui.SliderFloat("##DrawUISetings:fpszoff", ref xivr_Ex.cfg.data.offsetAmountZFPS, -150, 150, "%.0f"))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.Text(lngOptions.targetCursorSize_Line1); ImGui.SameLine();
            if (ImGui.SliderInt("##DrawUISetings:targetcur", ref xivr_Ex.cfg.data.targetCursorSize, 50, 255))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.ultrawideshadows_Line1, ref xivr_Ex.cfg.data.ultrawideshadows))
                xivr_Ex.Plugin.doUpdate = true;

            if (ImGui.Checkbox(lngOptions.asymmetricProjection_Line1, ref xivr_Ex.cfg.data.asymmetricProjection))
                xivr_Ex.Plugin.doUpdate = true;

            ImGui.EndChild();
        }


        public static void ShowKofi(uiOptionStrings lngOptions)
        {
            ImGui.BeginChild("Support", new Vector2(350, 50) * ImGuiHelpers.GlobalScale, true);

            ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005E5BFF);
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005E5BFF);
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005E5BFF);
            if (ImGui.Button(lngOptions.support_Line1))
            {
                Process.Start(new ProcessStartInfo { FileName = "https://ko-fi.com/projectmimer", UseShellExecute = true });
            }
            ImGui.PopStyleColor(3);
            ImGui.EndChild();
        }
    }
}
