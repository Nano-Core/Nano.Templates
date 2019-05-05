using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace Nano.Template.VSIX
{
    /// <summary>
    /// Wizard.
    /// </summary>
    public class Wizard : IWizard
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        public void ProjectFinishedGenerating(Project project)
        {
            var script = project.ProjectItems.Cast<ProjectItem>().FirstOrDefault(x => x.Name.Equals("nano.ps1"));

            var a = Directory.GetCurrentDirectory();
            Console.WriteLine(a);

            if (script == null)
            {
                Console.WriteLine("script not found");
                return;
            }

            System.Diagnostics.Process
                .Start("powershell", $"-NoProfile -ExecutionPolicy Unrestricted \"{script.FileNames[0]}\" \"{project.FullName}\"");

            //if (process != null)
            //{
            //    process.WaitForExit();
            //}

            //script.Delete();
        }

        /// <summary>
        /// This method is only called for item templates, not for project templates.
        /// </summary>
        /// <param name="projectItem"></param>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// This method is called before opening any item that has the OpenInEditor attribute.
        /// </summary>
        /// <param name="projectItem"></param>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// This method is called after the project is created.
        /// </summary>
        public void RunFinished()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
        }

        /// <summary>
        /// This method is only called for item templates not for project templates.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}