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
                    copyFile = new RelayCommand(
                        arg => {
                            string filePath;
                            string folderPath;
                            string fileName;

                            if (LeftPanelTC.CurrentFolder != null)
                            {
                                filePath = leftPanelTC.CurrentPath + "\\" + leftPanelTC.CurrentFolder.Trim();
                                folderPath = rightPanelTC.CurrentPath;
                                fileName = Path.GetFileName(filePath);
                            }
                            else
                            {
                                filePath = rightPanelTC.CurrentPath + "\\" + rightPanelTC.CurrentFolder.Trim();
                                folderPath = leftPanelTC.CurrentPath;
                                fileName = Path.GetFileName(filePath);
                            }

                            File.Copy(filePath, folderPath + "\\" + fileName);

                            LeftPanelTC.CurrentPath = leftPanelTC.CurrentPath;
                            RightPanelTC.CurrentPath = rightPanelTC.CurrentPath;

                    }, arg => {                       
                        if (leftPanelTC.CurrentPath == rightPanelTC.CurrentPath)
                            return false;
                        if ((rightPanelTC.CurrentFolder != null && leftPanelTC.CurrentFolderContent != null && leftPanelTC.CurrentFolderContent.Contains(rightPanelTC.CurrentFolder))
                            || (leftPanelTC.CurrentFolder != null && rightPanelTC.CurrentFolderContent != null && rightPanelTC.CurrentFolderContent.Contains(leftPanelTC.CurrentFolder)))
                            return false;
                        if (leftPanelTC.CurrentFolder != null && !leftPanelTC.CurrentFolder.Contains("<D>")
                            && leftPanelTC.CurrentFolder != "..." && rightPanelTC.CurrentPath != null)
                            return true;
                        if (rightPanelTC.CurrentFolder != null && !rightPanelTC.CurrentFolder.Contains("<D>")
                            && rightPanelTC.CurrentFolder != "..." && leftPanelTC.CurrentPath != null)
                            return true;

                        return false;
                    });

                return copyFile;
            }
        }
    }
}
