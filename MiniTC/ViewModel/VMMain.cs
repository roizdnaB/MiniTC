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
    class VMMain : ViewModelBase
    {
        private VMPanelTC leftPanelTC;
        private VMPanelTC rightPanelTC;

        private ICommand copyFile = null;

        public VMMain()
        {
            leftPanelTC = new VMPanelTC();
            rightPanelTC = new VMPanelTC();
        }

        public VMPanelTC LeftPanelTC
        {
            get => leftPanelTC;
            set { leftPanelTC = value; onPropertyChanged(nameof(LeftPanelTC)); }
        }

        public VMPanelTC RightPanelTC
        {
            get => rightPanelTC;
            set { rightPanelTC = value; onPropertyChanged(nameof(RightPanelTC)); }
        }

        public ICommand CopyFile
        {
            get
            {
                if (copyFile == null)
                    copyFile = new RelayCommand(arg => Copy(), arg => ifCopy());

                return copyFile;
            }
        }

        private void Copy()
        {
            string filePath;
            string folderPath;
            string fileName;

            if (LeftPanelTC.CurrentFolder != null)
            {
                filePath = LeftPanelTC.CurrentPath + "\\" + LeftPanelTC.CurrentFolder.Trim();
                folderPath = RightPanelTC.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            else
            {
                filePath = RightPanelTC.CurrentPath + "\\" + RightPanelTC.CurrentFolder.Trim();
                folderPath = LeftPanelTC.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }

            File.Copy(filePath, folderPath + "\\" + fileName);

            LeftPanelTC.CurrentPath = LeftPanelTC.CurrentPath;
            RightPanelTC.CurrentPath = RightPanelTC.CurrentPath;
        }

        private bool ifCopy()
        {
            if (LeftPanelTC.CurrentPath == RightPanelTC.CurrentPath)
                return false;
            if ((RightPanelTC.CurrentFolder != null && leftPanelTC.CurrentFolderContent != null && leftPanelTC.CurrentFolderContent.Contains(RightPanelTC.CurrentFolder))
                || (LeftPanelTC.CurrentFolder != null && RightPanelTC.CurrentFolderContent != null && RightPanelTC.CurrentFolderContent.Contains(LeftPanelTC.CurrentFolder)))
                return false;
            if (LeftPanelTC.CurrentFolder != null && !LeftPanelTC.CurrentFolder.Contains("[D]")
                && LeftPanelTC.CurrentFolder != "..." && RightPanelTC.CurrentPath != null)
                return true;
            if (RightPanelTC.CurrentFolder != null && !RightPanelTC.CurrentFolder.Contains("[D]")
                && RightPanelTC.CurrentFolder != "..." && LeftPanelTC.CurrentPath != null)
                return true;

            return false;
        }
    }
}
