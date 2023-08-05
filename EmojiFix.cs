using System.IO;
namespace Oxide.Plugins
{
    [Info("EmojiFix", "bmgjet", "1.0.0")]
    class EmojiFix : RustPlugin
    {
        private void OnServerInitialized()
        {
            foreach (string text in Directory.EnumerateFiles(ConVar.Server.GetServerFolder("serveremoji")))
            {
                FileInfo fileInfo = new FileInfo(text);
                bool flag = fileInfo.Extension == ".png";
                bool flag2 = fileInfo.Extension == ".jpg";
                if (flag)
                {
                    byte[] bytes = File.ReadAllBytes(text);
                    if (!ImageProcessing.IsValidPNG(bytes, 256))
                    {
                        Puts("Patching Image " +  text);
                        bytes[0] = 137;
                        bytes[1] = 80;
                        bytes[2] = 78;
                        bytes[3] = 71;
                        bytes[4] = 13;
                        bytes[5] = 10;
                        bytes[6] = 26;
                        bytes[7] = 10;
                        bytes[8] = 0;
                        bytes[9] = 0;
                        bytes[10] = 0;
                        bytes[11] = 13;
                        bytes[12] = 73;
                        bytes[13] = 72;
                        bytes[14] = 68;
                        bytes[15] = 82;
                        File.WriteAllBytes(text, bytes);
                    }
                }
                else if (flag2)
                {
                    byte[] bytes = File.ReadAllBytes(text);
                    if (!ImageProcessing.IsValidJPG(bytes, 256))
                    {
                        Puts("Patching Image " + text);
                        bytes[0] = 255;
                        bytes[1] = 216;
                        bytes[2] = 255;
                        bytes[3] = 224;
                        bytes[6] = 74;
                        bytes[7] = 70;
                        bytes[8] = 73;
                        bytes[9] = 70;
                        bytes[10] = 0;
                        bytes[13] = 0;
                        bytes[14] = bytes[16];
                        bytes[15] = bytes[17];
                        File.WriteAllBytes(text, bytes);
                    }
                }
            }
        }
    }
}