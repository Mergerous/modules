using System;

namespace Modules.Common.Editor
{
    [Serializable]
    public struct Folder
    {
        public string name;
        public Folder[] folders;
        public Script[] scripts;

        public Folder(string name)
        {
            this.name = name;
            folders = new Folder[0];
            scripts = new Script[0];
        }
    }
}