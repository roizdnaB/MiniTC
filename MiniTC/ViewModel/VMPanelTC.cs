using MiniTC.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    internal class VMPanelTC : ViewModelBase
    {
        private string[] drivers;
        private string currentDrive;
        private string currentPath;
        private string currentFolder;
        private List<string> currentFolderContent;

        private ICommand changeDirectory = null;

        public VMPanelTC()
        {
            drivers = Directory.GetLogicalDrives();
        }

        public string[] Drivers
        {
            get => drivers;
            set { drivers = value; 
                onPropertyChanged(nameof(Drivers)); }
        }

        public string CurrentDrive
        {
            get => currentDrive;
            set { currentDrive = value; 
                onPropertyChanged(nameof(CurrentDrive));
                CurrentPath = currentDrive;
            }
        }

        public string CurrentPath
        {
            get => currentPath; 
            set { currentPath = value; 
                onPropertyChanged(nameof(CurrentPath));
                UpdateListBox();
            }
        }

        public string CurrentFolder
        {
            get => currentFolder;
            set { currentFolder = value; 
                onPropertyChanged(nameof(CurrentFolder)); }
        }
        
        public List<string> CurrentFolderContent
        {
            get => currentFolderContent;
            set { currentFolderContent = value; 
                onPropertyChanged(nameof(CurrentFolderContent)); }
        }

        public ICommand ChangeDirectory
        {
            get
            {
                if (changeDirectory == null)
                {
                    changeDirectory = new RelayCommand(
                        arg =>
                        {
                            if (currentFolder == "...")
                                CurrentPath = Directory.GetParent(CurrentPath).FullName;
                            else
                            {
                                if (CurrentPath.EndsWith("\\"))
                                    CurrentPath += currentFolder.Replace("[D] ", "");
                                else
                                    CurrentPath += "\\" + currentFolder.Replace("[D] ", "");
                            }
                        },
                        arg => PreviewEntry());
                }
                return changeDirectory;
            }
        }
        
        private bool PreviewEntry()
        {
            if (currentFolder == "...")
                return true;
            if (currentFolder != null && currentFolder.Contains("[D]"))
                return true;
            return false;
        }

        private void UpdateListBox()
        {
            List<string> content = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(CurrentPath);
                string[] folders = Directory.GetDirectories(CurrentPath);
                DirectoryInfo parentFile = Directory.GetParent(CurrentPath);

                if (parentFile != null)
                    content.Add("...");
                foreach (string folder in folders)
                    if (!(new DirectoryInfo(folder).Attributes.HasFlag(FileAttributes.Hidden)))
                        content.Add("[D] " + Path.GetFileName(folder));
                foreach (string file in files)
                    if (!(new FileInfo(file).Attributes.HasFlag(FileAttributes.Hidden)))
                        content.Add("      " + Path.GetFileName(file));
            }
            catch { }
            CurrentFolderContent = content;
        }

    }
}
