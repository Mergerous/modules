using System.Collections.Generic;
using System.Text;

namespace Modules.Common.Editor
{
    public static class TemplateExtensions
    {
        private const char SLASH_SIGN = '/';
        public static IEnumerable<Path<Folder>> GetPath(this Folder folder, string root)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(root);
            stringBuilder.Append(SLASH_SIGN);
            
            yield return new Path<Folder>()
            {
                content = folder,
                link = stringBuilder.ToString().TrimEnd(SLASH_SIGN)
            };
            
            if (folder.folders is {Length: > 0 })
            {
                stringBuilder.Append(folder.name);
                foreach (Folder child in folder.folders)
                {
                    foreach (Path<Folder> path in GetPath(child, stringBuilder.ToString()))
                    {
                        yield return path;
                    }
                }
            }
        }

        public static IEnumerable<Path<Script>> GetScriptPath(this Folder folder, string root)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .Append(root)
                .Append(SLASH_SIGN);

            foreach (Script script in folder.scripts)
            {
                StringBuilder scriptBuilder = new StringBuilder();
                scriptBuilder
                    .Append(stringBuilder)
                    .Append(folder.name)
                    .Append(SLASH_SIGN)
                    .Append(script.name)
                    .Append(".cs");
                
                yield return new Path<Script>()
                {
                    content = script,
                    link = scriptBuilder.ToString()
                };
            }
            
            if (folder.folders is {Length: > 0 })
            {
                stringBuilder.Append(folder.name);
                foreach (Folder child in folder.folders)
                {
                    foreach (Path<Script> path in GetScriptPath(child, stringBuilder.ToString()))
                    {
                        yield return path;
                    }
                }
            }
        }
    }
}