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

        private ICommand changeFolder = null;

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
                Update();
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

        public ICommand ChangeFolder
        {
            get
            {
                if (changeFolder == null)
                {
                    changeFolder = new RelayCommand(
                        arg =>
                        {
                            if (currentFolder == "...")
                                CurrentPath = Directory.GetParent(CurrentPath).FullName;
                            else
                            {
                                if (CurrentPath.EndsWith("\\"))
                                    CurrentPath += CurrentFolder.Replace("<D> ", "");
                                else
                                    CurrentPath += "\\" + CurrentFolder.Replace("<D> ", "");
                            }
                        },
                        arg => {
                            if (CurrentFolder == "...")
                                return true;
                            if (CurrentFolder != null && CurrentFolder.Contains("<D>"))
                                return true;
                            return false;
                        });
                }
                return changeFolder;
            }
        }

        private void Update()
        {
            try
            {
                currentFolderContent = new List<string>();
                if (currentPath != currentDrive)
                    currentFolderContent.Add("...");
                Directory.GetDirectories(currentPath).ToList().ForEach(x => currentFolderContent.Add($"<D> {x}"));
                Directory.GetFiles(currentPath).ToList().ForEach(x => currentFolderContent.Add(x));

                for (int i = 0; i < currentFolderContent.Count; i++)
                {
                    string[] arr = currentFolderContent[i].Split('\\');
                    if (currentFolderContent[i].Contains("<D>"))
                        currentFolderContent[i] = $"<D> {arr.Last()}";
                    else
                        currentFolderContent[i] = arr.Last();
                }
            }
            catch { }

            CurrentFolderContent = currentFolderContent;
        }
    }
}
